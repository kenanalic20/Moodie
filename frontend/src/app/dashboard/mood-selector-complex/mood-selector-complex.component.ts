import { Component, Input } from '@angular/core';
import {
  faFaceGrin,
  faFaceMeh,
  faFaceSadCry,
  faFaceSmile,
  faFaceTired,
} from '@fortawesome/free-regular-svg-icons';

@Component({
  selector: 'app-mood-selector-complex',
  templateUrl: './mood-selector-complex.component.html',
})
export class MoodSelectorComplexComponent {
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
  value = -1;

  setValue(value?: number) {
    const selector = document.getElementById('moodrange');
    if (selector) {
      if (!value) {
        this.value = parseInt((selector as HTMLInputElement).value);
      } else {
        this.value = value;
        (selector as HTMLInputElement).value = value.toString();
      }
    }
  }
}
