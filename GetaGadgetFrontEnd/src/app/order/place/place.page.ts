import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastController } from '@ionic/angular';
import { first, take } from 'rxjs/operators';
import { Coupon } from 'src/app/models/order/coupon';
import { PlaceOrder } from 'src/app/models/order/placeOrder';
import { ProductData } from 'src/app/models/product/productData';
import { ProductService } from 'src/app/product/product.service';
import { OrderService } from '../order.service';

@Component({
  selector: 'app-place',
  templateUrl: './place.page.html',
  styleUrls: ['./place.page.scss'],
})
export class PlacePage implements OnInit {
  private productData: ProductData;
  public isLoading: boolean;
  public deliveryMethod: number;
  public coupons: Coupon[];

  constructor(private productService : ProductService, private orderService : OrderService, private router: Router, private toastCtrl: ToastController) { }

  ngOnInit() {
  }

  ionViewWillEnter() {
    this.productService.getProductData().pipe(take(1)).subscribe(
      data => {
        this.productData = data;
      }
    );
  }

  setDeliveryMethod(deliveryMethodId: number){
    this.deliveryMethod = deliveryMethodId;
  }

  onPlaceOrder(orderForm: NgForm) {
    this.isLoading = true;

    var model = new PlaceOrder(orderForm.value.county,
                               orderForm.value.city,
                               orderForm.value.address,
                               orderForm.value.postalCode,
                               orderForm.value.cardNumber,
                               orderForm.value.cardCsv,
                               orderForm.value.cardExpirationDate,
                               this.deliveryMethod,
                               orderForm.value.coupon);

    this.orderService.placeOrder(model).pipe(first()).subscribe(
      data => {
        if (data == true) {
          this.router.navigateByUrl('/order/all');
          this.toastCtrl.create({
            message: 'Order placed successfully.',
            duration: 5000,
            position: 'bottom',
            color: 'success',
            buttons: ['Dismiss']
          }).then((el) => el.present());
        }
        else {
          this.toastCtrl.create({
            message: 'Placing order failed',
            duration: 5000,
            position: 'bottom',
            color: 'danger',
            buttons: ['Dismiss']
          }).then((el) => el.present());
        }
        this.isLoading = false;
      },
      error => {
        this.toastCtrl.create({
          message: 'Placing order failed',
          duration: 5000,
          position: 'bottom',
          color: 'danger',
          buttons: ['Dismiss']
        }).then((el) => el.present());

        this.isLoading = false;
      }
    )
  }
}
