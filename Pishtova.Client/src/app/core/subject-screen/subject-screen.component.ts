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

  public subject: SubjectModel|null = null;
  public showTest: boolean = this.actRoute.snapshot.url.length == 4 ? this.actRoute.snapshot.url[2].path == ('test') : false;;
  public showNavigations: boolean = !this.showTest;

  constructor(
    private actRoute: ActivatedRoute,
    private subjectService: SubjectService,
    private router: Router,
    private dialog: MatDialog) {}

  ngOnInit(): void {
    this.subjectService.getSubjectById(this.actRoute.snapshot.params.id)
    .subscribe(s => this.subject = s, () => this.router.navigate(['main']));
    
  }

  handelStartTest(){
    const dialogData = new ConfirmationDialogModel(`Стартирате тест по ${this.subject?.name}.`);
    const dialogRef = this.dialog.open(ConfirmationDialogComponent, { 
        closeOnNavigation: true,
        data: dialogData
    })
    dialogRef.afterClosed().subscribe(dialogResult => {
      if (dialogResult) {
        localStorage.setItem('test','in')
        this.router.navigate([`subject/${this.subject?.id}/test/1`]);
      }
  });
  }
}
