import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';

import { ConfirmationDialogModel } from 'src/app/shared/confirmation-dialog/confirmation-dialog';
import { ConfirmationDialogComponent } from 'src/app/shared/confirmation-dialog/confirmation-dialog.component';
import { SubjectService, TestService  } from 'src/app/services';
import { SubjectModel } from 'src/app/models/subject/subject';

@Component({
  selector: 'app-subject-screen',
  templateUrl: './subject-screen.component.html',
  styleUrls: ['../main-screen/main-screen.component.css','./subject-screen.component.css']
})
export class SubjectScreenComponent implements OnInit {

  public subject: SubjectModel| null= null;
  public showTest: boolean = false;

  constructor(
    private router: Router,
    private dialog: MatDialog,
    private subjectService: SubjectService,
    private testService: TestService
    ){}
    
    ngOnInit(): void {
      this.showTest = this.testService.isInTest();
      this.testService.inTestChanged.subscribe( inTest => this.showTest = inTest);
      this.subject = this.subjectService.getCurrentSubject();
      this.subjectService.subjectChanged.subscribe( sbj => this.subject = sbj);
    }

  handelStartTest(){
    this.router.navigate([`subjects/${this.subject?.id}/test`]);
  }

  public handelRankingRedirect = ():void => {
    this.router.navigate([`subjects/${this.subject?.id}/ranking`]);
  }

  public handelMaterialsRedirect = ():void => {
    this.router.navigate([`subjects/${this.subject?.id}/themes`]);
  }
}
