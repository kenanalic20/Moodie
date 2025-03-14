import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SettingComponent } from './setting/setting.component';
import { TranslateModule } from '@ngx-translate/core';

@NgModule({
    declarations: [SettingComponent],
    exports: [SettingComponent],
    imports: [CommonModule, TranslateModule],
})
export class SettingsModule {}
