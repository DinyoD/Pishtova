import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { environment as env } from 'src/environments/environment';
import { ProfileModel } from 'src/app/models/profile';

@Injectable({
  providedIn: 'root'
})
export class UserService {; 

  constructor(private httpClient: HttpClient) { }

  getUserInfo = (): Observable<ProfileModel> => {
    return this.httpClient.get<ProfileModel>(env.API_URL + '/users/getprofile');
  }
}
