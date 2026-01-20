import { Component, Input } from '@angular/core';
import { SaleBrowserItemResponse } from '../../../../../../core/dto/offers/sale/sale-browser-item-response';
import { RouterLink } from '@angular/router';
import { DecimalPipe } from '@angular/common';

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
  protected readonly imageBaseUrl = "http://localhost:5099/Images/";
}
