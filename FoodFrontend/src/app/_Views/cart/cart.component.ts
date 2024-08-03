import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Products } from 'src/app/_Models/products';
import { faTrash } from '@fortawesome/free-solid-svg-icons';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import ValidateForm from 'src/app/_helper/validate-form';
import { OrderDetail} from 'src/app/_Models/orderDetail';
import { AddressModel } from 'src/app/_Models/addressModel';
import { Router } from '@angular/router';
import { StoredataService } from 'src/app/_Services/storedata.service';
import { Subscription } from 'rxjs';
import { CartService } from 'src/app/_Services/cart.service';
import { ApiDataService } from 'src/app/_Services/api-data.service';
declare var Razorpay: any; 

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {
  deleteProduct = faTrash;
  loading: boolean = false;
  userlist: any[] = [];
  userId: any;
  cartItemList: any[] = [];
  itemCount: number;
  cartDetail: any
  public addressForm!: FormGroup;
  showForm= false;
  orderDetail: any;
  currentUser: any;
  userObj: any;
  //deliveryAddress: any;
  OrderDetailML: OrderDetail;
  addressML: AddressModel;
  constructor(private cartService: CartService, private apiService: ApiDataService, private toastr: ToastrService, private fb: FormBuilder, private route: Router, 
    private globalData: StoredataService) 
  {
   
  }

  ngOnInit(): void {
    this.getAllUser().then(() => {
      this.getCartItemsDetail();
    });
   
  }
 
  getAllUser() {
    this.loading = true;
    return new Promise((resolve: any) => {
      this.apiService.getAllUsers().subscribe((user) => {
        //console.log(user);
        this.userlist = user;
        console.log(this.userlist);
        this.getUserData();
        resolve();
      })
    })
  }

  getUserData(){
    let obj = JSON.parse(sessionStorage.getItem('UserObj')!)
    //console.log(obj);
    if(obj){
      let email = obj.username;
      let data =  this.userlist.filter(x => x.Email.toUpperCase() == email.toUpperCase());
      this.currentUser = data;
      var object = this.currentUser.reduce(
        (obj: any) => Object.assign(obj, {}));
      console.log(object)
      this.userObj = object;
      let id: any = data.map(m => m.Id).toString();
      this.userId = id;
    }
  }

  getCartItemsDetail(){
    this.cartService.getCartDetail(this.userId).subscribe((data) => {
      console.log(data);
      if(data.Success){
        this.cartDetail = data.Result;
        console.log(this.cartDetail);
        this.cartItemList = data.Result.products
        console.log(this.cartItemList);
        this.itemCount = this.cartItemList.length;
       // this.globalData.setCartCount(this.cartItemList.length)
      }
      else
      {
        this.toastr.error('Cart is empty.', 'Error!')
      }
    })
    this.loading = false;
  }

  quantityCount(value: string, index: any, product: any){
    let qty = product.Quantity
    if(qty < 8 && value == 'max'){
      qty  +=1;
      this.cartItemList[index] = product
    }
    else if(qty > 1 && value == 'min'){
      qty -=1;
      this.cartItemList[index] = product
    }
    //console.log(product);
     this.updateQuantity(qty, product.Id)
  }

  updateQuantity(quantity: number, itemId: number){
    this.cartService.updateItemQuantity(quantity, itemId, this.userId).subscribe((data) => {
      console.log(data);
      if(data.Success){
        let updatedProduct = data.Result
        console.log(updatedProduct);
        this.toastr.success('Quantity Updated Successfully', 'Success!')
        console.log(this.cartItemList);
        this.cartItemList.map(obj => {
          if(obj.Id == updatedProduct.ProductId){
            obj.Quantity = updatedProduct.Quantity;
            obj.TotalPrice = updatedProduct.TotalPrice;
          }
        })
        this.getCartItemsDetail();
      }
      else{
        this.toastr.error('something went wrong', 'Error!')
      }
    })
  }

  delteItemfromCart(itemId: number){
    console.log(itemId);
     this.cartService.deleteCartItem(itemId).subscribe((data) => {
      console.log(data);
      if(data.Success){
        this.toastr.success('Item deleted successfully.')
        this.getAllUser().then(() => {this.getCartItemsDetail()})
      }
      else{
        this.toastr.success('Something went wrong.')
      }
     })
  }
  get f()
  {
      return this.addressForm.controls;
  }
  checkout(){
    this.showForm = true;
    this.addressForm = this.fb.group({
       houseAddress: new FormControl("",[Validators.required, Validators.maxLength(50)]),
       zipcode: new FormControl("", [Validators.required]),
       state: new FormControl("",  [Validators.required]),
       country:new FormControl("",  [Validators.required]),
       phoneNumber: new FormControl("",  [Validators.required]),
    })
  }

  submitForm() {

    if (this.addressForm.valid) {
      //console.log(this.addressForm.value);
      this.addressML = new AddressModel();
      this.addressML.Address = this.addressForm.get('houseAddress')?.value;
      this.addressML.ZipCode = this.addressForm.get('zipcode')?.value;
      this.addressML.Country = this.addressForm.get('country')?.value;
      this.addressML.State = this.addressForm.get('state')?.value;
      this.addressML.PhoneNumber = this.addressForm.get('phoneNumber')?.value;
      console.log(this.cartDetail);
      this.apiService.cartOrder(this.cartDetail).subscribe(data => {
        //console.log(data);
        if (data.Success) {
          this.orderDetail = data.Result
          console.log(this.orderDetail);
          this.openTransactionModal(this.orderDetail)
        }
      })
    }
    else {
      ValidateForm.validateAllFormFields(this.addressForm);
    }
  }

  closeForm(){
    this.addressForm.reset();
    this.showForm = false;
  }


  /*-------------------------PaymentDetail--------------------------*/
  openTransactionModal(response: any) {
    console.log(this.userObj.Email)
    var options = {
      "key": response.RazorpayKey,
      "amount": response.GrandTotal * 100,
      "currency": response.Currency,
      "name":response.Name,
      "description": response.Description,
      "image": "/assets/Images/header_img.avif",
      "order_id": response.OrderId,
      handler: (response: any) => {
        this.processResponse(response);
      },
      "prefill": {
        "name": this.userObj.FirstName + this.userObj.LastName,
        "email": this.userObj.Email,
        "contact": this.userObj.PhoneNumber
      },
      "notes": {
        "address": "NA"
      },
      "theme": {
        "color": "#4285F4"
      }
    };

    var razorpayObj = new Razorpay(options)
    razorpayObj.open();
  }

  processResponse(response: any){
    //console.log(this.userId);
    //console.log(response);
    return new Promise((resolve: any) => {
      this.OrderDetailML = new OrderDetail();
      this.OrderDetailML.rzp_PaymentId = response.razorpay_payment_id;
      this.OrderDetailML.rzp_OrderId = response.razorpay_order_id;
      this.OrderDetailML.rzp_Signature = response.razorpay_signature;
      this.OrderDetailML.Currency = this.orderDetail.Currency;
      this.OrderDetailML.Receipt = this.orderDetail.Receipt;
      this.OrderDetailML.UserId = this.userId;
      this.OrderDetailML.deliveryAddress = this.addressML;
      this.OrderDetailML.cartModel = this.cartDetail;
      //this.globalData._userOrderDetail = this.OrderDetailML   // here we store the info into service for using in receipt
      this.apiService.placeOrder(this.OrderDetailML).subscribe(data => {
        console.log(data);
        if(data.Success){
          sessionStorage.setItem('order_detail', JSON.stringify(this.OrderDetailML))
        }
        let ele = document.getElementById('close-btn')
        ele?.click();
        this.cartItemList = [];
        this.cartDetail = [];
        this.cartService.removeItemsFromCart(this.userId).subscribe(data => {
          console.log(data);
        })
      })
     // this.getCartItemsDetail();
       this.route.navigate(['/receipt'])
       resolve();
    })
  }
}
