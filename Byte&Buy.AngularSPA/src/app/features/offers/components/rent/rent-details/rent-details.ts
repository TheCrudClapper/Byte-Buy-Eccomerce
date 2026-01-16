import { Component, inject, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { DeliveryOption } from '../../../shared/delivery-option';
import { CommonModule } from '@angular/common';
import { SellerInfo } from '../../../shared/seller-info/seller-info';
import { Guid } from 'guid-typescript';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-sale-details',
  imports: [FormsModule, CommonModule, SellerInfo],
  templateUrl: './rent-details.html',
  styleUrls: [
    './rent-details.scss',
    '../../../shared/offers-shared-styles.scss'],
  standalone: true
})

export class RentDetails implements OnInit {
  rentOfferId!: Guid;
  private readonly route = inject(ActivatedRoute);

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      const id = params.get('id');
      if (id) {
        this.rentOfferId = Guid.parse(id);
      }
    });
  }

  deliveryOptions: DeliveryOption[] = [
    {
      name: 'InPost Parcel Locker - S',
      priceAndCurrency: '10.95 PLN',
      carrier: 'Inpost',
      deliveryChannel: 'Parcel Locker'
    },
    {
      name: 'InPost Parcel Locker - M',
      priceAndCurrency: '10.95 PLN',
      carrier: 'Inpost',
      deliveryChannel: 'Parcel Locker'
    },
    {
      name: 'InPost Parcel Locker - L',
      priceAndCurrency: '10.95 PLN',
      carrier: 'Inpost',
      deliveryChannel: 'Parcel Locker'
    },
    {
      name: 'Courier DPD',
      priceAndCurrency: '10.95 PLN',
      carrier: 'DPD',
      deliveryChannel: 'Courier'
    },
    {
      name: 'Courier DHL',
      priceAndCurrency: '10.95 PLN',
      carrier: 'DHL',
      deliveryChannel: 'Courier'
    }
  ];

  quantity: number = 56;
  price: number = 49.99;
  rentalDays: number = 5;

  maxRentalDays: number = 10;
  currency: string = "PLN"
  condition: string = "Used";
  category: string = "CPU";
  quantityAvaliable: number = 69;
  title: string = "Komputer Ryzen 5 5600 + RTX 3070 Ti + 16Gb RAM";
  isSellerCompany: boolean = false;

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

  addToCart(): void {
    console.log(this.quantity);
  }
}
