import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { MenuController } from '@ionic/angular';
import { AuthService } from './auth/auth.service';
@Component({
  selector: 'app-root',
  templateUrl: 'app.component.html',
  styleUrls: ['app.component.scss'],
})
export class AppComponent {
  public appPages = [
    { title: 'Inbox', url: '/', icon: 'mail' },
    { title: 'Outbox', url: '/', icon: 'paper-plane' },
    { title: 'Favorites', url: '/', icon: 'heart' },
    { title: 'Archived', url: '/', icon: 'archive' },
    { title: 'Trash', url: '/', icon: 'trash' },
    { title: 'Spam', url: '/', icon: 'warning' },
  ];

  constructor(private authService: AuthService, private router: Router, private menu: MenuController) {}

  public name(){
    return this.authService.currentUserName;
  }

  public email(){
    return this.authService.currentUserEmail;
  }

  public isAdmin(){
    return this.authService.isAdmin();
  }

  public onLogout(){
    this.menu.close();
    this.authService.logout();
    this.router.navigateByUrl("/auth");
  }
}
