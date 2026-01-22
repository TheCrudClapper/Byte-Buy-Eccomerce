import { Component, inject, OnInit, signal } from '@angular/core';
import { OfferApiService } from '../../../../core/clients/offers/common/offer-api-service';
import { UserPanelOfferUnion } from '../../../../core/dto/offers/common/user-panel-union';
import { ToastService } from '../../../../shared/services/snackbar/toast-service';
import { ProblemDetails } from '../../../../core/dto/problem-details';
import { DatePipe } from '@angular/common';
import { Guid } from 'guid-typescript';

@Component({
  selector: 'app-my-offers',
  imports: [DatePipe],
  templateUrl: './my-offers.html',
  styleUrl: './my-offers.scss',
})
export class MyOffers implements OnInit {
  private readonly offerApiService = inject(OfferApiService);
  private readonly toastService = inject(ToastService);

  userOffers = signal<UserPanelOfferUnion[]>([]);

  ngOnInit(): void {
    this.offerApiService.getUserOffers().subscribe({
      next: data => this.userOffers.set(data),
      error: (err: ProblemDetails) => {
        this.toastService.error(err?.detail ?? "Something went wrong while fetching your offers")
      }
    });
  }

  remove(id: Guid){
    if(confirm("You want to delete selected offer ?")){
      
    }
  }
}
