import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { faEnvelope, faEye, faEyeSlash, faLock, faMobile, faUser } from '@fortawesome/free-solid-svg-icons';
import { ToastrService } from 'ngx-toastr';
import { AuthenticationService } from 'src/app/_Services/authentication.service';
import ValidateForm from 'src/app/_helper/validate-form';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent implements OnInit {

  signupForm: FormGroup;
  type: string = 'password';
  isText: boolean = false;
  eyeIcon =faEyeSlash;
  eye = faEye
  userIcon = faUser;
  lockIcon = faLock;
  mobile = faMobile;
  mail = faEnvelope;
  constructor(private fb: FormBuilder, private auth: AuthenticationService,private router: Router,private toastr: ToastrService) { }

  ngOnInit(): void {
    this.signupForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['',Validators.required],
      password: ['', Validators.required],
      phoneNumber: ['', Validators.required]
    })
  }


  hideShowPass() {
    this.isText = !this.isText;
    this.isText ? (this.eyeIcon = this.eye) : this.eyeIcon;
    this.isText ? (this.type = 'text') : (this.type = 'password');
  }

  
  onSubmit(){
    if(this.signupForm.valid){
      console.log(this.signupForm.value);
       let signupObj = this.signupForm.value;
      this.auth.SignUp(signupObj).subscribe({
        next: (res => {
          console.log(res);
          this.signupForm.reset();
          this.router.navigate(['login']);
        }),
        error: (err)=> {
          this.toastr.error('Somethng went wrong', 'Error'!, {
            timeOut: 5000
          });
          console.log(err);
        }
      })
    }
    else{
      ValidateForm.validateAllFormFields(this.signupForm)
    }
  }


}
