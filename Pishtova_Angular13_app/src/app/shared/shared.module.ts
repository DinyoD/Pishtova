import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TimeCounterComponent } from './time-counter/time-counter.component';
import { ConfirmationDialogComponent } from './confirmation-dialog/confirmation-dialog.component';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { AppRoutingModule } from '../app-routing.module';
import { LoadingIndicatorComponent } from './loading-indicator/loading-indicator.component';
import { ProgressBarComponent } from './progress-bar/progress-bar.component';
import { UploadFileDialogComponent } from './upload-file-dialog/upload-file-dialog.component';



@NgModule({
  declarations: [
    TimeCounterComponent,
    ConfirmationDialogComponent,
    NavBarComponent,
    LoadingIndicatorComponent,
    ProgressBarComponent,
    UploadFileDialogComponent
  ],
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule,
    AppRoutingModule
  ],
  exports: [
    TimeCounterComponent,
    ConfirmationDialogComponent,
    NavBarComponent,
    LoadingIndicatorComponent,
    ProgressBarComponent
  ]
})
export class SharedModule { }
