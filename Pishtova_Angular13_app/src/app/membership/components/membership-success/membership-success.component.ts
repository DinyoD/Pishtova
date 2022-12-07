import { Component } from '@angular/core';
import { AuthService } from 'src/app/services';

@Component({
  selector: 'app-membership-success',
  templateUrl: './membership-success.component.html',
  styleUrls: ['../membership-failure/membership-failure.component.css']
})
export class MembershipSuccessComponent {
  constructor(
    private authService: AuthService
  ) { 
    this.authService.logout();
  }
}
