import { Component } from '@angular/core';
import { faGoogle } from '@fortawesome/free-brands-svg-icons';
import { isDev } from '../../globals';
import { faCode } from '@fortawesome/free-solid-svg-icons';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
})
export class LoginComponent {
    faGoogle = faGoogle;
    isDevelopment = isDev;
    codeIcon = faCode;
    Email:string='';
    Password:string='';
    constructor(private authService: AuthService,private router:Router) {}
    login() {
        if(this.Email=='' && this.Password==''){
            alert('Please enter your email and password');
        }
        else{
            this.authService.login(this.Email, this.Password).subscribe((data) => {
                console.log(data);
                alert('Login Successful');
                this.router.navigate(['/dashboard']);
              
            }
            )
        }
      }
    handleValueChange(email?: string, password?: string) {   
        this.Email=email || this.Email;
        this.Password=password || this.Password;
    }
    
    
     
}
