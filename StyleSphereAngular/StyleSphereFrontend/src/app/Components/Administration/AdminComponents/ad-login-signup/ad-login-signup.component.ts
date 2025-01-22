import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ApiService } from 'src/app/Services/api.service';
import { AuthenticationService } from 'src/app/Services/authentication.service';

@Component({
  selector: 'app-ad-login-signup',
  templateUrl: './ad-login-signup.component.html',
  styleUrls: ['./ad-login-signup.component.css']
})
export class AdLoginSignupComponent implements OnInit {

  loginform: FormGroup;
  signupForm: FormGroup;
  loading = false;
  showform = false;
  constructor(private fb: FormBuilder, private api: ApiService, private router: Router, private authService: AuthenticationService, 
    private toastr: ToastrService,
    
  ) { }

  ngOnInit(): void {
    this.loginform = this.fb.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    })

    this.signupForm = this.fb.group({
      firstname: ['', Validators.required],
      lastname: ['', Validators.required],
      email: ['', Validators.required],
      password: ['', Validators.required],
      confirmPassword: ['', Validators.required]
    }, 
    {
      validators: this.passwordMatchValidator()
    })
  }

  validateUser() {
    this.loading = true;
    console.log(this.loginform.value);
    if (this.loginform.valid) {
      this.api.verifyUser(this.loginform.value).subscribe(response => {
        if (response.Success) {
          this.authService.savetoken(response.Result);
          this.router.navigate(['/admin']);
          this.loading = false;
        }
        else {
          this.toastr.error('Invalid credential', "Error!");
          this.loading = false;
        }
      })
    }
    else {
      this.toastr.error('Inalid input', "Error!");
      this.loading = false;
    }
  }

  openSignUpForm() {
    this.showform = true;
  }

  saveUser() {
    this.loading = true;
    console.log(this.signupForm.value);
    if(this.signupForm.valid){
      this.api.insertOrUpdateUser(this.signupForm.value).subscribe(resp => {
        console.log(resp);
        if(resp.Success){
           this.toastr.success("User added successfully", 'Success!')
            this.signupForm.reset();
            this.loading = false
            this.router.navigate(['/admin'])
        }
        else{
          this.toastr.error("Something went wrong", 'Error!');
          this.loading = false;
        }
      })
    }
    else{
        this.toastr.warning("Invalid input", "Warning!");
        this.loading = false;
    }
   
  }



  passwordMatchValidator() {
    return (formGroup: FormGroup) => {
      const control = formGroup.controls['password'];
      const matchingControl = formGroup.controls['confirmPassword'];
      if (matchingControl.errors && !matchingControl.errors['mustMatch']) {
        return;
      }
      if (control.value !== matchingControl.value) {   //set error if vlaidator failed.
        matchingControl.setErrors({ mustMatch: true });
      } else {
        matchingControl.setErrors(null);
      }
    }
  }

  
}
