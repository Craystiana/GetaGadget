import { Component, OnInit } from '@angular/core';
import { take } from 'rxjs/operators';
import { Order } from '../models/order/order';
import { OrderService } from './order.service';

@Component({
  selector: 'app-order',
  templateUrl: './order.page.html',
})
export class OrderPage implements OnInit {
  public order: Order;

  constructor(private orderService : OrderService) { }

  ngOnInit() {  }
  
  ionViewWillEnter(){
    this.orderService.getOrder().pipe(take(1)).subscribe(data =>{
      this.order = data;
    });
  }

  initData(){
    this.orderService.getOrder().pipe(take(1)).subscribe(data =>{
      this.order = data;
    });
  }
}
