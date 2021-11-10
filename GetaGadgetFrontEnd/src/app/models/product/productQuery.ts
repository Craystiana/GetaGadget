export class ProductQuery {
    public providerIds: number[];
    public deliveryMethodIds: number[];
    public categoryIds: number[];
    public sortById: number;
    public searchTerm: string;

    public constructor(providerIds: number[], deliveryMethodIds: number[], categoryIds: number[], sortById: number, searchTerm: string){
        this.providerIds = providerIds;
        this.deliveryMethodIds = deliveryMethodIds;
        this.categoryIds = categoryIds;
        this.searchTerm = searchTerm;
        this.sortById = sortById;
    }
}