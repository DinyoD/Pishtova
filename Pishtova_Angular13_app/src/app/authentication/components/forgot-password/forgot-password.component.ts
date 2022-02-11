import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';

import { ForgotPasswordModel } from '../../models/forgotPassword';
import { AuthService } from '../../../services/auth/auth.service';
import { environment as env} from 'src/environments/environment';

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

  constructor(private userService: AuthService) { }

  public validateControl = (controlName: string) => {
    return this.form.controls[controlName].invalid && this.form.controls[controlName].touched
  }

  public hasError = (controlName: string, errorName: string) => {
    return this.form.controls[controlName].hasError(errorName)
  }

  public forgotPassword = () => {
    const formValues = { ...this.form.value };
    const forgotPassDto: ForgotPasswordModel = {
      email: formValues.email,
      clientURI: env.CLIENT_URI + '/auth/resetpassword'
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
