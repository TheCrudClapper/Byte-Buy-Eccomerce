import { Component, effect, inject, OnInit, signal } from '@angular/core';
import { CompanyInfoApiService } from '../../../core/clients/company-info/company-info-api-service';
import { CompanyInfoResponse } from '../../../core/dto/company/company-info-response';


@Component({
  selector: 'app-footer',
  standalone: true,
  templateUrl: './footer.html',
  styleUrls: ['./footer.scss'],
  imports: []
})

export class Footer implements OnInit {
  private readonly companyInfoApiService = inject(CompanyInfoApiService);
  companyInfo = signal<CompanyInfoResponse | null>(null);
  isLoading = signal<boolean>(false);

  readonly githubLink = "https://github.com/TheCrudClapper";
  readonly instagramLink = "https://www.instagram.com/muchawojciech/";
  readonly discordLink = "#";

  ngOnInit() {
    this.isLoading.set(true);
    this.companyInfoApiService.getCompanyInfo().subscribe({
      next: (data) => {
        this.companyInfo.set(data);
        this.isLoading.set(false);
      },
      error: (err) => {
        console.error('Error while fetching data', err)
        this.isLoading.set(false);
      }
    });
  }
}
