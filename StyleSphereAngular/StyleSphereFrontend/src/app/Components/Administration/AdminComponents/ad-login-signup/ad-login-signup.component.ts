import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ApiService } from 'src/app/Services/api.service';
import { AuthenticationService } from 'src/app/Services/authentication.service';

@Component({
  selector: 'app-ad-login-signup',
  templateUrl: './ad-login-signup.component.html',
  styleUrls: ['./ad-login-signup.component.css']
})
export class AdLoginSignupComponent implements OnInit {

  loginform: FormGroup;
  loading = false;
  constructor(private fb: FormBuilder, private api: ApiService, private router: Router, private authService: AuthenticationService) { }

  ngOnInit(): void {
    this.loginform = this.fb.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    })
  }

  validateUser(){
    this.loading = true;
    console.log(this.loginform.value);
    if(this.loginform.valid){
      this.api.verifyUser(this.loginform.value).subscribe(response => {
        if(response.Success){
          this.authService.savetoken(response.Result);
          this.router.navigate(['/admin/ad-header']);
          this.loading = false;
        }
        else{
           this.loading = false;
        }
      })
    }
    else{
        this.loading = false;
    }
  }
}
