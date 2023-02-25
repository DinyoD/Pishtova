import { Component, OnInit } from '@angular/core';
import { AuthService, MembershipService, UserService } from 'src/app/services';import { PriceModel } from '../../models/Price';
;
import { ProductModel } from '../../models/Product';

@Component({
  selector: 'app-membership-options',
  templateUrl: './membership-options.component.html',
  styleUrls: ['./membership-options.component.css']
})
export class MembershipOptionsComponent implements OnInit{

  public product:ProductModel|null = null;
  public prices: PriceModel[]|null = null
  public clickedPriceId: string|null = null;

  constructor(
    private membershipService: MembershipService) {}

  ngOnInit(): void {
    this.membershipService.getMembership().subscribe(product => {
      this.product = product;
      this.prices = product.prices;
      this.prices.forEach( x => x.subscription = x.subscription[0].toUpperCase() + x.subscription.slice(1))
    })
  }

  public Checkout = (priceId: string): void =>  {
    if (priceId != this.clickedPriceId) return;
    this.membershipService.requestMemberSession(priceId);
  }

  public setClickedPrice(priceId: string){
    this.clickedPriceId = priceId;
  }

}
