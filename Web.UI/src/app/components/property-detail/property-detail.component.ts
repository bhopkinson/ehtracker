import { Component, OnInit, Input } from '@angular/core';
import { Property } from 'src/app/models/property';
import { ActivatedRoute } from '@angular/router';
import { PropertyService } from 'src/app/services/property.service';

@Component({
  selector: 'app-property-detail',
  templateUrl: './property-detail.component.html',
  styleUrls: ['./property-detail.component.css']
})
export class PropertyDetailComponent implements OnInit {

  private property: Property;

  constructor(
    private route: ActivatedRoute,
    private propertyService: PropertyService,
    private location: Location
  ) { }

  ngOnInit() {
    this.getProperty();
  }

  getProperty(): void {
    const id = +this.route.snapshot.paramMap.get('id');
    this.propertyService.getProperty(id)
      .subscribe(property => this.property = property);
  }
}
