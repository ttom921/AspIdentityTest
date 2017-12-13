import { Component, OnInit, Input } from '@angular/core';
import { User } from 'oidc-client';
import { AuthService } from '../services/auth.service';
import { NavbarComponent } from '../navbar/navbar.component';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  public loggedInUser: User;

  constructor(
    private _navcom:NavbarComponent
  ) { }

  ngOnInit() {
   this.loggedInUser= this._navcom.loggedInUser;
   console.log("aaaaa=>"+this.loggedInUser)
  }

}
