import { ChangeDetectorRef, Component, OnInit, Pipe, PipeTransform } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { ActivatedRoute, Router } from '@angular/router';
import { PoemDetailsModel } from 'src/app/models/poem/poemDetails';
import { PoemsService } from 'src/app/services/poems/poems.service';

@Pipe({ name: 'safe' })
export class SafePipe implements PipeTransform {
  constructor(private sanitizer: DomSanitizer) { }
  transform(url: string) {
    return this.sanitizer.bypassSecurityTrustResourceUrl(url);
  }
}
@Component({
  selector: 'app-poem-details-screen',
  templateUrl: './poem-details-screen.component.html',
  styleUrls: ['./poem-details-screen.component.css']
})
export class PoemDetailsScreenComponent implements OnInit {
  
  public poem : PoemDetailsModel|null = null;
  public url: string = "https://pishtovyapp.com/themes/rodnotoichuzhdoto/jelezniatsvetilnik/text.html";
  public activeLink: number = 0;

  constructor(
    private actRoute: ActivatedRoute,
    private router: Router,
    private cd: ChangeDetectorRef,
    private poemsService: PoemsService
  ) { }

  ngOnInit(): void {
    const poemId = this.actRoute.snapshot.paramMap.get('id');
    if(poemId == null) {
      this.router.navigate(['/']);
      return;
    };
    this.poemsService.getPoem(poemId).subscribe(poem => {
      this.poem = poem;
      this.activeLink = 1;
      //this.url = poem.textUrl;
    })
  }

  public showText =(): void => {
    this.url = this.poem != null ? this.poem.textUrl : '';
    this.activeLink = 1;
    this.url = "https://pishtovyapp.com/themes/rodnotoichuzhdoto/jelezniatsvetilnik/text.html";

  }

  public showAnalisys =(): void => {
    this.url = this.poem != null ? this.poem.analysisUrl : '';
    this.activeLink = 2;
    this.url = "https://pishtovyapp.com/themes/rodnotoichuzhdoto/jelezniatsvetilnik/analisys.html";
  }

  public showExtras =(): void => {
    this.url = this.poem != null ? this.poem.extrasUrl : '';
    this.activeLink = 3;
    this.url = "https://pishtovyapp.com/themes/rodnotoichuzhdoto/jelezniatsvetilnik/extra.html";
  }

}
