import { Component } from '@angular/core';
import { faGoogle } from '@fortawesome/free-brands-svg-icons';
import { isDev } from '../../globals';
import { faCode } from '@fortawesome/free-solid-svg-icons';
import { AuthService } from 'src/app/services/auth.service';
import {  Router } from '@angular/router';
import {ToastrService} from "ngx-toastr";

@Component({
    selector: 'app-register',
    templateUrl: './register.component.html',
})
export class RegisterComponent {
   constructor(private authService: AuthService,private router:Router,private toastr: ToastrService) { }
    faGoogle = faGoogle;
    isDevelopment = isDev;
    codeIcon = faCode;


    username: string = '';
    email: string = '';
    password: string = '';
    confirmPassword: string = '';
    emailRegex = /^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$/;

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
      if(  this.password === '' || this.confirmPassword === ''|| this.username === '' || this.email === '' || !this.email.match(this.emailRegex) ||this.password.length<8) {
          this.toastr.success('Please enter valid credentials', 'Error')
        return;
      }
      if(this.password !== this.confirmPassword) {
          this.toastr.success('Passwords do not match', 'Error')
          return;
      }
     console.log(this.username,this.email,this.password,this.confirmPassword);
      this.authService.register(this.username, this.email, this.password).subscribe((response) => {
        console.log(response);
          this.toastr.success('Registered successfully', 'Success')
        this.router.navigate(['/login']);
      });
    }
}
