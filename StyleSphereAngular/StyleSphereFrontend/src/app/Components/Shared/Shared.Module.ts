import { NgModule } from "@angular/core";
import { UsHeaderpageComponent } from "./SharedComponent/us-headerpage/us-headerpage.component";
import { RouterModule } from "@angular/router";
import { FooterComponent } from "./SharedComponent/footer/footer.component";
import { MaterialModule } from "src/app/material.module";
import { SideNavbarComponent } from "./SharedComponent/side-navbar/side-navbar.component";
import { CommonModule } from "@angular/common";


@NgModule({
    declarations: [UsHeaderpageComponent, FooterComponent, SideNavbarComponent],
    imports: [RouterModule, CommonModule],
    exports: [UsHeaderpageComponent, FooterComponent]
})

export class SharedModule { }