import { Component, Input } from '@angular/core';
import { IconDefinition } from '@fortawesome/fontawesome-svg-core';

@Component({
    selector: 'app-entry',
    templateUrl: './entry.component.html',
})
export class EntryComponent {
    @Input() active!: boolean;
    @Input() link!: string;
    @Input() text!: string;
    @Input() icon!: IconDefinition | undefined;
    activeClasses = 'font-bold text-white';
}
