import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { MembershipService } from 'src/app/services';
import { MemberShipPlanModel } from '../models/MembershipPlan';

@Component({
  selector: 'app-membership-options',
  templateUrl: './membership-options.component.html',
  styleUrls: ['./membership-options.component.css']
})
export class MembershipOptionsComponent{

  $membership: Observable<MemberShipPlanModel>;
  constructor(private membershipService: MembershipService) { 
    this.$membership = this.membershipService.getMembership();
  }

  //ngOnInit(): void {}

}
