import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-side-navbar',
  templateUrl: './side-navbar.component.html',
  styleUrls: ['./side-navbar.component.css']
})
export class SideNavbarComponent implements OnInit {
  navlist = [
    {
      number: '1',
      name: 'home',
      icon: 'fa-solid fa-house'
    },
    {
      number: '2',
      name: 'Admin',
      icon: 'fa-solid fa-house'
    }, 
    {
      number: '1',
      name: 'Settings',
      icon: 'fa-solid fa-house'
    }
  ]
  constructor() { }

  ngOnInit(): void {
    console.log(this.navlist);
  }

}
