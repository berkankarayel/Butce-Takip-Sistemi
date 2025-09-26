import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  username: string = '';
  password: string = '';
  errorMessage: string = '';

constructor(private authService: AuthService, private router: Router) { }


onLogin() {
  console.log("Login butonuna basıldı ✅");
  this.authService.login(this.username, this.password).subscribe({
    next: (response) => {
      console.log("Giriş başarılı ✅", response);

      // token’ı localStorage’a kaydet
      localStorage.setItem("token", response.token);

      // ✅ admin sayfasına yönlendir
      this.router.navigate(['/admin/users']);
    },
    error: (err) => {
      console.error("Login hatası ❌", err);
    }
  });
}
}
