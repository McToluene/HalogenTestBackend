import { HttpClient } from '@angular/common/http';
import { Component, EventEmitter, Output } from '@angular/core';
import { IResult } from '../type/result.type';

@Component({
  selector: 'app-generate',
  templateUrl: './generate.component.html',
  styleUrls: ['./generate.component.css'],
})
export class GenerateComponent {
  constructor(private http: HttpClient) {}
  @Output() childToParent = new EventEmitter<IResult[]>();

  public generateResult = (event: Event) => {
    this.http
      .get<IResult[]>('https://3556-95-210-55-194.eu.ngrok.io/Number/process')
      .subscribe((event) => {
        this.childToParent.emit(event);
      });
  };
}
