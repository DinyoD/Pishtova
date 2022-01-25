import { ChangeDetectorRef, Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { select, Store } from '@ngrx/store';
import { ProblemModel } from 'src/app/models/problem';
import { ProblemService } from 'src/app/services'
import { SubjectState } from '../+store/core.state';
import * as StateActions from '../+store/actions';
import { AnswerModel } from 'src/app/models/answer';

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

  constructor(
    private problemService: ProblemService,
    private store: Store<SubjectState>,
    private cd: ChangeDetectorRef) { 

      this.problemNumber$.subscribe( n => this.problemNumber = n);
      this.store.dispatch(new StateActions.SetProblemNumber(1));
    }
    
    ngOnInit(): void {
      this.problemService.generateTestBySubjectId(this.subjectId).subscribe( (problems) => this.problems = problems);
    }
    
    nextProblem(){
      if (!this.someAnswerIsClicked) {
        return;
      }
      this.store.dispatch(new StateActions.SetProblemNumber(this.problemNumber + 1));
      this.someAnswerIsClicked = false;
       
    }

    chooseAnswer(selectedAnswer: AnswerModel){
      if (this.someAnswerIsClicked) {
        return;
      }
      this.someAnswerIsClicked = true;
      this.selectedAnswerId = selectedAnswer.id;
      this.cd.detectChanges();
      
    }
  }



