import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

interface Restaurant {
  id: number;
  name: string;
  Description: string;
  Category: string;
  HasDelivery: boolean,
  City: string,
  Street: string,
  PostalCode: string,
  dishes: DishDto[],
  // Dodaj inne pola, jeśli są w API
}

interface DishDto {
  id: number;
  name: string;
  description: string;
  price: number;
}

interface PagedResult<T> {
  items: T[];
  totalPages: number;
  itemFrom: number;
  itemTo: number;
  totalItemsCount: number;
}

@Injectable({
  providedIn: 'root'
})
export class RestaurantService {
  private apiUrl = 'https://localhost:7055/api/restaurant';

  constructor(private http: HttpClient) {}

  getRestaurants(
    searchPhrase: string = '',
    pageNumber: number = 1,
    pageSize: number = 10,
    sortBy: string = 'Name',
    sortDirection: string = 'DESC'
  ): Observable<PagedResult<Restaurant>> {
    let params = new HttpParams()
      .set('searchPhrase', searchPhrase)
      .set('PageNumber', pageNumber)
      .set('PageSize', pageSize)
      .set('SortBy', sortBy)
      .set('SortDirection', sortDirection);

    var result = this.http.get<PagedResult<Restaurant>>(this.apiUrl, { params });
    return result;
  }
}
