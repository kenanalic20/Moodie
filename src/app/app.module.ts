import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './components/app/app.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { RouterOutlet } from '@angular/router';
import { AppRoutingModule } from './components/app-routing/app-routing.module';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { FrontPageComponent } from './components/front-page/front-page.component';
import { AboutComponent } from './components/about/about.component';

@NgModule({
  declarations: [AppComponent, LoginComponent, RegisterComponent, FrontPageComponent, AboutComponent],
  imports: [BrowserModule, RouterOutlet, AppRoutingModule, FontAwesomeModule],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
