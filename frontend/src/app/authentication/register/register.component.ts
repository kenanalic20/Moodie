import { Component } from '@angular/core';
import { faGoogle } from '@fortawesome/free-brands-svg-icons';
import { isDev } from '../../globals';
import { faCode } from '@fortawesome/free-solid-svg-icons';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';


@Component({
    selector: 'app-register',
    templateUrl: './register.component.html',
})
export class RegisterComponent {
    constructor(private authService: AuthService,private router:Router) {}
    faGoogle = faGoogle;
    isDevelopment = isDev;
    codeIcon = faCode;
    Username:string='';
    Email:string='';
    Password:string='';
    register() {
        if(this.Username=='' && this.Email=='' && this.Password==''){
            alert('Please enter your username, email and password');
        }
        else{
           
            this.authService.register(this.Username,this.Email, this.Password).subscribe((data) => {
                console.log(data);
                alert('Registration Successful');          
                this.router.navigate(['/login']);
                
              }
            )

           
        } 
    }
    handleValueChange(username?: string,email?: string, password?: string) {
        this.Username=username || this.Username;
        this.Email=email || this.Email;
        this.Password=password || this.Password;
    }
}
