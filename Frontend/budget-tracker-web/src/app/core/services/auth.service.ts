import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { LoginRequest } from '../models/auth/login-request';
import { AuthResponse } from '../models/auth/auth-response';
import { Observable, tap } from 'rxjs';

// bu servisi angulara tanıtıyoruz
// providedIn: 'root' ile uygulama genelinde tek bir instance kullanılır
// yani biz autservice'i nerede inject edersek edelim aynı instance'ı kullanırız
@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  // Login
  login(dto: LoginRequest): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(`${this.apiUrl}/Auth/login`, dto).pipe(
      tap(res => {
        // gelen token’ı localStorage’a kaydediyoruz
        localStorage.setItem('token', res.token);
      })
    );
  }

  // Token getter
  get token(): string | null {
    return localStorage.getItem('token');
  }

  // Logout
  logout() {
    localStorage.removeItem('token');
  }
}
