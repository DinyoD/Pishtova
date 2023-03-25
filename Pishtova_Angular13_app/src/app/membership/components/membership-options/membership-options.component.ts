import { Component, OnInit } from '@angular/core';
import { TestService } from 'src/app/services';

@Component({
  selector: 'app-membership-options',
  templateUrl: './membership-options.component.html',
  styleUrls: ['./membership-options.component.css'],
})
export class MembershipOptionsComponent implements OnInit {
  constructor(private testService: TestService) {}

  ngOnInit(): void {
    this.testService.sendInTestStateChangeNotification(false);
  }
}
