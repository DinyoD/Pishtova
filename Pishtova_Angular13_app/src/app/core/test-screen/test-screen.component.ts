import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { ProblemModel } from 'src/app/models/problem/problem';
import { AnswerModel } from 'src/app/models/answer';
import { ProblemScoreModel } from 'src/app/models/problem/problemScore';
import { ProblemService, PointsService, TestService, BadgesService } from 'src/app/services'
import { GreetingDialogComponent } from 'src/app/shared/greeting-dialog/greeting-dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { getBadgeCodeByPoints } from 'src/app/resource/data';
import { ISaveTestResult } from 'src/app/models/operation.result/saveTest';

@Component({
  selector: 'app-test-screen',
  templateUrl: './test-screen.component.html',
  styleUrls: ['./test-screen.component.css']
})
export class TestScreenComponent implements OnInit {

  public subjectId: number | null = null;

  public problems: ProblemModel[] = [];
  public problemNumber: number = 1;

  public someAnswerIsClicked: boolean = false;
  public selectedAnswerId: string|null = null
  public points: number = 0;
  public maxScore: number = 20;
  public navigationName: string = 'Тест';
  public navigationUrl: string = '';

  constructor(
    private problemService: ProblemService,
    private pointsService: PointsService,
    private testService: TestService,
    private badgeService: BadgesService,
    private dialog: MatDialog,
    private cd: ChangeDetectorRef,
    private actRoute: ActivatedRoute,
    private router: Router) {
      if (this.actRoute.snapshot.paramMap.get('id') != null) {
        this.subjectId = Number(this.actRoute.snapshot.paramMap.get('id'));
        this.navigationUrl = 'subject/' + Number(this.actRoute.snapshot.paramMap.get('id')) + '/test';
      }
    }

    ngOnInit(): void {
      this.problemService.generateTestBySubjectId(this.subjectId).subscribe(problems => this.problems = problems);
      this.pointsService.pointsChanged.subscribe(p => { 
        this.points = p;
        this.maxScore = p + 20 - this.problemNumber;
      });
    }
    
    chooseAnswer(selectedAnswer: AnswerModel, subjectCategoryId: number){
      
      if (this.someAnswerIsClicked) {
        return;
      }
      this.someAnswerIsClicked = true;
      this.selectedAnswerId = selectedAnswer.id;
      const problemPointModel: ProblemScoreModel = {
        subjectCategoryId: subjectCategoryId,
        points: selectedAnswer.isCorrect ? 1 : 0
      }
      this.pointsService.saveScore(problemPointModel).subscribe(() => {
          if (problemPointModel.points == 0 ) {
            return;
          }
          const points = this.pointsService.gettingPoints();
          switch (points) {
          case 14:
            this.dialog.open( GreetingDialogComponent, { data:  70})
            break;
          case 16:
            this.dialog.open( GreetingDialogComponent, { data:  80})
          break;   
          case 18:
            this.dialog.open( GreetingDialogComponent, { data:  90})
            break;
          case 20:
            this.dialog.open( GreetingDialogComponent, { data:  100})
          break; 
          default:
            break;
          }
        }
      );
      this.cd.detectChanges();      
    }

    nextProblem(){
      if (!this.someAnswerIsClicked) {
        return;
      }
      this.problemNumber += 1;
      this.someAnswerIsClicked = false;    
    }

    finishTest(){
      if (!this.someAnswerIsClicked || !this.subjectId) {
        return;
      }
      this.testService.saveTest(this.subjectId).subscribe( (res: ISaveTestResult) => {          
        const code = getBadgeCodeByPoints().get(this.points);
        if (code) {
          this.badgeService.saveBadge(code, res.testId).subscribe(
            () => this.router.navigate(['/test-result'], {state: {testId: res.testId}})
          );
        }else{
          this.router.navigate(['/test-result'], {state: {testId: res.testId}});
        }
      } );

    }
  }



