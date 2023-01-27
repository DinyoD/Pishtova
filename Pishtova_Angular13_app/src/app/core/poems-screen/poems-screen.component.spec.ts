import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PoemsScreenComponent } from './poems-screen.component';

describe('PoemsScreenComponent', () => {
  let component: PoemsScreenComponent;
  let fixture: ComponentFixture<PoemsScreenComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PoemsScreenComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PoemsScreenComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
