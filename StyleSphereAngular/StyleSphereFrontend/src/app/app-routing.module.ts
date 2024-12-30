import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  //  {
  //   path: '',
  //   redirectTo: 'user',
  //   pathMatch: 'full'
  //  },
   {
     path: 'user',
     loadChildren: () => import("./Components/User/user.module").then(m => m.UserModule)
   },
   {
    path: 'admin',
    loadChildren: () => import("./Components/Administration/Administration.module").then(m => m.AdministrationModule)
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
