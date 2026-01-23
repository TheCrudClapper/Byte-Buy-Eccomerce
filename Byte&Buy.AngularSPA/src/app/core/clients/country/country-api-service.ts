import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { SelectListItem } from '../../../shared/models/select-list-item';
import { SelectListItemResponse } from '../../dto/common/select-list-item-response';
import { API_ENDPOINTS } from '../../constants/api-constants';
import { environment } from '../../../../environments/environment';

@Injectable({
  providedIn: 'root',
})

export class CountryApiService {
  private readonly httpClient = inject(HttpClient);
  private readonly baseUrl = environment.apiBaseUrl;

  getSelectList(): Observable<SelectListItem[]> {
    return this.httpClient.get<SelectListItemResponse[]>(`${this.baseUrl}${API_ENDPOINTS.countries.options}`).pipe(
      map(response => response.map(item => ({
        id: item.id,
        title: item.title
      })))
    );
  }
}
