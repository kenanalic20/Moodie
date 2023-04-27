import {Component, Input} from '@angular/core';
import {isDev} from "../../globals";

@Component({
  selector: 'app-entry',
  templateUrl: './entry.component.html',
})
export class EntryComponent {
  @Input() active!: boolean;
  @Input() link!: string;
  @Input() text!: string;
  activeClasses = 'border-opacity-10 border-b-4';
  isDevelopment = isDev;
}
