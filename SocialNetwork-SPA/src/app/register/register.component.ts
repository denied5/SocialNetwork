import { Component, OnInit } from '@angular/core';
import { User } from '../_model/User';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { BsDatepickerConfig } from 'ngx-bootstrap';
import { AuthService } from '../Services/Auth.service';
import { AlertifyService } from '../Services/alertify.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  user: User;
  registerForm: FormGroup;
  bsConfig: Partial<BsDatepickerConfig>;

  constructor(private authService: AuthService, private fb: FormBuilder,
              private alertifyService: AlertifyService) { }

  register() {
    if (this.registerForm.valid) {
      this.user = Object.assign({}, this.registerForm.value);
      this.authService.register(this.user).subscribe(() => {
        this.alertifyService.success('register confirm');
        this.authService.login(this.user).subscribe();
      }, error => {
        this.alertifyService.error(error);
      });
    }
  }

  createRegisterForm() {
    this.registerForm = this.fb.group({
      gender: ['male', ],
      username: ['', Validators.required],
      knownAs: ['', Validators.required],
      dateOfBirth: [null, Validators.required],
      city: ['', Validators.required],
      country: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(8)]],
      confirmPassword: ['', Validators.required]
    }, { validator: this.passwordMatchValidator });
  }

  passwordMatchValidator(g: FormGroup) {
    return g.get('password').value == g.get('confirmPassword').value ? null : { mismatch: true };
  }

  ngOnInit() {
    this.bsConfig = {
      containerClass: 'theme-red'
    },
      this.createRegisterForm();
  }

}
