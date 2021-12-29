import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { EmailConfirmationComponent } from './email-confirmation/email-confirmation.component';
import { RouterModule } from '@angular/router';
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';
import { ResetPasswordComponent } from './reset-password/reset-password.component';


@NgModule({
  declarations: [    
    RegisterComponent,
    LoginComponent,
    EmailConfirmationComponent,
    ForgotPasswordComponent,
    ResetPasswordComponent
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
      { path: 'auth/register', component: RegisterComponent },
      //{ path: 'auth/login', component: LoginComponent },
      { path: 'auth/emailconfirmation', component: EmailConfirmationComponent },
      { path: 'auth/forgotpassword', component: ForgotPasswordComponent },
      { path: 'auth/resetpassword', component: ResetPasswordComponent },
    ])
  ],
  exports: [ 
    RegisterComponent,
    LoginComponent,
    EmailConfirmationComponent,
    ForgotPasswordComponent,
    ResetPasswordComponent
  ]

})
export class AuthenticationModule  { }
