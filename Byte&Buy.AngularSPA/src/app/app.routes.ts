import { Routes } from '@angular/router';
import { LoginPage } from './features/auth/components/login-page/login-page';
import { RegisterPage } from './features/auth/components/register-page/register-page';
import { RentCreate } from './features/offers/components/rent/rent-create/rent-create';
import { RentEdit } from './features/offers/components/rent/rent-edit/rent-edit';
import { RentDetails } from './features/offers/components/rent/rent-details/rent-details';
import { SaleCreate } from './features/offers/components/sale/sale-create/sale-create';
import { SaleEdit } from './features/offers/components/sale/sale-edit/sale-edit';
import { SaleDetails } from './features/offers/components/sale/sale-details/sale-details';
import { HomePage } from './features/home/components/home-page/home-page';
import { ProfilePage } from './features/profile/components/profile-page/profile-page';
import { PersonalInfo } from './features/profile/components/personal-info/personal-info';
import { Addresses } from './features/profile/components/addresses/addresses';
import { MyOffers } from './features/profile/components/my-offers/my-offers';
import { OfferBrowser } from './features/offers/components/offer-browser/offer-browser';
import { Fobidden } from './shared/components/fobidden/fobidden';
import { authGuard } from './core/guards/auth-guard';
import { CartPage } from './features/cart/components/cart-page/cart-page/cart-page';
import { NotFound } from './shared/components/not-found/not-found/not-found';
import { guidParameterGuard } from './core/guards/guid-parameter/guid-parameter-guard';
import { CheckoutPage } from './features/orders/components/checkout-page/checkout-page';
import { PaymentGateway } from './features/payment-gateway/components/payment-gateway/payment-gateway';
import { MyOrders } from './features/profile/components/my-orders/my-orders/my-orders';
import { BuyerOrderDetails } from './features/orders/components/buyer-order-details/buyer-order-details';
import { SellerOrderDetails } from './features/orders/components/seller-order-details/seller-order-details';
import { ClientOrders } from './features/orders/components/client-orders/client-orders';
import { MyRentals } from './features/rentals/components/my-rentals/my-rentals';
import { ClientRentals } from './features/rentals/components/client-rentals/client-rentals';

export const routes: Routes = [
    { path: '', component: HomePage},
    { path: 'login', component: LoginPage},
    { path: 'register', component: RegisterPage},
    {
        canActivate: [authGuard],
        path: '',
        children: [
            { path: 'offers/rent/create', component: RentCreate}, 
            { path: 'offers/rent/edit/:id', component: RentEdit, canMatch: [guidParameterGuard]}, 
            { path: 'offers/rent/details/:id', component: RentDetails, canMatch: [guidParameterGuard]}, 
            { path: 'offers/sale/create', component: SaleCreate}, 
            { path: 'offers/sale/edit/:id', component: SaleEdit, canMatch: [guidParameterGuard]}, 
            { path: 'offers/sale/details/:id', component: SaleDetails, canMatch: [guidParameterGuard]},
            { path: 'order/details/:id', component: BuyerOrderDetails, canMatch: [guidParameterGuard]},
            { path: 'order/seller-details/:id', component: SellerOrderDetails, canMatch: [guidParameterGuard]},
            { path: 'client-orders', component: ClientOrders},
            { path: 'client-rentals', component: ClientRentals, canMatch: [guidParameterGuard]},
            { path: 'cart', component: CartPage},
            {
                path: 'profile',
                component: ProfilePage,
                children: [
                    { path: '', redirectTo: 'personal-info', pathMatch: 'full' },
                    { path: 'personal-info', component: PersonalInfo },
                    { path: 'addresses', component: Addresses},
                    { path: 'my-offers', component: MyOffers},
                    { path: 'my-orders', component: MyOrders},
                    { path: 'my-rentals', component: MyRentals}
                ]
            },
            { path: 'checkout', component: CheckoutPage},
            { path: 'payment/:id', component: PaymentGateway, canMatch: [guidParameterGuard]},
        ]
    },
    { path: 'forbidden', component: Fobidden },
    { path: 'offers', component: OfferBrowser},
    { path: '**', component: NotFound }
];
