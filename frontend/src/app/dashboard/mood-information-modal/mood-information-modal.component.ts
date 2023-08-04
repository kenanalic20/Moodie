import { Component, Input } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';


@Component({
    selector: 'app-mood-information-modal',
    templateUrl: './mood-information-modal.component.html',
})
export class MoodInformationModalComponent {
    constructor(public bsModalRef: BsModalRef ) {}

    @Input() mood = 0;

    CloseModal() {
        this.bsModalRef.hide();
    }

}
