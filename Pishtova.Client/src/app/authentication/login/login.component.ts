import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UserForLogin } from 'src/app/interfaces/userForLogin';
import { UserService } from 'src/app/services';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  public errorMessage: string = '';
  public showError: boolean = false;

  public form: FormGroup = new FormGroup({

    email: new FormControl(null, [Validators.required, Validators.email]),
    password: new FormControl(null, [Validators.required])
  })

  constructor(
    private userService: UserService,
    private route: Router
  ) { }
  
  
  // TODO display error message on BG depends on responce status code!!!!
  login(){
    const formValues = {...this.form.value}
    const user: UserForLogin = {
      email: formValues.email,
      password: formValues.password
    }
   this.userService.login(user)
    .subscribe(res => {
      localStorage.setItem('token', res.token)
      this.route.navigate(['/'])
    }, err => {
      this.showError = true;
      this.errorMessage = err.error.message;
    });

  }

  get email(): FormControl {
    return this.form.get('email') as FormControl;
  }

  get password(): FormControl {
    return this.form.get('password') as FormControl;
  }
}
