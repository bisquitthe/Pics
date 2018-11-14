import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';

import { environment } from '../../environments/environment';

@Injectable()
export class AuthenticationService {
  constructor(private http: HttpClient) {}

  login(login: string, password: string) {
    return this.http.post<any>(`${environment.apiUrl}/user/signin`, { login: login, password: password })
      .pipe(map(tokenResponse => {
        if (tokenResponse && tokenResponse.access_token) {
          localStorage.setItem('currentUser', JSON.stringify(tokenResponse));
        }

        return tokenResponse;
      }));
  }

  logout() {
    localStorage.removeItem('currentUser');
  }
}
