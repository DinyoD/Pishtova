import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { PoemWithAuthorModel } from 'src/app/models/poem/poemWithAuthor';
import { PoemsService } from 'src/app/services/poems/poems.service';

@Component({
  selector: 'app-poems-screen',
  templateUrl: './poems-screen.component.html',
  styleUrls: ['../main-screen/main-screen.component.css','./poems-screen.component.css']
})
export class PoemsScreenComponent implements OnInit {

  public themeId: string|null = null;
  public poems: PoemWithAuthorModel[]|null = null;

  constructor(
    private actRoute: ActivatedRoute,
    private router: Router,
    private poemsService: PoemsService
  ) { }

  ngOnInit(): void {
    this.themeId = this.actRoute.snapshot.paramMap.get('themeId');
    if(this.themeId == null) {
      this.router.navigate(['/']);
      return;
    };
    this.poemsService.getThemesBySubjectId(this.themeId).subscribe(poems => this.poems = poems);
  }

  public handelPoemRedirect = (poemId: string, poemName: string): void => {
    if(poemId) {
      this.poemsService.setPoem(poemName);
      this.router.navigate([`/poems/${poemId}`]);
    }
  }
}
