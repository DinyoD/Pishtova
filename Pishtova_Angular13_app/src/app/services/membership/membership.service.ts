import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';

import { environment as env} from 'src/environments/environment';
import { MemberShipPlanModel } from 'src/app/membership/models/MembershipPlan';
import { SessionModel } from 'src/app/membership/models/Session';
import { RequestMemberSessionModel } from 'src/app/membership/models/RequestMemberSession';

declare const Stripe: any;

@Injectable({
  providedIn: 'root'
})

export class MembershipService {

  constructor(private http: HttpClient) { }

  getMembership(): Observable<MemberShipPlanModel> {
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
  
requestMemberSession(priceId: string): void {
    const model: RequestMemberSessionModel = {
      priceId: priceId,
      successUrl: env.API_PAY_SUCCESS_URL,
      failureUrl: env.API_PAY_CANCEL_URL
    }
    this.http.post<SessionModel>(env.API_URL+ '/payments/create-checkout-session', model)
             .subscribe((session) => {this.redirectToCheckout(session)});
  }

  redirectToCheckout(session: SessionModel) {
    const stripe = Stripe(session.publicKey);

    stripe.redirectToCheckout({
      sessionId: session.sessionId,
    });
  }
}
