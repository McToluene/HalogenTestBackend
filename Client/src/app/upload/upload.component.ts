import { Component, EventEmitter, Output } from '@angular/core';
import { HttpClient, HttpEventType } from '@angular/common/http';

@Component({
  selector: 'app-upload',
  templateUrl: './upload.component.html',
})
export class UploadComponent {
  public message: string;
  public progress: number;
  @Output() public onUploadFinished = new EventEmitter();

  constructor(private http: HttpClient) {
    this.message = '';
    this.progress = 0;
  }

  public uploadFile = (event: Event) => {
    const element = event.currentTarget as HTMLInputElement;
    const fileList: FileList | null = element.files;
    if (fileList === null) return;
    let fileToUpload = fileList[0];
    const formData = new FormData();
    formData.append('file', fileToUpload, fileToUpload.name);
    this.makeUploadRequest(formData);
    element.files = null;
  };

  private makeUploadRequest = (formData: FormData) => {
    this.http
      .post('https://335d-95-210-55-194.eu.ngrok.io/Number/upload', formData, {
        reportProgress: true,
        observe: 'events',
      })
      .subscribe((event) => {
        if (event.type === HttpEventType.UploadProgress) {
          const total = event.total || 0;
          this.progress = Math.round((100 * event.loaded) / total);
        } else if (event.type === HttpEventType.Response) {
          this.message = 'Upload successfully';
          this.onUploadFinished.emit(<string>event.body);
        }
      });
  };
}
