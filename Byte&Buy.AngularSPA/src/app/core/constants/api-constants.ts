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
        post: '/orders',
        details: '/orders/details/'
    },

    payments:{
        get: '/payments',
        blik: '/blik',
        card: '/card',
    }
}
