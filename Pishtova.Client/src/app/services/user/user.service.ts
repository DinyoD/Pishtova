import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CustomEncoder } from 'src/app/helpers/custom-encoder';
import { UserForLogin } from 'src/app/interfaces/userForLogin';

import { UserForRegistration } from 'src/app/interfaces/userForRegistration';
import  ILoginResult  from '../../interfaces/results/LoginResult';
import { environment as env } from 'src/environments/environment';
import { IForgotPassword } from 'src/app/interfaces/forgotPassword';
import { IResetPassword } from 'src/app/interfaces/resetPassword';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private httpClient: HttpClient) { }

  public createUser = (user: UserForRegistration) => {
    return this.httpClient.post(env.API_URL + '/identity/register', user);
  }

  public login = (user: UserForLogin) => {
    return this.httpClient.post<ILoginResult>(env.API_URL + `/identity/login`, user)
  }

  public confirmEmail = (token: string, email: string): Observable<Object> => {
    let params = new HttpParams({ encoder: new CustomEncoder() })
    params = params.append('token', token);
    params = params.append('email', email);
      return this.httpClient.get(env.API_URL + `/identity/emailconfirmation`,{ params: params})
  }

  public forgotPassword(body: IForgotPassword) {
    return this.httpClient.post(env.API_URL + '/identity/forgotpassword', body);
  }

  public resetPassword (body: IResetPassword){
    return this.httpClient.post(env.API_URL + '/identity/resetpassword', body)
  }
}
