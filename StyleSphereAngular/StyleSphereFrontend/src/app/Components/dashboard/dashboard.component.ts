import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { ApiService } from 'src/app/Services/api.service';

@Component({
  selector: 'app-us-headerpage',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  isSideNavhide: boolean = true;
  constructor(private router: Router, private apiServcie: ApiService) { }

  ngOnInit(): void {
   this.apiServcie.isAdmin = false;
  }

  sidenavToggle(){
    this.isSideNavhide = !this.isSideNavhide;
    console.log(this.isSideNavhide);
  }
}
