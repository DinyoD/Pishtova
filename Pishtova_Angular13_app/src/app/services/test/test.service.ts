import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';

import { environment as env } from '../../../environments/environment';
import { ISaveTestResult } from 'src/app/models/operation.result/saveTest';
import { TestToSaveModel } from 'src/app/models/test/testToSave';
import { Storage} from '../../utilities/constants/storage';

@Injectable({
  providedIn: 'root'
})
export class TestService {

  private _inTestChangeSub: Subject<boolean> = new Subject<boolean>();
  public inTestChanged: Observable<boolean> = this._inTestChangeSub.asObservable();

  constructor(
    private httpClient: HttpClient) { }


  public saveTest = (model: TestToSaveModel): Observable<ISaveTestResult> => {
    return this.httpClient.post<ISaveTestResult>(env.API_URL + `/tests`, model);
  }

  public sendInTestStateChangeNotification = (inTest: boolean): void => {
    this._inTestChangeSub.next(inTest);
    if (inTest) {
      sessionStorage.setItem(Storage.TEST, 'in');
    }else{
      sessionStorage.removeItem(Storage.TEST);
    }
  }

  public isInTest = (): boolean => {
    const storageItem = sessionStorage.getItem(Storage.TEST);
    return storageItem != null;
  }
}
