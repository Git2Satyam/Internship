import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { ToastrModule } from 'ngx-toastr';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgxLoadingModule } from "ngx-loading";
import { CookieService } from 'ngx-cookie-service';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ProductComponent } from './_Views/product/product.component';
import { LoginComponent } from './_Views/login/login.component';
import { HttpClientModule } from '@angular/common/http';
import { DefaltLayoutComponent } from './_Views/defalt-layout/defalt-layout.component';
import { SignupComponent } from './_Views/signup/signup.component';
import { AutologoutService } from './_Services/autologout.service';
import { AdminComponent } from './_Views/admin/admin.component';
import { AngMaterialModule } from './ang-material/ang-material.module';
import { UserlistComponent } from './_Views/userlist/userlist.component';
import { MenubarComponent } from './_Views/menubar/menubar.component';
import { MonitorchartComponent } from './_Views/monitorchart/monitorchart.component';
import { CartComponent } from './_Views/cart/cart.component';
import { ReceiptComponent } from './_Views/receipt/receipt.component';
import { StoredataService } from './_Services/storedata.service';



@NgModule({
  declarations: [
    AppComponent,
    ProductComponent,
    LoginComponent,
    DefaltLayoutComponent,
    SignupComponent,
    AdminComponent,
    UserlistComponent,
    MenubarComponent,
    MonitorchartComponent,
    CartComponent,
    ReceiptComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    HttpClientModule,
    FontAwesomeModule,
    FormsModule,
    ReactiveFormsModule,
    ToastrModule.forRoot(),
    AngMaterialModule,
    NgxLoadingModule.forRoot({})
    
  ],
  providers: [CookieService],
  bootstrap: [AppComponent]
})
export class AppModule {
 }
