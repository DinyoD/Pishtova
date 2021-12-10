import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

import { CustomValidators } from '../../_helpers/custom-validators';
import { IMunicipality } from 'src/app/interfaces/municipality';
import { ITown } from 'src/app/interfaces/town';
import { ISchool } from 'src/app/interfaces/school';
import { MunicipalityService, TownService, SchoolService, UserService } from 'src/app/services';
import { UserForRegistration } from 'src/app/interfaces/userForRegistration';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})

export class RegisterComponent implements OnInit  {

  public errorMessage: string = '';
  public showError: boolean = false;
  
  public municipalities?: Array<IMunicipality>;
  public towns?: Array<ITown>;
  public schools?: Array<ISchool> | null;

  public form: FormGroup = new FormGroup({
    municipality: new FormControl(null, [Validators.required]),
    town: new FormControl(null, [Validators.required]),
    school: new FormControl(null, [Validators.required]),
    email: new FormControl(null, [Validators.required, Validators.email]),
    grade: new FormControl(null, [Validators.required]),
    username: new FormControl(null, [Validators.required]),
    password: new FormControl(null, [Validators.required]),
    passwordConfirm: new FormControl(null, [Validators.required]),
  },
    { validators: CustomValidators.passwordsMatching }
  );
  constructor(
    private municipalityService: MunicipalityService, 
    private townService: TownService, 
    private schoolService: SchoolService,
    private userService: UserService,
    private route: Router
    ) {}
    
  ngOnInit(): void {   
      this.municipalityService.getAllMunicipalities().subscribe(m => {this.municipalities = m.sort((a,b) => this.sortName(a.name, b.name))});
    }
    
  getTowns(id: number){
      this.townService.getTownsByMunicipalityId(id).subscribe(t => {this.towns = t.sort((a,b) => this.sortName(a.name, b.name))});
      this.schools= null;
  }

  getSchools(id: number){
    this.schoolService.getSchoolsByTownId(id).subscribe(sch => {this.schools = sch.sort((a,b) => this.sortName(a.name, b.name))});
  }

  register = () => {
    if (this.form.valid) {
      const forValues = {...this.form.value}
      const user: UserForRegistration = {
        username: forValues.username,
        email: forValues.email,
        password: forValues.password,
        confirmPassword: forValues.confirmPassword,
        grade: forValues.grade,
        schoolId: forValues.schoolId,
        clientURI: `http://localhost:4200/emailconfirmation`,
      }
      this.userService.createUser(user).subscribe(()=>{
        this.route.navigate(['/login']);
      },
      error => {
        
      })
    }
  }

  get email(): FormControl {
    return this.form.get('email') as FormControl;
  }
  get username(): FormControl {
    return this.form.get('username') as FormControl;
  }
  get password(): FormControl {
    return this.form.get('password') as FormControl;
  }
  get passwordConfirm(): FormControl {
    return this.form.get('passwordConfirm') as FormControl;
  }
  get municipality(): FormControl {
    return this.form.get('municipality') as FormControl;
  }
  get town(): FormControl {
    return this.form.get('town') as FormControl;
  }
  get school(): FormControl {
    return this.form.get('school') as FormControl;
  }

  get grade(): FormControl {
    return this.form.get('grade') as FormControl;
  }

  sortName(x: string, y: string): number{
    return x > y ? 1 : -1;
  }

}
