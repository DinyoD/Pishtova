import { Component } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { ImageService } from 'src/app/services';

@Component({
  selector: 'app-upload-file-dialog',
  templateUrl: './upload-file-dialog.component.html',
  styleUrls: ['../update-profile-info-dialog/update-profile-info-dialog.component.css']
})
export class UploadFileDialogComponent {
  
  public file: File|null = null;

   constructor(
     public dialogRef: MatDialogRef<UploadFileDialogComponent>,
     private imageService: ImageService) {}

  public Choose = (ev: Event): void => {
    const element = ev.currentTarget as HTMLInputElement;
    this.file = element.files != null ? element.files[0] : null;
  }
  
  public Upload = (): void => {
    if (this.file != null) {
      this.imageService.uploadImageAsync(this.file).then( success => this.dialogRef.close(success));
    }
  }

}
