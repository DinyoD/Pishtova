import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { SchoolForRegisterModel } from 'src/app/models/school/schoolForRegister';
import { environment as env } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SchoolService {

  constructor(private httpClient : HttpClient) { }

  public getSchoolsByTownId = (townId: number): Observable<SchoolForRegisterModel[]> => {
    return this.httpClient.get<SchoolForRegisterModel[]>(env.API_URL + `/schools/bytown/${townId}`)
  }
}
