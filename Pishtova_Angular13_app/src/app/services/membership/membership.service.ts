import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';

import { environment as env} from 'src/environments/environment';
import { MemberShipPlanModel } from 'src/app/membership/models/MembershipPlan';
import { SessionModel } from 'src/app/membership/models/Session';

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
    this.http
      .post<SessionModel>(env.API_URL+ '/payments/create-checkout-session', {
        priceId: priceId,
      })
      .subscribe((session) => {
        this.redirectToCheckout(session.sessionId);
      });
  }

  redirectToCheckout(sessionId: string) {
    const stripe = Stripe('pk_test_51KlsUjBd9uAKWbJcxmeKwIhKST32aXS6qhxwwj51aNS6LucDgNJCh9dpZsGoSFoUzufMoNB20lsXbtgMaWTmSLMY001SEdj7lK');

    stripe.redirectToCheckout({
      sessionId: sessionId,
    });
  }
}
