import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot} from '@angular/router';

import { PointsService, SubjectService, TestService } from 'src/app/services';
import { ConfirmationDialogModel } from 'src/app/shared/confirmation-dialog/confirmation-dialog';
import { ConfirmationDialogComponent } from 'src/app/shared/confirmation-dialog/confirmation-dialog.component';


@Injectable({
  providedIn: 'root'
})
export class InTestGuard implements CanActivate {

  constructor(
    private router: Router,
    private dialog: MatDialog,
    private testService: TestService,
    private pointsService: PointsService
    ){};

  canActivate(
    _next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
    ) {
 
    if (this.testService.isInTest()) {
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
            this.testService.sendInTestStateChangeNotification(false);
            this.pointsService.clearPoints();
            this.router.navigate([url]);
          }
        })
  }

}

