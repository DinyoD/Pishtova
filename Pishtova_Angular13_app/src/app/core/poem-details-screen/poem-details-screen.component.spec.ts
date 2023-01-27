import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PoemDetailsScreenComponent } from './poem-details-screen.component';

describe('PoemDetailsScreenComponent', () => {
  let component: PoemDetailsScreenComponent;
  let fixture: ComponentFixture<PoemDetailsScreenComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PoemDetailsScreenComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PoemDetailsScreenComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
