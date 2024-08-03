import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { User } from '../_Models/user';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  private currentUserSubject: BehaviorSubject<User>;
  private currentUser: Observable<User>;

  apiUrl = environment.api;

  constructor(private http: HttpClient,private router: Router) { }

  login(userObj: any){
      return this.http.post<any>(`${this.apiUrl}Login/AuthUser`,userObj);
  }

  SignUp(signupObj: User){
    return this.http.post<User>(`${this.apiUrl}User/SaveUser`, signupObj);
  }

  logout(){
    sessionStorage.removeItem('UserObj');
    this.router.navigate(['login']);
  }
}
