import { HttpClient,  HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Cartmodel } from '../_Models/cartmodel';
import {  Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CartService {

  constructor(private _client: HttpClient) {}
  apiUrl = environment.api;
  public productCount = new Subject();

  getProductDetailForCart(Id: number) {
    let param = new HttpParams();
    param = param.set("id", Id)
    return this._client.get<any>(`${this.apiUrl}Cart/GetProductDetailByCartItem`, { params: param })
  }

  addItemToCart(items: Cartmodel) {
    return this._client.post<any>(`${this.apiUrl}Cart/AddItemToCart`, items)
  }

  getCartDetail(userId: number){
    let param = new HttpParams();
    param = param.set("id", userId)
    return this._client.get<any>(`${this.apiUrl}Cart/GetCartDetail`, {params: param})
  }

  deleteCartItem(id: number){
    let param = new HttpParams();
    param = param.set("itemId", id);
    return this._client.delete<any>(`${this.apiUrl}Cart/DeleteItemFromCart`, {params: param})
  }

  updateItemQuantity(quantity: any, ItemId: number, userId: number){
    let param = new HttpParams();
    param = param.set("quantity",quantity);
    param = param.set("itemId",ItemId);
    param = param.set("userId",userId);
    return this._client.get<any>(`${this.apiUrl}Cart/UpdateQuantity`, {params: param}) 
  }

  removeItemsFromCart(id: number){
    let param = new HttpParams();
    param = param.set('userId',id);
    return this._client.get<any>(`${this.apiUrl}Cart/RemoveOrderdItemFromCart`, {params: param})
  }

  getCartItemCount(id: number){
    let param = new HttpParams();
    param = param.set("userId", id)
    return this._client.get<any>(`${this.apiUrl}Cart/GetCartItemCount`, {params: param})
  }
}
