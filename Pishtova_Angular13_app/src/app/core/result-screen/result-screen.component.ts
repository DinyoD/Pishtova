import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { BadgesCountModel } from 'src/app/models/badge/badgesCount';

import { BadgesService, PointsService, SubjectService, TestService } from 'src/app/services';

@Component({
  selector: 'app-result-screen',
  templateUrl: './result-screen.component.html',
  styleUrls: ['./result-screen.component.css']
})
export class ResultScreenComponent implements OnInit {

  public points: number|null = null;
  public subjectId: number|undefined = undefined;
  public newBadgesCode: BadgesCountModel[] = [];

  constructor(
    private testService: TestService,
    private badgesService: BadgesService,
    private pointsService: PointsService,
    private subjectService: SubjectService,
    private router: Router) { }

  ngOnInit(): void {
    this.testService.sendInTestStateChangeNotification(false);
    this.points = this.pointsService.gettingPoints();
    this.subjectId = this.subjectService.getCurrentSubject()?.id;
    if(history.state.testId) this.badgesService.getUserBadgesByTestId(history.state.testId).subscribe(res => this.newBadgesCode = res);   
  }

  generateNewTest(): void{
    if (this.subjectId != undefined) {
      this.testService.sendInTestStateChangeNotification(true);
      this.pointsService.clearPoints();
      this.router.navigate([`/subject/${this.subjectId}/test`])
      return;
    }
    this.router.navigate(['/main']);
  }

  public badgeIsOwned = (code: number): boolean => { return this.newBadgesCode?.map(x => x.code).includes(code)}
}
