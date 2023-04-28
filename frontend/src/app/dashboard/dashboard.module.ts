import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DashboardRoutingModule } from './dashboard-routing.module';
import { DashboardComponent } from './dashboard.component';
import { MoodSelectorSimpleComponent } from './mood-selector-simple/mood-selector-simple.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { AuthenticationModule } from '../authentication/authentication.module';
import { HeaderModule } from '../header/header.module';
import { MoodSelectorComplexComponent } from './mood-selector-complex/mood-selector-complex.component';

@NgModule({
  declarations: [
    DashboardComponent,
    MoodSelectorSimpleComponent,
    MoodSelectorComplexComponent,
  ],
  imports: [
    CommonModule,
    DashboardRoutingModule,
    FontAwesomeModule,
    AuthenticationModule,
    HeaderModule,
  ],
  exports: [DashboardComponent, MoodSelectorSimpleComponent],
})
export class DashboardModule {}
