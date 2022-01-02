import { OrderProduct } from "./orderProduct";

export class Order {
    public orderId: number;
    public OrderDate : Date;
    public totalValue : number;
    public Products : OrderProduct[];
}