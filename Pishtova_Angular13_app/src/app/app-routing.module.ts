import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ForUnauthenticatedUserGuard } from './authentication/guards/unauth.guard';
import { HomeComponent } from './home/home.component';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    component: HomeComponent,
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
