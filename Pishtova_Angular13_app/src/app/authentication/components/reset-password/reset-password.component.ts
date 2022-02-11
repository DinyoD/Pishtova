import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';

import { ResetPasswordModel } from '../../models/resetPassword';
import { ActivatedRoute } from '@angular/router';
import { CustomValidators } from '../../helpers/custom-validators';
import { AuthService } from '../../../services/auth/auth.service';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.css']
})
export class ResetPasswordComponent implements OnInit {
  public showSuccess: boolean = false;
  public showError: boolean = false;
  public errorMessage: string = '';
  public form: FormGroup = new FormGroup({
    password: new FormControl('', [Validators.required]),
    confirmPassword: new FormControl('', [Validators.required])
  },
  { validators: [
    CustomValidators.passwordsMatching, 
    CustomValidators.passwordMatchingRegEx,
  ] }
  );

  private token: string = '';
  private email: string = '';

  constructor(
    private userService: AuthService, 
    private route: ActivatedRoute) { }

  ngOnInit(): void {    
      this.token = this.route.snapshot.queryParams['token'];
      this.email = this.route.snapshot.queryParams['email'];
  }

  public resetPassword = () => {
    const formValue = { ... this.form.value };
    const resetPassDto: ResetPasswordModel = {
      password: formValue.password,
      confirmPassword: formValue.confirm,
      token: this.token,
      email: this.email
    }

    this.userService.resetPassword(resetPassDto)
    .subscribe(() => {
      this.showSuccess = true;
    },
    error => {
      this.showError = true;
      this.errorMessage = error.error.message;
    })
  }

  get password(): FormControl {
    return this.form.get('password') as FormControl;
  }
  get confirmPassword(): FormControl {
    return this.form.get('confirmPassword') as FormControl;
  }
}
