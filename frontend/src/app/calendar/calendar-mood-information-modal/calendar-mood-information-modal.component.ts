import { Component, Input } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { Mood } from '../../types';

@Component({
    selector: 'app-calendar-mood-information-modal',
    templateUrl: './calendar-mood-information-modal.component.html',
})
export class CalendarMoodInformationModalComponent {
    constructor(public bsModalRef: BsModalRef) {}

    @Input() mood: Mood = { mood: 0, date: new Date(), note: '', image: '' };

    CloseModal() {
        this.bsModalRef.hide();
    }

    //     Close modal on click outside
}
