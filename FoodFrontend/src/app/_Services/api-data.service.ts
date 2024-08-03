import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import {HttpClient, HttpParams, HttpResponse} from '@angular/common/http'
import { Observable, catchError, map, observable, throwError } from 'rxjs';
import { Cartmodel } from '../_Models/cartmodel';
import { OrderDetail } from '../_Models/orderDetail';

@Injectable({
  providedIn: 'root'
})
export class ApiDataService {

  constructor(private client: HttpClient) {
   }

   apiUrl = environment.api;


    /* **********ProductController*************** */

  getAllProducts(): Observable<any>{
    return this.client.get<any>(this.apiUrl + 'Products/GetAllProducts')
    .pipe(
      map((data: HttpResponse<any>) => {
        return data;
      }))
  }

  saveProduct(productObj: any) : Observable<any>{
     return this.client.post<any>(this.apiUrl + 'Products/InsertOrUpdateProduct',productObj)
  }
 
  uploadImage(imgFile: any) : Observable<any>{
    return this.client.post<any>(this.apiUrl + 'Products/UploadImages',imgFile,{
      reportProgress: true,
      observe: 'events'
    })
  }

  deleteProdut(id: number){
    return this.client.delete(this.apiUrl + 'Products/DeleteProduct?id=' + id);
    
  }

  /* **********UserController*************** */
  getAllUsers(): Observable<any>{
    return this.client.get<any>(this.apiUrl + 'User/GetAllUser');
  }

  saveUser(userObj: any): Observable<any>{
    return this.client.post<any>(`${this.apiUrl}User/SaveUser`, userObj);
  }

  deleteUser(id: number): Observable<any>{
    return this.client.get<any>(`${this.apiUrl}User/DeleteUser?id=${id}`);
  }

   
  /* **********PaymentController*************** */
  cartOrder(cartObj: any): Observable<any>{
    console.log(cartObj);
     return this.client.post<any>(`${this.apiUrl}Payment/CreateOrder`, cartObj)
  }

  placeOrder(orderDetailObj: OrderDetail): Observable<any>{
     return this.client.post<any>(`${this.apiUrl}Payment/LogOrderAndPaymentDetailData`, orderDetailObj)
  }






  // public ToHttpParams(request: any): HttpParams {
  //   let httpParams = new HttpParams();
  //   Object.keys(request).forEach(function (key) {
  //     httpParams = httpParams.append(key, request[key]);
  //   });
  //   return httpParams;
  // }
  //  getCartid(): Observable<any>{
  //   return this.client.get<any>(`${this.apiUrl}Cart/SendGuidId`);
  //  }

  //  getProductDetailForCart(name: string){
  //   return this.client.get<any>(`${this.apiUrl}Cart/GetProductDetailByCartItem?name=${name}`)
  //  }
}
