import { Component, OnInit } from '@angular/core';
import { Product } from 'src/app/models/product/product';

@Component({
  selector: 'app-search',
  templateUrl: './search.page.html',
  styleUrls: ['./search.page.scss'],
})
export class SearchPage implements OnInit {
  public productList: Product[];
  public searchTerm: string;

  constructor() { }

  ngOnInit() {
    console.log(this.productList);
  }

}
