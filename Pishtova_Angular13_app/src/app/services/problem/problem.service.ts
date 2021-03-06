import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { environment as env } from 'src/environments/environment';
import { ProblemModel } from 'src/app/models/problem/problem';

@Injectable({
  providedIn: 'root'
})
export class ProblemService {

  constructor(private httpClient : HttpClient) { }

  public generateTestBySubjectId = (subjectId: number| null): Observable<ProblemModel[]> => {
    
    return this.httpClient.get<ProblemModel[]>(env.API_URL + `/problems/generateTest/${subjectId}`)
  }
}
