import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthorModel } from 'src/app/models/author';

import { environment as env } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MaterialsService {

  constructor(
    private httpClient : HttpClient) { }

  public getAuthorsWithWorks = (subjectId: number): Observable<AuthorModel[]> => {
    return this.httpClient.get<AuthorModel[]>(env.API_URL + `/authors/subject/${subjectId}`)
  }
}
