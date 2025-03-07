import { Component, Input, Output, EventEmitter } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-confirmation-dialog',
  templateUrl: './confirmation-dialog.component.html'
})
export class ConfirmationDialogComponent {
  constructor(public bsModalRef: BsModalRef) {}

  @Input() title: string = 'Confirm Action';
  @Input() message: string = 'Are you sure you want to proceed?';
  
  @Output() onClose = new EventEmitter<boolean>();

  confirm(): void {
    this.onClose.emit(true);
    this.bsModalRef.hide();
  }

  decline(): void {
    this.onClose.emit(false);
    this.bsModalRef.hide();
  }
}
