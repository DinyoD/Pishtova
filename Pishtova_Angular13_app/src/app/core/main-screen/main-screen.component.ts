import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { SubjectModel } from 'src/app/models/subject/subject';
import { SubjectService} from '../../services';

@Component({
  selector: 'app-main-screen',
  templateUrl: './main-screen.component.html',
  styleUrls: ['./main-screen.component.css']
})
export class MainScreenComponent implements OnInit{

  public subjects: Observable<SubjectModel[]> = this.subjectService.getAllSubjects();;

  constructor(
    private subjectService: SubjectService, 
    private router : Router 
    ) {
      this.subjectService.setSubject(null);
  }
  ngOnInit(): void {
    this.subjectService.subjectChanged.subscribe(s =>{ 
      if (s) {   
        this.router.navigate([`subject/${s?.id}`]);
      }
    });;
  }
  public chooseSubject(sbj: SubjectModel): void{
    this.subjectService.setSubject(sbj);
  }

  // TODO Optimize method!!
  public setClassBySbjName(name: string): string {
    let result: string = '';
    switch (name) {
      case "Биология":
        result = "bio"
        break;
      case "Български език":
        result = "bel"
        break;
    }

    return result;
  }

}
