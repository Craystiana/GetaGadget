import { OrderProduct } from "./orderProduct";

export class Order {
    public orderId: number;
    public orderDate : string;
    public totalValue : number;
    public products : OrderProduct[];
}