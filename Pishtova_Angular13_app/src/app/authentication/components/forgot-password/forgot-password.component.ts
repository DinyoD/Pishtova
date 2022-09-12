import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';

import { ForgotPasswordModel } from '../../models/forgotPassword';
import { AuthService } from '../../../services/auth/auth.service';
import { environment as env} from 'src/environments/environment';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['../styles/form.css']
})
export class ForgotPasswordComponent {

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

    this.userService.logout();
    const formValues = { ...this.form.value };
    const forgotPassDto: ForgotPasswordModel = {
      email: formValues.email,
      clientURI: env.CLIENT_URI + '/auth/resetpassword'
    }

    this.userService.forgotPassword(forgotPassDto)
      .subscribe(() => {
        this.showSuccess = true;
      },
      err => {
        this.showError = true;
        this.errorMessage = err;
      })
  }

  public changeInput(): void {
    this.showError = false;
  }

  get email(): FormControl {
    return this.form.get('email') as FormControl;
  }
}
