import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { SubjectModel } from 'src/app/models/subject';

import { ConfirmationDialogModel } from 'src/app/shared/confirmation-dialog/confirmation-dialog';
import { ConfirmationDialogComponent } from 'src/app/shared/confirmation-dialog/confirmation-dialog.component';
import { SubjectService} from '../../services';

@Component({
  selector: 'app-subject-screen',
  templateUrl: './subject-screen.component.html',
  styleUrls: ['./subject-screen.component.css', '../main-screen/main-screen.component.css']
})
export class SubjectScreenComponent implements OnInit {

  public subjectId: number|null = null;
  public subjectName = localStorage.getItem('subjectName');
  public showTest: boolean = localStorage.getItem('test') != null;
  public showNavigations: boolean = !this.showTest;

  constructor(
    private actRoute: ActivatedRoute,
    private subjectService: SubjectService,
    private router: Router,
    private dialog: MatDialog) {}

  ngOnInit(): void {
    if (localStorage.getItem('subjectId') != this.actRoute.snapshot.params.id) {
      this.router.navigate(['main'])
    }
    
  }

  handelStartTest(){
    const dialogData = new ConfirmationDialogModel(`Стартирате тест по ${this.subjectName}.`);
    const dialogRef = this.dialog.open(ConfirmationDialogComponent, { 
        closeOnNavigation: true,
        data: dialogData
    })
    dialogRef.afterClosed().subscribe(dialogResult => {
      if (dialogResult) {
        localStorage.setItem('test','in')
        this.router.navigate([`subject/${this.subjectId}/test/1`]);
      }
  });
  }
}
