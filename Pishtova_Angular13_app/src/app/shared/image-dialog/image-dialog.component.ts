import { Component, Inject} from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-image-dialog',
  templateUrl: './image-dialog.component.html',
  styleUrls: ['./image-dialog.component.css']
})
export class ImageDialogComponent {

  public imageUrl: string|null = null

  constructor(public dialogRef: MatDialogRef<string>,
    @Inject(MAT_DIALOG_DATA) public data: string) {
      this.imageUrl = data
    }

    public close(): void {
      this.dialogRef.close();
    }
    
}


