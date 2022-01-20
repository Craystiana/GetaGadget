import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { API_URL, PRODUCT_DATA_URL, PRODUCT_DETAIL_URL, PRODUCT_EDIT_URL, PRODUCT_DELETE_URL, PRODUCT_LIST_URL } from 'src/environments/environment';
import { Coupon } from '../models/order/coupon';
import { Product } from '../models/product/product';
import { ProductData } from '../models/product/productData';
import { ProductEdit } from '../models/product/productEdit';
import { ProductQuery } from '../models/product/productQuery';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor(private http: HttpClient) { }

  public getProductData() {
    return this.http.get(API_URL + PRODUCT_DATA_URL).pipe(
      map((data : ProductData) => {
        return data;
      })
    )
  }

  public getProductDetails(productId : number) {
    return this.http.get(API_URL + PRODUCT_DETAIL_URL + productId).pipe(
      map((data : Product) => {
        return data;
      })
    )
  }

  public getProduct(productId : number) {
    return this.http.get(API_URL + PRODUCT_EDIT_URL + "?productId=" + productId).pipe(
      map((data : ProductEdit) => {
        return data;
      })
    )
  }

  public edit(model: ProductEdit){
    return this.http.post(API_URL + PRODUCT_EDIT_URL, model).pipe(
      map((result: boolean) =>{
        return result;
      })
    );
  }

  public delete(productId: number){
    return this.http.post(API_URL + PRODUCT_DELETE_URL + productId, null).pipe(
      map((data : boolean) =>{
        return data;
      })
    );
  }

  public getProductList(model: ProductQuery){
    return this.http.post(API_URL + PRODUCT_LIST_URL, model).pipe(
      map((result: Product[]) =>{
        return result;
      })
    );
  }

  public getCoupons(){
    return this.http.get(API_URL + "/order/GetCoupons").pipe(
      map((result: Coupon[]) =>{
        return result;
      })
    );
  }
}
