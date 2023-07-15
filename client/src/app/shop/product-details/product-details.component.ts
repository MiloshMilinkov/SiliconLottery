import { Component, OnInit } from '@angular/core';
import { Product } from 'src/app/shared/models/product';
import { ShopService } from '../shop.service';
import { ActivatedRoute } from '@angular/router';
import { BreadcrumbService } from 'xng-breadcrumb';
import { BasketService } from 'src/app/basket/basket.service';
import { take } from 'rxjs';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {
  product?:Product;
  quantitySelected=1;
  quantityInBasket=0;

  constructor(private shopService:ShopService,private activatedRoute:ActivatedRoute
             ,private breadCrumbService: BreadcrumbService
             ,private basketService: BasketService){}

  ngOnInit(): void {
    this.getProduct();
  }

  getProduct(){
    const id = this.activatedRoute.snapshot.paramMap.get('id');
    if(id){
      this.shopService.getProduct(+id).subscribe({
        next: product => {
          this.product=product;
          this.breadCrumbService.set('@productDetails',product.name);
          this.basketService.basketSource$.pipe(take(1)).subscribe({
            next: basket =>{
              const item = basket?.items.find(x => x.id === +id);
              if(item){
                this.quantitySelected=item.quantity;
                this.quantityInBasket=item.quantity;
              }
            }
          })
        },
        error: error => console.log(error)
      })
    }
    
  }

  incrementQuantity(){
    this.quantitySelected++;
  }

  decrementQuantity(){
    this.quantitySelected--;
  }

  updateBasket(){
    if(this.product){
      if(this.quantitySelected>this.quantityInBasket){
        const itemsToAdd=this.quantitySelected-this.quantityInBasket;
        this.quantityInBasket+=itemsToAdd;
        this.basketService.addItemToBasket(this.product,itemsToAdd)
      }
      else{
        const itemsToRemove=this.quantityInBasket-this.quantitySelected;
        this.quantityInBasket+=itemsToRemove;
        this.basketService.removeItemFromBasket(this.product.id,itemsToRemove)
      }
    }
  }

  get buttonText(){
    return this.quantityInBasket===0? 'Add to basket' : 'Update basket'
  }
}
