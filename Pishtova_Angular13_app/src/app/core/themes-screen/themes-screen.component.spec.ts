import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ThemesScreenComponent } from './themes-screen.component';

describe('ThemesScreenComponent', () => {
  let component: ThemesScreenComponent;
  let fixture: ComponentFixture<ThemesScreenComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ThemesScreenComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ThemesScreenComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
