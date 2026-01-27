import { Component } from '@angular/core';
import { API_ENDPOINTS } from '../../../../core/constants/api-constants';
import { environment } from '../../../../../environments/environment';

@Component({
  selector: 'app-checkout-page',
  imports: [],
  templateUrl: './checkout-page.html',
  styleUrl: './checkout-page.scss',
})
export class CheckoutPage {
  imageUrl: string = environment.staticImagesBaseUrl;
}
