import { Component, OnChanges, OnInit } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import { IMunicipality } from 'src/app/interfaces/municipality';
import { ITown } from 'src/app/interfaces/town';
import { ISchool } from 'src/app/interfaces/school';
import { MunicipalityService, TownService, SchoolService } from 'src/app/services';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})

export class RegisterComponent implements OnInit, OnChanges {
  municipalities?: Array<IMunicipality>;
  towns?: Array<ITown>;
  schools?: Array<ISchool>;
  registerForm: FormGroup;

  municipalityId: number = 301;
  townId: number = 1274;
  
  constructor(
    private http : HttpClient,
    private municipalityService: MunicipalityService,
    private townService: TownService,
    private schoolService: SchoolService,
    fb: FormBuilder
    ) { 
      this.registerForm = fb.group({
        fullName: ['', [Validators.required]],
        email: ['', [Validators.required, Validators.email]],
      })
    }

  ngOnInit(): void {  
    this.municipalityService.getAllMunicipalities().subscribe(m => this.municipalities = m);
    this.townService.getTownsByMunicipalityId(this.municipalityId).subscribe(t => this.towns = t);
    this.schoolService.getSchoolsByTownId(this.townId).subscribe(s => this.schools = s); 
  }

  ngOnChanges(): void{
    console.log();
    
  }

  submitHandler(): void {
    console.log();
    
  }

}
