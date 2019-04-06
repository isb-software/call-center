import { Component } from '@angular/core';

interface AppRoutes {
  icon?: string
  route?: string;
  title?: string;
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  myWorkRoutes: AppRoutes[] = [
    {
      icon: 'dashboard',
      route: 'home/dashboard',
      title: 'Dashboard',
    }
  ];
}
