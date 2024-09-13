import { Component, HostListener, Input, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [CommonModule,RouterModule],
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  
  @Input() collapsed = false;
  @Input() screenWidth = 0 ;

  canShowSearchAsOverlay = false;
 login=localStorage.getItem("login");
  constructor() {}

  @HostListener('window:resize',['$event'])
  onResize(event:any){
    this.checkCanShowSearchAsOverlay(window.innerWidth);
  }


  ngOnInit(): void {
    this.checkCanShowSearchAsOverlay(window.innerWidth);
  }

  getHeadClass() : string {
    let styleClass = '';
    if (this.collapsed && this.screenWidth> 768) {
      styleClass = 'head-trimed';
    }
    else if(this.collapsed && this.screenWidth <= 768 && this.screenWidth> 0){
      styleClass = 'head-md-screen'
    }
    return styleClass;
  }

  checkCanShowSearchAsOverlay(innerWidth : number):void{
    if(innerWidth < 845){
      this.canShowSearchAsOverlay = true;
    }
    else{
      this.canShowSearchAsOverlay = false;
    }
  }
}
