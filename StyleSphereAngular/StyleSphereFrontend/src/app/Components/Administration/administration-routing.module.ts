import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { AdHeaderComponent } from "./AdminComponents/ad-header/ad-header.component";
import { ProductManagementComponent } from "./AdminComponents/product-management/product-management.component";
import { AdLoginSignupComponent } from "./AdminComponents/ad-login-signup/ad-login-signup.component";


const route: Routes = [
     {
          path: '',
          component: AdLoginSignupComponent
     },
     {
          path: 'ad-header',
          component: AdHeaderComponent
     },
     {
          path: 'product-management',
          component: ProductManagementComponent,
     },
   
]

@NgModule({
    imports: [RouterModule.forChild(route)],
    exports: [RouterModule]
})

export class AdministrationRoutingModule { }