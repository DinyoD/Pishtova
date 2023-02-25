import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Observable, Subject } from 'rxjs';

import { environment as env } from 'src/environments/environment';
import { ProfileModel } from 'src/app/models/profile/profile';
import { UpdateEmailModel } from 'src/app/models/profile/updateEmail';
import { ChangeProfileInfoModel } from 'src/app/models/profile/changeProfileInfo';
import { UserInfoModel } from 'src/app/models/user/userInfo';
import { UpdatePictureModel } from 'src/app/models/profile/updatePicture';
import { CurrentUserModel } from 'src/app/models/user/currentUser';
import { StorageService } from '../storage/storage.service';
import { Storage} from '../../utilities/constants/storage';

@Injectable({
  providedIn: 'root'
})
export class UserService {; 

  private _avatarChangeSub: Subject<string> = new Subject<string>();
  public isAvatarChange: Observable<string> = this._avatarChangeSub.asObservable();

  constructor(
    private httpClient: HttpClient,
    private storage: StorageService,
    private jwtHelper: JwtHelperService) { }

  public getUserProfile = (userId: string): Observable<ProfileModel> => {
    return this.httpClient.get<ProfileModel>(env.API_URL + `/users/${userId}`);
  }

  public getUserInfo = (userId: string): Observable<UserInfoModel> => {
    return this.httpClient.get<UserInfoModel>(env.API_URL + `/users/${userId}/info`);
  }

  public updatePictureUrl = (dataModel: UpdatePictureModel): Observable<object> => {
    return this.httpClient.put(env.API_URL + '/users/updatepictureurl', dataModel);
  }

  public updateInfo = (dataModel: ChangeProfileInfoModel): Observable<object> => {
    return this.httpClient.put(env.API_URL + '/users/updateinfo', dataModel);
  }

  public updateEmail = (dataModel: UpdateEmailModel): Observable<object> => {
    return this.httpClient.put(env.API_URL + '/users/updateemail', dataModel);
  }

  public isSubscriber = (): Observable<boolean> => {
    return this.httpClient.get<boolean>(env.API_URL + `/users/${this.getCurrentUser()?.id}/issubscriber`);
  }

  public getCurrentUser = (): CurrentUserModel|null => {
    let user: CurrentUserModel|null = null;
    const token: string|null = this.storage.getItem<string>(Storage.TOKEN);
    if (token) {
      const decodedToken = this.jwtHelper.decodeToken(token);
      user = {
         id: decodedToken.userId,
         email: decodedToken.email,
      }
    }
    return user;
  }

  public getAvatarUrl = (): string|null => {
    return this.storage.getItem<string>(Storage.AVATAR_URL);
  }

  public setAvatarState = (token: string): void => {
    const avatarUrl = this.getAvatarUrlByToken(token);
    this.updateAvatar(avatarUrl);
  }

  public updateAvatar = (avatarUrl: string|null): void => {
    if(avatarUrl == null) return;
    this.storage.setItem(Storage.AVATAR_URL, avatarUrl);
    this.sendAvatarChangeNotification(avatarUrl); 
  }


  private getAvatarUrlByToken = (token: string): string|null => {
    if(token == null) return null;
    const decodedToken = this.jwtHelper.decodeToken(token);
    return decodedToken.avatarUrl;
  }

  private sendAvatarChangeNotification = (avatarUrl: string): void => {
    this._avatarChangeSub.next(avatarUrl);
  }
}

