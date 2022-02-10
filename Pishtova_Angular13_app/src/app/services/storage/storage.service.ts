import { isPlatformBrowser } from '@angular/common';
import { Injectable, Provider, PLATFORM_ID } from '@angular/core';

export interface IStorageService{
  setItem<T>(key: string, item: T): T;
  getItem<T>(key: string): T|string|null;
  removeItem(key: string): boolean;
  clear(): void;
}

export class StorageService implements IStorageService{
  setItem<T>(key: string, item: T): T { return item; }
  getItem<T>(key: string): T|string|null { return null; }
  removeItem(key: string): boolean { return true; }
  clear(): void {}
}

export function storageFactory(platformId: string): any{
  if (isPlatformBrowser(platformId)) {
    return new BrowserStorage();
  }
}

export const storageServiceProvider: Provider = {
  provide: StorageService,
  useFactory: storageFactory,
  deps: [PLATFORM_ID]
}

@Injectable()
export class BrowserStorage{
  private localStorage = localStorage;

  public setItem<T>(key: string, item: T): T|null {
    if(this.localStorage.getItem('token') == null && key != 'token'){
      return null;
    }
    const itemAsString = typeof item === 'string' ? item : JSON.stringify(item);
    this.localStorage.setItem(key, itemAsString);
    return item;
  }

  public getItem<T>(key: string): T|string|null {
    let item: T|string|null = null;
    const tmp = this.localStorage.getItem(key) ? this.localStorage.getItem(key) : null;
    if (tmp) {
      try {
        item = JSON.parse(tmp);        
      } catch  {
        item = tmp;
      }
    } 
    return item;
  }

  public removeItem<T>(key: string): boolean {
    let result: boolean = false;
    let item:  T|string|null = this.getItem(key);
    if (item) {
      this.localStorage.removeItem(key);
      result = true;
    }
    return result;
  }

  public clear(): void {
    this.localStorage.clear();
  }
}

@Injectable()
export class MobileStorage{

}