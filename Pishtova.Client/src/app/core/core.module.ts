import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { RouterModule } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatMenuModule } from '@angular/material/menu';
import {MatProgressBarModule} from '@angular/material/progress-bar';
import { StoreModule } from '@ngrx/store';

import { reducers } from './+store';
import { HeaderComponent } from './header/header.component';
import { FooterComponent } from './footer/footer.component';
import { SubjectScreenComponent } from './subject-screen/subject-screen.component';
import { TestScreenComponent } from './test-screen/test-screen.component';
import { ForAuthenticatedUserGuard } from '../authentication/guards/auth.guard';
import { ProfileComponent } from './profile/profile.component';
import { SharedModule } from '../shared/shared.module';
import { AppRoutingModule } from '../app-routing.module';
import { MainScreenComponent } from './main-screen/main-screen.component';
import { InTestGuard } from './guards/inTest.guard';

@NgModule({
  declarations: [
    HeaderComponent,
    FooterComponent,
    MainScreenComponent,
    ProfileComponent,
    SubjectScreenComponent,
    TestScreenComponent
  ],
  imports: [
    CommonModule,
    MatToolbarModule,
    MatIconModule,
    MatCardModule,
    SharedModule,
    MatMenuModule,
    MatProgressBarModule,
    AppRoutingModule,
    RouterModule.forChild([
      { path: 'main', component: MainScreenComponent, canActivate: [ForAuthenticatedUserGuard, InTestGuard] },
      { path: 'profile', component: ProfileComponent, canActivate: [ForAuthenticatedUserGuard, InTestGuard] },
      { path: 'subject/:id', component: SubjectScreenComponent, canActivate: [ForAuthenticatedUserGuard, InTestGuard] },
      { path: 'subject/:id/test', component: SubjectScreenComponent, canActivate: [ForAuthenticatedUserGuard] },
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
