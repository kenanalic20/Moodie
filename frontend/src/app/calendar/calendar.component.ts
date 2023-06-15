import { Component } from '@angular/core';
import { Day } from '../types';

@Component({
    selector: 'app-calendar',
    templateUrl: './calendar.component.html',
})
export class CalendarComponent {
    daysInCurrentMonth = new Date(
        new Date().getFullYear(),
        new Date().getMonth() + 1,
        0
    ).getDate();

    // a day is an object with a day of the month and a day name
    days = Array.from({ length: this.daysInCurrentMonth }, (_, i) => i + 1)
        .map((dayOfMonth): Day => {
            const dayName = new Date(
                new Date().getFullYear(),
                new Date().getMonth(),
                dayOfMonth
            ).toLocaleString('en-us', { weekday: 'long' });
            return { dayOfMonth, dayName, isLast: false };
        })
        .filter(day => day.dayOfMonth <= new Date().getDate());

    constructor() {
        this.days[this.days.length - 1].isLast = true;
        this.days[this.days.length - 1].mood = {
            mood: 5,
            date: new Date(),
            note: 'Everything is great!',
            image: 'https://fastly.picsum.photos/id/342/300/300.jpg?hmac=lzgyDROXQkWGPPmmAOi9SDpRiTB5B768G2tnXHhkbz8',
        };
    }
}
