import { Component } from '@angular/core';
import { RouterLink, RouterModule } from "@angular/router";

@Component({
  selector: 'app-offer-browser',
  imports: [RouterLink],
  templateUrl: './offer-browser.html',
  styleUrls: [
    './offer-browser.scss',
    '../../shared/offers-shared-styles.scss'
  ],
})
export class OfferBrowser {

}
