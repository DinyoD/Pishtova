import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { MunicipalityModel } from 'src/app/models/municipality';
import { environment as evn } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MunicipalityService {

  constructor(private httpClient : HttpClient) { }

  getAllMunicipalities(): Observable<MunicipalityModel[]>{
    return this.httpClient.get<MunicipalityModel[]>(evn.API_URL + '/municipalities/all');
  }
}
