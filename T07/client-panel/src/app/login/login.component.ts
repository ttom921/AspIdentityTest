import { Claim } from './../models/claim';
import { ClaimService } from './../services/claim.service';
import { Component, OnInit, Input } from '@angular/core';
import { User } from 'oidc-client';
import { AuthService } from '../services/auth.service';
import { NavbarComponent } from '../navbar/navbar.component';
import { Observable } from 'rxjs/Observable';
import { Http, Response } from '@angular/http';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  public loggedInUser: User;

  public mydata: any;


  //protected apiBase = 'http://localhost:5100/api/Identity/get';
  //
  constructor(
    private _navcom: NavbarComponent,
    private http: Http,
    private service: ClaimService
  ) { }

  ngOnInit() {
    this.loggedInUser = this._navcom.loggedInUser;
    // console.log('aaaaa=>' + this.loggedInUser);

  }
  get(): any {
    // return { a: 'aaaa', b: 'bbbb' };
    this.mydata = { a: 'aaaa', b: 'ggggg' };
     this.mydata = this.getPosts();
    // this.mydata = this.service.get();
  }
  getPosts(): Observable<Claim[]> {
    const apiBase = 'http://localhost:5100/api/values';
    return this.http.get(apiBase)
      .map(res => res.json() as Claim[]);
  }

}
