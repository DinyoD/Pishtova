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

@NgModule({
  declarations: [
    HeaderComponent,
    FooterComponent,
    MainScreenComponent
  ],
  imports: [
    CommonModule,
    MatToolbarModule,
    MatIconModule,
    SharedModule,
    AppRoutingModule,
    RouterModule.forChild([
      { path: 'main', component: MainScreenComponent },

    ])
  ],
  exports: [
    HeaderComponent,
    FooterComponent,
    MainScreenComponent
  ]
})
export class CoreModule { }
