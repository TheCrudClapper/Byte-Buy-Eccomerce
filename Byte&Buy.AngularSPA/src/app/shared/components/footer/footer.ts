import { Component, effect, inject, OnInit, signal } from '@angular/core';
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
  private readonly companyInfoApiService = inject(CompanyInfoApiService);
  companyInfo = signal<CompanyInfoResponse | null>(null);

  ngOnInit(){
    this.companyInfoApiService.getCompanyInfo().subscribe({
        next: (data) => this.companyInfo.set(data),
        error: (err) => console.error('Error while fetching data', err)
    });
  }
}
