import { Component, OnInit } from "@angular/core";
import { MoodService } from "../../services/mood.service";

@Component({
	selector: "app-yearly-stats",
	template: `
        <div class="p-4">
            <h3 class="text-xl font-semibold mb-4">Yearly Mood Trends</h3>
            <!-- Implement yearly stats visualization -->
        </div>
    `,
})
export class YearlyStatsComponent implements OnInit {
	constructor(private moodService: MoodService) {}

	ngOnInit() {
		// Implement yearly stats logic
	}
}
