import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ApiService } from 'src/app/Services/api.service';


@Component({
  selector: 'app-side-navbar',
  templateUrl: './side-navbar.component.html',
  styleUrls: ['./side-navbar.component.css']
})
export class SideNavbarComponent implements OnInit {
  navlist: any[] = [];
  constructor(private apiService: ApiService, private activatedRoute: ActivatedRoute) { }

  ngOnInit(): void {
    console.log(this.apiService.isAdmin);
    let flag = this.apiService.isAdmin;
    if(flag){
      this.getAdminNavItems();
    }
    else{
      this.getNavItems();
    }
  }

  getNavItems(){
    this.apiService.getNavItems().subscribe((response: any) => {
      //console.log(response);
      if(response.Success){
        this.navlist = response.Result;
        console.log(this.navlist);
      }
    })
  }

  getAdminNavItems(){
    this.apiService.getAdminNavItems().subscribe(resp => {
      console.log(resp);
      if(resp.Success){
        this.navlist = resp.Result;
        console.log(this.navlist);
      }
    })
  }

}
