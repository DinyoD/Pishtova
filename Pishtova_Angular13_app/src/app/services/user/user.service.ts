import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { environment as env } from 'src/environments/environment';
import { ProfileModel } from 'src/app/models/profile/profile';
import { ChangeProfileEmailModel } from 'src/app/models/profile/changeProfileEmail';
import { ChangeProfileInfoModel } from 'src/app/models/profile/changeProfileInfo';
import { UserInfoModel } from 'src/app/models/user/userInfo';

@Injectable({
  providedIn: 'root'
})
export class UserService {; 

  constructor(private httpClient: HttpClient) { }

  public getUserProfile = (): Observable<ProfileModel> => {
    return this.httpClient.get<ProfileModel>(env.API_URL + '/users/profile');
  }

  public getUserInfo = (userId: string): Observable<UserInfoModel> => {
    return this.httpClient.get<UserInfoModel>(env.API_URL + `/users/info/${userId}`);
  }

  public setUserPictureUrl = (url: string): Observable<object> => {
    return this.httpClient.put(env.API_URL + '/users/updatepictureurl', {pictureUrl: url})
  }

  public updateUserInfo = (dataModel: ChangeProfileInfoModel): Observable<object> => {
    return this.httpClient.put(env.API_URL + '/users/updateuserinfo', dataModel)
  }

  public updateUserEmail = (dataModel: ChangeProfileEmailModel): Observable<object> => {
    return this.httpClient.put(env.API_URL + '/users/updateuseremail', dataModel)
  }
}
