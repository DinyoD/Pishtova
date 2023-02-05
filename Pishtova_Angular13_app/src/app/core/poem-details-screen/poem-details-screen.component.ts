import { ChangeDetectorRef, Component, OnInit, Pipe, PipeTransform } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { ActivatedRoute, Router } from '@angular/router';
import { PoemDetailsModel } from 'src/app/models/poem/poemDetails';
import { PoemsService } from 'src/app/services/poems/poems.service';
import { Poem } from '../../utilities/constants/poem'

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
  public url: string = '';
  public activeLink: string|undefined = '';

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
      this.activeLink = Poem.TEXT;
      this.url = poem.textUrl;
      if(this.url?.includes('jelezniatsvetilnik')) this.url = "https://pishtovyapp.com/themes/rodnotoichuzhdoto/jelezniatsvetilnik/text.html";
    })
  }

  public showText =(): void => {
    this.url = this.poem != null ? this.poem.textUrl : '';
    this.activeLink = Poem.TEXT;
    if(this.url?.includes('jelezniatsvetilnik')) this.url = "https://pishtovyapp.com/themes/rodnotoichuzhdoto/jelezniatsvetilnik/text.html";

  }

  public showAnalisys =(): void => {
    this.url = this.poem != null ? this.poem.analysisUrl : '';
    this.activeLink = Poem.ANALISYS;
    if(this.url?.includes('jelezniatsvetilnik')) this.url = "https://pishtovyapp.com/themes/rodnotoichuzhdoto/jelezniatsvetilnik/analisys.html";
  }

  public showExtras =(): void => {
    this.url = this.poem != null ? this.poem.extrasUrl : '';
    this.activeLink = Poem.EXTRAS;
    if(this.url?.includes('jelezniatsvetilnik')) this.url = "https://pishtovyapp.com/themes/rodnotoichuzhdoto/jelezniatsvetilnik/extra.html";
  }

}
