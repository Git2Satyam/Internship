import { NgModule } from "@angular/core";
import {RouterModule, Routes } from "@angular/router";

const route: Routes = [
    // {
    //     path: '',
    //     component: UsHeaderpageComponent
    // }
]

@NgModule({
    imports: [RouterModule.forChild(route)],
    exports: [RouterModule]
})

export class UserRoutingModule { }