import { ChangeDetectorRef, Component, OnInit, Pipe, PipeTransform } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';

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
  public authorInd = 1;
  public poemInd = 4;

  constructor(private cd: ChangeDetectorRef) { }

  ngOnInit(): void {
    this.url = `https://pishtovyapp.com/${this.authorInd}/${this.poemInd}/1.html`;
  }

  public handlerTextBtn = ():void => {
    this.url =`https://pishtovyapp.com/${this.authorInd}/${this.poemInd}/1.html`;
    this.cd.detectChanges();
  }

  public handlerAnaliseBtn = ():void => {
    this.url =`https://pishtovyapp.com/${this.authorInd}/${this.poemInd}/2.html`;
    this.cd.detectChanges();
  }

  public handlerExtrasBtn = ():void => {
    this.url =`https://pishtovyapp.com/${this.authorInd}/${this.poemInd}/3.html`;
    this.cd.detectChanges();
  }

}
