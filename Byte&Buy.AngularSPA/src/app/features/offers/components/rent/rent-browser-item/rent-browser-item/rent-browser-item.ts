import { DecimalPipe } from '@angular/common';
import { Component, Input } from '@angular/core';
import { RouterLink } from '@angular/router';
import { RentBrowserItemResponse } from '../../../../../../core/dto/offers/rent/rent-browser-item-response';

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
   protected readonly imageBaseUrl = "http://localhost:5099/Images/";
}
