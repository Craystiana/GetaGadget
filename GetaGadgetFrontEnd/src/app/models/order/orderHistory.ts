import { Order } from "./order";

export class OrderHistory extends Order {
    public address : string;
    public cardDetails : string;
    public deliveryMethod : string;
}