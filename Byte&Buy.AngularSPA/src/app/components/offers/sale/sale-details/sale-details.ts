import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { DeliveryOption } from '../../shared/delivery-option';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-sale-details',
  imports: [FormsModule, CommonModule],
  templateUrl: './sale-details.html',
  styleUrls: [
    './sale-details.scss',
    '../../shared/offers-shared-styles.scss'],
    standalone: true
})
export class SaleDetails {
  googleApiBase = "https://www.google.com/maps/search/?api=1&query=";

  deliveryOptions: DeliveryOption[]  = [
      {
        name: 'InPost Paczkomat S',
        priceAndCurrency: '10.95 PLN',
        carrier: 'Inpost',
        deliveryChannel: 'Parcel Locker'
      },
      {
        name: 'Kurier DPD',
        priceAndCurrency: '10.95 PLN',
        carrier: 'DPD',
        deliveryChannel: 'Courier'
      }
    ];

  quantity: number = 56;
  condition: string = "Used";
  category: string = "CPU";
  quantityAvaliable: number = 69;
  title: string = "Komputer Ryzen 5 5600 + RTX 3070 Ti + 16Gb RAM";
  isSellerCompany: boolean = false;
  sellerEmail: string = "wojciechmucha12@gmail.com";
  sellerCreated: string = "2025-12-12";
  sellerCity: string = "Siedlce";
  sellerPostalCode: string = "33-322";
  sellerPostalCity: string = "Korzenna";
  sellerPhone: string = "724075416";
  sellerName: string = "Wojciech";

  description: string = `   🔥 Unleash Elite Performance – AMD Ryzen 7 5700X3D 🔥

                            Experience next-level computing with
                            the Ryzen 7 5700X3D, engineered for gamers, creators, and power users who demand maximum
                            performance. Featuring AMD’s cutting-edge 3D V-Cache™ technology, this 8-core, 16-thread
                            processor delivers exceptional speed, responsiveness, and multitasking capabilities.

                            ⚙️ Key Features:

                            3D V-Cache™ for up to 96MB L3 cache – massive boost in gaming and productivity workloads

                            8 cores / 16 threads – seamless multitasking and high-performance computing

                            Base Clock: 3.0 GHz | Boost Clock: Up to 4.1 GHz

                            Socket AM4 – compatible with a wide range of motherboards

                            Unlocked for overclocking – push your system to the limit

                            🎮 Whether you're dominating in AAA titles or rendering high-res content, the Ryzen 5700X3D
                            offers unmatched efficiency and power.

                            🛒 Limited-time offer – grab yours now and elevate your rig with one of AMD’s most advanced
                            desktop processors!`;


  get descriptionWithBr(): string {
    return this.description.trim().replace(/\n/g, '<br>');
  }

  googleMapsLink(): string {
    const query = (this.sellerCity + '+' + this.sellerPostalCity + '+' + this.sellerPostalCode);
    return this.googleApiBase + query;
  }

  addToCart(): void {
    console.log(this.quantity);
  }
}

