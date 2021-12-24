import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from './header/header.component';
import { FooterComponent } from './footer/footer.component';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { SharedModule } from '../shared/shared.module';
import { AppRoutingModule } from '../app-routing.module';
import { MainScreenComponent } from './main-screen/main-screen.component';
import { RouterModule } from '@angular/router';
import { ForAuthenticatedUserGuard } from '../guards/auth.guard';
import { ProfileComponent } from './profile/profile.component';
import { MatCardModule } from '@angular/material/card';
import { MatMenuModule } from '@angular/material/menu';

@NgModule({
  declarations: [
    HeaderComponent,
    FooterComponent,
    MainScreenComponent,
    ProfileComponent
  ],
  imports: [
    CommonModule,
    MatToolbarModule,
    MatIconModule,
    MatCardModule,
    SharedModule,
    MatMenuModule,
    AppRoutingModule,
    RouterModule.forChild([
      { path: 'main', component: MainScreenComponent, canActivate: [ForAuthenticatedUserGuard] },
      { path: 'profile', component: ProfileComponent, canActivate: [ForAuthenticatedUserGuard] },
    ])
  ],
  exports: [
    HeaderComponent,
    FooterComponent,
    MainScreenComponent,
    ProfileComponent
  ]
})
export class CoreModule { }
