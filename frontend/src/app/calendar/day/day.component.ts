import { Component } from '@angular/core';
import {
  faFaceGrin,
  faFaceMeh,
  faFaceSadCry,
  faFaceSmile,
  faFaceTired,
} from '@fortawesome/free-regular-svg-icons';

@Component({
  selector: 'app-day',
  templateUrl: './day.component.html',
})
export class DayComponent {
  icons = [
    {
      icon: faFaceGrin,
      name: 'Great',
      value: 5,
    },
    {
      icon: faFaceSmile,
      name: 'Good',
      value: 4,
    },
    {
      icon: faFaceMeh,
      name: 'Okay',
      value: 3,
    },
    {
      icon: faFaceTired,
      name: 'Bad',
      value: 2,
    },
    {
      icon: faFaceSadCry,
      name: 'Awful',
      value: 1,
    },
  ];
}
