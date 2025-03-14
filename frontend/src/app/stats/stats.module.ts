import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActivityEditModalComponent } from './activity-edit-modal/activity-edit-modal.component';
import { ActivityInformationModalComponent } from './activity-information-modal/activity-information-modal.component';
import { TranslateModule } from '@ngx-translate/core';

@NgModule({
    declarations: [ActivityInformationModalComponent],
    imports: [CommonModule, FormsModule, TranslateModule],
    exports: [ActivityInformationModalComponent],
})
export class StatsModule {}
import { SharedModule } from '../shared/shared.module';
