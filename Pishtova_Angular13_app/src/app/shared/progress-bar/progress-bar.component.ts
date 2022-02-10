import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-progress-bar',
  templateUrl: './progress-bar.component.html',
  styleUrls: ['./progress-bar.component.css']
})
export class ProgressBarComponent implements OnInit {

  @Input() count: number|null = null;
  @Input() problemNumber: number = 1;

  public countArray: number[]|null = null;

  constructor() { }

  ngOnInit(): void {
    console.log(this.count);
    
    this.countArray = Array(this.count).fill(1).map((x, i) => i + 1);
    console.log(this.countArray);
    
  }

}
