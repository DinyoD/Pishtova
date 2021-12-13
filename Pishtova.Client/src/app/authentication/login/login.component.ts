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

  public form: FormGroup = new FormGroup({

    email: new FormControl(null, [Validators.required, Validators.email]),
    password: new FormControl(null, [Validators.required])
  })

  constructor(
    private userService: UserService,
    private route: Router
  ) { }

  login(){
    const formValues = {...this.form.value}
    const user: UserForLogin = {
      email: formValues.email,
      password: formValues.password
    }
    this.userService.login(user)
    .subscribe((a) => {
      console.log(a);
      
    }, err =>{
      console.log(err);
      
    });
  }

  get email(): FormControl {
    return this.form.get('email') as FormControl;
  }

  get password(): FormControl {
    return this.form.get('password') as FormControl;
  }
}
