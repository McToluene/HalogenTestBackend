import { Component } from '@angular/core';
import { IResult } from './type/result.type';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  title = 'Client';
  public results: IResult[] = [];

  onResult = (results: IResult[]) => {
    this.results = results;
  };
}
