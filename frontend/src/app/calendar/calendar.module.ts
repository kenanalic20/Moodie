import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CalendarRoutingModule } from './calendar-routing.module';
import { CalendarComponent } from './calendar.component';
import { HeaderModule } from '../header/header.module';
import { DayComponent } from './day/day.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { CalendarMoodInformationModalComponent } from './calendar-mood-information-modal/calendar-mood-information-modal.component';

@NgModule({
    declarations: [CalendarComponent, DayComponent, CalendarMoodInformationModalComponent],
    imports: [
        CommonModule,
        CalendarRoutingModule,
        HeaderModule,
        FontAwesomeModule,
    ],
    exports: [CalendarComponent],
})
export class CalendarModule {}
