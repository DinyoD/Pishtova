import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatMenuModule } from '@angular/material/menu';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { StoreModule } from '@ngrx/store';

import { reducers } from 'src/app/core/+store';
import { InTestGuard } from 'src/app/core/guards/inTest.guard';
import { HeaderComponent } from 'src/app/core/header/header.component';
import { FooterComponent } from 'src/app/core/footer/footer.component';
import { ProfileComponent } from 'src/app/core/profile/profile.component';
import { MainScreenComponent } from 'src/app/core/main-screen/main-screen.component';
import { TestScreenComponent } from 'src/app/core/test-screen/test-screen.component';
import { ResultScreenComponent } from 'src/app/core/result-screen/result-screen.component';
import { SubjectScreenComponent } from 'src/app/core/subject-screen/subject-screen.component';

import { SharedModule } from 'src/app/shared/shared.module';
import { AppRoutingModule } from 'src/app/app-routing.module';
import { ForAuthenticatedUserGuard } from 'src/app/authentication/guards/auth.guard';

@NgModule({
  declarations: [
    HeaderComponent,
    FooterComponent,
    MainScreenComponent,
    ProfileComponent,
    SubjectScreenComponent,
    TestScreenComponent,
    ResultScreenComponent
  ],
  imports: [
    CommonModule,
    MatToolbarModule,
    MatIconModule,
    MatCardModule,
    SharedModule,
    MatMenuModule,
    MatProgressBarModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    RouterModule.forChild([
      { path: 'main', component: MainScreenComponent, canActivate: [ForAuthenticatedUserGuard, InTestGuard] },
      { path: 'profile', component: ProfileComponent, canActivate: [ForAuthenticatedUserGuard, InTestGuard] },
      { path: 'subject/:id', component: SubjectScreenComponent, canActivate: [ForAuthenticatedUserGuard, InTestGuard] },
      { path: 'subject/:id/test', component: SubjectScreenComponent, canActivate: [ForAuthenticatedUserGuard] },
      { path: 'test-result', component: ResultScreenComponent, canActivate: [ForAuthenticatedUserGuard] },
    ]),
    StoreModule.forRoot(reducers)
  ],
  exports: [
    HeaderComponent,
    FooterComponent,
    MainScreenComponent,
    ProfileComponent,
    SubjectScreenComponent,
    TestScreenComponent
  ]
})
export class CoreModule { }
