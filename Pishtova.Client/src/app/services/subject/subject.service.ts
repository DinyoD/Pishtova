import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { SubjectModel } from 'src/app/models/subject';
import { environment as env } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SubjectService {

  constructor(private httpClient : HttpClient) { }

  getAllSubjects() : Observable<SubjectModel[]>{
    return this.httpClient.get<SubjectModel[]>(env.API_URL + `/subject/all`)
  }
  getSubjectById(id: number): Observable<SubjectModel>{
    return this.httpClient.get<SubjectModel>(env.API_URL + `/subject/${id}`);
  }

}
