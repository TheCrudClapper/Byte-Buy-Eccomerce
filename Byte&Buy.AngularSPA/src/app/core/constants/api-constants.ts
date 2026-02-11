import { Guid } from "guid-typescript";

export const API_ENDPOINTS = {
    //Auth
    auth: {
        login: '/auth/login',
        register: '/auth/register'
    },

    //Carts
    carts: {
        base: '/carts',
        saleOffer: '/carts/sale-offer',
        rentOffer: '/carts/rent-offer',
        clear: '/carts/clear'
    },

    //Addresses
    addresses: {
        base: '/me',
        homeAddress: '/me/home-address',
        shippingAddressesList: '/me/shipping-addresses/list',
        shippingAddressById: '/me/shipping-addresses',
        shippingAddressAdd: '/me/shipping-addresses',
        shippingAddressDelete: '/me/shipping-addresses',
        shippingAddressCheckout: '/me/shipping-addresses/checkout'
    },

    categories: {
        options: '/categories/options'
    },

    companyInfo: {
        get: '/companyInfo'
    },

    conditions: {
        options: '/conditions/options'
    },

    countries: {
        options: '/countries/options'
    },

    deliveries: {
        options: '/deliveries/options',
        list: '/deliveries/list',
        offer: '/deliveries/offer',
        available: '/deliveries/available'
    },

    rentOffer: {
        base: '/me/rent-offer',
        byId: '/me/rent-offer'
    },

    saleOffer: {
        base: '/me/sale-offer',
        byId: '/me/sale-offer'
    },

    users: {
        password: '/users/password'
    },

    portalUsers: {
        me: '/portalusers/me'
    },

    checkout: {
        base: '/checkout'   
    },

    orders: {
        base: '/orders',
        sellerOrders: '/orders/seller',
        cancel: (id: string | Guid) => `/orders/${id}/cancel`,
        details: (id: string | Guid) => `/orders/details/${id}`,
        return: (id: string | Guid) => `/orders/${id}/return`,
        ship: (id: string | Guid) => `/orders/${id}/ship`,
        deliver: (id: string | Guid) => `/orders/${id}/deliver`,
    },

    payments:{
        get: '/payments',
        blik: '/blik',
        card: '/card',
    },

    rentals: {
        borrowerList: '/rentals/borrower',
        lenderList: '/rentals/lender',
        return: (id: string | Guid) => `/rentals/${id}/return`,  
    },

     documents: {
        orderDetails: (id: string | Guid) => `/documents/order-details/${id}`
    },
}
