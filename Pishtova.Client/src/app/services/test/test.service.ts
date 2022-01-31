import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { StorageService } from '..';

@Injectable({
  providedIn: 'root'
})
export class TestService {

  private _inTestChangeSub: Subject<boolean> = new Subject<boolean>();
  public inTestChanged: Observable<boolean> = this._inTestChangeSub.asObservable();

  constructor(private storage: StorageService) { }

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
