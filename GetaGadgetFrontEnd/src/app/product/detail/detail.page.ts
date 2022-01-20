import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { LoadingController, ToastController } from '@ionic/angular';
import { first, take } from 'rxjs/operators';
import { AuthService } from 'src/app/auth/auth.service';
import { Product } from 'src/app/models/product/product';
import { OrderService } from 'src/app/order/order.service';
import { WishlistService } from 'src/app/wishlist/wishlist.service';
import { ProductService } from '../product.service';

@Component({
  selector: 'app-detail',
  templateUrl: './detail.page.html',
  styleUrls: ['./detail.page.scss'],
})
export class DetailPage implements OnInit {
  private product : Product;
  private productId : number;

  public isLoading: boolean;

  constructor(private route: ActivatedRoute, 
              private loadingController: LoadingController, 
              private productService : ProductService,
              private orderService : OrderService, 
              private wishlistService : WishlistService,
              private router: Router, 
              private toastCtrl : ToastController,
              private authService : AuthService) { }

  async ngOnInit() {
    const loading = await this.loadingController.create({
      message: 'Loading product. Please wait...',
      backdropDismiss: false
    });

    //await loading.present();

    this.route.queryParams.subscribe(params => {
      if (params) {
        this.productId = params['productId'];
      }
      if(this.productId === undefined){
        loading.dismiss();
        this.router.navigateByUrl("/product");
      }
    });

    this.productService.getProductDetails(this.productId)
    .pipe(take(1))
    .subscribe(
      data => {
        this.product = data;
        //loading.dismiss;
      }
    );
  }

  onDelete(){
    this.productService.delete(this.productId)
    .pipe(take(1))
    .subscribe(
      data => {
        if(data === true){
          this.router.navigateByUrl("/product");
          this.toastCtrl.create({
            message: 'Product removed successfully',
            duration: 5000,
            position: 'bottom',
            color: 'warning',
            buttons: ['Dismiss']
          }).then((el) => el.present());
        } else {
         this.presentError();
        }
      },
      error => {
        this.presentError();
      }
    );
  }

  editPage(){
    this.router.navigateByUrl("/product/edit?productId=" + this.productId);
  }

  presentError(){
    this.toastCtrl.create({
      message: 'Failed to remove product',
      duration: 5000,
      position: 'bottom',
      color: 'danger',
      buttons: ['Dismiss']
    }).then((el) => el.present());
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

  addToWishlist(productId){
    this.wishlistService.addProduct(productId).pipe(first()).subscribe(
      () => {
        this.toastCtrl.create({
          message: 'Product added to wishlist',
          duration: 5000,
          position: 'bottom',
          color: 'success',
          buttons: ['Dismiss']
        }).then((el) => el.present());
      },
      () => {
        this.toastCtrl.create({
          message: 'Unable to add product to wishlist',
          duration: 5000,
          position: 'bottom',
          color: 'danger',
          buttons: ['Dismiss']
        }).then((el) => el.present());
    });
  }
}
