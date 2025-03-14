import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AchievementsComponent } from './achievements.component';
import { TranslateModule } from '@ngx-translate/core';

@NgModule({
    declarations: [AchievementsComponent],
    imports: [CommonModule, TranslateModule],
})
export class AchievementsModule {}
