import { Component, inject, OnInit, signal } from '@angular/core';
import { RentalApiService } from '../../../../core/clients/rental/rental-api-service';
import { UserRentalLenderResponse } from '../../../../core/dto/rental/user-rental-lender-response';
import { environment } from '../../../../../environments/environment';
import { ToastService } from '../../../../shared/services/snackbar/toast-service';
import { DatePipe, DecimalPipe } from '@angular/common';
import { RentalStatus } from '../../../../core/dto/rental/enum/rental-status';

@Component({
  selector: 'app-client-rentals',
  imports: [DecimalPipe, DatePipe],
  standalone: true,
  templateUrl: './client-rentals.html',
  styleUrl: './client-rentals.scss',
})

export class ClientRentals implements OnInit {
  private readonly rentalApiSerivce = inject(RentalApiService);
  protected readonly imageBaseUrl = environment.staticImagesBaseUrl;
  private readonly toastService = inject(ToastService);

  rentalsList = signal<UserRentalLenderResponse[] | undefined>(undefined);
  readonly RentalStatus = RentalStatus;
  
  ngOnInit(): void {
    this.rentalApiSerivce.getLenderRentals().subscribe({
      next: data => {
        this.rentalsList.set(data) 
        console.log(data);
      },
      error: (err) => this.toastService.error(err?.detail ?? "Failed to fetch your rented offers")
    });
  }

}
