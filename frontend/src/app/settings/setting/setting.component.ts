import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-setting',
  templateUrl: './setting.component.html',
})
export class SettingComponent {
  @Input() settingName!: string;
  @Input() settingDescription!: string;
  @Input() settingOptions!: string[];
  @Input() settingValue!: string;
  @Input() setter!: (value: string) => void;
  @Input() onChange!: (value: string) => void;

  constructor() {
    this.setter = (value: string) => {
      this.settingValue = value;
      localStorage.setItem(this.settingName, value);
      this.onChange(value);
    };
  }

  // A calculated property that returns the current value of the setting.
  get currentValue(): string {
    return localStorage.getItem(this.settingName) ?? this.settingValue;
  }
}
