export class ProductEdit{
    public productId : number;
    public name : string;
    public description : string;
    public price : number;
    public stock : number;
    public provider : string;
    public deliveryMethod : string;
    public category : string;
    public photo : string;

    public constructor(productId: number,
                       name : string,
                       description : string,
                       price : number, 
                       stock : number,
                       provider : string,
                       deliveryMehod : string,
                       category : string,
                       photo : string) {
        this.productId = productId;     
        this.name = name;  
        this.description = description;
        this.price = price; 
        this.stock = stock; 
        this.provider = provider,
        this.deliveryMethod = deliveryMehod,
        this.category = category,          
        this.photo = photo;
    }
}