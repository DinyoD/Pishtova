import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { ThemeModel } from 'src/app/models/theme/theme';

import { environment as env } from 'src/environments/environment';
import { StorageService } from '../storage/storage.service';
import { Storage} from '../../utilities/constants/storage';

@Injectable({
  providedIn: 'root'
})
export class ThemesService {

  private _themeChangeSub: Subject<string|null> = new Subject<string|null>();
  public themeChanged: Observable<string|null> = this._themeChangeSub.asObservable();

  constructor(
    private httpClient : HttpClient,
    private storage: StorageService) { }

  public getThemesBySubjectId = (subjectId: string): Observable<ThemeModel[]> => {
    return this.httpClient.get<ThemeModel[]>(env.API_URL + `/themes/subject/${subjectId}`);
  }

  public setTheme = (themeName: string): void => {
    this._themeChangeSub.next(themeName);
    themeName != null ? this.storage.setItem(Storage.THEME, themeName)
                      : this.storage.removeItem(Storage.THEME);
  }

  public getCurrentTheme = (): string|null => {
    return this.storage.getItem<string>(Storage.THEME);
  }
}
