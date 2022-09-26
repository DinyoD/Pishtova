import { Component, Input, OnInit } from '@angular/core';
import { NavigationElement } from 'src/app/models/navigation/navigationElement';
import { SubjectModel } from 'src/app/models/subject/subject';
import { SubjectService, TestService } from 'src/app/services';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.css']
})
export class NavigationComponent implements OnInit {

  @Input() elementName!: string;
  @Input() elementUrl!: string;

  public elements: NavigationElement[] = [];
  public sbjId: number|null = null;
  public showNavigation: boolean = false;
  
  constructor( private subjectService: SubjectService) {}

  ngOnInit(): void {
    const hasSbj = this.AddSubjectInNavigation(this.subjectService.getCurrentSubject());
    if (hasSbj) {
      
      this.elements = [...this.elements, {name: this.elementName, url: this.elementUrl}];
    }
    this.SubscribeForSubjectChanges();
  }

  private AddSubjectInNavigation(currentSbjModel: SubjectModel|null): boolean {
    if (currentSbjModel) {
      this.showNavigation = true;
      this.sbjId = currentSbjModel.id;
      this.elements = [{ name: currentSbjModel.name, url: '/subject/' + currentSbjModel.id}];
    }
    return currentSbjModel != null;
  }

  private SubscribeForSubjectChanges(): void {
    this.subjectService.subjectChanged.subscribe( (result) => this.AddSubjectInNavigation(result))
  }

}
