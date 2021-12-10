import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { ISchool } from 'src/app/interfaces/school';
import { environment as evn } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SchoolService {

  constructor(private httpClient : HttpClient) { }

  getSchoolsByTownId(townId: number): Observable<ISchool[]>{
    return this.httpClient.get<ISchool[]>(evn.API_URL + `/schools/bytown/${townId}`)
  }
}
