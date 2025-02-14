import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from './header.component';
import { EntryComponent } from './entry/entry.component';
import { RouterLink } from '@angular/router';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { TranslateModule } from '@ngx-translate/core';

@NgModule({
    declarations: [HeaderComponent, EntryComponent],
    imports: [CommonModule, RouterLink, FontAwesomeModule,TranslateModule],
    exports: [HeaderComponent, EntryComponent],
})
export class HeaderModule {}
