import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {ClaimsListComponent} from './components/claims-list/claims-list.component';
import {ClaimDetailsComponent} from './components/claim-details/claim-details.component';
import {AddClaimComponent} from './components/add-claim/add-claim.component';
import { SigninRedirectCallbackComponent } from './components/signin-redirect-callback/signin-redirect-callback.component';
import { SignoutRedirectCallbackComponent } from './components/signout-redirect-callback/signout-redirect-callback.component';

const routes: Routes = [
  {path:'',redirectTo: 'claims', pathMatch: 'full'},
  {path: 'claims', component:ClaimsListComponent},
  {path: 'claims/:id', component:ClaimDetailsComponent},
  {path: 'add',component:AddClaimComponent}

];
RouterModule.forRoot([
  { path: 'claims', component: ClaimsListComponent },
//  { path: 'company', loadChildren: () => import('./company/company.module').then(m => m.CompanyModule) },
  { path: 'signin-callback', component: SigninRedirectCallbackComponent },
  { path: 'signout-callback', component: SignoutRedirectCallbackComponent },
 // { path: '404', component : NotFoundComponent},
  { path: '', redirectTo: '/home', pathMatch: 'full' },
  { path: '**', redirectTo: '/404', pathMatch: 'full'}
])

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
