import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProductComponent } from './_Views/product/product.component';
import { DefaltLayoutComponent } from './_Views/defalt-layout/defalt-layout.component';
import { LoginComponent } from './_Views/login/login.component';
import { SignupComponent } from './_Views/signup/signup.component';
import { AdminComponent } from './_Views/admin/admin.component';
import { UserlistComponent } from './_Views/userlist/userlist.component';
import { MenubarComponent } from './_Views/menubar/menubar.component';
import { MonitorchartComponent } from './_Views/monitorchart/monitorchart.component';
import { CartComponent } from './_Views/cart/cart.component';
import { ReceiptComponent } from './_Views/receipt/receipt.component';

const routes: Routes = [
  {path:'', component: DefaltLayoutComponent},
  {path:'login', component: LoginComponent},
  {path:'signup', component: SignupComponent},
  {path:'Admin', component: AdminComponent},
  {path: 'User', component: UserlistComponent},
  {path: 'Menu', component: MenubarComponent},
  {path:'chart', component: MonitorchartComponent},
  {path: 'cart', component: CartComponent},
  {path:'receipt',component: ReceiptComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
