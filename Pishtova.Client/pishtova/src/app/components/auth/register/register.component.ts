import { Component, OnInit } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import { FormBuilder, Validators } from '@angular/forms';

import { Municipality } from '../../../models/municipality';
import { Town } from '../../../models/town';
import { School } from '../../../models/school';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})

export class RegisterComponent implements OnInit {
  municipalities?: Array<Municipality>;
  towns?: Array<Town>;
  schools?: Array<School>;

  municipalityId: number = 301;
  townId: number = 1274;
  
  constructor(private http : HttpClient) { }
  
  // registartionForm = this.fb.group({
  //   schoolId: [0, [Validators.required]],
  // });

  ngOnInit(): void {  
    this.fetchMunicipalities();
    this.fetchTowns(this.municipalityId);
    this.fetchSchools(this.townId);
  }

  fetchMunicipalities(){
    return this.http.get<Array<Municipality>>('https://localhost:44329/api/municipalities/all')
    .subscribe(municipalities => {this.municipalities = municipalities});
  }
  fetchTowns(municipalityId: number){
    return this.http.get<Array<Town>>(`https://localhost:44329/api/towns/bymunicipality/${municipalityId}`)
    .subscribe(town => {this.towns = town});
  }
  fetchSchools(townId: number){
    return this.http.get<Array<School>>(`https://localhost:44329/api/schools/bytown/${townId}`)
    .subscribe(schol => {this.schools = schol});
  }

}
