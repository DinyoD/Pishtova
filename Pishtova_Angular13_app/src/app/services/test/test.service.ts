import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';

import { environment as env } from '../../../environments/environment';
import { StorageService } from '..';
import { SubjectBaseModel } from 'src/app/models/subject/subjectBase';
import { ISaveTestResult } from 'src/app/models/operation.result/saveTest';

@Injectable({
  providedIn: 'root'
})
export class TestService {

  private _inTestChangeSub: Subject<boolean> = new Subject<boolean>();
  public inTestChanged: Observable<boolean> = this._inTestChangeSub.asObservable();

  constructor(
    private storage: StorageService,
    private httpClient: HttpClient) { }


  public saveTest = (subjectId: number): Observable<ISaveTestResult> => {
    return this.httpClient.post<ISaveTestResult>(env.API_URL + `/tests/save`, subjectId);
  }

  public sendInTestStateChangeNotification = (inTest: boolean): void => {
    this._inTestChangeSub.next(inTest);
    if (inTest) {
      this.storage.setItem('test', 'in');
    }else{
      this.storage.removeItem('test');
    }
  }

  public isInTest = (): boolean => {
    const storageItem = this.storage.getItem("test");
    return storageItem != null;
  }
}
