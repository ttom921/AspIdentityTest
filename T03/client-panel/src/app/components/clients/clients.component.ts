import { Component, OnInit } from '@angular/core';
import { Client } from '../../models/client';
import { ClientService } from '../../services/client.service';

@Component({
  selector: 'app-clients',
  templateUrl: './clients.component.html',
  styleUrls: ['./clients.component.css']
})
export class ClientsComponent implements OnInit {

  public clients:Client[];
  constructor(private service:ClientService) { }

  ngOnInit() {
    //console.log('clients');
    this.service.getAll().subscribe(
      clients => {
        this.clients= clients;
        console.log(this.clients);
      });
  }

}
