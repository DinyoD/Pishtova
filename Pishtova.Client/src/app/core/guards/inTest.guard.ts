import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot} from '@angular/router';
import { ConfirmationDialogModel } from 'src/app/shared/confirmation-dialog/confirmation-dialog';
import { ConfirmationDialogComponent } from 'src/app/shared/confirmation-dialog/confirmation-dialog.component';


@Injectable({
  providedIn: 'root'
})
export class InTestGuard implements CanActivate {

  constructor(
    private router: Router,
    private dialog: MatDialog
    ){};

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
    ) {
      
    if (localStorage.getItem('test')) {
      const dialogData = new ConfirmationDialogModel(`Потвърждавате ли прекратяване на теста?`);
      const dialogRef = this.dialog.open(ConfirmationDialogComponent, { 
          closeOnNavigation: true,
          data: dialogData
      })
  
      dialogRef.afterClosed().subscribe(dialogResult => {
        if (dialogResult) {
          localStorage.removeItem('test')
          return true;
        }
        return false;
      })
    }
    
    return true;
  }
  
}
