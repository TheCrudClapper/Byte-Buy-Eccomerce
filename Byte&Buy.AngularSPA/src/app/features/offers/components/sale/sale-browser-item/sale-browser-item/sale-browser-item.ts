import { Component, Input } from '@angular/core';
import { SaleBrowserItemResponse } from '../../../../../../core/dto/offers/sale/sale-browser-item-response';
import { RouterLink } from '@angular/router';
import { DecimalPipe } from '@angular/common';
import { OfferStatus } from '../../../../../../core/dto/offers/enum/offer-status';
import { environment } from '../../../../../../../environments/environment';

@Component({
  selector: 'app-sale-browser-item',
  imports: [RouterLink, DecimalPipe],
  templateUrl: './sale-browser-item.html',
  standalone: true,
  styleUrls: ['./sale-browser-item.scss',
    '../../../../shared/styles/offers-shared-styles.scss',
    '../../../../shared/styles/browser-item-style.scss',
  ],
})
export class SaleBrowserItem {
  @Input({ required: true }) offer!: SaleBrowserItemResponse;
  protected readonly imageBaseUrl = environment.staticImagesBaseUrl;
  readonly OfferStatus = OfferStatus;
}
