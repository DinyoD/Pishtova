import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TimeCounterComponent } from './time-counter/time-counter.component';



@NgModule({
  declarations: [
    TimeCounterComponent
  ],
  imports: [
    CommonModule
  ],
  exports: [
    TimeCounterComponent
  ]
})
export class SharedModule { }
