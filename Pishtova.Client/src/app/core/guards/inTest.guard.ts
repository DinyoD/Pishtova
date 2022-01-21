import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot} from '@angular/router';
import { SubjectService } from 'src/app/services';
import { ConfirmationDialogModel } from 'src/app/shared/confirmation-dialog/confirmation-dialog';
import { ConfirmationDialogComponent } from 'src/app/shared/confirmation-dialog/confirmation-dialog.component';


@Injectable({
  providedIn: 'root'
})
export class InTestGuard implements CanActivate {

  constructor(
    private router: Router,
    private dialog: MatDialog,
    private subjectService: SubjectService
    ){};

  canActivate(
    _next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
    ) {
 
    if (this.subjectService.isInTest()) {
      this.openDialog(state.url)
      return false;     
    }
    
    return true;
  }
  
  openDialog(url: string): void {
    const dialogData = new ConfirmationDialogModel(`Прекратявате ли теста?`, '*Незавършеният тест ще се изтрие!');
        const dialogRef = this.dialog.open(ConfirmationDialogComponent, { 
            closeOnNavigation: true,
            data: dialogData
        })
    
        dialogRef.afterClosed().subscribe(dialogResult => {
          if (dialogResult) {
            localStorage.removeItem('test');
            this.router.navigate([url]);
          }
        })
  }
}

