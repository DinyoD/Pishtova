import { Component, Inject} from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ImageService } from 'src/app/services';

@Component({
  selector: 'app-upload-file-dialog',
  templateUrl: './upload-file-dialog.component.html',
  styleUrls: ['./upload-file-dialog.component.css']
})
export class UploadFileDialogComponent {
  public file: File|null = null;

   constructor(
     public dialogRef: MatDialogRef<UploadFileDialogComponent>,
     private imageService: ImageService) {}

  Choose(ev: Event): void {
    const element = ev.currentTarget as HTMLInputElement;
    this.file = element.files != null ? element.files[0] : null;
  }
  
  Upload(): void {
    console.log(this.file);
    if (this.file != null) {
      this.imageService.uploadImageAsync(this.file);
    }
    this.dialogRef.close(true);
  }

}