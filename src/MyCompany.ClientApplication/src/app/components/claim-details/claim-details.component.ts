import { Component, OnInit } from '@angular/core';
import {ClaimService} from 'src/app/services/claim.service';
import {ActivatedRoute,Router} from '@angular/router';
import {Claim} from 'src/app/models/claim.model';

@Component({
  selector: 'app-claim-details',
  templateUrl: './claim-details.component.html',
  styleUrls: ['./claim-details.component.css']
})
export class ClaimDetailsComponent implements OnInit {

  currentClaim:Claim = {
    id : 0,
    user_id : 0,
    date: new Date(),
    address: '',
    damaged_item: '',
    description: '',
    status: '',
    incidence: '' 
  };
  message= '';
  constructor(
    private claimService:ClaimService,
    private route:ActivatedRoute,
    private router:Router ) { }

  ngOnInit(): void {
    this.message= '';
    this.getClaim(this.route.snapshot.params.id);
  }
  getClaim(id: string): void {
    this.claimService.get(id)
      .subscribe(
        data => {
          this.currentClaim = data;
          console.log(data);
        },
        error => {
          console.log(error);
        });
  }

  // updatePublished(status: boolean): void {
  //   const data = {  
  //    // date: this.currentClaim.date,
  //     address: this.currentClaim.address,
  //    // damagedItem: this.currentClaim.damaged_item,
  //     description: this.currentClaim.description,
  //    // status: this.currentClaim.status,
  //    // incidence: this.currentClaim.incidence,
  //   };

  //   this.claimService.update(this.currentClaim.id, data)
  //     .subscribe(
  //       response => {
  //         this.currentClaim.address = data.address;
  //         console.log(response);
  //         this.message = response.message;
  //         this.router.navigate(['/claims']);
  //       },
  //       error => {
  //         console.log(error);
  //       });
  // }

  updateClaim(): void {
    console.log("update called");
    this.claimService.update(this.currentClaim.id, this.currentClaim)
      .subscribe(

        response => {
          console.log(response);
          this.message = response.message;
          this.router.navigate(['/claims']);
        },
        error => {
          console.log(error);
        });
  }

  deleteClaim(): void {
    this.claimService.delete(this.currentClaim.id)
      .subscribe(
        response => {
          console.log(response);
          this.router.navigate(['/claims']);
        },
        error => {
          console.log(error);
        });
  }
}
