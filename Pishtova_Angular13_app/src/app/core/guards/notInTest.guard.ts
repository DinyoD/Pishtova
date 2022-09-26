import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router} from '@angular/router';

import { TestService } from 'src/app/services';


@Injectable({
  providedIn: 'root'
})
export class NotInTestGuard implements CanActivate {

  constructor(
    private router: Router,
    private testService: TestService
    ){};

  canActivate(
    _next: ActivatedRouteSnapshot
    ) {
 
    if (this.testService.isInTest()) {
        return true;     
    }
    this.router.navigate(['/']);
    return false;
  }

}