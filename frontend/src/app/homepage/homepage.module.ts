import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { HomepageRoutingModule } from './homepage-routing.module';
import { FrontpageComponent } from './frontpage/frontpage.component';
import { AboutComponent } from './about/about.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { TranslateModule } from '@ngx-translate/core';
import { SharedModule } from '../shared/shared.module';
@NgModule({
    declarations: [FrontpageComponent, AboutComponent],
    imports: [
        CommonModule,
        HomepageRoutingModule,
        FontAwesomeModule,
        TranslateModule,
        SharedModule,
    ],
    exports: [FrontpageComponent, AboutComponent],
})
export class HomepageModule {}
