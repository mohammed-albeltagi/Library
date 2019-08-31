import { ToastrService } from 'ngx-toastr';
import { UserService } from './../services/user.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styles: []
})
export class HomeComponent implements OnInit {
  categories: any;
  formModel = {
    name: '',
    parentCategoryId: null
   };
  constructor(private router: Router, private service: UserService, private toastr: ToastrService) { }

  ngOnInit() {
    this.service.getCategory().subscribe(
      res => {
        this.categories = res;
      },
      err => {
        console.log(err);
      },
    );
  }

  onSubmit(form: NgForm) {
    this.service.postCategory(form.value).subscribe(
      res => {
        this.ngOnInit();
        this.formModel.name = '';
        this.formModel.parentCategoryId = null;
      },
      err => {
        if (err.status === 400) {
          this.toastr.error('Incorrect username or password.', 'Authentication failed.');
        } else {
          console.log(err);
        }
      }
    );
  }

  onLogout() {
    localStorage.removeItem('token');
    this.router.navigate(['/user']);
  }
}
