import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { environment as env } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ImageService {

  private url: string;
  private clientId: string;
  private imageLink: string|null = null;
 
 
  constructor(private http: HttpClient) { 
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
     
      const imageData = await this.http.post(this.url, formData, {headers:header}).toPromise();
      console.log(imageData);
      return true;
      
    } catch{
      return false;
    }
    
    //this.imageLink = imageData['data'].link;
  }

}
