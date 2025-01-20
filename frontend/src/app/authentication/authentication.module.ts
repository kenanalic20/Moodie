import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AuthenticationRoutingModule } from './authentication-routing.module';
import { LoginComponent } from './login/login.component';
import { ResetComponent } from './reset/reset.component';
import { RegisterComponent } from './register/register.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { InputComponent } from './input/input.component';
import { HttpClientModule } from '@angular/common/http';
import { TranslateModule } from '@ngx-translate/core';
import { SharedModule } from '../shared/shared.module';


@NgModule({
    declarations: [
        LoginComponent,
        ResetComponent,
        RegisterComponent,
        InputComponent,
    ],
    imports: [CommonModule, AuthenticationRoutingModule, FontAwesomeModule,HttpClientModule,SharedModule,TranslateModule],
    exports: [
        LoginComponent,
        ResetComponent,
        RegisterComponent,
        InputComponent,
    ],
})
export class AuthenticationModule {}
