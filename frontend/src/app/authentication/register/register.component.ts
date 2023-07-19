import { Component } from '@angular/core';
import { faGoogle } from '@fortawesome/free-brands-svg-icons';
import { isDev } from '../../globals';
import { faCode } from '@fortawesome/free-solid-svg-icons';
import { AuthService } from 'src/app/services/auth.service';
import {  Router } from '@angular/router';

@Component({
    selector: 'app-register',
    templateUrl: './register.component.html',
})
export class RegisterComponent {
   constructor(private authService: AuthService,private router:Router) { }
    faGoogle = faGoogle;
    isDevelopment = isDev;
    codeIcon = faCode;


    username: string = '';
    email: string = '';
    password: string = '';
    confirmPassword: string = '';
  
    onUsernameChange(username: any) {
      this.username = username;
    }
  
    onEmailChange(email: any) {
      this.email = email;
    }
  
    onPasswordChange(password: any) {
      this.password = password;
    }
  
    onConfirmPasswordChange(confirmPassword: any) {
      this.confirmPassword = confirmPassword;
    }
  
    register() {
      if(  this.password === '' || this.confirmPassword === ''|| this.username === '' || this.email === '') {
        alert('Pleas fill all the fields');
        if(this.password !== this.confirmPassword) {
            alert('Password and Confirm Password do not match');
        }
        return;
      }
     console.log(this.username,this.email,this.password,this.confirmPassword);
      this.authService.register(this.username, this.email, this.password).subscribe((response) => {
        console.log(response);
        alert("Register Success");
        this.router.navigate(['/login']);
      });
    }
}
