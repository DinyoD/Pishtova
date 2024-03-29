import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class ErrorHandlerService implements HttpInterceptor {

    DefaultErrorValue: string = "За съжаление вашата заявка не е успешна. Моля, опитайте отново!";

  constructor(private _router: Router) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req)
    .pipe(
      catchError((error: HttpErrorResponse) => {
        let errorMessage = this.handleError(error);
        return throwError(errorMessage);
      })
    )
  }

  // TODO Add BG error message after check Pishtova status-code
  private handleError = (error: HttpErrorResponse) : string => {
    if(error.status === 404) {
      return this.handleNotFound(error);
      console.log(123);
      
    }
    else if(error.status === 400) {
      return this.handleBadRequest(error);
    }
    else if(error.status === 401) {
      return this.handleUnauthorized(error);
    }
    else if(error.status === 403) {
      return this.handleForbidden(error);
    }
    return this.DefaultErrorValue;
  }

  private handleForbidden = (error: HttpErrorResponse) => {
    this._router.navigate(["/forbidden"], { queryParams: { returnUrl: this._router.url }});
    return "Forbidden";
  }

  private handleUnauthorized = (error: HttpErrorResponse) => {
    if(this._router.url !== '/') {
      
      this._router.navigate(['/'], { queryParams: { returnUrl: this._router.url }});
    }
    return error.error.message;
  }

  private handleNotFound = (error: HttpErrorResponse): string => {
    this._router.navigate(['/404']);
    return error.message;
  }

  private handleBadRequest = (error: HttpErrorResponse): string => {
    return error.error.detail;
  }
}