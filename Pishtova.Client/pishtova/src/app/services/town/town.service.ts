import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { ITown } from '../../interfaces/town';
import { environment as evn } from 'src/environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TownService {

  constructor(private httpClient: HttpClient) { }

  getTownsByMunicipalityId(municipalityId: number): Observable<ITown[]>{
    return this.httpClient.get<ITown[]>(evn.API_URL + `towns/bymunicipality/${municipalityId}`)
  }
}
