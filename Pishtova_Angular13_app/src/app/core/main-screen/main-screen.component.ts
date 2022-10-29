import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';

import { SubjectModel } from 'src/app/models/subject/subject';
import { TestByDaysModel } from 'src/app/models/test/testsByDays';
import { TestScoreModel } from 'src/app/models/test/testScore';
import { AuthService, StatsService, SubjectService } from '../../services';
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
  public userLastTests: TestScoreModel[]|null = null;
  public userLastDays: TestByDaysModel[]|null = null;

  constructor(
    private subjectService: SubjectService, 
    private statsService: StatsService,
    private authService: AuthService,
    private router : Router 
  ) {
    this.subjectService.setSubject(null);
  }

  ngOnInit(): void {
    this.subjectService.subjectChanged.subscribe(sbjModel => { if(sbjModel) this.router.navigate([`subject/${sbjModel?.id}`])});
    const user = this.authService.getCurrentUser();
    if( user == null ) return;
    this.userId = user.id;
    this.statsService.getTestCount(this.userId).subscribe(x => this.userTestCount = x);
    this.statsService.getLastTests(this.userId, 10).subscribe(x => this.userLastTests = x);
    this.statsService.getTestsByDays(this.userId, 7).subscribe(x => this.userLastDays = x);
  }

  public chooseSubject(sbj: SubjectModel): void{
    this.subjectService.setSubject(sbj);
  }

  public setClassBySbjName(name: string): string {
    return HtmlHelper.getCodeBySubjectName(name);
  }

}
