import { Routes } from '@angular/router';
import { Login } from './components/auth/login/login';
import { Register } from './components/auth/register/register';
import { RentCreate } from './components/offers/rent/rent-create/rent-create';
import { RentEdit } from './components/offers/rent/rent-edit/rent-edit';
import { RentDetails } from './components/offers/rent/rent-details/rent-details';
import { SaleCreate } from './components/offers/sale/sale-create/sale-create';
import { SaleEdit } from './components/offers/sale/sale-edit/sale-edit';
import { SaleDetails } from './components/offers/sale/sale-details/sale-details';
import { Home } from './components/home/home';

export const routes: Routes = [
    { path: '', component: Home},
    { path: 'login', component: Login},
    { path: 'register', component: Register},
    { path: 'offers/rent/create', component: RentCreate}, 
    { path: 'offers/rent/:id/edit', component: RentEdit}, 
    { path: 'offers/rent/:id/details', component: RentDetails}, 
    { path: 'offers/sale/create', component: SaleCreate}, 
    { path: 'offers/sale/:id/edit', component: SaleEdit}, 
    { path: 'offers/sale/:id/details', component: SaleDetails}
];
