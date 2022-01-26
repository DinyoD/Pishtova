import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';

import { ProblemPointModel } from 'src/app/models/problemPoint';
import { StorageService } from '..';

@Injectable({
  providedIn: 'root'
})
export class PointsService {

  private _pointsChangeSub = new Subject<number>();
  public pointsChanged = this._pointsChangeSub.asObservable();

  constructor(
    private httpClient : HttpClient,
    private storage: StorageService) { }

  saveProblemScoreInDb(points: ProblemPointModel): void{
    this.settingPoints(points.points)
  }

  settingPoints(pointValue: number): void{
    const points = this.gettingPoints() + pointValue;
    this._pointsChangeSub.next(points);
    this.storage.setItem('points', points.toString());
  }

  gettingPoints(): number{
    const prevPoints = this.storage.getItem<string>('points');
    let points = 0;
    try {
      points = prevPoints != null ? +prevPoints : 0;
      return points;
    } catch (error) {
      return 0
    }
  }

  clearPoints(){
    this._pointsChangeSub.next(0);
    this.storage.removeItem('points');
  }
}
