import { ChangeDetectorRef, Component, OnInit, OnChanges, SimpleChanges } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { fromEvent, Observable, Subscription } from 'rxjs';
import { AuthService, StorageService } from 'src/app/services';
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

  constructor(
    private cd: ChangeDetectorRef, 
    private userService : AuthService,
    private dialog: MatDialog,
    private router: Router,
    private storage: StorageService ) {
      this.isAuth = this.storage.getItem('token') != null;
    }


  ngOnInit(): void {
    this.userService.authChanged.subscribe(isAuth => this.isAuth = isAuth);
  }

  changeShowModal(): void{
    this.showModal = !this.showModal;
    this.cd.detectChanges();
    //this.clickDocumentSubstiption(true)
  }

  hideModal(): void{
    this.showModal = false;
    this.cd.detectChanges();   
    //this.clickDocumentSubstiption(false)
  };

  // clickDocumentSubstiption(subscripe: boolean): void{
  //   let subscription = new Subscription();
  //   if (subscripe) {
  //     subscription = fromEvent(document, 'click',).subscribe(() => {
  //       console.log(this.shouldShowModale);
  //       this.hideModal();
  //       console.log(this.shouldShowModale);
  //     })
  //   }
  //   else{
  //     subscription.unsubscribe();
  //     console.log(subscription);     
  //   }
  // }

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
            this.hideModal()
            this.router.navigate(["/"]);
        }
    });
  }
}
