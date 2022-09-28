import { Injectable } from '@angular/core';
import { BreadCrumbElement } from 'src/app/models/breadcrumb/breadcrumbElement';
import { StorageService } from '../storage/storage.service';

@Injectable({
  providedIn: 'root'
})
export class BreadCrumbService {

  constructor(private storageService: StorageService) { }

  public addItem(item: BreadCrumbElement): void {
    let breadCrumbValue = this.storageService.getItem<BreadCrumbElement[]>("breadcrumb");
    if (typeof breadCrumbValue !== "string" && breadCrumbValue) {
      const newValue = [...breadCrumbValue, item];
      this.storageService.setItem<BreadCrumbElement[]>("breadcrumb", newValue)
    }
    if (breadCrumbValue == null) {
      this.storageService.setItem<BreadCrumbElement[]>("breadcrumb", [item])
    }
  }

  public getItems(): BreadCrumbElement[]|null {
    let breadCrumbValue = this.storageService.getItem<BreadCrumbElement[]>("breadcrumb");
    return typeof breadCrumbValue === "string" ? null : breadCrumbValue;
  }

  public remove(): void {
    this.storageService.removeItem("breadcrumb");
  }
}
