import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/core/services/auth.service';
import { LoginRequest } from 'src/app/core/models/auth/login-request';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  // reactive form
  form = this.fb.group({
    userName: ['', Validators.required],
    password: ['', Validators.required]
  });

  loading = false;
  error = '';

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {}

  submit() {
    if (this.form.invalid) return;

    const dto: LoginRequest = this.form.value as LoginRequest;

    this.loading = true;
    this.error = '';

    this.authService.login(dto).subscribe({
      next: (res) => {
        this.loading = false;
        console.log('Gelen token:', res.token);
        this.router.navigate(['/admin']); // login başarılıysa dashboarda yönlendir
      },
      error: (err) => {
        this.loading = false;
        this.error = 'Giriş başarısız. Bilgileri kontrol edin.';
        console.error(err);
      }
    });
  }
}

