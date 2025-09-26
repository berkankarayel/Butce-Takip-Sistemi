import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../models/user.model';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private apiUrl = 'https://localhost:7040/api/Users';

  constructor(private http: HttpClient) {}

  // 🔑 İleride token gerekiyorsa header buradan ayarlanabilir
  private getAuthHeaders(): HttpHeaders {
    const token = localStorage.getItem('token');
    return new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: token ? `Bearer ${token}` : ''
    });
  }

  // 📌 Tüm kullanıcıları getir
  getUsers(): Observable<User[]> {
    return this.http.get<User[]>(this.apiUrl, { headers: this.getAuthHeaders() });
  }

  // 📌 Tek kullanıcı getir
  getUser(id: string): Observable<User> {
    return this.http.get<User>(`${this.apiUrl}/${id}`, { headers: this.getAuthHeaders() });
  }

  // 📌 Kullanıcı güncelle
  updateUser(id: string, user: User): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, user, { headers: this.getAuthHeaders() });
  }

  // 📌 Kullanıcı sil
  deleteUser(id: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`, { headers: this.getAuthHeaders() });
  }
}
