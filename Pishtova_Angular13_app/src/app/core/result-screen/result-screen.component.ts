import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { PointsService, SubjectService, TestService } from 'src/app/services';

@Component({
  selector: 'app-result-screen',
  templateUrl: './result-screen.component.html',
  styleUrls: ['./result-screen.component.css']
})
export class ResultScreenComponent implements OnInit {

  public points: number|null = null
  public subjectId: number|undefined = undefined;
  constructor(
    private testService: TestService,
    private pointsService: PointsService,
    private subjectService: SubjectService,
    private router: Router) { }

  ngOnInit(): void {
    this.testService.sendInTestStateChangeNotification(false);
    this.points = this.pointsService.gettingPoints();
    this.subjectId = this.subjectService.getCurrentSubject()?.id ;
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
}
