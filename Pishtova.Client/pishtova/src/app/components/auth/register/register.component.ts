import { Component, OnInit } from '@angular/core';
import {HttpClient} from '@angular/common/http';

import { Minicipality } from '../../../models/municipality';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  minicipalities: Array<Minicipality>
  constructor(private http : HttpClient) { }

  ngOnInit(): void {  
    this.fetchMunicipalities()
  }

  fetchMunicipalities(){
    return this.http.get<Array<Minicipality>>('https://localhost:44329/api/municipalities/all')
    .subscribe(m => this.minicipalities = m);
  }

}
