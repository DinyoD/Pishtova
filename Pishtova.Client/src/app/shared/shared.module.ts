import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TimeCounterComponent } from './time-counter/time-counter.component';
import { ConfirmationDialogComponent } from './confirmation-dialog/confirmation-dialog.component';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { NavBarComponent } from './nav-bar/nav-bar.component';



@NgModule({
  declarations: [
    TimeCounterComponent,
    ConfirmationDialogComponent,
    NavBarComponent
  ],
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule,
  ],
  exports: [
    TimeCounterComponent,
    ConfirmationDialogComponent,
    NavBarComponent
  ]
})
export class SharedModule { }
