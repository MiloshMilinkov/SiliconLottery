import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Basket, BasketItem } from '../models/basket';
import { BasketService } from 'src/app/basket/basket.service';

@Component({
  selector: 'app-basket-summary',
  templateUrl: './basket-summary.component.html',
  styleUrls: ['./basket-summary.component.scss']
})
export class BasketSummaryComponent {
 @Output() addItem = new EventEmitter<BasketItem>();
@Output() decrementItem = new EventEmitter<{id: number}>();
 @Output() removeItem = new EventEmitter<{id: number, quantity: number}>();
@Input() isBasket = true;
 constructor(public basketService: BasketService){};

 addBasketItem(item: BasketItem){
  this.addItem.emit(item);
 }
unaddItem(id: number){
  this.decrementItem.emit({id});
}

 removeBasketItem(id: number, quantity = 1){
  this.removeItem.emit({id, quantity});
 }
}
