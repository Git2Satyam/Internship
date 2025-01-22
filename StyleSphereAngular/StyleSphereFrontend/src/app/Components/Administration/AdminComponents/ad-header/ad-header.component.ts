import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ApiService } from 'src/app/Services/api.service';
import { AuthenticationService } from 'src/app/Services/authentication.service';

@Component({
  selector: 'app-ad-header',
  templateUrl: './ad-header.component.html',
  styleUrls: ['./ad-header.component.css']
})
export class AdHeaderComponent implements OnInit {

  isExpanded = true;
  showSubmenu: boolean = false;
  isShowing = false;
  showSubSubMenu: boolean = false;
  sidenavOpen: boolean = true;
  constructor(private apiService: ApiService, private auth: AuthenticationService, private router: Router) { }

  ngOnInit(): void {
    let userlogged = this.auth.isLogged();
    if(!userlogged){
       this.router.navigate(['/admin/ad-login-signup'])
    }
    this.apiService.isAdmin = true;
  }

  mouseenter() {
    if (!this.isExpanded) {
      this.isShowing = true;
    }
  }

  mouseleave() {
    if (!this.isExpanded) {
      this.isShowing = false;
    }
  }

  sidenavToggle(){
    this.sidenavOpen = !this.sidenavOpen;
  }
}
