import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment as env } from '../../../environments/environment';
import { UserBadgesModel } from 'src/app/models/user/userBadges';
import { BadgeToSaveModel } from 'src/app/models/badge/badgeToSave';

@Injectable({
  providedIn: 'root'
})
export class BadgesService {

  constructor( private httpClient: HttpClient ) { }
    
  public saveBadge = (model: BadgeToSaveModel): Observable<Object> => {
    return this.httpClient.post(env.API_URL + `/userbadges/create`, model)
  }

  public getUserBadges = (userId?: string): Observable<UserBadgesModel> => {
    return this.httpClient.get<UserBadgesModel>(env.API_URL + `/userbadges/getall/${userId}`)
  }

  public getUserBadgesByTestId = (testId: number): Observable<number[]> => {
    return this.httpClient.get<number[]>(env.API_URL + `/userbadges/test/${testId}`)
  }
}
