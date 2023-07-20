import { Component } from '@angular/core';
import { faGoogle } from '@fortawesome/free-brands-svg-icons';
import { isDev } from '../../globals';
import { faCode } from '@fortawesome/free-solid-svg-icons';
import { AuthService } from 'src/app/services/auth.service';
import { Router } from '@angular/router';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
})
export class LoginComponent {
    constructor(private authService:AuthService,private router:Router) { }
    faGoogle = faGoogle;
    isDevelopment = isDev;
    codeIcon = faCode;
    emailRegex = /^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$/;
    email: string = '';
    password: string = '';
   
 
    
      onEmailChange(email: any) {
        this.email = email;
      }
    
      onPasswordChange(password: any) {
        this.password = password;
      }
    
     
    
      login() {
        if(  this.password === '' ||  this.email === '' || !this.email.match(this.emailRegex) ||this.password.length<8) {
          alert("Invalid Credentials");
          return;
        }
       
        this.authService.login(this.email, this.password).subscribe((response) => {
          console.log(response);
          alert("Login Success");
          this.router.navigate(['/dashboard']);
        });
      }
    
}
