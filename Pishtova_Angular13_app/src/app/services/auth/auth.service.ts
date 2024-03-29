import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { JwtHelperService } from '@auth0/angular-jwt';

import { CustomEncoder } from 'src/app/authentication/helpers/custom-encoder';
import { UserForLoginModel } from '../../authentication/models/userForLogin';
import { UserForRegistrationModel } from '../../authentication/models/userForRegistration';
import { ForgotPasswordModel } from '../../authentication/models/forgotPassword';
import { ResetPasswordModel } from '../../authentication/models/resetPassword';
import ILoginResult from '../../authentication/models/results/LoginResult';
import { environment as env } from 'src/environments/environment';
import { Storage } from '../../utilities/constants/storage';
import { StorageService } from '..';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private _authChangeSub: Subject<boolean> = new Subject<boolean>();
  public isAuthChange: Observable<boolean> = this._authChangeSub.asObservable();

  constructor(
    private httpClient: HttpClient,
    private jwtHelper: JwtHelperService,
    private storage: StorageService
  ) {}

  public register = (user: UserForRegistrationModel): Observable<Object> => {
    return this.httpClient.post(env.API_URL + '/identity/register', user);
  };

  public delete = (id: string): Observable<Object> => {
    return this.httpClient.delete(env.API_URL + `/identity/delete/${id}`);
  };

  public login = (user: UserForLoginModel): Observable<ILoginResult> => {
    return this.httpClient.post<ILoginResult>(
      env.API_URL + `/identity/login`,
      user
    );
  };

  public logout = () => {
    this.storage.clear();
    this.sendAuthStateChangeNotification(false);
  };

  public confirmEmail = (token: string, email: string): Observable<Object> => {
    let params = new HttpParams({ encoder: new CustomEncoder() });
    params = params.append('token', token);
    params = params.append('email', email);
    return this.httpClient.get(env.API_URL + `/identity/emailconfirmation`, {
      params: params,
    });
  };

  public forgotPassword = (body: ForgotPasswordModel): Observable<Object> => {
    return this.httpClient.post(env.API_URL + '/identity/forgotpassword', body);
  };

  public resetPassword = (body: ResetPasswordModel): Observable<Object> => {
    return this.httpClient.post(env.API_URL + '/identity/resetpassword', body);
  };

  public isUserAuthenticated = (): boolean => {
    const token = this.storage.getItem<string>(Storage.TOKEN);
    return token != null && !this.jwtHelper.isTokenExpired(token);
  };

  public setAuthState = (token: string): void => {
    if (token) this.storage.setItem(Storage.TOKEN, token);
    this.sendAuthStateChangeNotification(token != null);
  };

  private sendAuthStateChangeNotification = (
    isAuthenticated: boolean
  ): void => {
    this._authChangeSub.next(isAuthenticated);
  };
}
