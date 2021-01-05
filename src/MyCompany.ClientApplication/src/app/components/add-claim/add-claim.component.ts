import { Component, OnInit } from '@angular/core';
import {Claim} from 'src/app/models/claim.model';
import {ClaimService} from 'src/app/services/claim.service';
import {ActivatedRoute,Router} from '@angular/router';

@Component({
  selector: 'app-add-claim',
  templateUrl: './add-claim.component.html',
  styleUrls: ['./add-claim.component.css']
})
export class AddClaimComponent implements OnInit {
  claim: Claim ={
    user_id:0,
    date: new Date(),
    address: '',
    damaged_item: '',
    description: '', 
   // status: '',
    incidence: '',
 
  }
  submitted = false; 
  message ='';
   
  constructor(private claimService: ClaimService,private route:ActivatedRoute,
    private router:Router ) { }

  ngOnInit(): void { }
  
  saveClaim() : void{
    console.log("call save");
    const data ={ 
      user_id:0,
      status:1,
      date: this.claim.date,
      address: this.claim.address,
      damaged_item: this.claim.damaged_item,
      description: this.claim.description, 
      incidence: this.claim.incidence, 
    };
   
    if(!(this.claim.date && this.claim.address && this.claim.damaged_item && this.claim.incidence))
    {
      this.submitted = false;
      this.message = "Please check differents fields";
    }else{
      this.claimService.create(data)
        .subscribe(
        
          response => {
            console.log("log response");
          // console.log(response);
            this.submitted = true;
            this.message = "The claim has been saved successfully";
            this.router.navigate(['/claims']);
          },
          error => {
            console.log("call error");
            this.submitted = false;
            this.message ="The operation has failed";
          });
          console.log("fin call create");
    }
  }

  newClaim(): void {
    this.submitted = false;
    this.claim = {
    user_id:0,
    date: new Date(),
    address: '',
    damaged_item: '',
    description: '', 
    incidence: '',
  
    };
  }
}
