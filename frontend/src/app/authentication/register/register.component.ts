import {Component} from '@angular/core';
import {faGoogle} from '@fortawesome/free-brands-svg-icons';
import {isDev} from '../../globals';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
})
export class RegisterComponent {
  faGoogle = faGoogle;
  isDevelopment = isDev;
}
