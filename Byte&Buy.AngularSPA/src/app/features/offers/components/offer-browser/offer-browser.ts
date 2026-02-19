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
import { OfferSortBy } from '../../models/offer-sort-by';;
import { Guid } from 'guid-typescript';
import { ActivatedRoute, Router } from '@angular/router';
import { EmptyStateModel } from '../../../../shared/models/empty-state-model';
import { EmptyState } from "../../../../shared/components/empty-state/empty-state";
import { Pagination } from "../../../../shared/components/pagination/pagination";
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-offer-browser',
  imports: [CommonModule, RentBrowserItem, SaleBrowserItem, EmptyState, Pagination, ReactiveFormsModule],
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

  readonly emptyStateModel: EmptyStateModel = {
    description: `Change your filters for better results.`,
    header: "0 offers found",
    mainIconClass: "fa-brands fa-sistrix",
  };

  filterForm = new FormGroup({
    searchPhrase: new FormControl<string | null>(null),
    city: new FormControl<string | null>(null),
    minPrice: new FormControl<number | null>(null),
    maxPrice: new FormControl<number | null>(null),
    minRentalDays: new FormControl<number | null>(null),
    maxRentalDays: new FormControl<number | null>(null),
    categoryIds: new FormControl<Guid[]>([]),
    conditionIds: new FormControl<Guid[]>([]),
    sortBy: new FormControl<number | null>(OfferSortBy.Newest)
  });

  conditions = signal<SelectListItem[] | undefined>(undefined);
  categories = signal<SelectListItem[] | undefined>(undefined);
  pagedList = signal<PagedList<OfferUnion> | undefined>(undefined);

  readonly OfferSortBy = OfferSortBy;
  readonly sortOptions = Object.values(OfferSortBy)
    .filter(v => typeof v === 'number');

  query = signal<OfferQueryParams>({
    pageNumber: 1,
    pageSize: this.PAGE_SIZE,
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
      const newQuery: OfferQueryParams = {
        pageNumber: Number(params['pageNumber'] ?? 1),
        pageSize: this.PAGE_SIZE,
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
        newQuery.categoryIds = this.normalizeArray(params['categoryIds']);

      if (params['conditionIds'])
        newQuery.conditionIds = this.normalizeArray(params['conditionIds']);

      if (params['sortBy'])
        newQuery.sortBy = Number(params['sortBy'] ?? 0);

      this.query.set(newQuery);

      this.filterForm.patchValue({
        city: newQuery.city,
        maxPrice: newQuery.maxPrice,
        searchPhrase: newQuery.searchPhrase,
        minPrice: newQuery.minPrice,
        maxRentalDays: newQuery.maxRentalDays,
        minRentalDays: newQuery.minRentalDays,
        categoryIds: newQuery.categoryIds ?? [],
        conditionIds: newQuery.conditionIds ?? [],
        sortBy: newQuery.sortBy ?? 0
      });
    });
  }

  private fetchOffers(query: OfferQueryParams) {
    this.offerApiService.browseOffers(query)
      .subscribe(data => {
        this.pagedList.set(data);
      });
  }

  private normalizeArray(value: any): Guid[] {
    return Array.isArray(value) ? value : [value];
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
    const formValue = this.filterForm.value;

    const newQuery: OfferQueryParams = {
      pageNumber: 1,
      pageSize: this.query().pageSize,
      sortBy: Number(formValue.sortBy ?? OfferSortBy.Newest)
    };

    if (formValue.categoryIds?.length)
      newQuery.categoryIds = formValue.categoryIds;

    if (formValue.conditionIds?.length)
      newQuery.conditionIds = formValue.conditionIds;

    if (formValue.minPrice)
      newQuery.minPrice = formValue.minPrice;

    if (formValue.maxPrice)
      newQuery.maxPrice = formValue.maxPrice;

    if (formValue.minRentalDays)
      newQuery.minRentalDays = formValue.minRentalDays;

    if (formValue.maxRentalDays)
      newQuery.maxRentalDays = formValue.maxRentalDays;

    if (formValue.city)
      newQuery.city = formValue.city;

    if (formValue.categoryIds?.length)
      newQuery.categoryIds = formValue.categoryIds;

    if (formValue.conditionIds?.length)
      newQuery.conditionIds = formValue.conditionIds;

    if (formValue.searchPhrase?.trim())
      newQuery.searchPhrase = formValue.searchPhrase.trim();

    this.query.set(newQuery);

    this.router.navigate([], {
      relativeTo: this.route,
      queryParams: newQuery,
    });

  }

  clearFilters() {
    this.filterForm.reset();

    this.router.navigate([], {
      relativeTo: this.route,
      queryParams: {
        pageNumber: 1,
        pageSize: this.PAGE_SIZE
      }
    });
  }

  toggleCategory(id: Guid, checked: boolean) {
    const current = this.filterForm.value.categoryIds ?? [];

    const updated = checked
      ? [...current, id]
      : current.filter(x => x !== id);

    this.filterForm.patchValue({ categoryIds: updated });
  }

  toggleCondition(id: Guid, checked: boolean) {
    const current = this.filterForm.value.conditionIds ?? [];

    const updated = checked
      ? [...current, id]
      : current.filter(x => x !== id);

    this.filterForm.patchValue({ conditionIds: updated });
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
