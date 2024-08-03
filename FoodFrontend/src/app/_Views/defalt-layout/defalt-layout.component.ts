import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FaIconLibrary } from '@fortawesome/angular-fontawesome';
import { faCog, faShoppingCart, faSignOutAlt } from '@fortawesome/free-solid-svg-icons';
import { CookieService } from 'ngx-cookie-service';
import { ToastrService } from 'ngx-toastr';
import { Cartmodel } from 'src/app/_Models/cartmodel';
import { ApiDataService } from 'src/app/_Services/api-data.service';
import { AuthenticationService } from 'src/app/_Services/authentication.service';
import { AutologoutService } from 'src/app/_Services/autologout.service';
import { CartService } from 'src/app/_Services/cart.service';
import { StoredataService } from 'src/app/_Services/storedata.service';

@Component({
  selector: 'app-defalt-layout',
  templateUrl: './defalt-layout.component.html',
  styleUrls: ['./defalt-layout.component.css']
})
export class DefaltLayoutComponent implements OnInit {
  shopping = faShoppingCart;
  signout = faSignOutAlt;
  setting = faCog;
  userId: any;
  productImages: any[] = [];
  userlist: any[] = [];
  cartModel: Cartmodel = new Cartmodel;
  itemLength: number = 0;
  name: 'Angular';
  constructor(private service: ApiDataService, private cartService: CartService, private logoutService: AutologoutService, private authService: AuthenticationService,
    private route: Router, private cookieService: CookieService, private toastr: ToastrService, private globalData: StoredataService) {
    this.name = this.logoutService.val;
  }

  ngOnInit(): void {
    this.GetAllProducts();
    this.getAllUser().then(() => {
      this.getUserData();
      this.getItemCount();
    });
    sessionStorage.removeItem('order_detail')
    this.cartService.productCount.subscribe((res: any) => {     // this logic will increament the itemcount on cart icon
      console.log(res);
      this.itemLength += parseInt(res);
    })
  }
  GetAllProducts() {
    this.service.getAllProducts().subscribe({
      next: (data) => {
        console.log(data);
        this.productImages = data;
        console.log(this.productImages);
      },
      error: (error) => {
        console.log(error);
      }
    })
  }

  getAllUser() {
    return new Promise((resolve: any) => {
      this.service.getAllUsers().subscribe((user) => {
        //console.log(user);
        this.userlist = user;
        console.log(this.userlist);
        resolve();
      })
    })

  }

  getUserData() {
    let obj = JSON.parse(sessionStorage.getItem('UserObj')!)
    //console.log(obj);
    if (obj) {
      let email = obj.username;
      //console.log(this.userlist);
      let data = this.userlist.filter(x => x.Email.toUpperCase() == email.toUpperCase()).map(m => m.Id);
      let id: any = data.toString();
      console.log(id);
      this.userId = id;
    }
    else {
      this.route.navigate(['/login'])
    }
  }

  Signout() {
    this.authService.logout();
  }

  addToCart(productId: number, price: number) {
    return new Promise((resolve: any) => {
      console.log(productId)
      this.cartModel = new Cartmodel();
      this.cartModel.UserId = this.userId;
      this.cartModel.ProductId = productId;
      this.cartModel.Unitprice = price;
      this.cartModel.Quantity = 1;
      this.cartService.addItemToCart(this.cartModel).subscribe((data) => {
        console.log(data);
        //let id = data.Result.Id;
        if (data.Success) {
          this.toastr.show('<div style=@"background-color: white">Item added successfully</div><br><a href="/cart">Go to cart</a>', 'Message', {
            enableHtml: true,
            closeButton: true,
            timeOut: 500
          })
          this.cartService.productCount.next(1);     
        }
        else {
          this.toastr.error('Something went wrong!')
        }
        //console.log(id);
        resolve();
      })
    })
  }

  getItemCount() {
    this.cartService.getCartItemCount(this.userId).subscribe(count => {
      this.itemLength  = count
      console.log(this.itemLength);
    })
  }

}
