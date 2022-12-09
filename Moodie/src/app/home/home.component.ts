import { Component, OnInit } from '@angular/core';
import { AuthService } from '../shared/services/auth.service';
import { AppComponent } from '../app.component';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor(public authService: AuthService, public router: AppComponent) { }
 showMe:boolean = true;
  ngOnInit(): void {
  }
  hideDiv() {
    if (this.showMe==true) {
      this.showMe = false;
    }
    else {
      this.showMe = true;
    }
  }
}

