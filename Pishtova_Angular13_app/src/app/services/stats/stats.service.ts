import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { TestByDaysModel } from 'src/app/models/test/testsByDays';
import { TestScoreModel } from 'src/app/models/test/testScore';
import { UserPointsForSubjectModel } from 'src/app/models/user/userPointBySubject';
import { UserRankModel } from 'src/app/models/user/userRank';
import { environment as env } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class StatsService {

  constructor(
    private httpClient: HttpClient
    ) { }

    public getTestCount = (userId: string): Observable<number> => {
      return this.httpClient.get<number>(env.API_URL + `/userstats/tests?userId=${userId}`);
    }

    public getLastTests = (userId: string, testsCount: number): Observable<TestScoreModel[]> => {
      return this.httpClient.get<TestScoreModel[]>(env.API_URL + `/userstats/lasttests?userId=${userId}&testsCount=${testsCount}`);
    }

    public getTestsByDays = (userId: string, daysCount: number): Observable<TestByDaysModel[]> => {
      return this.httpClient.get<TestByDaysModel[]>(env.API_URL + `/userstats/lastdays?userId=${userId}&daysCount=${daysCount}`);
    }

    public getSubjectRanking = (subjectId: string): Observable<UserPointsForSubjectModel[]> => {
      return this.httpClient.get<UserPointsForSubjectModel[]>(env.API_URL + `/userstats/subjectrank?subjectId=${subjectId}`)
    }

    public getUserBestRank = (userId: string): Observable<UserRankModel> => {
      return this.httpClient.get<UserRankModel>(env.API_URL + `/userstats/bestrank?userId=${userId}`)
    }
  }
