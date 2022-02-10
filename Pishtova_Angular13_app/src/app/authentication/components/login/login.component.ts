import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UserForLoginModel } from '../../models/userForLogin';
import { AuthService, StorageService } from 'src/app/services';

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
    private userService: AuthService,
    private route: Router,
    private storage: StorageService
  ) { }
  

  // TODO display error message on BG depends on responce status code!!!!
  login(){
    const formValues = {...this.form.value}
    const user: UserForLoginModel = {
      email: formValues.email,
      password: formValues.password
    }
   this.userService.login(user)
    .subscribe((res: { token: string; }) => {
      this.route.navigate(['/main'])
      this.userService.sendAuthStateChangeNotification(res.token != null)
      this.storage.setItem('token', res.token)
    }, (err) => {
      this.showError = true;
      this.errorMessage = err;
    });

  }

  get email(): FormControl {
    return this.form.get('email') as FormControl;
  }

  get password(): FormControl {
    return this.form.get('password') as FormControl;
  }
}
