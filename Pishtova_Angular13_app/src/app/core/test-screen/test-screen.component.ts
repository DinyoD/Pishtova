import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { ChangeDetectorRef, Component, OnInit } from '@angular/core';

import { AnswerModel } from 'src/app/models/answer';
import { ProblemModel } from 'src/app/models/problem/problem';
import { TestToSaveModel } from 'src/app/models/test/testToSave';
import { ProblemScoreModel } from 'src/app/models/problem/problemScore';
import { ProblemService, PointsService, TestService, UserService } from 'src/app/services';
import { ISaveTestResult } from 'src/app/models/operation.result/saveTest';
import { ImageDialogComponent } from 'src/app/shared/image-dialog/image-dialog.component';
import { GreetingDialogComponent } from 'src/app/shared/greeting-dialog/greeting-dialog.component';

@Component({
  selector: 'app-test-screen',
  templateUrl: './test-screen.component.html',
  styleUrls: ['./test-screen.component.css']
})

export class TestScreenComponent implements OnInit {

  public subjectId: string | null = null;

  public problems: ProblemModel[] = [];
  public problemsCount: number = 0;
  public problemNumber: number = 1;
  public userId: string|undefined;

  public someAnswerIsClicked: boolean = false;
  public selectedAnswerId: string|null = null
  public points: number = 0;
  public maxScore: number = 0;

  constructor(
    private problemService: ProblemService,
    private pointsService: PointsService,
    private testService: TestService,
    private userService: UserService,
    private dialog: MatDialog,
    private cd: ChangeDetectorRef,
    private actRoute: ActivatedRoute,
    private router: Router) {
      if(this.testService.isInTest()) router.navigate(['/']);

      if (this.actRoute.snapshot.paramMap.get('id') != null) {
        this.subjectId = this.actRoute.snapshot.paramMap.get('id');
        this.testService.sendInTestStateChangeNotification(true);
      }

    }

    ngOnInit(): void {
      this.problemService.generateTestBySubjectId(this.subjectId).subscribe(problems => {
        this.problems = problems;
        this.problemsCount =  problems.length;
        this.maxScore = problems.length;
      });
      this.pointsService.clearPoints();
      this.pointsService.pointsChanged.subscribe(p => { 
        this.points = p;
        this.maxScore = p + this.problems.length - this.problemNumber;
      });
      this.userId = this.userService.getCurrentUser()?.id;
    }
    
    public chooseAnswer(selectedAnswer: AnswerModel, subjectCategoryId: number): void {
      
      if (this.someAnswerIsClicked) return;
      this.someAnswerIsClicked = true;
      this.selectedAnswerId = selectedAnswer.id;
      if (!this.userId) return;
      const problemScoreModel: ProblemScoreModel = {
        userId: this.userId,
        subjectCategoryId: subjectCategoryId,
        points: selectedAnswer.isCorrect ? 1 : 0
      }
      this.pointsService.addPoints(problemScoreModel.points);
      this.pointsService.saveScore(problemScoreModel).subscribe(() => {
          if (problemScoreModel.points == 0 ) {
            return;
          }
          const points = this.pointsService.gettingPoints();
          switch (points) {
          case this.problemsCount*0.7:
            this.dialog.open( GreetingDialogComponent, { data:  70})
            break;
          case this.problemsCount*0.8:
            this.dialog.open( GreetingDialogComponent, { data:  80})
          break;   
          case this.problemsCount*0.9:
            this.dialog.open( GreetingDialogComponent, { data:  90})
            break;
          case this.problemsCount:
            this.dialog.open( GreetingDialogComponent, { data:  100})
          break; 
          default:
            break;
          }
        }
      );
      this.cd.detectChanges();      
    }

    public nextProblem(): void {
      if (!this.nextStepIsValid()) return;
      this.problemNumber += 1;
      this.someAnswerIsClicked = false;    
    }

    public finishTest(): void {

      if (!this.nextStepIsValid())  return;

      const testToSave = this.getTestToSave();
      if (!testToSave) return;

      this.testService.saveTest(testToSave).subscribe( (res: ISaveTestResult) => 
        this.router.navigate(['/subjects/' + this.subjectId +'/result'], {state: {testId: res.testId}})
      );

    }

    public openImage(imageUrl: string): void {
      const dialogData = imageUrl;
      console.log(dialogData);
      
      this.dialog.open(ImageDialogComponent, { 
          data: dialogData
      })
    };


    private getTestToSave(): TestToSaveModel|null {;

      if (!this.userId || this.subjectId == null) return null;

      const score = Math.trunc(this.points / this.problemsCount * 100);

      return {
        userId: this.userId,
        subjectId: this.subjectId,
        score: score
      }
    }

    private nextStepIsValid = (): boolean => this.someAnswerIsClicked && (this.subjectId != null);
    
  }
  