import { Component, OnInit } from '@angular/core';
import { Product } from '../shared/models/product';
import { ShopService } from './shop.service';
import { Type } from '../shared/models/type';
import { Brand } from '../shared/models/brand';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {
  products:Product[] = [];
  brands  :Brand[]   = [];
  types   :Type[]    = [];
  brandIdSelected=0;
  typeIdSelected=0;
  sortSelected='name';
  sortOptions=[
    {name:'A-z: Low to high',value:'nameAsc'},
    {name:'A-z: High to low',value:'nameDesc'},
    {name:'Price: Low to high',value:'priceAsc'},
    {name:'Price: High to low',value:'priceDesc'}
  ];

  constructor(private shopServiceL:ShopService){}

  ngOnInit(): void {
    this.getProducts();
    this.getBrands();
    this.getTypes();
  }

  getProducts(){
    this.shopServiceL.getProducts(this.brandIdSelected,this.typeIdSelected,this.sortSelected).subscribe({
      next: response => this.products=response.data,
      error: error => console.log(error),
      complete: () => {
        console.log('request completed');
      }
    })}

  getBrands(){
    this.shopServiceL.getBrands().subscribe({
      next: response => this.brands=[{id:0,name:'All'},...response],
      error: error => console.log(error),
      complete: () => {
        console.log('request completed');
      }
    })
  }

  getTypes(){
    this.shopServiceL.getTypes().subscribe({
      next: response => this.types=[{id:0,name:'All'},...response],
      error: error => console.log(error),
      complete: () => {
        console.log('request completed');
      }
    })
  }

  onBrandSelected(brandId:number){
    this.brandIdSelected=brandId;
    this.getProducts();
  }

  onTypeSelected(typeId:number){
    this.typeIdSelected=typeId;
    this.getProducts();
  }

  onSortSelected(event:any){
    this.sortSelected=event.target.value;
    this.getProducts();
  }
}
