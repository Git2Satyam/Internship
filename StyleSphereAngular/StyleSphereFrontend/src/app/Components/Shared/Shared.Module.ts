import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { FooterComponent } from "./SharedComponent/footer/footer.component";
import { CommonModule } from "@angular/common";
import { SideNavbarComponent } from "./SharedComponent/side-navbar/side-navbar.component";


@NgModule({
    declarations: [FooterComponent, SideNavbarComponent],
    imports: [RouterModule, CommonModule],
    exports: [FooterComponent, SideNavbarComponent]
})

export class SharedModule { }