export interface PrivateSeller{
    type: 'private'
    firstName: string,
    email: string,
    city: string,
    postalCode: string,
    postalCity: string,
    phone: string | null
    accountCreatedDate: Date
}