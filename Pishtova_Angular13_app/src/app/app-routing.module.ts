import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ForUnauthenticatedUserGuard } from './authentication/guards/unauth.guard';
import { LoginComponent } from './authentication/components/login/login.component';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    component: LoginComponent,
    canActivate: [ForUnauthenticatedUserGuard]
  }
];

// export const AppRoutingModule = RouterModule.forRoot(routes, {
//   onSameUrlNavigation: 'reload'
// });

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
