import { NgModule } from "@angular/core";
import { AdministrationRoutingModule } from "./administration-routing.module";
import { AdHeaderComponent } from './AdminComponents/ad-header/ad-header.component';

@NgModule({
    declarations: [
    AdHeaderComponent
  ],
    imports: [AdministrationRoutingModule],
    providers: []
})

export class AdministrationModule { }