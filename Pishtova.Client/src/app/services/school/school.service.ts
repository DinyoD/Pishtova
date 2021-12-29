import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { SchoolModel } from 'src/app/models/school';
import { environment as evn } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SchoolService {

  constructor(private httpClient : HttpClient) { }

  getSchoolsByTownId(townId: number): Observable<SchoolModel[]>{
    return this.httpClient.get<SchoolModel[]>(evn.API_URL + `/schools/bytown/${townId}`)
  }
}
