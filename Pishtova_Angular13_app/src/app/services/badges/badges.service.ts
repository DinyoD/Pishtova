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

  constructor(
    private httpClient: HttpClient ) { }

  public getUserBadges = (userId?: string): Observable<UserBadgesModel> => {
    return this.httpClient.get<UserBadgesModel>(env.API_URL + `/badges/${userId}`)
  }

  public saveBadge = (code: number): Observable<Object> => {
    const model: BadgeModel = {code: code}
    return this.httpClient.post(env.API_URL + `/badges/save`, model)
  }
}