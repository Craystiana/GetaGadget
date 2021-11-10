import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastController } from '@ionic/angular';
import { first, take } from 'rxjs/operators';
import { ProductData } from 'src/app/models/product/productData';
import { ProductEdit } from 'src/app/models/product/productEdit';
import { ProductService } from '../product.service';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.page.html',
  styleUrls: ['./edit.page.scss'],
})
export class EditPage implements OnInit {

  private productData : ProductData;
  private isLoading: boolean;
  private productId : number;
  private product = new ProductEdit(null, null, null, null, null, null, null, null, null);
  private pictureBase64 : string;
  private isPictureLoaded = true;

  constructor(private router: Router, private productService : ProductService, private toastCtrl: ToastController, private route: ActivatedRoute,) { }

  ngOnInit() {
    this.loadData();
  }

  ionViewWillEnter(){
    this.loadData();
  }

  loadData(){
    this.route.queryParams.subscribe(params => {
      if (params['productId']) {
        this.productId = parseInt(params['productId']);
      }
    });

    this.productService.getProductData().pipe(take(1)).subscribe(
      data => {
        this.productData = data;
      }
    );

    if(this.productId !== undefined){
      this.productService.getProduct(this.productId).pipe(take(1)).subscribe(
        data => {
          this.product = data;
        }
      );
    }
  }

  onEdit(editForm: NgForm){
    this.isLoading = true;
    var model = new ProductEdit(this.productId,
                            editForm.value.name,
                            editForm.value.description,
                            editForm.value.price,
                            editForm.value.stock,
                            editForm.value.provider,
                            editForm.value.deliveryMethod,
                            editForm.value.category,
                            this.pictureBase64);               
    
    this.productService.edit(model).pipe(first()).subscribe(
      data =>{
        if(data==true){
          if(this.productId !== undefined){
            this.router.navigateByUrl('/product/detail?productId=' + this.productId);
          }
          else{
            this.router.navigateByUrl('/product');
          }
          this.toastCtrl.create({
            message: this.productId !== undefined ? 'Product edited succesfully.' : 'Product added succesfully.',
            duration: 5000,
            position: 'bottom',
            color: 'success',
            buttons: ['Dismiss']
          }).then((el) => el.present());
        }
        else{
          this.toastCtrl.create({
            message: 'Something went wrong. Please try again.',
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
          message: 'Something went wrong. Please try again.',
          duration: 5000,
          position: 'bottom',
          color: 'danger',
          buttons: ['Dismiss']
        }).then((el) => el.present());
        
        this.isLoading = false;
      }
    )                        
  }

  onDocumentUpload(files) {
    const reader = new FileReader();

    reader.readAsDataURL(files.item(0));
    this.isPictureLoaded = false;

    reader.onload = () => {
      this.pictureBase64 = reader.result.toString().split('base64,').pop();
      this.isPictureLoaded = true;
    }
  }

}
