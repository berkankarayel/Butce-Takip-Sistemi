import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'https://localhost:7040/api/Auth'; // ✅ Backend URL'ini buraya yaz

  constructor(private http: HttpClient) {}

  login(username: string, password: string): Observable<any> {
    return this.http.post(`${this.apiUrl}/login`, { username, password });
  }

  register(fullName: string, username: string, email: string, password: string): Observable<any> {
    return this.http.post(`${this.apiUrl}/register`, { fullName, username, email, password });
  }
}
