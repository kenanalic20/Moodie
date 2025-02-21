import { Component, Input } from "@angular/core";
import { BsModalService } from "ngx-bootstrap/modal";
import { MoodInformationModalComponent } from "../mood-information-modal/mood-information-modal.component";
import { MoodService } from "../../services/mood.service";
import { ToastrService } from "ngx-toastr";

@Component({
	selector: "app-mood-selector-complex",
	templateUrl: "./mood-selector-complex.component.html",
})
export class MoodSelectorComplexComponent {
	constructor(
		private modalService: BsModalService,
		private moodService: MoodService,
		private toastr: ToastrService,
	) {}

	icons = [
		{
			emoji: "🥳",
			name: "Great",
			value: 5,
		},
		{
			emoji: "😊",
			name: "Good",
			value: 4,
		},
		{
			emoji: "😐",
			name: "Okay",
			value: 3,
		},
		{
			emoji: "😔",
			name: "Bad",
			value: 2,
		},
		{
			emoji: "😭",
			name: "Awful",
			value: 1,
		},
	];
	value = -1;

	modalRef: any;

	setValue(value?: number) {
		const selector = document.getElementById("moodrange");
		if (selector) {
			if (!value) {
				this.value = parseFloat((selector as HTMLInputElement).value);
			} else {
				this.value = value;
				(selector as HTMLInputElement).value = value.toString();
			}
		}
	}

	OpenModal() {
		this.modalRef = this.modalService.show(MoodInformationModalComponent);
		this.modalRef.content.mood = this.value;
	}
	CloseModal() {
		this.modalRef.hide();
	}
	addMood() {
		this.moodService
			.addMood({
				moodValue: this.value,
				date: new Date(),
			})
			.subscribe((res) => {
				console.log(res);
				this.toastr.success("Mood added successfully");
			});
	}
}
