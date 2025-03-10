import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { UserInfoComponent } from "./user-info.component";
import { TranslateModule } from "@ngx-translate/core";
import { ReactiveFormsModule } from "@angular/forms";
import { HeaderModule } from "../header/header.module";

@NgModule({
	declarations: [UserInfoComponent],
	imports: [CommonModule, TranslateModule, ReactiveFormsModule, HeaderModule],
	exports: [UserInfoComponent],
})
export class UserInfoModule {}
