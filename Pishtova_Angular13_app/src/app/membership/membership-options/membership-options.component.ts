import { Component, OnInit } from '@angular/core';
import { MembershipService } from 'src/app/services';import { PriceModel } from '../models/Price';
;
import { ProductModel } from '../models/Product';

@Component({
  selector: 'app-membership-options',
  templateUrl: './membership-options.component.html',
  styleUrls: ['./membership-options.component.css']
})
export class MembershipOptionsComponent implements OnInit{

  public product:ProductModel|null = null;
  public prices: PriceModel[]|null = null
  constructor(private membershipService: MembershipService) { 
  }

  ngOnInit(): void {
    this.membershipService.getMembership().subscribe(product => {
      this.product = product;
      this.prices = product.prices;
    })
  }

  public Checkout = (priceId: string): void =>  {
    this.membershipService.requestMemberSession(priceId);
  }

}
