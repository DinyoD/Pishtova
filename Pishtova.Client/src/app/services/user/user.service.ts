import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { CustomEncoder } from 'src/app/helpers/custom-encoder';
import { UserForLogin } from 'src/app/interfaces/auth/userForLogin';

import { UserForRegistration } from 'src/app/interfaces/auth/userForRegistration';
import  ILoginResult  from '../../interfaces/results/LoginResult';
import { environment as env } from 'src/environments/environment';
import { IForgotPassword } from 'src/app/interfaces/auth/forgotPassword';
import { IResetPassword } from 'src/app/interfaces/auth/resetPassword';
//import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private _authChangeSub = new Subject<boolean>()
  public authChanged = this._authChangeSub.asObservable();

  constructor(
    private httpClient: HttpClient, 
    //private jwtHelper: JwtHelperService
    ) { }

  public createUser = (user: UserForRegistration): Observable<Object> => {
    return this.httpClient.post(env.API_URL + '/identity/register', user);
  }

  
  public login = (user: UserForLogin): Observable<ILoginResult> => {
    return this.httpClient.post<ILoginResult>(env.API_URL + `/identity/login`, user)
  }

  public logout = () => {
    localStorage.removeItem("token");
    this.sendAuthStateChangeNotification(false);
  }

  public confirmEmail = (token: string, email: string): Observable<Object> => {
    let params = new HttpParams({ encoder: new CustomEncoder() })
    params = params.append('token', token);
    params = params.append('email', email);
    return this.httpClient.get(env.API_URL + `/identity/emailconfirmation`,{ params: params})
  }

  public forgotPassword(body: IForgotPassword): Observable<Object> {
    return this.httpClient.post(env.API_URL + '/identity/forgotpassword', body);
  }

  public resetPassword (body: IResetPassword): Observable<Object>{
    return this.httpClient.post(env.API_URL + '/identity/resetpassword', body)
  }

  public sendAuthStateChangeNotification = (isAuthenticated: boolean): void => {
    this._authChangeSub.next(isAuthenticated);
  }

  public isUserAuthenticated = (): boolean => {
    const token = localStorage.getItem("token");
    return token != null;
    // return token != null && !this.jwtHelper.isTokenExpired(token);
  }
}
