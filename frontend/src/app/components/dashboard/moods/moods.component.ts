import { Component } from '@angular/core';
import {
  faFaceSmile,
  faFaceTired,
  faFaceMeh,
  faFaceGrin,
  faFaceSadCry,
} from '@fortawesome/free-regular-svg-icons';

@Component({
  selector: 'app-moods',
  templateUrl: './moods.component.html',
})
export class MoodsComponent {
  // icons = [faFaceGrin, faFaceSmile, faFaceMeh, faFaceTired, faFaceSadCry];
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
