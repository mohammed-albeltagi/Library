import { Injectable } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private fb: FormBuilder, private http: HttpClient) { }
  readonly BaseURI = 'http://localhost:53745/api';

  login(formData) {
    return this.http.post(this.BaseURI + '/ApplicationUser/Login', formData);
  }
  register(formData) {
    return this.http.post(this.BaseURI + '/ApplicationUser/Register', formData);
  }
  postCategory(formData) {
    return this.http.post(this.BaseURI + '/Category' , formData);
  }
  getCategory() {
    return this.http.get(this.BaseURI + '/Category');
  }
}
