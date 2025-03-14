import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HabitsComponent } from './habits.component';
import { FormsModule } from '@angular/forms';
import { HeaderModule } from '../header/header.module';
import { TranslateModule } from '@ngx-translate/core';

@NgModule({
    declarations: [HabitsComponent],
    imports: [CommonModule, FormsModule, HeaderModule, TranslateModule],
    exports: [HabitsComponent],
})
export class HabitsModule {}
