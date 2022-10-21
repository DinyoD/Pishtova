import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { ProblemModel } from 'src/app/models/problem/problem';
import { AnswerModel } from 'src/app/models/answer';
import { ProblemScoreModel } from 'src/app/models/problem/problemScore';
import { ProblemService, PointsService, TestService, BadgesService, AuthService } from 'src/app/services'
import { GreetingDialogComponent } from 'src/app/shared/greeting-dialog/greeting-dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { getBadgeCodeByPoints } from 'src/app/resource/data';
import { ISaveTestResult } from 'src/app/models/operation.result/saveTest';
import { ImageDialogComponent } from 'src/app/shared/image-dialog/image-dialog.component';
import { BadgeToSaveModel } from 'src/app/models/badge/badgeToSave';
import { TestToSaveModel } from 'src/app/models/test/testToSave';

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
    private authService: AuthService,
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
      this.pointsService.clearPoints();
      this.pointsService.pointsChanged.subscribe(p => { 
        this.points = p;
        this.maxScore = p + 20 - this.problemNumber;
      });
    }
    
    public chooseAnswer(selectedAnswer: AnswerModel, subjectCategoryId: number): void {
      
      if (this.someAnswerIsClicked) return;
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

    public nextProblem(): void {
      if (!this.nextStepIsValid) return;
      this.problemNumber += 1;
      this.someAnswerIsClicked = false;    
    }

    public finishTest(): void {

      if (!this.nextStepIsValid)  return;

      const testToSave = this.getTestToSave();
      if (!testToSave) return;

      this.testService.saveTest(testToSave).subscribe( (res: ISaveTestResult) => {          
        const winnedBadge = this.getWinnedBadge(res.testId);
        winnedBadge !== null ? this.badgeService.saveBadge(winnedBadge).subscribe(() => this.router.navigate(['/subject/' + this.subjectId +'/result'], {state: {testId: res.testId}}))
                             : this.router.navigate(['/subject/' + this.subjectId +'/result'], {state: {testId: res.testId}});
      });

    }

    public openImage(imageUrl: string): void {
      const dialogData = imageUrl;
      console.log(dialogData);
      
      this.dialog.open(ImageDialogComponent, { 
          data: dialogData
      })
    };

    private getWinnedBadge(testId: number): BadgeToSaveModel|null {
      const code = getBadgeCodeByPoints().get(this.points);
      if(!code) return null;
      const userId = this.authService.getCurrentUser()?.id;
      if (!userId) return null;
      const badge: BadgeToSaveModel = {
        userId: userId,
        testId: testId,
        badgeCode: code,
      };
      return badge;
    }
    private saveWinnedBadge(badge: BadgeToSaveModel): void {
      this.badgeService.saveBadge(badge).subscribe();
    }
    private getTestToSave(): TestToSaveModel|null {
      const userId = this.authService.getCurrentUser()?.id;
      if (!userId || this.subjectId == null) return null;
      const score = Math.trunc(this.points / 20 * 100);

      return {
        userId: userId,
        subjectId: this.subjectId,
        score: score
      }
    }
    private nextStepIsValid = (): boolean => this.someAnswerIsClicked && this.subjectId != null;
  }
  


