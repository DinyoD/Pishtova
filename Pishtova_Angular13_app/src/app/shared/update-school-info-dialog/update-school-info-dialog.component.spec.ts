import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdateSchoolInfoDialogComponent } from './update-school-info-dialog.component';

describe('UpdateSchoolInfoDialogComponent', () => {
  let component: UpdateSchoolInfoDialogComponent;
  let fixture: ComponentFixture<UpdateSchoolInfoDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UpdateSchoolInfoDialogComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UpdateSchoolInfoDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
