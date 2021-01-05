import { Component, OnInit } from '@angular/core';
import {Claim} from 'src/app/models/claim.model';
import {ClaimService} from 'src/app/services/claim.service'; 

@Component({
  selector: 'app-claims-list',
  templateUrl: './claims-list.component.html',
  styleUrls: ['./claims-list.component.css']
})
export class ClaimsListComponent implements OnInit {
  claims?:Claim[];
  currentClaim?:Claim;
  currentIndex = -1;
  keysearch='';
 
  constructor(private claimService:ClaimService) { }

  ngOnInit(): void {
    this.retrieveClaims();
  }
 
 
  retrieveClaims(): void{ 
    this.claimService.getAll().subscribe( 
      data => { 
        console.log("retrive claims called");
        this.claims= data;
        console.log(data);
      },
      error=> {
        console.log(error);
      });  
  }
 
  refreshList(): void {
    this.retrieveClaims();
    this.currentClaim = undefined;
    this.currentIndex = -1;
  }

  setActiveClaim(claim: Claim, index: number): void {
    this.currentClaim = claim;
    this.currentIndex = index;
  }

  searchClaims(): void{
    this.claimService.search(this.keysearch)
      .subscribe(data=> {
        this.claims= data;
        console.log(data);
      },
      error=> {
        console.log(error);
      });
  }

}
