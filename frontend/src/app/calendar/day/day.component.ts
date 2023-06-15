import { Component, Input } from '@angular/core';
import {
    faFaceGrin,
    faFaceMeh,
    faFaceSadCry,
    faFaceSmile,
    faFaceTired,
} from '@fortawesome/free-regular-svg-icons';
import { Day, Mood } from '../../types';
import { BsModalService } from 'ngx-bootstrap/modal';
import { CalendarMoodInformationModalComponent } from '../calendar-mood-information-modal/calendar-mood-information-modal.component';

@Component({
    selector: 'app-day',
    templateUrl: './day.component.html',
})
export class DayComponent {
    constructor(private modalService: BsModalService) {}

    @Input() day: Day = { dayOfMonth: 0, dayName: '', isLast: false };
    icons = [
        {
            icon: faFaceGrin,
            name: 'Great',
            value: 5,
        },
        {
            icon: faFaceSmile,
            name: 'Good',
            value: 4,
        },
        {
            icon: faFaceMeh,
            name: 'Okay',
            value: 3,
        },
        {
            icon: faFaceTired,
            name: 'Bad',
            value: 2,
        },
        {
            icon: faFaceSadCry,
            name: 'Awful',
            value: 1,
        },
    ];

    modalRef: any;

    GetIcon() {
        const icon = this.icons.find(
            icon =>
                icon.value === parseInt(this.day.mood?.mood.toString() || '1')
        )?.icon;

        if (icon === undefined) {
            return faFaceSadCry;
        }

        return icon;
    }

    OpenModal() {
        this.modalRef = this.modalService.show(
            CalendarMoodInformationModalComponent
        );
        this.modalRef.content.mood = this.day.mood;
    }

    parseInt(mood: Mood) {
        return parseInt(mood.mood.toString());
    }
}
