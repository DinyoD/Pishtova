import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment as env } from '../../../environments/environment';
import { BadgesCountModel } from 'src/app/models/badge/badgesCount';

@Injectable({
  providedIn: 'root'
})
export class BadgesService {

  constructor( private httpClient: HttpClient ) { }

  public getUserBadges = (userId?: string): Observable<BadgesCountModel[]> => {
    return this.httpClient.get<BadgesCountModel[]>(env.API_URL + `/userbadges/users/${userId}`)
  }

  public getTestBadges = (testId: number): Observable<BadgesCountModel[]> => {
    return this.httpClient.get<BadgesCountModel[]>(env.API_URL + `/userbadges/tests/${testId}`)
  }
}
