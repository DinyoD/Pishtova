import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RankingScreenComponent } from './ranking-screen.component';

describe('RankingScreenComponent', () => {
  let component: RankingScreenComponent;
  let fixture: ComponentFixture<RankingScreenComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RankingScreenComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RankingScreenComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
