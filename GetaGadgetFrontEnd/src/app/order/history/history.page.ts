import { Component, OnInit } from '@angular/core';
import { take } from 'rxjs/operators';
import { Order } from 'src/app/models/order/order';
import { OrderHistory } from 'src/app/models/order/orderHistory';
import { OrderService } from '../order.service';

@Component({
  selector: 'app-history',
  templateUrl: './history.page.html',
  styleUrls: ['./history.page.scss'],
})
export class HistoryPage implements OnInit {
  private history : OrderHistory[];
  private currentOrder: Order;

  constructor(private orderService : OrderService) { }

  ngOnInit() {
  }

  ionViewWillEnter(){
    this.initData();
  }

  initData() {
    this.orderService.getOrderHistory().pipe(take(1)).subscribe(
      data => {
        this.history = data;
      }
    );
    this.currentOrder = undefined;
  }

  setOrder(order: Order){
    this.currentOrder = order;
    this.history = undefined;
  }
}
