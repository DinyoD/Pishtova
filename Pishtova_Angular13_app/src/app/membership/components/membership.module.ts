import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { CommonModule } from '@angular/common';
// import { FormsModule } from '@angular/forms';
// import { HttpClientModule } from '@angular/common/http';

import { MembershipOptionsComponent } from './membership-options/membership-options.component';
import { RouterModule } from '@angular/router';
import { ForAuthenticatedUserGuard } from '../../authentication/guards/auth.guard';
import { InTestGuard } from '../../core/guards/inTest.guard';
import { MembershipPlanComponent } from './membership-plan/membership-plan.component';
import { ForSubscribersGuard } from '../guards/subscriber.guard';
// import { MembershipFailureComponent } from './membership-failure/membership-failure.component';
// import { MembershipSuccessComponent } from './membership-success/membership-success.component';

@NgModule({
  declarations: [
    MembershipOptionsComponent,
    // MembershipFailureComponent,
    // MembershipSuccessComponent,
    // MembershipPlanComponent,
  ],
  imports: [
    CommonModule,
    // HttpClientModule,
    // FormsModule,
    RouterModule.forChild([
      {
        path: 'memberships',
        component: MembershipOptionsComponent,
        canActivate: [ForAuthenticatedUserGuard],
      },
      {
        path: 'memberships/plan',
        component: MembershipPlanComponent,
        canActivate: [
          ForAuthenticatedUserGuard,
          ForSubscribersGuard,
          InTestGuard,
        ],
      },
      // { path: 'memberships/failure', component: MembershipFailureComponent, canActivate: [ForAuthenticatedUserGuard,InTestGuard] },
      // { path: 'memberships/success', component: MembershipSuccessComponent, canActivate: [ForAuthenticatedUserGuard, InTestGuard] },
    ]),
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  exports: [
    MembershipOptionsComponent,
    // MembershipFailureComponent,
    // MembershipSuccessComponent,
    // MembershipPlanComponent,
  ],
})
export class MembershipModule {}
