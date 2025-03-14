import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ExportComponent } from './export.component';
import { TranslateModule } from '@ngx-translate/core';
import { HeaderModule } from '../header/header.module';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
    declarations: [ExportComponent],
    imports: [
        CommonModule,
        HeaderModule,
        TranslateModule,
        ReactiveFormsModule,
        HttpClientModule,
    ],
    exports: [ExportComponent],
})
export class ExportModule {}
