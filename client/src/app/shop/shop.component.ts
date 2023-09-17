import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Product } from '../shared/models/product';
import { ShopService } from './shop.service';
import { Type } from '../shared/models/type';
import { Brand } from '../shared/models/brand';
import { ShopParams } from '../shared/models/shopParams';
import {NgFor} from '@angular/common';
import {CdkAccordionModule} from '@angular/cdk/accordion';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {
  @ViewChild('search') searchTerm?:ElementRef;
  products:Product[] = [];
  brands  :Brand[]   = [];
  types   :Type[]    = [];

  items = ['Product Brands', 'Product Types'];
  expandedIndex = 0;
  shopParams=new ShopParams()
  sortOptions=[
    {name:'A-z: Low to high',value:'nameAsc'},
    {name:'A-z: High to low',value:'nameDesc'},
    {name:'Price: Low to high',value:'priceAsc'},
    {name:'Price: High to low',value:'priceDesc'}
  ];
  count=0;

  constructor(private shopService:ShopService){}

  ngOnInit(): void {
    this.getProducts();
    this.getBrands();
    this.getTypes();
  }

  getProducts(){
    this.shopService.getProducts(this.shopParams).subscribe({
      next: response =>{
        console.log(response);
        this.products = response.data;
        this.shopParams.pageIndex=response.pageIndex;
        this.shopParams.pageSize=response.pageSize;
        this.count=response.count;
        console.log(response.count);
      },
      error: error => console.log(error),
      complete: () => {
        console.log('request completed');
      }
    })}

  getBrands(){
    this.shopService.getBrands().subscribe({
      next: response => this.brands=[{id:0,name:'All'},...response],
      error: error => console.log(error),
      complete: () => {
        console.log('request completed');
      }
    })
  }

  getTypes(){
    this.shopService.getTypes().subscribe({
      next: response => this.types=[{id:0,name:'All'},...response],
      error: error => console.log(error),
      complete: () => {
        console.log('request completed');
      }
    })
  }

  onBrandSelected(brandId:number){
    this.shopParams.brandId=brandId;
    this.shopParams.pageIndex=1;
    this.getProducts();
  }

  onTypeSelected(typeId:number){
    this.shopParams.typeId=typeId;
    this.shopParams.pageIndex=1;
    this.getProducts();
  }

  onSortSelected(event:any){
    this.shopParams.orderBy=event.target.value;
    this.getProducts();
  }

  onPageChanged(event:any){
    if(this.shopParams.pageIndex!==event){
      this.shopParams.pageIndex=event;
      this.getProducts();
    }
  }

  onSearch(){
    this.shopParams.searchTerm=this.searchTerm?.nativeElement.value;
    this.shopParams.pageIndex=1;
    this.getProducts();
  }

  onResetSearch(){
    if(this.searchTerm){
      this.searchTerm.nativeElement.value='';
    }
    this.shopParams=new ShopParams();
    this.getProducts();
  }
  
}
