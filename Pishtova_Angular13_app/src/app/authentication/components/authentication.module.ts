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
import { ForUnauthenticatedUserGuard } from '../guards/unauth.guard';


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
      { path: 'auth/login', component: LoginComponent, canActivate: [ForUnauthenticatedUserGuard] },
      { path: 'auth/register', component: RegisterComponent, canActivate: [ForUnauthenticatedUserGuard]  },
      { path: 'auth/emailconfirmation', component: EmailConfirmationComponent, canActivate: [ForUnauthenticatedUserGuard]  },
      { path: 'auth/forgotpassword', component: ForgotPasswordComponent, canActivate: [ForUnauthenticatedUserGuard]  },
      { path: 'auth/resetpassword', component: ResetPasswordComponent,  canActivate: [ForUnauthenticatedUserGuard]  },
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
