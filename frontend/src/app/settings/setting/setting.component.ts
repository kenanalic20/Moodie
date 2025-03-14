import { Component, Input } from '@angular/core';

@Component({
    selector: 'app-setting',
    templateUrl: './setting.component.html',
})
export class SettingComponent {
    @Input() settingName!: string;
    @Input() settingDescription!: string;
    @Input() settingOptions!: Array<{ label: string; value: string }>;
    @Input() settingValue!: string;
    @Input() onChange!: (value: string) => void;

    constructor() {}

    handleClick(value: string) {
        this.settingValue = value;
        localStorage.setItem(this.settingName, value);
        this.onChange(value);
    }
}
