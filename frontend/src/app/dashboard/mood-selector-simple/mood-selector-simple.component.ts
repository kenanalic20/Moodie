import { Component, Input } from '@angular/core';
import { MoodInformationModalComponent } from '../mood-information-modal/mood-information-modal.component';
import { BsModalService } from 'ngx-bootstrap/modal';
import { MoodService } from '../../services/mood.service';
import { ToastrService } from 'ngx-toastr';
import { TranslateService } from '@ngx-translate/core';

interface MoodIcon {
    emoji: string;
    name: string;
    value: number;
}

@Component({
    selector: 'app-mood-selector-simple',
    templateUrl: './mood-selector-simple.component.html',
})
export class MoodSelectorSimpleComponent {
    @Input() selectedDate?: Date;

    constructor(
        private modalService: BsModalService,
        private moodService: MoodService,
        private toastrService: ToastrService,
        private translateService: TranslateService
    ) {}

    icons: MoodIcon[] = [
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

    modalRef: any;
    selected = -1;

    select(value: number) {
        this.selected = value;
    }

    OpenModal(mood?: any) {
        this.modalRef = this.modalService.show(MoodInformationModalComponent);
        this.modalRef.content.mood = this.selected;

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
                moodValue: this.selected,
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
