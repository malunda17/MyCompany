import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms'; 
import { HttpClientModule } from '@angular/common/http'; 

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AddClaimComponent } from './components/add-claim/add-claim.component';
import { ClaimDetailsComponent } from './components/claim-details/claim-details.component';
import { ClaimsListComponent } from './components/claims-list/claims-list.component';
import { SigninRedirectCallbackComponent } from './components/signin-redirect-callback/signin-redirect-callback.component';
import { SignoutRedirectCallbackComponent } from './components/signout-redirect-callback/signout-redirect-callback.component';



@NgModule({
  declarations: [
    AppComponent,
    AddClaimComponent,
    ClaimDetailsComponent,
    ClaimsListComponent,
    SigninRedirectCallbackComponent,
    SignoutRedirectCallbackComponent,
    
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})

 
export class AppModule { 


}
