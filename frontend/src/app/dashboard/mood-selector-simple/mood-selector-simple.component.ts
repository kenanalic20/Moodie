import { Component, Input } from "@angular/core";
import { MoodInformationModalComponent } from "../mood-information-modal/mood-information-modal.component";
import { BsModalService } from "ngx-bootstrap/modal";
import { MoodService } from "../../services/mood.service";
import { ToastrService } from "ngx-toastr";

interface MoodIcon {
	emoji: string;
	name: string;
	value: number;
}

@Component({
	selector: "app-mood-selector-simple",
	templateUrl: "./mood-selector-simple.component.html",
})
export class MoodSelectorSimpleComponent {
	@Input() selectedDate?: Date;

	constructor(
		private modalService: BsModalService,
		private moodService: MoodService,
		private toastr: ToastrService,
	) {}

	icons: MoodIcon[] = [
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

	modalRef: any;
	selected = -1;

	select(value: number) {
		this.selected = value;
	}

	OpenModal(mood?:any) {
		this.modalRef = this.modalService.show(MoodInformationModalComponent);
		this.modalRef.content.mood = this.selected;
		this.modalRef.content.moodId = mood.id;
	}
	CloseModal() {
		this.modalRef.hide(MoodInformationModalComponent);
	}
	addMood() {
		this.moodService
			.addMood({
				moodValue: this.selected,
				date: new Date(),
			})
			.subscribe((res) => {
				console.log(res);
				this.OpenModal(res)
				this.toastr.success("Mood added successfully");
			});
	}
}
