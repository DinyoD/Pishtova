import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { JwtModule } from "@auth0/angular-jwt";

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { AuthenticationModule } from './authentication/components/authentication.module';
import { CoreModule } from './core/core.module';
import { ErrorHandlerService } from './error/error-handler.service';
import { MatDialogModule } from '@angular/material/dialog';
import { storageServiceProvider } from './services';
import { LoadingIndicatorInterceptor } from './interceptor/loading-indicator-interceptor.service';
import { SharedModule } from './shared/shared.module';
//import { NotFoundComponent } from './error/error-screens/not-found/not-found.component';


// TODO This work only in Browser!!!!
export function tokenGetter() {
  return localStorage.getItem("token");
}

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    AuthenticationModule,
    MatDialogModule,
    CoreModule,
    SharedModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        allowedDomains: ["localhost:44329","pishtova-api.azurewebsites.net"],
        disallowedRoutes: [],
        
      }
    })
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ErrorHandlerService,
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: LoadingIndicatorInterceptor,
      multi: true
    },
    storageServiceProvider
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
