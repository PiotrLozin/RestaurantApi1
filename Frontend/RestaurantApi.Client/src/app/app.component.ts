import { Component } from '@angular/core';
import { RestaurantService } from './features/restaurant.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'RestaurantApi.Client';
  restaurants: any[] = [];

  constructor (private restaurantService: RestaurantService) {}

  ngOnInit() {
    this.restaurantService.getRestaurants('aut').subscribe(
      (data) => {
        console.log('Pobrane restauracje:', data);
        this.restaurants = data.items;
      },
      (error) => {
        console.error('Błąd podczas pobierania restauracji', error);
      }
    );
  }
}
