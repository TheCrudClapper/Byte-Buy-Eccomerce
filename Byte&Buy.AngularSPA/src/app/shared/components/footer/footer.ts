import { Component, OnInit } from '@angular/core';
import { CompanyInfoApiService } from '../../../core/services/company-info/company-info-api-service';
import { CompanyInfoResponse } from '../../../core/api-dto/company-info-response';

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
      postalCity: "PLACEHOLDER",
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
