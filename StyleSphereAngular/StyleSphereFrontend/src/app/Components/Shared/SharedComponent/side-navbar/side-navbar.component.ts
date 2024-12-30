import { Component, Input, OnInit } from '@angular/core';
import { ApiService } from 'src/app/Services/api.service';

@Component({
  selector: 'app-side-navbar',
  templateUrl: './side-navbar.component.html',
  styleUrls: ['./side-navbar.component.css']
})
export class SideNavbarComponent implements OnInit {
  navlist: any[] = [];
  constructor(private apiService: ApiService) { }

  ngOnInit(): void {
    this.getNavItems();
  }

  getNavItems(){
    this.apiService.getNavItems().subscribe(response => {
      //console.log(response);
      if(response.Success){
        this.navlist = response.Result;
        console.log(this.navlist);
      }

    })
  }

}
