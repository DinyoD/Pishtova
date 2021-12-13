import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LoginComponent } from '../authentication/login/login.component';
import { RegisterComponent } from '../authentication/register/register.component';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { EmailConfirmationComponent } from '../authentication/email-confirmation/email-confirmation.component';
import { RouterModule } from '@angular/router';


@NgModule({
  declarations: [    
    RegisterComponent,
    LoginComponent,
    EmailConfirmationComponent
  ],
  imports: [
    CommonModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatSelectModule,
    RouterModule.forChild([
      { path: 'register', component: RegisterComponent },
      { path: 'login', component: LoginComponent },
      { path: 'emailconfirmation', component: EmailConfirmationComponent },
      //{ path: 'forgotpassword', component: ForgotPasswordComponent },
      //{ path: 'resetpassword', component: ResetPasswordComponent },
    ])
  ],
  exports: [ 
    RegisterComponent,
    LoginComponent,
    EmailConfirmationComponent
  ]

})
export class AuthenticationModule  { }