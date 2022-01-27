import { ChangeDetectorRef, Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { select, Store } from '@ngrx/store';

import { ProblemModel } from 'src/app/models/problem';
import { AnswerModel } from 'src/app/models/answer';
import { ProblemPointModel } from 'src/app/models/problemPoint';
import { ProblemService, PointsService } from 'src/app/services'
import { SubjectState } from '../+store/core.state';
import * as StateActions from '../+store/actions';

@Component({
  selector: 'app-test-screen',
  templateUrl: './test-screen.component.html',
  styleUrls: ['./test-screen.component.css']
})
export class TestScreenComponent implements OnInit {

  @Input() subjectId: number | undefined;
  public problemNumber: number = 1;
  public problemNumber$ = this.store.pipe(select(x => x.subjectStateModel.problemNumber));

  public problems: ProblemModel[] = [];

  public someAnswerIsClicked: boolean = false;
  public selectedAnswerId: string|null = null

  public points: number|null = null;
  public maxScore: number = 20;

  constructor(
    private problemService: ProblemService,
    private pointsService: PointsService,
    private store: Store<SubjectState>,
    private cd: ChangeDetectorRef) { 

      this.problemNumber$.subscribe( n => this.problemNumber = n);
      this.store.dispatch(new StateActions.SetProblemNumber(1));
      this.points = this.pointsService.gettingPoints();
    }
    
    ngOnInit(): void {
      this.problemService.generateTestBySubjectId(this.subjectId).subscribe( (problems) => this.problems = problems);
      this.pointsService.pointsChanged.subscribe(p =>{ 
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
      const problemPointModel: ProblemPointModel = {
        subjectCategoryId: subjectCategoryId,
        points: selectedAnswer.isCorrect ? 1 : 0
      }
      this.pointsService.saveProblemScoreInDb(problemPointModel);
      this.cd.detectChanges();      
    }

    nextProblem(){
      if (!this.someAnswerIsClicked) {
        return;
      }
      this.store.dispatch(new StateActions.SetProblemNumber(this.problemNumber + 1));
      this.someAnswerIsClicked = false;    
    }

    finishTest(){
      
    }

  }



