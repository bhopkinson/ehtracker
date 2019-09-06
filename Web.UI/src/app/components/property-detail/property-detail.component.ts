import { Component, OnInit } from '@angular/core';
import { Property } from 'src/app/models/property';
import { ActivatedRoute } from '@angular/router';
import { PropertyService } from 'src/app/services/property.service';
import { Location } from '@angular/common';
import { GeolocationService } from 'src/app/services/geolocation.service';

@Component({
  selector: 'app-property-detail',
  templateUrl: './property-detail.component.html',
  styleUrls: ['./property-detail.component.css']
})
export class PropertyDetailComponent implements OnInit {

  property: Property;

  latitude: number;
  longitude: number;

  screenWidth: number;
  constructor(
    private route: ActivatedRoute,
    private propertyService: PropertyService,
    private geolocationService: GeolocationService,
    private location: Location
  ) { }
  ngOnInit() {
    this.getProperty();
    this.getCurrentPosition();
  }

  getProperty(): void {
    const id = +this.route.snapshot.paramMap.get('id');
    this.propertyService.getProperty(id)
      .subscribe(property => this.property = property);
  }

  getCurrentPosition(): void {
    this.geolocationService.getCurrentPosition()
      .subscribe(position => {
        this.latitude = position.coords.latitude;
        this.longitude = position.coords.longitude;
      });
  }

  getHeroImageUrl(): string{
    return `${this.property.imageUrl}?w=${window.innerWidth}&mode=crop&scale=both&quality=100`;
  }
}
