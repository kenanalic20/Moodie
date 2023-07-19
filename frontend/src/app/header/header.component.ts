import { Component } from '@angular/core';
import { isDev } from '../globals';
import { faCode } from '@fortawesome/free-solid-svg-icons';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Component({
    selector: 'app-header',
    templateUrl: './header.component.html',
})
export class HeaderComponent {
    active = window.location.pathname.split('/')[1];
    isDevelopment = isDev;
    codeIcon = faCode;
    constructor(private auth:AuthService,private route:Router) {}
    logOut(){
       this.auth.logout().subscribe((msg)=>{
         console.log(msg);
         this.route.navigate(['/login']);
       })
    }
    User(){
        this.auth.user().subscribe((data)=>{
            console.log(data);
        })
    }
}
