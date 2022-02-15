import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { MunicipalityModel } from 'src/app/models/municipality';
import { SchoolForRegisterModel } from 'src/app/models/school/schoolForRegister';
import { SchoolBaseModel } from 'src/app/models/school/schoolBase';
import { TownModel } from 'src/app/models/town';
import { MunicipalityService, SchoolService, TownService } from 'src/app/services';

@Component({
  selector: 'app-update-school-info-dialog',
  templateUrl: './update-school-info-dialog.component.html',
  styleUrls: ['../update-profile-info-dialog/update-profile-info-dialog.component.css','../upload-file-dialog/upload-file-dialog.component.css']
})
export class UpdateSchoolInfoDialogComponent implements OnInit {
  
  public municipalities?: Array<MunicipalityModel>;
  public towns?: Array<TownModel>;
  public schools?: Array<SchoolForRegisterModel> | null;
  

  public form: FormGroup = new FormGroup({
    municipality: new FormControl(null, [Validators.required]),
    town: new FormControl(null, [Validators.required]),
    school: new FormControl(null, [Validators.required]),
  });


  constructor(
    public dialogRef: MatDialogRef<UpdateSchoolInfoDialogComponent>,
    private municipalityService: MunicipalityService, 
    private townService: TownService, 
    private schoolService: SchoolService,
    private cdr: ChangeDetectorRef) { }

    
  ngOnInit(): void {   
    this.municipalityService.getAllMunicipalities().subscribe(m => {this.municipalities = m.sort((a,b) => this.sortName(a.name, b.name))});
    this.cdr.detectChanges();
  }
    
  getTowns(id: number){
      this.townService.getTownsByMunicipalityId(id).subscribe(t => {this.towns = t.sort((a,b) => this.sortName(a.name, b.name))});
      this.schools= null;
  }

  getSchools(id: number){
    this.schoolService.getSchoolsByTownId(id).subscribe(sch => {this.schools = sch.sort((a,b) => this.sortName(a.name, b.name))});
  }
  
  public cancel  = (): void => {
    this.dialogRef.close(false);
  }

  public confirm = (): void => {
    this.dialogRef.close(this.school.value as SchoolBaseModel);
  }

  get school(): FormControl {
    return this.form.get('school') as FormControl;
  }
  get municipality(): FormControl {
    return this.form.get('municipality') as FormControl;
  }
  get town(): FormControl {
    return this.form.get('town') as FormControl;
  }
  sortName(x: string, y: string): number{
    return x > y ? 1 : -1;
  }
}
