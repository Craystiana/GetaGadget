import { Component, OnInit } from '@angular/core';
import { ToastController } from '@ionic/angular';
import { first, take } from 'rxjs/operators';
import { WishlistProduct } from '../models/wishlist/wishlistProduct';
import { WishlistService } from './wishlist.service';

@Component({
  selector: 'app-wishlist',
  templateUrl: './wishlist.page.html',
  styleUrls: ['./wishlist.page.scss'],
})
export class WishlistPage implements OnInit {
  public wishlist: WishlistProduct[];

  constructor(private wishlistService : WishlistService, private toastCtrl : ToastController,) { }

  ngOnInit() {
  }

  ionViewWillEnter() {
    this.initData();
  }

  initData(){
    this.wishlistService.getWishlist().pipe(take(1)).subscribe(data =>{
      this.wishlist = data;
    });
  }

  removeFromWishlist(productId){
    this.wishlistService.deleteProduct(productId).pipe(first()).subscribe(
      () => {
        this.toastCtrl.create({
          message: 'Product removed from wishlist',
          duration: 5000,
          position: 'bottom',
          color: 'success',
          buttons: ['Dismiss']
        }).then((el) => el.present());
        this.initData();
      },
      () => {
        this.toastCtrl.create({
          message: 'Unable to remove product from wishlist',
          duration: 5000,
          position: 'bottom',
          color: 'danger',
          buttons: ['Dismiss']
        }).then((el) => el.present());
    });
  }
}
