import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Chart, registerables } from 'chart.js';
import { Observable } from 'rxjs';

import { SubjectModel } from 'src/app/models/subject/subject';
import { TestByDaysModel } from 'src/app/models/test/testsByDays';
import { TestScoreModel } from 'src/app/models/test/testScore';
import { UserRankModel } from 'src/app/models/user/userRank';
import { AuthService, StatsService, SubjectService } from '../../services';
import { HtmlHelper } from "../helpers/subjectHelper";

@Component({
  selector: 'app-main-screen',
  templateUrl: './main-screen.component.html',
  styleUrls: ['./main-screen.component.css']
})
export class MainScreenComponent implements OnInit{

  public userId: string|null = null;
  public subjects: Observable<SubjectModel[]> = this.subjectService.getAllSubjects();
  public userTestCount: number|null = null;
  public userBestRank: UserRankModel|null = null;
  public userLastTestsChart: Chart|null = null;
  public userLastDaysChart: Chart|null = null;

  constructor(
    private subjectService: SubjectService, 
    private statsService: StatsService,
    private authService: AuthService,
    private router: Router
  ) {
    this.subjectService.setSubject(null);
    Chart.register(...registerables);
  }

  ngOnInit(): void {
    this.subjectService.subjectChanged.subscribe(sbjModel => { if(sbjModel) this.router.navigate([`subject/${sbjModel?.id}`])});
    const user = this.authService.getCurrentUser();
    if( user == null ) return;
    this.userId = user.id;
    this.statsService.getTestCount(this.userId).subscribe(x => this.userTestCount = x);
    this.statsService.getUserBestRank(this.userId).subscribe(x => this.userBestRank = x);
    this.statsService.getLastTests(this.userId, 10).subscribe(x => this.generateTestsChart(x));
    this.statsService.getTestsByDays(this.userId, 7).subscribe(x => this.generateDaysChart(x));
  }

  public chooseSubject(sbj: SubjectModel): void{
    this.subjectService.setSubject(sbj);
  }

  public setClassBySbjName(name: string): string {
    return HtmlHelper.getCodeBySubjectName(name);
  }

  private generateTestsChart(data:TestScoreModel[] ): void {
    this.userLastTestsChart = new Chart('testsChart', {
      type: 'line',
      data: {
          labels: data.map(x => x.subjectName).map(x => HtmlHelper.getCodeBySubjectName(x)),
          datasets: [{
              label: 'успеваемост в процент',
              data: data.map(x => x.score),
              backgroundColor: 'rgba(51,51,51,0.8)',
              borderWidth: 1
          }]
      }
    })
  }

  private generateDaysChart(data:TestByDaysModel[] ): void {
    this.userLastDaysChart = new Chart('daysChart', {
      type: 'bar',
      data: {
          labels: data.map(x => x.date),
          datasets: [{
              label: 'брой тестове',
              data: data.map(x => x.testsCount),
              backgroundColor: 'rgba(51,51,51,0.6)',
              borderWidth: 1
          }]
      }
    })
  }

}
