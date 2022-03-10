import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment as env } from '../../../environments/environment';
import { UserBadgesModel } from 'src/app/models/user/userBadges';

@Injectable({
  providedIn: 'root'
})
export class BadgesService {

  constructor(
    private httpClients: HttpClient ) { }

  public getUserBadges = (userId?: string): Observable<UserBadgesModel> => {
    return this.httpClients.get<UserBadgesModel>(env.API_URL + `/badges/${userId}`)
  }
}
