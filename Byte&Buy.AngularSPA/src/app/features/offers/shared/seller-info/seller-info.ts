import { Component, computed, input } from '@angular/core';
import { PrivateSeller } from '../../models/private-seller';
import { CompanySeller } from '../../models/company-seller';
import { CommonModule } from '@angular/common';
import { API_ENDPOINTS } from '../../../../core/constants/api-constants';

@Component({
  selector: 'app-seller-info',
  imports: [CommonModule],
  standalone: true,
  templateUrl: './seller-info.html',
  styleUrls: ['./seller-info.scss',
    '../../shared/styles/offers-shared-styles.scss'
  ],
})
export class SellerInfo {
  googleApiBase = API_ENDPOINTS.google.googleMapsApi;

  seller = input<PrivateSeller | CompanySeller | undefined>();

  googleMapsLink = computed(() => {
    const sellerData = this.seller();
    if(!sellerData) return;

    const query = `${sellerData.city}+${sellerData.postalCity}+${sellerData.postalCode}`;
    return this.googleApiBase + encodeURIComponent(query);
  });
}



