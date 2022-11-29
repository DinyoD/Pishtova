import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ForAuthenticatedUserGuard } from './authentication/guards/auth.guard';
import { MainScreenComponent } from './core/main-screen/main-screen.component';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    component: MainScreenComponent,
    canActivate: [ForAuthenticatedUserGuard]
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
