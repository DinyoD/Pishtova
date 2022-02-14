import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdateProfileInfoDialogComponent } from './update-profile-info-dialog.component';

describe('UpdateProfileInfoDialogComponent', () => {
  let component: UpdateProfileInfoDialogComponent;
  let fixture: ComponentFixture<UpdateProfileInfoDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UpdateProfileInfoDialogComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UpdateProfileInfoDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
