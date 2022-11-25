import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router,ActivatedRoute } from '@angular/router';
import { UserForLoginModel } from '../../models/userForLogin';
import { AuthService, StorageService } from 'src/app/services';
import ILoginResult from '../../models/results/LoginResult';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['../styles/form.css','./login.component.css']
})
export class LoginComponent implements OnInit {

  public errorMessage: string = '';
  public showError: boolean = false;
  public returnUrl: string = '/main';

  public form: FormGroup = new FormGroup({

    email: new FormControl(null, [Validators.required, Validators.email]),
    password: new FormControl(null, [Validators.required])
  })

  constructor(
    private userService: AuthService,
    private route: Router,
    private actRouter: ActivatedRoute,
    private storage: StorageService
  ) { }
    
  ngOnInit(): void {
    this.actRouter.queryParams.subscribe(p => this.returnUrl = p.returnUrl || '/main');
  }
  

  // TODO display error message on BG depends on responce status code!!!!
  login(){
    const formValues = {...this.form.value}
    const user: UserForLoginModel = {
      email: formValues.email,
      password: formValues.password
    }
   this.userService.login(user)
    .subscribe((res: ILoginResult) => {
      this.route.navigateByUrl(this.returnUrl);
      this.storage.setItem('token', res.token)
      this.userService.sendAuthStateChangeNotification(res.token != null)
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
