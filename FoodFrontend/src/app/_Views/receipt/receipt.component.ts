import { Component, Input, OnInit } from '@angular/core';
import { OrderDetail } from 'src/app/_Models/orderDetail';
import { StoredataService } from 'src/app/_Services/storedata.service';

@Component({
  selector: 'app-receipt',
  templateUrl: './receipt.component.html',
  styleUrls: ['./receipt.component.css']
})
export class ReceiptComponent implements OnInit {
  //private orderModel: OrderDetail
  orderData: any
  user: any
  constructor(private globalData: StoredataService) { }

  ngOnInit(): void {
    //this.orderModel = this.globalData._userOrderDetail
    //console.log(this.orderModel);
    this.orderData = JSON.parse(sessionStorage.getItem('order_detail')!);
    console.log(this.orderData);
   this.user = JSON.parse(sessionStorage.getItem('UserObj')!);
   console.log(this.user);
  }

}
