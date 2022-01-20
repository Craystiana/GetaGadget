import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { API_URL, ORDER_ADD_URL, ORDER_DELETE_URL, ORDER_HISTORY_URL, ORDER_INDEX_URL, ORDER_PLACE_URL } from 'src/environments/environment';
import { Order } from '../models/order/order';
import { OrderHistory } from '../models/order/orderHistory';
import { PlaceOrder } from '../models/order/placeOrder';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  constructor(private http: HttpClient) { }

  public getOrder(){
    return this.http.get(API_URL + ORDER_INDEX_URL).pipe(
      map((data : Order) => {
        return data;
      })
    )
  }

  public addProduct(productId : number){
    return this.http.post(API_URL + ORDER_ADD_URL + '?productId=' + productId, null).pipe(
      map((result : boolean) => {
        return result;
      })
    );
  }

  public deleteProduct(productId : number){
    return this.http.delete(API_URL + ORDER_DELETE_URL + '?productId=' + productId).pipe(
      map((result : boolean) => {
        return result;
      })
    );
  }

  public getOrderHistory(){
    return this.http.get(API_URL + ORDER_HISTORY_URL).pipe(
      map((data : OrderHistory[]) => {
        return data;
      })
    )
  }

  placeOrder(model: PlaceOrder) {
    return this.http.post(API_URL + ORDER_PLACE_URL, model).pipe(
      map((result: boolean) => {
        return result;
      })
    );
  }
}
