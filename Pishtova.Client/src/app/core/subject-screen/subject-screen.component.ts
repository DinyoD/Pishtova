import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';

import { ConfirmationDialogModel } from 'src/app/shared/confirmation-dialog/confirmation-dialog';
import { ConfirmationDialogComponent } from 'src/app/shared/confirmation-dialog/confirmation-dialog.component';
import { PointsService, SubjectService, TestService  } from 'src/app/services';
import { SubjectModel } from 'src/app/models/subject';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-subject-screen',
  templateUrl: './subject-screen.component.html',
  styleUrls: ['./subject-screen.component.css', '../main-screen/main-screen.component.css']
})
export class SubjectScreenComponent implements OnInit {

  public subject: SubjectModel| null= null;
  public showTest: boolean = false;

  constructor(
    private actRoute: ActivatedRoute,
    private router: Router,
    private dialog: MatDialog,
    private subjectService: SubjectService,
    private testService: TestService,
    private pointsService: PointsService) {

      this.showTest = this.testService.isInTest();
      this.subject = this.subjectService.getCurrentSubject();
    }

  ngOnInit(): void {
    this.pointsService.clearPoints();
    this.subjectService.subjectChanged.subscribe( sbj => {
      this.subject = sbj;
    })
    

    this.testService.inTestChanged.subscribe( inTest => this.showTest = inTest);
  }

  handelStartTest(){
    const dialogData = new ConfirmationDialogModel(`Стартирате тест по ${this.subject?.name}.`);
    const dialogRef = this.dialog.open(ConfirmationDialogComponent, { 
        closeOnNavigation: true,
        data: dialogData
    })
    dialogRef.afterClosed().subscribe(dialogResult => {
      if (dialogResult) {
        this.testService.sendInTestStateChangeNotification(true)
        this.router.navigate([`subject/${this.subject?.id}/test`]);
      }
  });
  }
}
