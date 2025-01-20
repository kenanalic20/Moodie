import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LanguageSwitcherComponent } from '../language-switcher/language-switcher.component';
import { TranslateModule } from '@ngx-translate/core';



@NgModule({
  declarations: [
    LanguageSwitcherComponent
  ],
  imports: [
    CommonModule,
    TranslateModule
  ],
  exports: [
    LanguageSwitcherComponent
  ]
})
export class SharedModule { }
