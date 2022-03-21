import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment as env } from '../../../environments/environment';
import { UserBadgesModel } from 'src/app/models/user/userBadges';
import { BadgeModel } from 'src/app/models/badge/badge';

@Injectable({
  providedIn: 'root'
})
export class BadgesService {

  constructor( private httpClient: HttpClient ) { }
    
  public saveBadge = (code: number, testId: number): Observable<Object> => {
    const model: BadgeModel = {badgeCode: code, testId: testId}
    return this.httpClient.post(env.API_URL + `/userbadges/save`, model)
  }

  public getUserBadges = (userId?: string): Observable<UserBadgesModel> => {
    return this.httpClient.get<UserBadgesModel>(env.API_URL + `/userbadges/${userId}/all`)
  }

  public getUserBadgesByTestId = (testId: number): Observable<number[]> => {
    return this.httpClient.get<number[]>(env.API_URL + `/userbadges/test/${testId}`)
  }
}
