import { NgModule } from '@angular/core';
import { LoginComponent } from '../login/login.component';
import { RegisterComponent } from '../register/register.component';
import { RouterModule, Routes } from '@angular/router';
import { FrontPageComponent } from '../front-page/front-page.component';
import { AboutComponent } from '../about/about.component';

const routes: Routes = [
  { path: '', component: FrontPageComponent },
  { path: 'about', component: AboutComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
