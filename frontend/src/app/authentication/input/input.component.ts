import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
    selector: 'app-input',
    templateUrl: './input.component.html',
})
export class InputComponent {
    @Input() type!: 'text' | 'password' | 'email';
    @Input() placeholder!: string;

    @Output() emailChange = new EventEmitter<string>();
    @Output() passwordChange = new EventEmitter<string>();
    @Output() usernameChange = new EventEmitter<string>();
    email: string = '';
    password: string = '';
    username: string = '';
    
    onEmailChanged() {
        this.emailChange.emit(this.email);
    }
    onPasswordChanged() {
        this.passwordChange.emit(this.password);
    }
    onUsernameChanged() {
        this.usernameChange.emit(this.username);
    }
}
