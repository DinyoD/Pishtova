import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';

import { SubjectModel } from 'src/app/models/subject/subject';
import { AuthService, SubjectService, TestService } from '../../services';
import { HtmlHelper } from "../helpers/subjectHelper";

@Component({
  selector: 'app-main-screen',
  templateUrl: './main-screen.component.html',
  styleUrls: ['./main-screen.component.css']
})
export class MainScreenComponent implements OnInit{

  public subjects: Observable<SubjectModel[]> = this.subjectService.getAllSubjects();
  public userTestCount: number|null = null;
  public userId: string|null = null;

  constructor(
    private subjectService: SubjectService, 
    private testService: TestService,
    private authService: AuthService,
    private router : Router 
  ) {
    this.subjectService.setSubject(null);
  }

  ngOnInit(): void {
    this.subjectService.subjectChanged.subscribe(sbjModel => { if(sbjModel) this.router.navigate([`subject/${sbjModel?.id}`])});
    const user = this.authService.getCurrentUser();
    if (user) this.userId = user.id;
    if(this.userId) this.testService.getUserTestsCount(this.userId).subscribe(x => this.userTestCount = x)
  }

  public chooseSubject(sbj: SubjectModel): void{
    this.subjectService.setSubject(sbj);
  }

  public setClassBySbjName(name: string): string {
    return HtmlHelper.getCodeBySubjectName(name);
  }

}
