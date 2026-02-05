import { Component, inject, signal } from '@angular/core';
import { RentalApiService } from '../../../../core/clients/rental/rental-api-service';
import { environment } from '../../../../../environments/environment';
import { ToastService } from '../../../../shared/services/snackbar/toast-service';
import { UserRentalBorrowerResponse } from '../../../../core/dto/rental/user-rental-borrower-response';
import { RentalStatus } from '../../../../core/dto/rental/enum/rental-status';
import { DatePipe, DecimalPipe } from '@angular/common';
import { Guid } from 'guid-typescript';

@Component({
  selector: 'app-my-rentals',
  imports: [DecimalPipe, DatePipe],
  standalone: true,
  templateUrl: './my-rentals.html',
  styleUrl: './my-rentals.scss',
})
export class MyRentals {
  private readonly rentalApiSerivce = inject(RentalApiService);
  protected readonly imageBaseUrl = environment.staticImagesBaseUrl;
  private readonly toastService = inject(ToastService);

  rentalsList = signal<UserRentalBorrowerResponse[] | undefined>(undefined);
  readonly RentalStatus = RentalStatus;

  ngOnInit(): void {
    this.rentalApiSerivce.getBorrowerRentals().subscribe({
      next: data => {
        this.rentalsList.set(data);
      },
      error: (err) => this.toastService.error(err?.detail ?? "Failed to fetch your rentals")
    });
  }

  returnRental(id: Guid) {
    this.rentalApiSerivce.returnRental(id).subscribe({
      next: () => {
        this.rentalsList.update(list => {
          if (!list) return list;

          return list.map(rental =>
            rental.id === id
              ? {...rental, status: RentalStatus.Completed }
              : rental
          );
        });
        this.toastService.success("Successfully returned item")
      },
      error: (err) => this.toastService.error(err?.detail ?? "Failed to return item back to lender")
    });
  }
}
