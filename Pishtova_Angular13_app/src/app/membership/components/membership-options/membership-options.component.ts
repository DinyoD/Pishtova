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
  public isSubscriber: boolean|undefined = false;

  constructor(
    private membershipService: MembershipService,
    private userService: UserService
    ) { 
  }

  ngOnInit(): void {
    this.membershipService.getMembership().subscribe(product => {
      this.product = product;
      this.prices = product.prices;
      this.prices.forEach( x => x.subscription = x.subscription[0].toUpperCase() + x.subscription.slice(1))
    })
    this.isSubscriber = this.userService.getCurrentUser()?.isSubscriber; 
  }

  public Checkout = (priceId: string): void =>  {
    if (priceId != this.clickedPriceId) return;
    this.membershipService.requestMemberSession(priceId);
  }

  public setClickedPrice(priceId: string){
    this.clickedPriceId = priceId;
  }

}
