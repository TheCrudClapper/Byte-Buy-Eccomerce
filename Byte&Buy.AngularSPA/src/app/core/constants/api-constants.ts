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

    //Shipping Addresses
    shippingAddresses: {
        base: '/me/shiping-addresses',
        shippingAddressesList: '/me/shipping-addresses/list',
        shippingAddressById: (id: string | Guid) => `/me/shipping-addresses/${id}`,
        shippingAddressAdd: '/me/shipping-addresses',
        shippingAddressDelete: (id: string | Guid) => `/me/shipping-addresses/${id}`,
        shippingAddressCheckout: (id: string | Guid | undefined) => `/me/shipping-addresses/checkout/${id ?? ''}`
    },

    //Home Address
    homeAdresses: {
        base: '/me/home-address',
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

    payments: {
        get: (id: string | Guid)  => `/me/payments/${id}`,
        blik: (id: string | Guid) => `/me/payments/${id}/blik`,
        card: (id: string | Guid) => `/me/payments/${id}/card`,
    },

    rentals: {
        borrowerList: '/me/rentals/borrower',
        lenderList: '/me/rentals/lender',
        return: (id: string | Guid) => `/me/rentals/${id}/return`,
    },

    documents: {
        orderDetails: (id: string | Guid) => `/company/documents/order-details/${id}`
    },

    google: {
        googleMapsApi: "https://www.google.com/maps/search/?api=1&query="
    }
}
