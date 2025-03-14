import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { RouterModule } from "@angular/router";
import { HeaderComponent } from "./header.component";
import { EntryComponent } from "./entry/entry.component";
import { TranslateModule } from "@ngx-translate/core";
import { SvgIconComponent } from "./svg-icon/svg-icon.component";

@NgModule({
	declarations: [HeaderComponent, EntryComponent, SvgIconComponent],
	imports: [CommonModule, RouterModule, TranslateModule],
	exports: [HeaderComponent, EntryComponent],
})
export class HeaderModule {}
