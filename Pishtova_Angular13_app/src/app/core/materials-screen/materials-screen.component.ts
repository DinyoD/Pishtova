import { ChangeDetectorRef, Component, OnInit, Pipe, PipeTransform } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { ActivatedRoute } from '@angular/router';
import { AuthorModel } from 'src/app/models/author';
import { WorkModel } from 'src/app/models/work';
import { getAuthorPictureLinksCollection } from 'src/app/resource/data';
import { MaterialsService } from 'src/app/services/materials/materials.service';

@Pipe({ name: 'safe' })
export class SafePipe implements PipeTransform {
  constructor(private sanitizer: DomSanitizer) { }
  transform(url: string) {
    return this.sanitizer.bypassSecurityTrustResourceUrl(url);
  }
}

@Component({
  selector: 'app-materials-screen',
  templateUrl: './materials-screen.component.html',
  styleUrls: ['./materials-screen.component.css']
})

export class MaterialsScreenComponent implements OnInit {

  public url: string = "";
  public authors: AuthorModel[]|null = null;
  public works: WorkModel[]|null = null
  public authorIndex: number|null = null;
  public selectedAuthorIndex: number|null = null;
  public workIndex: number|null = null;
  public selectedWorkIndex: number|null = null;
  public authorPicLinks: string[] =[];

  public showButton: boolean = false;

  public selectedAuthorInd: number|null= null;
  public selectedWorkInd: number|null= null;
  public selectedButton: string|null= null;

  constructor(
    private cd: ChangeDetectorRef,
    private materialsService: MaterialsService,
    private actRoute: ActivatedRoute,) { }

  ngOnInit(): void {;
    const urlId = Number(this.actRoute.snapshot.paramMap.get('id'));
    this.materialsService.getAuthorsWithWorks(urlId).subscribe( a => this.authors = a.sort((x,y) => x.index - y.index));
    this.authorPicLinks = getAuthorPictureLinksCollection();
  }

  public chooseAuthor = (author: AuthorModel): void => {
    this.works = author.works.sort((x,y) => x.index - y.index);
    this.selectedAuthorIndex = author.index;
    this.selectedWorkInd = null;
  }

  public chooseWork = (workIndex: number):void => {
    this.workIndex = workIndex;
    this.selectedWorkInd = workIndex;
    this.authorIndex = this.selectedAuthorIndex;
    this.handlerTextBtn();
    this.showButton = true;
  }

  public handlerTextBtn = ():void => {
    this.url = this.setUrl();
    this.cd.detectChanges();
    this.selectedButton = 'text';
  }

  public handlerAnaliseBtn = ():void => {
    this.url = this.setUrl(2);
    this.cd.detectChanges();
    this.selectedButton = 'analize';
  }

  public handlerExtrasBtn = ():void => {
    this.url =this.setUrl(3);
    this.cd.detectChanges();
    this.selectedButton = 'extras';
  }

  private setUrl = (htmlNumber: number = 1): string => {
    let url: string =  `https://pishtovyapp.com/${this.authorIndex}/${this.workIndex}/${htmlNumber}.html`;
    return this.checkUrl(url);
  }

  private checkUrl = (url: string): string => {
    fetch(new Request(url))
    .then((a) => {console.log(a)})
    .catch((r)=>{ console.log(r)})

    return url;
  }

}

