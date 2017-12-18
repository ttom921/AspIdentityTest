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

  public mydata: Claim[];


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
  get() {
    this.service.get().subscribe(
      claims => {
        this.mydata = claims;
      },
      err => {
        console.log(err);
      }
    );

    // return { a: 'aaaa', b: 'bbbb' };
    // this.mydata = { a: 'aaaa', b: 'ggggg' };
      this.getPosts().subscribe( claims => this.mydata = claims,
      err => {
        console.log(err);
      });
    // this.mydata = this.service.get();
  }
  getPosts(): Observable<Claim[]> {
    const apiBase = 'http://localhost:5100/api/Identity/get';
    return this.http.get(apiBase)
      .map((res: Response) => res.json())
      // ...error any
      .catch((error: any) => Observable.throw(error.json().error || 'Server error'));
  }

  getfreeuser() {
    // const apiBase = 'http://localhost:5100/api/Identity/get';

    const apiadd = 'http://localhost:5100/api/Identity/getfree';

    this.service.getfreeuser(apiadd).subscribe(
      claims => {
        this.mydata = claims;
      },
      err => {
        console.log(err);
      });
  }
  getbyfree() {
    this.service.get().subscribe(
      claims => {
        this.mydata = claims;
      },
      err => {
        console.log(err);
      }
    );
  }
  // -------------
  getpromse() {
    this.service.getService()
    .then(result => {
      this.mydata = result as Claim[];
    })
    .catch(error => console.log(error));
  }

}
