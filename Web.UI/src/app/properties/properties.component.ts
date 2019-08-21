import { Component, OnInit } from '@angular/core';
import { Property } from '../models/property';

@Component({
  selector: 'app-properties',
  templateUrl: './properties.component.html',
  styleUrls: ['./properties.component.css']
})
export class PropertiesComponent implements OnInit {

  properties: Property[];

  constructor() { }

  ngOnInit() {
  }

}
