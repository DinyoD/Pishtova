import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ThemeModel } from 'src/app/models/theme/theme';
import { ThemesService } from 'src/app/services';

@Component({
  selector: 'app-themes-screen',
  templateUrl: './themes-screen.component.html',
  styleUrls: ['../main-screen/main-screen.component.css','./themes-screen.component.css']
})
export class ThemesScreenComponent implements OnInit {

  public subjectId: string|null = null;
  public themes: ThemeModel[]|null = null;

  constructor(
    private actRoute: ActivatedRoute,
    private router: Router,
    private themesService: ThemesService) { }

  ngOnInit(): void {;
    this.subjectId = this.actRoute.snapshot.paramMap.get('id');
    if(this.subjectId == null) {
      this.router.navigate(['/']);
      return;
    };
    this.themesService.getThemesBySubjectId(this.subjectId).subscribe( themes => this.themes = themes)
  }

  public handelThemeRedirect = (theme: ThemeModel): void => {
    if(theme) {
      this.themesService.setTheme(theme);
      this.router.navigate([`/subjects/${this.subjectId}/themes/${theme.id}`]);
    }
  }
}
