import { Component, computed, input } from '@angular/core';
import { PrivateSeller } from '../../models/private-seller';
import { CompanySeller } from '../../models/company-seller';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-seller-info',
  imports: [CommonModule],
  standalone: true,
  templateUrl: './seller-info.html',
  styleUrls: ['./seller-info.scss',
    '../../shared/offers-shared-styles.scss'
  ],
})
export class SellerInfo {
  googleApiBase = "https://www.google.com/maps/search/?api=1&query=";

  seller = input<PrivateSeller | CompanySeller | undefined>();

  googleMapsLink = computed(() => {
    const sellerData = this.seller();
    if(!sellerData) return;

    const query = `${sellerData.city}+${sellerData.postalCity}+${sellerData.postalCode}`;
    return this.googleApiBase + encodeURIComponent(query);
  });
}



