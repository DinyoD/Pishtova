import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TimeCounterComponent } from './time-counter/time-counter.component';
import { ConfirmationDialogComponent } from './confirmation-dialog/confirmation-dialog.component';



@NgModule({
  declarations: [
    TimeCounterComponent,
    ConfirmationDialogComponent
  ],
  imports: [
    CommonModule
  ],
  exports: [
    TimeCounterComponent,
    ConfirmationDialogComponent
  ]
})
export class SharedModule { }
