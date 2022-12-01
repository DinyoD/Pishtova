import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { TownModel } from '../../models/town';
import { environment as env } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class TownService {

  constructor(private httpClient: HttpClient) { }

  public getTownsByMunicipalityId = (municipalityId: number): Observable<TownModel[]> => {
    return this.httpClient.get<TownModel[]>(env.API_URL + `/towns/municipality/${municipalityId}`)
  }
}
