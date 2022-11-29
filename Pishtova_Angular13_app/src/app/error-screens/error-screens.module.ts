import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NotFoundComponent } from './not-found/not-found.component';
import { RouterModule } from '@angular/router';



@NgModule({
  declarations: [
    NotFoundComponent,
  ],
  imports: [
    CommonModule,
    RouterModule.forChild([
      { path: '404', component: NotFoundComponent },
    ]),
  ], 
  exports: [
    NotFoundComponent
  ]
})
export class ErrorScreensModule { }
