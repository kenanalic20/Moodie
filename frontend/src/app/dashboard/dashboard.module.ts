import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ModalModule } from 'ngx-bootstrap/modal';
import { DashboardRoutingModule } from './dashboard-routing.module';
import { DashboardComponent } from './dashboard.component';
import { MoodSelectorSimpleComponent } from './mood-selector-simple/mood-selector-simple.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { AuthenticationModule } from '../authentication/authentication.module';
import { HeaderModule } from '../header/header.module';
import { MoodSelectorComplexComponent } from './mood-selector-complex/mood-selector-complex.component';
import { MoodInformationModalComponent } from './mood-information-modal/mood-information-modal.component';

@NgModule({
    declarations: [
        DashboardComponent,
        MoodSelectorSimpleComponent,
        MoodSelectorComplexComponent,
        MoodInformationModalComponent,
    ],
    imports: [
        CommonModule,
        DashboardRoutingModule,
        FontAwesomeModule,
        AuthenticationModule,
        HeaderModule,
        ModalModule.forRoot(),
    ],
    exports: [DashboardComponent, MoodSelectorSimpleComponent],
})
export class DashboardModule {}
