import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { PoemWithAuthorModel } from 'src/app/models/poem/poemWithAuthor';
import { environment as env } from 'src/environments/environment';
import { StorageService } from '../storage/storage.service';
import { Storage} from '../../utilities/constants/storage';

@Injectable({
  providedIn: 'root'
})
export class PoemsService {

  private _poemChangeSub: Subject<string|null> = new Subject<string|null>();
  public poemChanged: Observable<string|null> = this._poemChangeSub.asObservable();

  constructor(
    private httpClient : HttpClient,
    private storage: StorageService) { }

  public getThemesBySubjectId = (themeId: string): Observable<PoemWithAuthorModel[]> => {
    return this.httpClient.get<PoemWithAuthorModel[]>(env.API_URL + `/poems/theme/${themeId}`);
  }

  public setPoem = (poemName: string): void => {
    this._poemChangeSub.next(poemName);
    poemName != null ? this.storage.setItem(Storage.POEM, poemName)
                      : this.storage.removeItem(Storage.POEM);
  }

  public getCurrentPoem = (): string|null => {
    return this.storage.getItem<string>(Storage.POEM);
  }
}
