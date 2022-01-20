import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { map } from "rxjs/operators";
import { API_URL, WISHLIST_ADD_URL, WISHLIST_DELETE_URL, WISHLIST_INDEX_URL } from "src/environments/environment";
import { WishlistProduct } from "../models/wishlist/wishlistProduct";

@Injectable({
    providedIn: 'root'
  })
  export class WishlistService {
  
    constructor(private http: HttpClient) { }

    public getWishlist(){
        return this.http.get(API_URL + WISHLIST_INDEX_URL).pipe(
          map((data : WishlistProduct[]) => {
            return data;
          })
        )
      }

    public addProduct(productId : number){
        return this.http.post(API_URL + WISHLIST_ADD_URL + '?productId=' + productId, null).pipe(
          map((result : boolean) => {
            return result;
          })
        );
    }

    public deleteProduct(productId : number){
        return this.http.delete(API_URL + WISHLIST_DELETE_URL + '?productId=' + productId).pipe(
          map((result : boolean) => {
            return result;
          })
        );
      }
}