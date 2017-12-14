import { Claim } from './../models/claim';
import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { AuthService } from './auth.service';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class ClaimService {
  protected apiBase = 'http://localhost:5100/api/Identity/get';
  private headers: Headers;

  constructor(
    private http: Http,
    private authService: AuthService
  ) {
   
  }

  private setHeaders() {

    console.log('setHeaders started');

    this.headers = new Headers();
    this.headers.append('Content-Type', 'application/json');
    this.headers.append('Accept', 'application/json');

    const token = this.authService.token;
    if (token !== '') {
      const tokenValue = 'Bearer ' + token;
      console.log('tokenValue:' + tokenValue);
      this.headers.append('Authorization', tokenValue);
    }
  }

  get(): Observable<Claim[]> {
    this.setHeaders();
    const options = new RequestOptions({ headers: this.headers, body: '' });
   /*  return this.http.get(this.apiBase, options)
      .map(res => res.json()); */

     /*  return this.http.get(this.apiBase)
      .map(res => res.json()); */

      return this.http.get(this.apiBase)
      .map(response => response.json() as Claim[]);

  }
  
}
