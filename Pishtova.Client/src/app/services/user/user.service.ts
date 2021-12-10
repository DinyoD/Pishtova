import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { UserForRegistration } from 'src/app/interfaces/userForRegistration';
//import { IOperationResult} from '../../operationResult/IOperationResult';
import { environment as env } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private httpClient: HttpClient) { }

  createUser(user: UserForRegistration): Observable<Object>{
    return this.httpClient.post(env.API_URL + 'register', user);
  }
  confirmEmail(token: string, email: string): Observable<Object>{
    return this.httpClient.post(env.API_URL + `emailconfirmation`,{
      token: token,
      email: email
    })
  }
}
