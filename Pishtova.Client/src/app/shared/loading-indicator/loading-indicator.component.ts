import { Component } from '@angular/core';
import { LoadingIndicatorService } from 'src/app/services';

@Component({
  selector: 'app-loading-indicator',
  templateUrl: './loading-indicator.component.html',
  styleUrls: ['./loading-indicator.component.css']
})
export class LoadingIndicatorComponent { 
 
  constructor(private readonly loadingIndicatorService: LoadingIndicatorService) { } 
 
  get loading$() { 
    return this.loadingIndicatorService.loading$; 
  } 
 
} 
