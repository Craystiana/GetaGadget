export class PlaceOrder {
    public county: string;
    public city: string;
    public fullAddress : string;
    public postalCode : string;
    public cardNumber: string;
    public cardCsv: number;
    public cardExpirationDate: Date;
    public deliveryMethodId: number;
    public coupon : string;

    public constructor(county: string,
                       city: string,
                       fullAddress : string,
                       postalCode : string,
                       cardNumber: string,
                       cardCsv: number,
                       cardExpirationDate: Date,
                       deliveryMethodId: number,
                       coupon: string) 
    {
        this.county = county;
        this.city = city;
        this.fullAddress = fullAddress;
        this.postalCode = postalCode;
        this.cardNumber = cardNumber;
        this.cardCsv = cardCsv;
        this.cardExpirationDate = cardExpirationDate;
        this.deliveryMethodId = deliveryMethodId;
        this.coupon = coupon;
    }
}