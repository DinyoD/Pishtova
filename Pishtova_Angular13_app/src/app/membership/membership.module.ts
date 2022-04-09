import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MembershipCheckoutComponent } from './membership-checkout/membership-checkout.component';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { MembershipOptionsComponent } from './membership-options/membership-options.component';
import { RouterModule } from '@angular/router';
import { ForAuthenticatedUserGuard } from '../authentication/guards/auth.guard';
import { InTestGuard } from '../core/guards/inTest.guard';



@NgModule({
  declarations: [
    MembershipCheckoutComponent,
    MembershipOptionsComponent
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    FormsModule,
    RouterModule.forChild([
      { path: 'checkout', component: MembershipCheckoutComponent, canActivate: [ForAuthenticatedUserGuard, InTestGuard] },
      { path: 'memberships', component: MembershipOptionsComponent, canActivate: [ForAuthenticatedUserGuard, InTestGuard] },
    ]),
  ],
  exports: [
    MembershipOptionsComponent,
    // MembershipFailureComponent,
    // MembershipSuccessComponent,
  ],
})
export class MembershipModule { }