import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { AdHeaderComponent } from "./AdminComponents/ad-header/ad-header.component";


const route: Routes = [
   {
    path: '',
    component: AdHeaderComponent
   }
]

@NgModule({
    imports: [RouterModule.forChild(route)],
    exports: [RouterModule]
})

export class AdministrationRoutingModule { }