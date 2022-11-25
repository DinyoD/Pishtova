import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from 'src/app/services';

@Component({
  selector: 'app-email-confirmation',
  templateUrl: './email-confirmation.component.html',
  styleUrls: ['./email-confirmation.component.css']
})
export class EmailConfirmationComponent implements OnInit {

  public showSuccess: boolean = true;
  public showError: boolean = false;
  public errorMessage: string = '';

  constructor(
    private userService: AuthService,
    private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.confirmEmail();
  }

  // TODO display error message on BG depends on responce status code!!!!
  private confirmEmail = () => {
    this.showError = this.showSuccess = false;

    const token = this.route.snapshot.queryParams['token'];
    const email = this.route.snapshot.queryParams['email'];

    this.userService.confirmEmail(token, email)
    .subscribe(() => {
      this.showSuccess = true;
    },
    error => {
      this.showError = true;
      this.errorMessage = error;
    })
  }

}
