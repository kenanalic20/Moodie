import { Component, EventEmitter, Input, OnInit, Output, OnChanges, SimpleChanges } from "@angular/core";

type PasswordInputEvent = CustomEvent<{
	detail: string;
}>;

@Component({
	selector: "app-input",
	templateUrl: "./input.component.html",
})
export class InputComponent implements OnInit, OnChanges {
	@Input() type!: "text" | "password" | "email";
	@Input() placeholder!: string;
	@Input() isConfirmPassword: boolean = false;
	@Input() defaultValue = "";

	@Output() valueChange = new EventEmitter<string>();

	inputValue = "";
	passwordInputValue = "";
	validConfirmPassword = true;
	emailRegex = /^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$/;

	// If type is password, and not confirm password, dispatch a custom event 'passwordInput' with the input value
	onInput(event: Event) {
		this.inputValue = (event.target as HTMLInputElement).value;
		this.valueChange.emit(this.inputValue);
		if (this.type === "password" && !this.isConfirmPassword) {
			const passwordInputEvent = new CustomEvent("passwordInput", {
				detail: this.inputValue,
			});
			document.dispatchEvent(passwordInputEvent);
		} else {
			this.validConfirmPassword = this.inputValue === this.passwordInputValue;
		}
	}

	ngOnChanges(changes: SimpleChanges) {
		// Check if defaultValue has changed
		if (changes["defaultValue"] && changes["defaultValue"].currentValue) {
			this.inputValue = changes["defaultValue"].currentValue;
			this.valueChange.emit(this.inputValue);
		}
	}

	ngOnInit() {
		if (this.isConfirmPassword) {
			document.addEventListener("passwordInput", (event) => {
				//@ts-ignore
				this.validConfirmPassword = this.inputValue === event.detail;
				//@ts-ignore
				this.passwordInputValue = event.detail;
			});
		}

		// Initialize with default value if provided
		if (this.defaultValue) {
			this.inputValue = this.defaultValue;
			this.valueChange.emit(this.inputValue);
		}
	}
}
