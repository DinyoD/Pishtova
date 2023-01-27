import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';

import { SubjectModel } from 'src/app/models/subject/subject';
import { environment as env } from 'src/environments/environment';
import { StorageService } from '..';
import { Storage} from '../../utilities/constants/storage';

@Injectable({
  providedIn: 'root'
})
export class SubjectService {

  private _subjectChangeSub: Subject<SubjectModel|null> = new Subject<SubjectModel|null>();
  public subjectChanged: Observable<SubjectModel|null> = this._subjectChangeSub.asObservable();

  constructor(
    private httpClient : HttpClient,
    private storage: StorageService) { }

  public getAllSubjects = () : Observable<SubjectModel[]> => {
    return this.httpClient.get<SubjectModel[]>(env.API_URL + `/subjects`)
  }

  public getSubjectById = (id: number): Observable<SubjectModel> => {
    return this.httpClient.get<SubjectModel>(env.API_URL + `/subjects/${id}`);
  }

  public setSubject = (sbj: SubjectModel|null): void=> {
    this._subjectChangeSub.next(sbj);
    if (sbj != null) {
      this.storage.setItem(Storage.SUBJECT_NAME, sbj.name);
      this.storage.setItem(Storage.SUBJECT_ID, sbj.id);     
    }else{
      this.storage.removeItem(Storage.SUBJECT_NAME);
      this.storage.removeItem(Storage.SUBJECT_ID);    
    }
  }

  public getCurrentSubject = (): SubjectModel|null => {
    const name= this.storage.getItem<string>(Storage.SUBJECT_NAME);
    const  id =this.storage.getItem<string>(Storage.SUBJECT_ID)
    if (name == null || id == null) {
      return null;
    }
    return {
       name: name, 
       id: id
    }
  }
}
