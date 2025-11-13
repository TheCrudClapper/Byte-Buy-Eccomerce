import { Component, OnInit } from '@angular/core';
import { CompanyInfoApiService } from '../../services/companyInfo/company-info-api-service';
import { CompanyInfoResponse } from '../../models/companyInfo/CompanyInfoResponse';

@Component({
  selector: 'app-footer',
  standalone: true,
  templateUrl: './footer.html',
  styleUrls: ['./footer.scss'],
  imports: [] 
})

export class Footer implements OnInit{
  
  companyInfo :CompanyInfoResponse | null  = {
    companyName: "PLACEHOLDER",
    slogan: "PLACEHOLDER",
    email: "PLACEHOLDER",
    phone: "PLACEHOLDER",
    TIN: "PLACEHOLDER",
    address: {
      city: "PLACEHOLDER",
      flatNumber: "PLACEHOLDER",
      country: "PLACEHOLDER",
      houseNumber: "PLACEHOLDER",
      street: "PLACEHOLDER",
      postalCode: "PLACEHOLDER"
    }
  }

  constructor(private companyInfoService: CompanyInfoApiService) {}
  ngOnInit(){
    this.companyInfoService.getCompanyInfo().subscribe({
        next: (data) => {
          this.companyInfo = data;
        },
        error: (err) => console.error('Error while fetching data', err)
    });
  }
  
}
