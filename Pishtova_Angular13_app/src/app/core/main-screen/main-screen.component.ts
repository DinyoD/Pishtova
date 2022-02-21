import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { SubjectModel } from 'src/app/models/subject/subject';
import { SubjectService} from '../../services';

@Component({
  selector: 'app-main-screen',
  templateUrl: './main-screen.component.html',
  styleUrls: ['./main-screen.component.css']
})
export class MainScreenComponent{

  public subjects: Observable<SubjectModel[]> = this.subjectService.getAllSubjects()

  constructor(
    private subjectService: SubjectService, 
    private router : Router 
    ) {
      this.subjectService.settingSubjectModel(null);
    }

  chooseSubject(sbj: SubjectModel): void{
    this.subjectService.subjectChanged.subscribe(s =>{ 
      if (s) {   
        this.router.navigate([`subject/${s?.id}`]);
      }
    });
    this.subjectService.settingSubjectModel(sbj);
  }

}
