import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TimeCounterComponent } from './time-counter/time-counter.component';
import { ConfirmationDialogComponent } from './confirmation-dialog/confirmation-dialog.component';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { AppRoutingModule } from '../app-routing.module';
import { LoadingIndicatorComponent } from './loading-indicator/loading-indicator.component';
import { ProgressBarComponent } from './progress-bar/progress-bar.component';
import { UploadFileDialogComponent } from './upload-file-dialog/upload-file-dialog.component';
import { UpdateProfileInfoDialogComponent } from './update-profile-info-dialog/update-profile-info-dialog.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { UpdateSchoolInfoDialogComponent } from './update-school-info-dialog/update-school-info-dialog.component';
import { MatIconModule } from '@angular/material/icon';
import { GreetingDialogComponent } from './greeting-dialog/greeting-dialog.component';
import { UserInfoDialogComponent } from './user-info-dialog/user-info-dialog.component';
import { BreadcrumbComponent } from './breadcrumb/breadcrumb.component';



@NgModule({
  declarations: [
    TimeCounterComponent,
    ConfirmationDialogComponent,
    LoadingIndicatorComponent,
    ProgressBarComponent,
    UploadFileDialogComponent,
    UpdateProfileInfoDialogComponent,
    UpdateSchoolInfoDialogComponent,
    GreetingDialogComponent,
    UserInfoDialogComponent,
    BreadcrumbComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatSelectModule,
    MatIconModule,
    AppRoutingModule
  ],
  exports: [
    TimeCounterComponent,
    ConfirmationDialogComponent,
    LoadingIndicatorComponent,
    ProgressBarComponent,
    BreadcrumbComponent
  ]
})
export class SharedModule { }
