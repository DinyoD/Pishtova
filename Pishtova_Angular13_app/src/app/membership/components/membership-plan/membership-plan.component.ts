import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { MembershipService } from 'src/app/services';

@Component({
  selector: 'app-membership-plan',
  templateUrl: './membership-plan.component.html',
  styleUrls: ['./membership-plan.component.css']
})
export class MembershipPlanComponent {

  constructor(
    private membershipService: MembershipService,
    private route: Router
  ) { }


  public toBillingPortal(){
    this.membershipService.redirectToCustomerPortal().subscribe((data) => { window.location.href = data.url });
  }
  
  public toMembershipOptions() {
    this.route.navigate(['/memberships'])
  }
}
