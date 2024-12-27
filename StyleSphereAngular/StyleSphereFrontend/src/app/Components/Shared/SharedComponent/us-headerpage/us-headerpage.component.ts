import { Component, EventEmitter, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-us-headerpage',
  templateUrl: './us-headerpage.component.html',
  styleUrls: ['./us-headerpage.component.css']
})
export class UsHeaderpageComponent implements OnInit {
 isSideNavVisible: boolean = false;
  constructor() { }

  ngOnInit(): void {
  
  }

  sidenavToggle(){
    this.isSideNavVisible = !this.isSideNavVisible;
  }
}
