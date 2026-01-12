import { Routes } from '@angular/router';
import { Login } from './features/auth/login/login';
import { Register } from './features/auth/register/register';
import { RentCreate } from './features/offers/rent/rent-create/rent-create';
import { RentEdit } from './features/offers/rent/rent-edit/rent-edit';
import { RentDetails } from './features/offers/rent/rent-details/rent-details';
import { SaleCreate } from './features/offers/sale/sale-create/sale-create';
import { SaleEdit } from './features/offers/sale/sale-edit/sale-edit';
import { SaleDetails } from './features/offers/sale/sale-details/sale-details';
import { Home } from './features/home/home';
import { ProfileIndex } from './features/profile/profile-index/profile-index';
import { PersonalInfo } from './features/profile/personal-info/personal-info/personal-info';
import { Addresses } from './features/profile/addresses/addresses/addresses';
import { MyOffers } from './features/profile/my-offers/my-offers/my-offers';
import { OfferBrowser } from './features/offers/browser/offer-browser/offer-browser';
import { Cart } from './features/cart/cart/cart';
import { Fobidden } from './shared/ui/fobidden/fobidden';
import { authGuard } from './core/guards/auth-guard';

export const routes: Routes = [
    { path: '', component: Home},
    { path: 'login', component: Login},
    { path: 'register', component: Register},
    {
        canActivate: [authGuard],
        path: '',
        children: [
            { path: 'offers/rent/create', component: RentCreate}, 
            { path: 'offers/rent/:id/edit', component: RentEdit}, 
            { path: 'offers/rent/:id/details', component: RentDetails}, 
            { path: 'offers/sale/create', component: SaleCreate}, 
            { path: 'offers/sale/:id/edit', component: SaleEdit}, 
            { path: 'offers/sale/:id/details', component: SaleDetails},
            { path: 'cart', component: Cart},
            {
                path: 'profile',
                component: ProfileIndex,
                children: [
                    { path: '', redirectTo: 'personal-info', pathMatch: 'full' },
                    { path: 'personal-info', component: PersonalInfo },
                    { path: 'addresses', component: Addresses},
                    { path: 'my-offers', component: MyOffers}
                ]
            }
        ]
    },
    { path: 'forbidden', component: Fobidden },
    { path: 'offers', component: OfferBrowser},
    
];
