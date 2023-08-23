import {Component, ElementRef, ViewChild} from '@angular/core';
import {MoodService} from "../../services/mood.service";
import {absRound} from "ngx-bootstrap/chronos/utils/abs-round";


@Component({
    selector: 'app-stat-graph',
    templateUrl: './stat-graph.component.html',
})
export class StatGraphComponent {
    moodData: { moodValue: number, date: string,dayName:string }[] = [];

    @ViewChild('monday') monday!: ElementRef;
    @ViewChild('tuesday') tuesday!: ElementRef;
    @ViewChild('wednesday') wednesday!: ElementRef;
    @ViewChild('thursday') thursday!: ElementRef;
   @ViewChild('friday') friday!: ElementRef;
    @ViewChild('saturday') saturday!: ElementRef;
    @ViewChild('sunday') sunday!: ElementRef;
    constructor(private moodService:MoodService) {}
    ngOnInit() {
        this.moodService.getMoods().subscribe((data:any )=>{
            this.moodData = data.map((mood:any) => {
                return {
                    moodValue: mood.moodValue,
                    date: mood.date,
                    dayName: this.getDayNameFromDate(mood.date)
                };
            });
            this.calculateHeight(this.monday, 'Monday');
            this.calculateHeight(this.tuesday, 'Tuesday');
            this.calculateHeight(this.wednesday, 'Wednesday');
            this.calculateHeight(this.thursday, 'Thursday');
            this.calculateHeight(this.friday, 'Friday');
            this.calculateHeight(this.saturday, 'Saturday');
            this.calculateHeight(this.sunday, 'Sunday');

        })



    }
    calculateAverageMood(dayName: string): number {
        const dayMoods = this.moodData.filter(mood => mood.dayName === dayName);
        const dayMoodSum = dayMoods.reduce((sum, mood) => sum + mood.moodValue, 0);
        const dayMoodAvg = dayMoodSum / dayMoods.length || 0;
        return dayMoodAvg;
    }
    calculateHeight(element: ElementRef, dayName: string) {
        element.nativeElement.innerText = this.calculateAverageMood(dayName).toFixed(2).toString();
        element.nativeElement.style.height = `${this.calculateAverageMood(dayName) * 24}px`;
    }
    getDayNameFromDate(dateString: string) {
        const date = new Date(dateString);
        const options: Intl.DateTimeFormatOptions = { weekday: 'long' }; // 'long' for full day name, 'short' for abbreviated day name
        return date.toLocaleString('en-US',options);
    }
}
