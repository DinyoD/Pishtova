import { Component, Input, OnInit } from '@angular/core';
import { ProblemModel } from 'src/app/models/problem';
import { ProblemService } from 'src/app/services'

@Component({
  selector: 'app-test-screen',
  templateUrl: './test-screen.component.html',
  styleUrls: ['./test-screen.component.css']
})
export class TestScreenComponent implements OnInit {

  @Input() subjectId: number | undefined;

  public problems: ProblemModel[] = [];

  constructor(private problemService: ProblemService) { }

  ngOnInit(): void {
    this.problemService.generateTestBySubjectId(this.subjectId).subscribe( (problems) => {
      this.problems = problems;
    })
  }

}
