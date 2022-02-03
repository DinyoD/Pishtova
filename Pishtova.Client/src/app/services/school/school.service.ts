import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { SchoolModel } from 'src/app/models/school';
import { environment as env } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SchoolService {

  constructor(private httpClient : HttpClient) { }

  public getSchoolsByTownId = (townId: number): Observable<SchoolModel[]> => {
    return this.httpClient.get<SchoolModel[]>(env.API_URL + `/schools/bytown/${townId}`)
  }
}
