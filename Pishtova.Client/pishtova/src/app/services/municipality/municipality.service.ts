import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { IMunicipality } from 'src/app/interfaces/municipality';
import { environment as evn } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MunicipalityService {

  constructor(private httpClient : HttpClient) { }

  getAllMunicipalities(): Observable<IMunicipality[]>{
    return this.httpClient.get<IMunicipality[]>(evn.API_URL + 'municipalities/all');
  }
}
