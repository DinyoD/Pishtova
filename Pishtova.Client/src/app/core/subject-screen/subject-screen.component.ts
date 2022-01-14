import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-subject-screen',
  templateUrl: './subject-screen.component.html',
  styleUrls: ['./subject-screen.component.css']
})
export class SubjectScreenComponent implements OnInit {
  public subjectId: number = 0;

  constructor(private actRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.subjectId = this.actRoute.snapshot.params.id;
  }

}
