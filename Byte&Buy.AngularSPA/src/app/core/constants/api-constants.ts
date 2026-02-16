import { Guid } from "guid-typescript";

export const API_ENDPOINTS = {
    //Auth
    auth: {
        login: '/auth/login',
        register: '/auth/register'
    },

    //Carts
    carts: {
        base: '/me/carts',
        saleOffer: '/me/carts/sale-offer',
        rentOffer: '/me/carts/rent-offer',
        clear: '/me/carts/clear'
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
        get: '/public/company/info'
    },

    conditions: {
        options: '/conditions/options'
    },

    countries: {
        options: '/countries/options'
    },

    deliveries: {
        options: '/deliveries/options',
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
        password: '/me/password'
    },

    portalUsers: {
        me: '/me'
    },

    checkout: {
        base: '/me/checkout'   
    },

    orders: {
        base: '/me/orders',
        sellerOrders: '/me/orders/seller',
        cancel: (id: string | Guid) => `/me/orders/${id}/cancel`,
        details: (id: string | Guid) => `/me/orders/details/${id}`,
        return: (id: string | Guid) => `/me/orders/${id}/return`,
        ship: (id: string | Guid) => `/me/orders/${id}/ship`,
        deliver: (id: string | Guid) => `/me/orders/${id}/deliver`,
    },

    payments:{
        get: '/me/payments',
        blik: '/me/blik',
        card: '/me/card',
    },

    rentals: {
        borrowerList: '/me/rentals/borrower',
        lenderList: '/me/rentals/lender',
        return: (id: string | Guid) => `/me/rentals/${id}/return`,  
    },

     documents: {
        orderDetails: (id: string | Guid) => `/company/documents/order-details/${id}`
    },
}
