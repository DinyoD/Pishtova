import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MaterialsScreenComponent } from './materials-screen.component';

describe('MaterialsScreenComponent', () => {
  let component: MaterialsScreenComponent;
  let fixture: ComponentFixture<MaterialsScreenComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MaterialsScreenComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MaterialsScreenComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
