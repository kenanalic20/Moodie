import { Component, OnInit,Input } from '@angular/core';
import { AuthService } from '../shared/services/auth.service';
import { AppComponent } from '../app.component';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(public authService: AuthService,public router:AppComponent) { }
    
  ngOnInit(): void {}
 


}



