import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShopComponent } from './shop.component';
import { ProductItemComponent } from './product-item/product-item.component';
import { SharedModule } from '../shared/shared.module';
import { ProductDetailsComponent } from './product-details/product-details.component';
import { ShopRoutingModule } from './shop-routing.module';
import {CdkAccordionModule} from '@angular/cdk/accordion';
import { AccordionModule } from 'ngx-bootstrap/accordion';
import { CoreModule } from "../core/core.module";


@NgModule({
    declarations: [
        ShopComponent,
        ProductItemComponent,
        ProductDetailsComponent
    ],
    bootstrap: [ShopComponent],
    imports: [
        CommonModule,
        SharedModule,
        ShopRoutingModule,
        CdkAccordionModule,
        AccordionModule.forRoot(),
        CoreModule
    ]
})
export class ShopModule { }
