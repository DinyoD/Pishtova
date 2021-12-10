import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { ITown } from '../../interfaces/town';
import { environment as env } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class TownService {

  constructor(private httpClient: HttpClient) { }

  getTownsByMunicipalityId(municipalityId: number): Observable<ITown[]>{
    return this.httpClient.get<ITown[]>(env.API_URL + `towns/bymunicipality/${municipalityId}`)
  }
}
