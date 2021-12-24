import { ChangeDetectorRef, Component, OnInit, OnChanges, SimpleChanges } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { UserService } from 'src/app/services';
import { ConfirmationDialogModel } from 'src/app/shared/confirmation-dialog/confirmation-dialog';
import { ConfirmationDialogComponent } from 'src/app/shared/confirmation-dialog/confirmation-dialog.component';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit{
  showModale: boolean = false;
  isAuth: boolean = localStorage.getItem('token') != null;

  constructor(
    private cd: ChangeDetectorRef, 
    private userService : UserService,
    private dialog: MatDialog,
    private router: Router ) {}


  ngOnInit(): void {
    this.userService.authChanged.subscribe(isAuth => this.isAuth = isAuth);
  }

  changesChowModal(){
    this.showModale = !this.showModale;  
    this.cd.detectChanges();
  }

  hideModal(){
    this.showModale = false;
  }

  handleSignOut() {
    const dialogData = new ConfirmationDialogModel('Моля, потвърдете отписване!');
    const dialogRef = this.dialog.open(ConfirmationDialogComponent, { 
        closeOnNavigation: true,
        data: dialogData
    })

    dialogRef.afterClosed().subscribe(dialogResult => {
        if (dialogResult) {
            this.userService.logout();
            this.hideModal()
            this.router.navigate(["/"]);
        }
    });
  }
}
