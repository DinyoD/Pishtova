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

  private _themeChangeSub: Subject<ThemeModel|null> = new Subject<ThemeModel|null>();
  public themeChanged: Observable<ThemeModel|null> = this._themeChangeSub.asObservable();

  constructor(
    private httpClient : HttpClient,
    private storage: StorageService) { }

  public getThemesBySubjectId = (subjectId: string): Observable<ThemeModel[]> => {
    return this.httpClient.get<ThemeModel[]>(env.API_URL + `/themes/subject/${subjectId}`);
  }

  public setTheme = (theme: ThemeModel): void => {
    this._themeChangeSub.next(theme);
    if(theme != null){
      this.storage.setItem(Storage.THEME_NAME, theme.name);
      this.storage.setItem(Storage.THEME_ID, theme.id);
    }else{
      this.storage.removeItem(Storage.THEME_NAME);
      this.storage.removeItem(Storage.THEME_ID);
    }
  }

  public getCurrentThemeName = (): string|null => {
    return this.storage.getItem<string>(Storage.THEME_NAME);
  }

  public getCurrentThemeId = (): string|null => {
    return this.storage.getItem<string>(Storage.THEME_ID);
  }
}
