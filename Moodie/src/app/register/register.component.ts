import { Component, OnInit } from '@angular/core';
import { AuthService } from '../shared/services/auth.service';
import { AppComponent } from '../app.component';
@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  constructor( public authService: AuthService,public router:AppComponent) { }
  ngOnInit() {
  }
 
}
