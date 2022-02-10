import { ChangeDetectorRef, Component, OnInit, OnChanges, SimpleChanges } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';

import { AuthService, SubjectService, TestService  } from 'src/app/services';
import { ConfirmationDialogModel } from 'src/app/shared/confirmation-dialog/confirmation-dialog';
import { ConfirmationDialogComponent } from 'src/app/shared/confirmation-dialog/confirmation-dialog.component';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit{

  public showModal: boolean = false;
  public isAuth: boolean = false;
  public inTest: boolean = false;
  public subjectName : string|undefined = undefined;

  constructor(
    private cd: ChangeDetectorRef, 
    private dialog: MatDialog,
    private router: Router,
    private userService : AuthService,
    private subjectService: SubjectService,
    private testService: TestService) {}
    
    
  ngOnInit(): void {
    this.isAuth = this.userService.isUserAuthenticated();
    this.inTest = this.testService.isInTest();
    this.subjectName = this.subjectService.getCurrentSubject()?.name;
    this.userService.isAuthChange.subscribe(isAuth => this.isAuth = isAuth);
    this.testService.inTestChanged.subscribe(inTest => this.inTest = inTest);
    this.subjectService.subjectChanged.subscribe(sbj => this.subjectName = sbj?.name);
  }

  changeShowModal(): void{
    this.showModal = !this.showModal;
    this.cd.detectChanges();
  }

  hideModal(): void{
    this.showModal = false;
    this.cd.detectChanges();   
  };

  handleSignOut() {
    this.hideModal();
    const dialogData = new ConfirmationDialogModel('Моля, потвърдете отписване!', '*Ако сте в тест, той ще се прекрати и изтрие!');
    const dialogRef = this.dialog.open(ConfirmationDialogComponent, { 
        closeOnNavigation: true,
        data: dialogData
    })

    dialogRef.afterClosed().subscribe(dialogResult => {
        if (dialogResult) {
            this.userService.logout();
            this.hideModal();
            this.router.navigate(["/"]);
        }
    });
  }
}
