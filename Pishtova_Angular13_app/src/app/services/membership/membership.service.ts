import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { environment as env} from 'src/environments/environment';
import { SessionModel } from 'src/app/membership/models/Session';
import { RequestMemberSessionModel } from 'src/app/membership/models/RequestMemberSession';
import { CustomerPortalModel } from 'src/app/membership/models/CustomerPortal';
import { ProductModel } from 'src/app/membership/models/Product';

declare const Stripe: any;

@Injectable({
  providedIn: 'root'
})

export class MembershipService {

  constructor(private http: HttpClient) { }

  public getMembership = (): Observable<ProductModel> => {
    return this.http.get<ProductModel>(env.API_URL + `/payments/product`);
  }
  
  public requestMemberSession = (priceId: string): void => {
      const model: RequestMemberSessionModel = {
        priceId: priceId,
        successUrl: env.PAY_SUCCESS_URL,
        failureUrl: env.PAY_CANCEL_URL
      }
      this.http.post<SessionModel>(env.API_URL+ '/payments/create-checkout-session', model)
              .subscribe((session) => {this.redirectToCheckout(session)});
  }

  private redirectToCheckout = (session: SessionModel): void => {
    const stripe = Stripe(session.publicKey);
    stripe.redirectToCheckout({ sessionId: session.sessionId });
  }

  public redirectToCustomerPortal(): Observable<CustomerPortalModel> {
     return this.http.post<CustomerPortalModel>( env.API_URL + '/payments/customer-portal', { returnUrl: env.HOME_URL } );
  }

}
