import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { faUser } from '@fortawesome/free-regular-svg-icons';
import { faEye, faEyeSlash, faLock, faSpinner } from '@fortawesome/free-solid-svg-icons';
import { ToastrService } from 'ngx-toastr';
import { AuthenticationService } from 'src/app/_Services/authentication.service';
import ValidateForm from 'src/app/_helper/validate-form';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
 public loginForm!: FormGroup;
  type: string = 'password';
  isText: boolean = false;
  eyeIcon =faEyeSlash;
  eye = faEye
  userIcon = faUser;
  lockIcon = faLock;
  spinner = faSpinner;
  loading = false;

  constructor(private fb: FormBuilder, private auth: AuthenticationService, private toast: ToastrService, private router: Router) { }

  ngOnInit(): void {
    this.loginForm = this.fb.group({
      username: ['',Validators.required],
      password: ['', Validators.required]
    })
  }

  hideShowPass() {
    this.isText = !this.isText;
    this.isText ? (this.eyeIcon = this.eye) : this.eyeIcon;
    this.isText ? (this.type = 'text') : (this.type = 'password');
  }

  onSubmit() {
    this.loading = true;
    if (this.loginForm.valid) {
      console.log(this.loginForm.value);
      this.auth.login(this.loginForm.value).subscribe({
        next: (res) => {
          console.log(res.Message);
          this.loading = false;
          sessionStorage.setItem('UserObj', JSON.stringify(this.loginForm.value));
          this.loginForm.reset();
          //this.auth.storeToken(res.accessToken);
          //this.auth.storeRefreshToken(res.refreshToken);
          //const tokenPayload = this.auth.decodedToken();
          //this.userStore.setFullNameForStore(tokenPayload.name);
          //this.userStore.setRoleForStore(tokenPayload.role);
          this.toast.success('Login in successfully.','Login successful!');
          this.router.navigate(['/'])
        },
        error: (err) => {
          this.loading = false;
          this.toast.error('Invalid Credentials.','Error!');
          console.log(err);
        },
      });
    } else {
      ValidateForm.validateAllFormFields(this.loginForm);
    }
  }

}
