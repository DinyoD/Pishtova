import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

import { environment as env} from 'src/environments/environment';
import { CustomValidators } from '../../helpers/custom-validators';
import { MunicipalityModel } from 'src/app/models/municipality';
import { TownModel } from 'src/app/models/town';
import { SchoolForRegisterModel } from 'src/app/models/school/schoolForRegister';
import { MunicipalityService, TownService, SchoolService, AuthService } from 'src/app/services';
import { UserForRegistrationModel } from '../../models/userForRegistration';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})

export class RegisterComponent implements OnInit  {

  public errorMessage: string = '';
  public showError: boolean = false;
  
  public municipalities?: Array<MunicipalityModel>;
  public towns?: Array<TownModel>;
  public schools?: Array<SchoolForRegisterModel> | null;

  public form: FormGroup = new FormGroup({
    municipality: new FormControl(null, [Validators.required]),
    town: new FormControl(null, [Validators.required]),
    school: new FormControl(null, [Validators.required]),
    email: new FormControl(null, [Validators.required, Validators.email]),
    grade: new FormControl(null, [Validators.required]),
    name: new FormControl(null, [Validators.required]),
    password: new FormControl(null, [Validators.required]),
    confirmPassword: new FormControl(null, [Validators.required]),
  },
    { validators: [
      CustomValidators.passwordsMatching, 
      CustomValidators.passwordMatchingRegEx, 
      CustomValidators.nameMatchingRegEx,
      CustomValidators.gradeMatching
    ] }
  );
  constructor(
    private municipalityService: MunicipalityService, 
    private townService: TownService, 
    private schoolService: SchoolService,
    private userService: AuthService,
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
  

  // TODO display error message on BG depends on responce status code!!!!
  register = () => {
    if (this.form.valid) {
      const formValues = {...this.form.value}
      const user: UserForRegistrationModel = {
        name: formValues.name,
        email: formValues.email,
        password: formValues.password,
        confirmPassword: formValues.confirmPassword,
        grade: formValues.grade,
        schoolId: formValues.school,
        clientURI: env.CLIENT_URI + `/auth/emailconfirmation`,
      }
      console.log(user);
      
      this.userService.register(user)
      .subscribe(()=>{
        this.route.navigate(['/'])
      }, err => {
        this.showError = true;
        this.errorMessage = err.error.message ?  err.error.message : 'The form is not fullfiled correctly!';
      });
    }
  }

  get email(): FormControl {
    return this.form.get('email') as FormControl;
  }
  get name(): FormControl {
    return this.form.get('name') as FormControl;
  }
  get password(): FormControl {
    return this.form.get('password') as FormControl;
  }
  get confirmPassword(): FormControl {
    return this.form.get('confirmPassword') as FormControl;
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
