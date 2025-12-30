import { Component } from '@angular/core';

@Component({
  selector: 'app-seller-info',
  imports: [],
  templateUrl: './seller-info.html',
  styleUrls: ['./seller-info.scss',
    '../../shared/offers-shared-styles.scss'
  ],
})
export class SellerInfo {
  googleApiBase = "https://www.google.com/maps/search/?api=1&query=";

  sellerEmail: string = "wojciechmucha12@gmail.com";
  sellerCreated: string = "2025-12-12";
  sellerCity: string = "Siedlce";
  sellerPostalCode: string = "33-322";
  sellerPostalCity: string = "Korzenna";
  sellerPhone: string = "724075416";
  sellerName: string = "Wojciech";

  googleMapsLink(): string {
    const query = (this.sellerCity + '+' + this.sellerPostalCity + '+' + this.sellerPostalCode);
    return this.googleApiBase + query;
  }
}
