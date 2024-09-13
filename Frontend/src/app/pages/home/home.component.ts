import { Component } from '@angular/core';
import {SidenavComponent} from '../sidenav/sidenav.component'
import { BodyComponent } from '../body/body.component';
import { DashboardComponent } from '../dashboard/dashboard.component';
import { HeaderComponent } from "../header/header.component";

interface SideNavToggle{
  screenWidth: number;
  collapsed:boolean;
}

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [SidenavComponent, BodyComponent, DashboardComponent, HeaderComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
})
export class HomeComponent {
  title='sidenav';

  isSideNavCollapsed = false;
  screenWidth = 0;

  onToggleSideNav(data : SideNavToggle): void{
    this.screenWidth = data.screenWidth;
    this.isSideNavCollapsed = data.collapsed;
  }
}
