import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';

import { ProblemScoreModel } from 'src/app/models/problem/problemScore';
import { SubjectInfo } from 'src/app/models/profile/subjectInfo';
import { CategoryWithPointsModel } from 'src/app/models/subjectCategory/categoryPoints';
import { environment as env } from 'src/environments/environment';
import { StorageService } from '..';

@Injectable({
  providedIn: 'root'
})
export class PointsService {

  private _pointsChangeSub: Subject<number> = new Subject<number>();
  public pointsChanged: Observable<number> = this._pointsChangeSub.asObservable();

  constructor(
    private httpClient : HttpClient,
    private storage: StorageService) { }

  public saveScore = (score: ProblemScoreModel): Observable<Object> => {
    this.addPoints(score.points)
    return this.httpClient.post(env.API_URL + '/scores/save', score);
  }

  public getPointsBySubjects = (): Observable<SubjectInfo[]> => {
    return this.httpClient.get<SubjectInfo[]>(env.API_URL + `/scores/userpointsbysubject`)
  }

  public getPointsBySubjectCategories = (subjectId: number): Observable<CategoryWithPointsModel[]> => {
    return this.httpClient.get<CategoryWithPointsModel[]>(env.API_URL + `/scores/subjectpointsbycategories/${subjectId}`);
  }

  private addPoints = (pointValue: number): void => {
    const points = this.gettingPoints() + pointValue;
    this._pointsChangeSub.next(points);
    this.storage.setItem('points', points.toString());
  }

  public gettingPoints = (): number => {
    try {
      const prevPoints = this.storage.getItem<string>('points');
      return prevPoints != null ? +prevPoints : 0;
    } catch (error) {
      return 0;
    }
  }

  public clearPoints =(): void => {
    this.storage.removeItem('points');
  }
}
