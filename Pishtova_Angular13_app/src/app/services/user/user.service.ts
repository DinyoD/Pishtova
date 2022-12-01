import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { environment as env } from 'src/environments/environment';
import { ProfileModel } from 'src/app/models/profile/profile';
import { UpdateEmailModel } from 'src/app/models/profile/updateEmail';
import { ChangeProfileInfoModel } from 'src/app/models/profile/changeProfileInfo';
import { UserInfoModel } from 'src/app/models/user/userInfo';
import { UpdatePictureModel } from 'src/app/models/profile/updatePicture';

@Injectable({
  providedIn: 'root'
})
export class UserService {; 

  constructor(private httpClient: HttpClient) { }

  public getUserProfile = (userId: string): Observable<ProfileModel> => {
    return this.httpClient.get<ProfileModel>(env.API_URL + `/users/${userId}`);
  }

  public getUserInfo = (userId: string): Observable<UserInfoModel> => {
    return this.httpClient.get<UserInfoModel>(env.API_URL + `/users/${userId}/info`);
  }

  public updatePictureUrl = (dataModel: UpdatePictureModel): Observable<object> => {
    return this.httpClient.put(env.API_URL + '/users/updatepictureurl', dataModel)
  }

  public updateInfo = (dataModel: ChangeProfileInfoModel): Observable<object> => {
    return this.httpClient.put(env.API_URL + '/users/updateinfo', dataModel)
  }

  public updateEmail = (dataModel: UpdateEmailModel): Observable<object> => {
    return this.httpClient.put(env.API_URL + '/users/updateemail', dataModel)
  }
}

