import { Injectable } from '@angular/core';
import { PROPERTIES } from '../mock-properties';
import { Property } from '../models/property';
import { Observable, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PropertyService {

  constructor() { }

  getProperties(): Observable<Property[]> {
    return of(PROPERTIES);
  }

  getProperty(id: number): Observable<Property> {
    return of(PROPERTIES.find(property => property.id === id));
  }
}
