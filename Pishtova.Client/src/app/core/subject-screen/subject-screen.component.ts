import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { Store } from '@ngrx/store';

import { ConfirmationDialogModel } from 'src/app/shared/confirmation-dialog/confirmation-dialog';
import { ConfirmationDialogComponent } from 'src/app/shared/confirmation-dialog/confirmation-dialog.component';
import { SubjectState } from '../+store/core.state';
import * as StateActions from '../+store/actions';
import { StorageService } from 'src/app/services';

@Component({
  selector: 'app-subject-screen',
  templateUrl: './subject-screen.component.html',
  styleUrls: ['./subject-screen.component.css', '../main-screen/main-screen.component.css']
})
export class SubjectScreenComponent implements OnInit {
  public subjectId: number|undefined;
  public subjectName: string | null = null;
  public showTest: boolean = false;

  constructor(
    private actRoute: ActivatedRoute,
    private router: Router,
    private dialog: MatDialog,
    private store: Store<SubjectState>,
    private storage: StorageService) {
      this.subjectName = this.storage.getItem('subjectName');
      this.showTest = this.storage.getItem('test') != null;
    }

  ngOnInit(): void {
    if (this.storage.getItem('subjectId') != this.actRoute.snapshot.params.id) {
      this.router.navigate(['main'])
    }
    this.subjectId = this.actRoute.snapshot.params.id;
  }

  handelStartTest(){
    const dialogData = new ConfirmationDialogModel(`Стартирате тест по ${this.subjectName}.`);
    const dialogRef = this.dialog.open(ConfirmationDialogComponent, { 
        closeOnNavigation: true,
        data: dialogData
    })
    dialogRef.afterClosed().subscribe(dialogResult => {
      if (dialogResult) {
        this.storage.setItem('test','in');
        this.store.dispatch(new StateActions.SetProblemNumber(1))
        this.router.navigate([`subject/${this.actRoute.snapshot.params.id}/test`]);
      }
  });
  }
}
