import { Byte } from "@angular/compiler/src/util";

export class Product {
    public productId: number;
    public name : string;
    public price : number;
    public stock : number;
    public provider : string;
    public deliveryMethod : string;
    public category : string;
    public photo : Byte[];
}