import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';

import { SubjectModel } from 'src/app/models/subject';
import { environment as env } from 'src/environments/environment';
import { StorageService } from '..';

@Injectable({
  providedIn: 'root'
})
export class SubjectService {

  private _inTestChangeSub = new Subject<boolean>();
  public inTestChanged = this._inTestChangeSub.asObservable();

  constructor(
    private httpClient : HttpClient,
    private storage: StorageService
    ) { }

  getAllSubjects() : Observable<SubjectModel[]>{
    return this.httpClient.get<SubjectModel[]>(env.API_URL + `/subject/all`)
  }

  getSubjectById(id: number): Observable<SubjectModel>{
    return this.httpClient.get<SubjectModel>(env.API_URL + `/subject/${id}`);
  }

  public isInTest = (): boolean => {
    const storageItem = this.storage.getItem("test");
    return storageItem != null;
  }
}
