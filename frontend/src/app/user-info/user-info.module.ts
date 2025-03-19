import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserInfoComponent } from './user-info.component';
import { TranslateModule } from '@ngx-translate/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HeaderModule } from '../header/header.module';
import { UserImageModalComponent } from './user-image-modal/user-image-modal.component';

@NgModule({
    declarations: [UserInfoComponent, UserImageModalComponent],
    imports: [CommonModule, TranslateModule, ReactiveFormsModule, HeaderModule, FormsModule],
    exports: [UserInfoComponent],
})
export class UserInfoModule {}
