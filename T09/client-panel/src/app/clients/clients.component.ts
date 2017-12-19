import { ClientService } from './../services/client.service';
import { Component, OnInit } from '@angular/core';
import { Client } from '../models/client';

@Component({
  selector: 'app-clients',
  templateUrl: './clients.component.html',
  styleUrls: ['./clients.component.css']
})
export class ClientsComponent implements OnInit {

  public clients: Client[];
  constructor(private service: ClientService) { }

  ngOnInit() {
    this.service.getAll().subscribe(
      clients => {
        this.clients = clients;
        console.log(this.clients);
      }
    );
  }

}
