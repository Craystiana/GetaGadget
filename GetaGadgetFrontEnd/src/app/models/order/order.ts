import { OrderProduct } from "./orderProduct";

export class Order {
    public orderId: number;
    public orderDate : Date;
    public totalValue : number;
    public products : OrderProduct[];
}