import { HttpClient, HttpEventType, HttpResponse } from '@angular/common/http';
import { Component, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-download',
  templateUrl: './download.component.html',
})
export class DownloadComponent {
  public message: string;
  public progress: number;
  @Output() public onUploadFinished = new EventEmitter();

  constructor(private http: HttpClient) {
    this.message = '';
    this.progress = 0;
  }

  download = () => {
    this.http
      .get('https://335d-95-210-55-194.eu.ngrok.io/Number/download', {
        reportProgress: true,
        observe: 'events',
        responseType: 'blob',
      })
      .subscribe((event) => {
        if (event.type === HttpEventType.UploadProgress) {
          const total = event.total || 0;
          this.progress = Math.round((100 * event.loaded) / total);
        } else if (event.type === HttpEventType.Response) {
          this.message = 'Download success.';
          this.downloadFile(event);
        }
      });
  };

  private downloadFile = (data: HttpResponse<Blob>) => {
    if (data != null && data.body !== null) {
      const downloadedFile = new Blob([data.body], {
        type: data.body?.type,
      });
      const a = document.createElement('a');
      a.setAttribute('style', 'display:none;');
      document.body.appendChild(a);
      a.download = 'Sample.csv';
      a.href = URL.createObjectURL(downloadedFile);
      a.target = '_blank';
      a.click();
      document.body.removeChild(a);
    }
  };
}
