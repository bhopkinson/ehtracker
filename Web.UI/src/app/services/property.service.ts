import { Injectable } from '@angular/core';
import { PROPERTIES } from '../mock-properties';
import { Property } from '../models/property';

@Injectable({
  providedIn: 'root'
})
export class PropertyService {

  constructor() { }

  getProperties(): Property[] {
    return PROPERTIES;
  }
}
