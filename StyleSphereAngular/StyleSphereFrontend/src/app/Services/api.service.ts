import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  url = environment.apiUrl;
  constructor(private client: HttpClient) { }

  /*********************************************NavItemsApi************************************************************* */
  getNavItems(): Observable<any>{
    return this.client.get<any>(`${this.url}NavItems/GetAllNavItems`);
  }

}
