import { DecimalPipe } from '@angular/common';
import { Component, Input } from '@angular/core';
import { RouterLink } from '@angular/router';
import { RentBrowserItemResponse } from '../../../../../../core/dto/offers/rent/rent-browser-item-response';
import { OfferStatus } from '../../../../../../core/dto/offers/enum/offer-status';
import { environment } from '../../../../../../../environments/environment';

@Component({
  selector: 'app-rent-browser-item',
  imports: [RouterLink, DecimalPipe],
  templateUrl: './rent-browser-item.html',
  styleUrls: ['./rent-browser-item.scss',
    '../../../../shared/styles/offers-shared-styles.scss',
    '../../../../shared/styles/browser-item-style.scss',
  ],
})
export class RentBrowserItem {
   @Input({ required: true }) offer!: RentBrowserItemResponse;
  protected readonly imageBaseUrl = environment.staticImagesBaseUrl;;
   readonly OfferStatus = OfferStatus;
}
