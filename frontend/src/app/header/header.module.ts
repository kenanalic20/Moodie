import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {HeaderComponent} from "./header.component";
import {EntryComponent} from "./entry/entry.component";
import {RouterLink} from "@angular/router";


@NgModule({
  declarations: [
    HeaderComponent,
    EntryComponent
  ],
  imports: [
    CommonModule,
    RouterLink
  ],
  exports: [
    HeaderComponent,
    EntryComponent
  ]
})
export class HeaderModule {
}
