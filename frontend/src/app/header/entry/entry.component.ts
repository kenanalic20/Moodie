import { Component, Input } from '@angular/core';

@Component({
    selector: 'app-entry',
    templateUrl: './entry.component.html',
})
export class EntryComponent {
    @Input() label: string = '';
    @Input() route: string = '';
    @Input() iconName: string = '';
}
