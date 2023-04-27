import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';

import {DashboardRoutingModule} from './dashboard-routing.module';
import {DashboardComponent} from "./dashboard.component";
import {HeaderComponent} from "../header/header.component";
import {MoodsComponent} from "./moods/moods.component";
import {FontAwesomeModule} from "@fortawesome/angular-fontawesome";
import {AuthenticationModule} from "../authentication/authentication.module";
import {HeaderModule} from "../header/header.module";


@NgModule({
  declarations: [
    DashboardComponent,
    MoodsComponent
  ],
  imports: [
    CommonModule,
    DashboardRoutingModule,
    FontAwesomeModule,
    AuthenticationModule,
    HeaderModule
  ],
  exports: [
    DashboardComponent,
    MoodsComponent
  ]
})
export class DashboardModule {
}
