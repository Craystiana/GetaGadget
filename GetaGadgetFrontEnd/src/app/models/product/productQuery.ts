export class ProductQuery {
    public providerIds: number[];
    public deliveryMethodIds: number[];
    public categoryIds: number[];
    public sortById: number;
    public searchTerm: string;
    public inStock: boolean;

    public constructor(providerIds: number[], deliveryMethodIds: number[], categoryIds: number[], sortById: number, searchTerm: string, inStock: boolean){
        this.providerIds = providerIds;
        this.deliveryMethodIds = deliveryMethodIds;
        this.categoryIds = categoryIds;
        this.searchTerm = searchTerm;
        this.sortById = sortById;
        this.inStock = inStock;
    }
}