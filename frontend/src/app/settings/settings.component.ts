import { Component } from '@angular/core';

@Component({
    selector: 'app-settings',
    templateUrl: './settings.component.html',
})
export class SettingsComponent {
    onThemeChange: (value: string) => void = (value: string) => {
        value = value.toLowerCase();
        console.log(`Theme changed to ${value}!`);
        document.documentElement.classList.remove('dark');
        document.documentElement.classList.remove('light');
        document.documentElement.classList.add(value);
    };
    onLanguageChange: (value: string) => void = (value: string) => {
        console.log(`Language changed to ${value}!`);
    }
}
