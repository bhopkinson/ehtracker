import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AgmCoreModule } from '@agm/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { PropertiesComponent } from './components/properties/properties.component';
import { PropertyDetailComponent } from './components/property-detail/property-detail.component';

@NgModule({
  declarations: [
    AppComponent,
    PropertiesComponent,
    PropertyDetailComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    AgmCoreModule.forRoot({
      apiKey: 'AIzaSyCLQ_puB6P9wzoeAB1lnQ8NirBX2hzidP8'
    })
  ],
  providers: [Location],
  bootstrap: [AppComponent]
})
export class AppModule { }
