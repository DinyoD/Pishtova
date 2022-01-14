import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SubjectScreenComponent } from './subject-screen.component';

describe('SubjectScreenComponent', () => {
  let component: SubjectScreenComponent;
  let fixture: ComponentFixture<SubjectScreenComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SubjectScreenComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SubjectScreenComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
