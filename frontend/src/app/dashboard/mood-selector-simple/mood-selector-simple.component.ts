import { Component } from '@angular/core';
import {
    faFaceGrin,
    faFaceMeh,
    faFaceSadCry,
    faFaceSmile,
    faFaceTired,
} from '@fortawesome/free-regular-svg-icons';
import { MoodInformationModalComponent } from '../mood-information-modal/mood-information-modal.component';
import { BsModalService } from 'ngx-bootstrap/modal';

@Component({
    selector: 'app-mood-selector-simple',
    templateUrl: './mood-selector-simple.component.html',
})
export class MoodSelectorSimpleComponent {
    constructor(private modalService: BsModalService) {}

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
    selected = -1;

    select(value: number) {
        this.selected = value;
    }

    OpenModal() {
        this.modalRef = this.modalService.show(MoodInformationModalComponent);
        this.modalRef.content.mood = this.selected;
    }
}
