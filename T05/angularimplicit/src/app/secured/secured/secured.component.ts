import { AuthService } from './../../common/services/auth.service';
import { Component, OnInit , OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs/Subscription';
import { User } from 'oidc-client';

@Component({
  selector: 'app-secured',
  templateUrl: './secured.component.html',
  styleUrls: ['./secured.component.css']
})
export class SecuredComponent implements OnInit , OnDestroy {
  userSub: Subscription;
  user: User;
  constructor(private authService: AuthService) { 
    this.userSub = this.authService.userLoadededEvent.subscribe(u => this.user = u);
  }
  ngOnInit() {
  }
  ngOnDestroy(): void {
    this.userSub.unsubscribe();
  }

}
