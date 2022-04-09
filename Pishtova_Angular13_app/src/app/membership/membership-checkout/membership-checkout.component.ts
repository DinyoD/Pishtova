import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Observable } from 'rxjs';
import { MembershipService } from 'src/app/services';
import { MemberShipPlanModel } from '../models/MembershipPlan';

@Component({
  selector: 'app-membership-checkout',
  templateUrl: './membership-checkout.component.html',
  styleUrls: ['./membership-checkout.component.css']
})
export class MembershipCheckoutComponent {

  $membership: Observable<MemberShipPlanModel>;

  constructor(private membershipService: MembershipService) {

    this.$membership = this.membershipService.getMembership();
   }

  //ngOnInit(): void {}

  public onSubmit = (f: NgForm): void =>  {
    this.membershipService.requestMemberSession(f.value.priceId);
  }

}
