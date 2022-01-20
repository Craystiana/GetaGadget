import { Component, OnInit } from '@angular/core';
import { ToastController } from '@ionic/angular';
import { first, take } from 'rxjs/operators';
import { Coupon } from '../models/order/coupon';
import { Order } from '../models/order/order';
import { ProductService } from '../product/product.service';
import { OrderService } from './order.service';

@Component({
  selector: 'app-order',
  templateUrl: './order.page.html',
})
export class OrderPage implements OnInit {
  public order: Order;
  public coupons: Coupon[];

  constructor(private orderService : OrderService, private productService: ProductService, private toastCtrl : ToastController) { }

  ngOnInit() { 
  }
  
  ionViewWillEnter() {
    this.initData();
  }

  initData(){
    this.orderService.getOrder().pipe(take(1)).subscribe(data =>{
      this.order = data;
      this.productService.getCoupons().pipe(take(1)).subscribe(
        data => {
          this.coupons = data;
          var valid = null;
          for (var coupon of this.coupons) {
            if (this.order.totalValue > coupon.minOrderValue){
              valid = coupon;
            }
          }
        
          if (valid != null){
            this.toastCtrl.create({
              message: 'Use code ' + valid.code + ' to get a discount',
              position: 'top',
              color: 'success',
              duration: 5000,
              buttons: ['I\'m rich', 'I\'ll remember']
            }).then((el) => el.present())
          }
        }
      );
    });
  }

  addToCart(productId){
    this.orderService.addProduct(productId).pipe(first()).subscribe(
      () => {
        this.toastCtrl.create({
          message: 'Product added to cart',
          duration: 5000,
          position: 'bottom',
          color: 'success',
          buttons: ['Dismiss']
        }).then((el) => el.present());
        this.initData();
      },
      () => {
        this.toastCtrl.create({
          message: 'Unable to add product to cart',
          duration: 5000,
          position: 'bottom',
          color: 'danger',
          buttons: ['Dismiss']
        }).then((el) => el.present());
    });
  }

  removeFromCart(productId){
    this.orderService.deleteProduct(productId).pipe(first()).subscribe(
      () => {
        this.toastCtrl.create({
          message: 'Product deleted from cart',
          duration: 5000,
          position: 'bottom',
          color: 'success',
          buttons: ['Dismiss']
        }).then((el) => el.present());
        this.initData();
      },
      () => {
        this.toastCtrl.create({
          message: 'Unable to delete product from cart',
          duration: 5000,
          position: 'bottom',
          color: 'danger',
          buttons: ['Dismiss']
        }).then((el) => el.present());
    });
  }
}
