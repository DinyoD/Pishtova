import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { select, Store } from '@ngrx/store';
import { ProblemModel } from 'src/app/models/problem';
import { ProblemService } from 'src/app/services'
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

  constructor(
    private problemService: ProblemService,
    private store: Store<SubjectState>) { 
      this.problemNumber$.subscribe( n => this.problemNumber = n);
      this.store.dispatch(new StateActions.SetProblemNumber(1));
    }
    
    ngOnInit(): void {
      this.problemService.generateTestBySubjectId(this.subjectId).subscribe( (problems) => this.problems = problems);
    }
    
    nextProblem(){
      this.store.dispatch(new StateActions.SetProblemNumber(this.problemNumber + 1));
      console.log('next');     
      console.log(this.problemNumber);     
    }

    chooseAnswer(isCorrect: boolean){
      console.log(isCorrect);
      
    }
  }



