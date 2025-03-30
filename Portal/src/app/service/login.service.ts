import { inject, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { LoginRequest, LoginResponse } from '../interfaces/login.interface';
import { environment } from '../environment/environment';


@Injectable({ providedIn: 'root' })
export class LoginService {
  private baseUrl = environment.apiLogin;

  protected readonly http = inject(HttpClient);

  login(login: LoginRequest): Observable<LoginResponse> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post<LoginResponse>(`${this.baseUrl}/login`, login, { headers });
  }

}
