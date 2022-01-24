import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { SubjectModel } from 'src/app/models/subject';
import { StorageService, SubjectService} from '../../services';

@Component({
  selector: 'app-main-screen',
  templateUrl: './main-screen.component.html',
  styleUrls: ['./main-screen.component.css']
})
export class MainScreenComponent implements OnInit {


  public subjects: SubjectModel[] = []

  constructor(
    private subjectService: SubjectService, 
    private router : Router,
    private storage: StorageService
    ) { }

  ngOnInit(): void {
    this.subjectService.getAllSubjects().subscribe(
      s => this.subjects = s
    );
  }
  chooseSubject(subject: SubjectModel){
    this.storage.setItem('subjectName', subject.name);
    this.storage.setItem('subjectId', subject.id.toString());
    this.router.navigate([`subject/${subject.id}`]);
  }

}
