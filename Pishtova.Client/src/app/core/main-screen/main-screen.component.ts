import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { SubjectModel } from 'src/app/models/subject';
import { SubjectService} from '../../services';

@Component({
  selector: 'app-main-screen',
  templateUrl: './main-screen.component.html',
  styleUrls: ['./main-screen.component.css']
})
export class MainScreenComponent implements OnInit {


  public subjects: SubjectModel[] = []

  constructor(
    private subjectService: SubjectService, 
    private router : Router
    ) {
      this.subjectService.settingSubjectModel(null);
    }

  ngOnInit(): void {
    this.subjectService.getAllSubjects().subscribe(
      s => this.subjects = s
    );
  }
  chooseSubject(sbj: SubjectModel): void{
    this.subjectService.subjectChanged.subscribe(s =>{ 
      if (s) {   
        this.router.navigate([`subject/${s?.id}`]);
      }
    });
    this.subjectService.settingSubjectModel(sbj);
    console.log(sbj);
  }

}
