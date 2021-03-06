import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';

import { SubjectModel } from 'src/app/models/subject/subject';
import { SubjectWithUsersPointsModel } from 'src/app/models/subject/subjectWithUserPoints';
import { environment as env } from 'src/environments/environment';
import { StorageService } from '..';

@Injectable({
  providedIn: 'root'
})
export class SubjectService {

  private _subjectChangeSub: Subject<SubjectModel|null> = new Subject<SubjectModel|null>();
  public subjectChanged: Observable<SubjectModel|null> = this._subjectChangeSub.asObservable();

  constructor(
    private httpClient : HttpClient,
    private storage: StorageService
    ) { }

  public getAllSubjects = () : Observable<SubjectModel[]> => {
    return this.httpClient.get<SubjectModel[]>(env.API_URL + `/subject/all`)
  }

  public getSubjectById = (id: number): Observable<SubjectModel> => {
    return this.httpClient.get<SubjectModel>(env.API_URL + `/subject/${id}`);
  }

  public settingSubjectModel = (sbj: SubjectModel|null): void=> {
    this._subjectChangeSub.next(sbj);
    if (sbj != null) {
      this.storage.setItem('subjectName', sbj.name);
      this.storage.setItem('subjectId', sbj.id.toString());     
    }else{
      this.storage.removeItem('subjectName');
      this.storage.removeItem('subjectId');     
    }
  }

  public getCurrentSubject = (): SubjectModel|null => {
    const name= this.storage.getItem<string>('subjectName');
    const  id =this.storage.getItem<string>('subjectId')
    if (name == null || id == null) {
      return null;
    }
    return {
       name: name, 
       id: +id
    }
  }

  public getSubjectRanking = (id: number|null): Observable<SubjectWithUsersPointsModel> => {
    return this.httpClient.get<SubjectWithUsersPointsModel>(env.API_URL + `/subject/${id}/ranking`)
  }
}
