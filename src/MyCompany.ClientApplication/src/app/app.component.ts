 
import { Component, OnInit } from '@angular/core';
import { AuthService } from './shared/services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'InsuranceClientApp'; 
  public isUserAuthenticated: boolean = false;

  constructor(private _authService: AuthService){
    console.log("constructor called");
    this._authService.loginChanged
    .subscribe(userAuthenticated => {
      this.isUserAuthenticated = userAuthenticated;
    })
  }
  
  ngOnInit(): void {
    console.log("authentication called");
    this._authService.isAuthenticated()
    .then(userAuthenticated => {
      this.isUserAuthenticated = userAuthenticated;
    })
  }

  public login = () => { 
    console.log("login called");
    this._authService.login();
  }
 
  public logout = () => {
    this._authService.logout();
  }
}
