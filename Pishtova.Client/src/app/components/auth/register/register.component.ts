import { Component, OnChanges, OnInit } from '@angular/core';
import { IMunicipality } from 'src/app/interfaces/municipality';
import { ITown } from 'src/app/interfaces/town';
import { ISchool } from 'src/app/interfaces/school';
import { MunicipalityService, TownService, SchoolService } from 'src/app/services';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})

export class RegisterComponent implements OnInit {
  municipalities?: Array<IMunicipality>;
  towns?: Array<ITown>;
  schools?: Array<ISchool>;

  form: FormGroup = new FormGroup({
    email: new FormControl(null, [Validators.required, Validators.email]),
    username: new FormControl(null, [Validators.required]),
    password: new FormControl(null, [Validators.required]),
    passwordConfirm: new FormControl(null, [Validators.required])
  },
    //{ validators: CustomValidators.passwordsMatching }
  );

  municipalityId: number = 301;
  townId: number = 1274;
  
  constructor(private municipalityService: MunicipalityService, private townService: TownService, private schoolService: SchoolService) {}

  ngOnInit(): void {  
    this.municipalityService.getAllMunicipalities().subscribe(m => this.municipalities = m);
    this.townService.getTownsByMunicipalityId(this.municipalityId).subscribe(t => this.towns = t);
    this.schoolService.getSchoolsByTownId(this.townId).subscribe(s => this.schools = s); 
  }



  register() {
    // if (this.form.valid) {
    //   this.userService.create({
    //     email: this.email.value,
    //     password: this.password.value,
    //     username: this.username.value
    //   }).pipe(
    //     tap(() => this.router.navigate(['../login']))
    //   ).subscribe();
    // }
  }

  get municipality(): FormControl {
    return this.form.get('municipality') as FormControl;
  }

}
