import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { environment as env } from 'src/environments/environment';
import { UserService } from '..';


 interface IImageData{
  data: { link: string};
  success: boolean;
 }

@Injectable({
  providedIn: 'root'
})
export class ImageService {

  private url: string;
  private clientId: string;
 
 
  constructor(
    private http: HttpClient,
    private userService: UserService) { 
    this.clientId = env.Imgur_Client_ID;
    this.url = env.Imgur_Url;
  }
  
  async uploadImageAsync(imageFile: File): Promise<boolean> {
    try {
      let formData = new FormData();
      formData.append('image', imageFile, imageFile.name);
   
      let header = new HttpHeaders({
        "authorization": 'Client-ID '+this.clientId
      });
     
      const imageData: IImageData = await this.http.post(this.url, formData, {headers:header}).toPromise() as IImageData;
      if (imageData.success) {
         this.userService.SetUserPictureUrl(imageData.data.link).subscribe(
           result => console.log(result),
           err => console.log(err)
         );
      };
      return true;
    } catch{
      return false;
    }
    
  }

}
