import { Component, computed, effect, inject, OnInit, signal } from '@angular/core';
import { OfferApiService } from '../../../../core/clients/offers/common/offer-api-service';
import { CommonModule } from '@angular/common';
import { RentBrowserItem } from '../rent/rent-browser-item/rent-browser-item/rent-browser-item';
import { OfferUnion } from '../../../../core/dto/offers/common/offer-browser-union';
import { SaleBrowserItem } from '../sale/sale-browser-item/sale-browser-item/sale-browser-item';
import { PagedList } from '../../../../core/pagination/pagedList';
import { OfferQueryParams } from '../../../../core/dto/offers/query/offer-browser-query-params';
import { ConditionApiService } from '../../../../core/clients/condition/condition-api-service';
import { SelectListItem } from '../../../../shared/models/select-list-item';
import { CategoryApiService } from '../../../../core/clients/category/category-api-service';
import { OfferSortBy } from '../../models/offer-sort-by';
import { SellerType } from '../../../../shared/models/seller-type';
import { Guid } from 'guid-typescript';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-offer-browser',
  imports: [CommonModule, RentBrowserItem, SaleBrowserItem],
  templateUrl: './offer-browser.html',
  standalone: true,
  styleUrls: [
    './offer-browser.scss',
    '../../shared/styles/offers-shared-styles.scss'
  ],
})
export class OfferBrowser implements OnInit {
  private readonly PAGE_SIZE = 10;
  private readonly offerApiService = inject(OfferApiService);
  private readonly conditionApiService = inject(ConditionApiService);
  private readonly categoryApiSerivce = inject(CategoryApiService);
  private readonly route = inject(ActivatedRoute);
  private readonly router = inject(Router);

  conditions = signal<SelectListItem[] | undefined>(undefined);
  categories = signal<SelectListItem[] | undefined>(undefined);
  pagedList = signal<PagedList<OfferUnion> | undefined>(undefined);

  readonly OfferSortBy = OfferSortBy;
  readonly sortOptions = Object.values(OfferSortBy)
    .filter(v => typeof v === 'number');

  // Filtering Signals
  selectedCategoryIds = signal<Guid[]>([]);
  selectedConditionIds = signal<Guid[]>([]);
  selectedSellerTypes = signal<SellerType[]>([]);
  minPrice = signal<number | null>(null);
  maxPrice = signal<number | null>(null);
  searchPhrase = signal<string | null>(null);
  minRentalDays = signal<number | null>(null);
  maxRentalDays = signal<number | null>(null);
  sellerType = signal<SellerType | null>(null);
  city = signal<string | null>(null);
  sortByValue = signal<OfferSortBy | null>(null);

  query = signal<OfferQueryParams>({
    pageNumber: 1,
    pageSize: this.PAGE_SIZE,
    sortBy: OfferSortBy.Newest
  });

  constructor() {
    effect(() => {
      this.fetchOffers(this.query());
    });
  }

  ngOnInit(): void {
    this.fetchCategories();
    this.fetchConditions();

    this.route.queryParams.subscribe(params => {

      const categoryIds = params['categoryIds']
        ? (Array.isArray(params['categoryIds'])
          ? params['categoryIds']
          : [params['categoryIds']])
        : [];

      const conditionIds = params['conditionIds']
        ? (Array.isArray(params['conditionIds'])
          ? params['conditionIds']
          : [params['conditionIds']])
        : [];

      this.selectedCategoryIds.set(categoryIds);
      this.selectedConditionIds.set(conditionIds);
      const newQuery: OfferQueryParams = {
        pageNumber: 1,
        pageSize: this.PAGE_SIZE,
        sortBy: Number(params['sortBy'] ?? OfferSortBy.Newest)
      };

      if (params['searchPhrase'])
        newQuery.searchPhrase = params['searchPhrase'];

      if (params['city'])
        newQuery.city = params['city'];

      if (params['minPrice'])
        newQuery.minPrice = Number(params['minPrice']);

      if (params['maxPrice'])
        newQuery.maxPrice = Number(params['maxPrice']);

      if (params['minRentalDays'])
        newQuery.minRentalDays = Number(params['minRentalDays']);

      if (params['maxRentalDays'])
        newQuery.maxRentalDays = Number(params['maxRentalDays']);

      if (params['categoryIds'])
        newQuery.categoryIds = Array.isArray(params['categoryIds'])
          ? params['categoryIds']
          : [params['categoryIds']];

      if (params['conditionIds'])
        newQuery.conditionIds = Array.isArray(params['conditionIds'])
          ? params['conditionIds']
          : [params['conditionIds']];

      this.query.set(newQuery);
    });

  }

  private fetchOffers(query: OfferQueryParams) {
    this.offerApiService.browseOffers(query)
      .subscribe(data => {
        this.pagedList.set(data);
      });
  }

  private fetchConditions() {
    this.conditionApiService.getSelectList()
      .subscribe(data => {
        this.conditions.set(data);
      });
  }

  private fetchCategories() {
    this.categoryApiSerivce.getSelectList()
      .subscribe(data => {
        this.categories.set(data);
      });
  }

  onSortChange(value: string) {
    const sort = Number(value) as OfferSortBy;
    this.router.navigate([], {
      relativeTo: this.route,
      queryParams: {
        sortBy: sort,
        pageNumber: 1
      },
      queryParamsHandling: 'merge'
    });
  }

  applyFilters() {
    const newQuery: OfferQueryParams = {
      pageNumber: 1,
      pageSize: this.query().pageSize,
      sortBy: this.query().sortBy
    };

    const categories = this.selectedCategoryIds();
    if (categories.length > 0)
      newQuery.categoryIds = categories;

    const conditions = this.selectedConditionIds();
    if (conditions.length > 0)
      newQuery.conditionIds = conditions;

    if (this.sellerType())
      newQuery.sellerType = this.sellerType()!;

    if (this.minPrice() !== null)
      newQuery.minPrice = this.minPrice()!;

    if (this.maxPrice() !== null)
      newQuery.maxPrice = this.maxPrice()!;

    if (this.minRentalDays() !== null)
      newQuery.minRentalDays = this.minRentalDays()!;

    if (this.maxRentalDays() !== null)
      newQuery.maxRentalDays = this.maxRentalDays()!;

    const city = this.city()?.trim();
    if (city)
      newQuery.city = city;

    const phrase = this.searchPhrase()?.trim();
    if (phrase)
      newQuery.searchPhrase = phrase;

    this.query.set(newQuery);

    this.router.navigate([], {
      relativeTo: this.route,
      queryParams: newQuery,
      queryParamsHandling: 'merge'
    });

  }

  clearFilters() {
    this.selectedCategoryIds.set([]);
    this.selectedConditionIds.set([]);
    this.sellerType.set(null);
    this.city.set(null);
    this.minPrice.set(null);
    this.maxPrice.set(null);
    this.minRentalDays.set(null);
    this.maxRentalDays.set(null);
    this.searchPhrase.set(null);

    this.router.navigate([], {
      relativeTo: this.route,
      queryParams: {
        searchPhrase: this.searchPhrase()
      }
    });

  }

  toggleCategory(id: Guid, checked: boolean) {
    this.selectedCategoryIds.update(arr =>
      checked ? [...arr, id] : arr.filter(x => x !== id)
    );
  }

  toggleCondition(id: Guid, checked: boolean) {
    this.selectedConditionIds.update(arr =>
      checked ? [...arr, id] : arr.filter(x => x !== id)
    );
  }

  nextPage() {
    const meta = this.pagedList()?.metadata;
    if (!meta || !meta.hasNext) return;

    const current = this.query();
    this.goToPage(current.pageNumber + 1);
  }

   goToPage(page: number) {
    const meta = this.pagedList()?.metadata;
    if (!meta) return;

    const safePage = Math.min(
      Math.max(page, 1),
      meta.totalPages
    );

    if (safePage === this.query().pageNumber) return;

    this.query.update(q => ({
      ...q,
      pageNumber: safePage
    }));
  }

  previousPage() {
    const meta = this.pagedList()?.metadata;
    if (!meta || !meta.hasPrevious) return;

    const current = this.query();
    this.goToPage(current.pageNumber - 1);
  }
}
