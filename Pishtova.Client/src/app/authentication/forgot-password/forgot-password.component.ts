import { IForgotPassword } from '../../interfaces/forgotPassword';
import { UserService } from '../../services/user/user.service';
import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.css']
})
export class ForgotPasswordComponent {

  public successMessage: string = '';
  public errorMessage: string = '';
  public showSuccess: boolean = false;
  public showError: boolean = false;
  public form: FormGroup = new FormGroup({
    email: new FormControl(null, [Validators.required, Validators.email])
  })

  constructor(private userService: UserService) { }

  public validateControl = (controlName: string) => {
    return this.form.controls[controlName].invalid && this.form.controls[controlName].touched
  }

  public hasError = (controlName: string, errorName: string) => {
    return this.form.controls[controlName].hasError(errorName)
  }

  public forgotPassword = () => {
    const formValues = { ...this.form.value };
    const forgotPassDto: IForgotPassword = {
      email: formValues.email,
      clientURI: 'http://localhost:4200/auth/resetpassword'
    }

  this.userService.forgotPassword(forgotPassDto)
    .subscribe(() => {
      this.showSuccess = true;
      this.successMessage = 'The link has been sent, please check your email to reset your password.'
    },
    err => {
      this.showError = true;
      this.errorMessage = err.error.message ?  err.error.message : 'The form is not fullfiled correctly!';
    })
  }

  get email(): FormControl {
    return this.form.get('email') as FormControl;
  }
}
