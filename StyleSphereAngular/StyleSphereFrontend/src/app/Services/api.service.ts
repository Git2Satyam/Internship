import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { AuthenticationService } from './authentication.service';

@Injectable({
  providedIn: 'root',
})
export class ApiService {
  isAdmin: boolean = false;
  url = environment.apiUrl;
  constructor(
    private client: HttpClient,
    private authService: AuthenticationService
  ) {}

  /*********************************************NavItemsApi************************************************************* */
  getNavItems(): Observable<any> {
    return this.client.get<any>(`${this.url}NavItems/GetAllNavItems`);
  }

  /*********************************************AdminNavItemsApi************************************************************* */

  getAdminNavItems(): Observable<any> {
    return this.client.get<any>(`${this.url}NavItems/GetAdminNavItems`);
  }

  /*********************************************UserControllerApi************************************************************* */
  verifyUser(obj: any): Observable<any> {
    let param = new HttpParams();
    param = param.set('username', obj.username);
    param = param.set('password', obj.password);
    return this.client.get<any>(`${this.url}User/VerifyUser`, {
      params: param,
    });
  }

  /*********************************************ProductControllerApi************************************************************* */
  getProdcuts(): Observable<any> {
    const token = this.authService.getToken();
    let headerObj = new HttpHeaders().set('Authorization', 'Bearer' + token);
    return this.client.get<any>(`${this.url}Products/GetAllProducts`, {
      headers: headerObj,
    });
  }
}
