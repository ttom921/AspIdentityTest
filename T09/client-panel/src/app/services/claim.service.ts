import { Claim } from './../models/claim';
import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { AuthService } from './auth.service';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class ClaimService {
  protected apiBase = 'http://localhost:5100/api/Identity/';
  private headers: Headers;

  constructor(
    private http: Http,
    private authService: AuthService
  ) {
  }
  // private headers = new Headers({ 'Access-Control-Allow-Origin': '*' , 'Content-Type': 'application/json' });
  private setHeaders() {

    console.log('setHeaders started');

    this.headers = new Headers();
    this.headers.append('Access-Control-Allow-Origin', '*');
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
    const apimethod = this.apiBase + 'get';
    return this.http.get(apimethod, options)
      .map(res => res.json())
      .catch((error: any) => Observable.throw(error.json().error || 'Server error'))
      ;
  }
  
  getfreeuser( apiadress: string): Observable<Claim[]> {
    this.setHeaders();
    const options = new RequestOptions({ headers: this.headers, body: '' });
    const apibase = 'http://localhost:5100/api/Identity/getfree';
    return this.http.get(apiadress, options)
    .map(res => res.json() )
    .catch((error: any) => Observable.throw(error.json().error || 'Server error getfreeuser'))
   ;
  }
  // --------------------------------------------
  getService(): Promise<any> {
    this.setHeaders();
    const options = new RequestOptions({ headers: this.headers, body: '' });
    const apimethod = this.apiBase + 'get';
    return this.http.get(apimethod, options)
    .toPromise()
    .then(this.extractData)
    .catch(this.handleError);
  }
  private extractData(res: Response) {
    const body = res.json();
    return body || {};
}

private handleError(error: any): Promise<any> {
    console.error('An error occurred', error);
    return Promise.reject(error.message || error);
}
}
