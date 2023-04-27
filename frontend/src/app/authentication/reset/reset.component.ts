import {Component} from '@angular/core';
import {faGoogle} from '@fortawesome/free-brands-svg-icons';
import {isDev} from '../../globals';

@Component({
  selector: 'app-login',
  templateUrl: './reset.component.html',
})
export class ResetComponent {
  faGoogle = faGoogle;
  isDevelopment = isDev;
}
