import { Injectable } from '@angular/core';
import { Http, Headers } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { ErrorHandler } from '@angular/core';

import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';

import { Client } from '../modles/client';




@Injectable()
export class ClientService {

  private url = 'http://localhost:5200/api/client';
  private headers = new Headers({ 'Access-Control-Allow-Origin': '*' , 'Content-Type': 'application/json' });
  constructor(private http: Http) { }

  getAll(): Observable<Client[]> {
    return this.http.get(this.url)
      .map(response => response.json() as Client[]);
  }
}
