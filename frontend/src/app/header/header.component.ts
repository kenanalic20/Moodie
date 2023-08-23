import { Component } from '@angular/core';
import { isDev } from '../globals';
import { faCode } from '@fortawesome/free-solid-svg-icons';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import {ToastrService} from "ngx-toastr";

@Component({
    selector: 'app-header',
    templateUrl: './header.component.html',
})
export class HeaderComponent {
    active = window.location.pathname.split('/')[1];
    isDevelopment = isDev;
    codeIcon = faCode;
    constructor(private auth:AuthService,private route:Router, private toastr: ToastrService) {}
    logOut(){
       this.auth.logout().subscribe((msg)=>{
         console.log(msg);
         this.auth.clearUserCookie();
         this.route.navigate(['/login']);
       })
    }
    User(){
        this.auth.user().subscribe((data)=>{
          const user = data as unknown as {username:string,email:string};

          this.toastr.success(`Welcome ${user.username}`, 'This is you!');
        })
    }
}
