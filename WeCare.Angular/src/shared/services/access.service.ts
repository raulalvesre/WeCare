import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { environment } from 'src/environments/environment';
import { AccessCredential } from '../models/login.model';
import { JwtToken } from '../models/token.model';

@Injectable({
  providedIn: 'root'
})
export class AccessService {
  private readonly apiUrl = environment.apiUrl;

  private readonly tokenStorageKey = 'token';

  constructor(private httpClient: HttpClient) { }

  login(accessCredential: AccessCredential): Observable<JwtToken> {
    return this.httpClient
      .post<JwtToken>(`${this.apiUrl}/auth/login`, {
        email: accessCredential.email,
        password: accessCredential.password
      })
      .pipe(tap(jwtToken => {
        localStorage.setItem('token', jwtToken.token);
      }));
  }
}
