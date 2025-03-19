import { Component, Input } from '@angular/core';
import { BsModalService } from 'ngx-bootstrap/modal';
import { MoodInformationModalComponent } from '../mood-information-modal/mood-information-modal.component';
import { MoodService } from '../../services/mood.service';
import { ToastrService } from 'ngx-toastr';
import { TranslateService } from '@ngx-translate/core';

@Component({
    selector: 'app-mood-selector-complex',
    templateUrl: './mood-selector-complex.component.html',
})
export class MoodSelectorComplexComponent {
    constructor(
        private modalService: BsModalService,
        private moodService: MoodService,
        private toastrService: ToastrService,
        private translateService: TranslateService
    ) {}

    icons = [
        {
            emoji: '🥳',
            name: 'Great',
            value: 5,
        },
        {
            emoji: '😊',
            name: 'Good',
            value: 4,
        },
        {
            emoji: '😐',
            name: 'Okay',
            value: 3,
        },
        {
            emoji: '😔',
            name: 'Bad',
            value: 2,
        },
        {
            emoji: '😭',
            name: 'Awful',
            value: 1,
        },
    ];
    value = -1;

    modalRef: any;

    setValue(value?: number) {
        const selector = document.getElementById('moodrange');
        if (selector) {
            if (!value) {
                this.value = parseFloat((selector as HTMLInputElement).value);
            } else {
                this.value = value;
                (selector as HTMLInputElement).value = value.toString();
            }
        }
    }

    OpenModal(mood?: any) {
        this.modalRef = this.modalService.show(MoodInformationModalComponent);
        this.modalRef.content.mood = this.value;

        // Extract the mood ID from the response structure
        if (mood && mood.mood && mood.mood.id) {
            this.modalRef.content.moodId = mood.mood.id;
        }
    }

    CloseModal() {
        this.modalRef.hide(MoodInformationModalComponent);
    }
    addMood() {
        this.moodService
            .addMood({
                moodValue: this.value,
                date: new Date(),
            })
            .subscribe(res => {
                this.OpenModal(res);
                this.toastrService.success(
                    this.translateService.instant('Mood added successfully'),
                    this.translateService.instant('Success')
                );
            });
    }
}
