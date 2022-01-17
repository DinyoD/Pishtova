import { Component, Input, OnInit } from '@angular/core';
import { ProblemModel } from 'src/app/models/problem';
import { SubjectModel } from 'src/app/models/subject';

@Component({
  selector: 'app-test-screen',
  templateUrl: './test-screen.component.html',
  styleUrls: ['./test-screen.component.css']
})
export class TestScreenComponent implements OnInit {
  @Input() subject!: SubjectModel | undefined;

  public problems: ProblemModel[] = [];
  constructor() { }

  ngOnInit(): void {
    
  }

}
