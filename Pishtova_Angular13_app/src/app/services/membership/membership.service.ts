import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';

import { environment as env} from 'src/environments/environment';
import { MembershipPlanModel } from 'src/app/membership/models/MembershipPlan';
import { SessionModel } from 'src/app/membership/models/Session';
import { RequestMemberSessionModel } from 'src/app/membership/models/RequestMemberSession';
import { CustomerPortalModel } from 'src/app/membership/models/CustomerPortal';

declare const Stripe: any;

@Injectable({
  providedIn: 'root'
})

export class MembershipService {

  constructor(private http: HttpClient) { }

  public getMembership = (): Observable<MembershipPlanModel> => {
    return of({
      id: 'prod_LSupAmVWkLF05P',
      priceId: 'price_1KlyzeBd9uAKWbJc8pdwcItm',
      name: 'Premium Membership Plan',
      price: '15.84 BGN',
      features: [
        'Materials',
        'Tests',
        'Free cancelation',
      ],
    });
  }
  
  public requestMemberSession = (priceId: string): void => {
      const model: RequestMemberSessionModel = {
        priceId: priceId,
        successUrl: env.API_PAY_SUCCESS_URL,
        failureUrl: env.API_PAY_CANCEL_URL
      }
      this.http.post<SessionModel>(env.API_URL+ '/payments/create-checkout-session', model)
              .subscribe((session) => {this.redirectToCheckout(session)});
  }

  public redirectToCheckout = (session: SessionModel): void => {
    const stripe = Stripe(session.publicKey);
    stripe.redirectToCheckout({ sessionId: session.sessionId });
  }

  public redirectToCustomerPortal():Observable<CustomerPortalModel> {
     return this.http.post<CustomerPortalModel>( env.API_URL + '/payments/customer-portal', { returnUrl: env.API_HOME_URL } );
  }

}
