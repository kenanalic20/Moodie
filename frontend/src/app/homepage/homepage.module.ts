import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { HomepageRoutingModule } from './homepage-routing.module';
import { FrontpageComponent } from './frontpage/frontpage.component';
import { AboutComponent } from './about/about.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';

@NgModule({
    declarations: [FrontpageComponent, AboutComponent],
    imports: [CommonModule, HomepageRoutingModule, FontAwesomeModule],
    exports: [FrontpageComponent, AboutComponent],
})
export class HomepageModule {}
