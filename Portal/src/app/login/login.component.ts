import { Component, inject , OnInit} from '@angular/core';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { LoginRequest, LoginResponse } from '../interfaces/login.interface';
import { LoginService } from '../service/login.service';
import { NotificationService } from '../service/notification-dialog.service';
import { NotificationType } from '../interfaces/notification-dialog-data.interface';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
  ],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit{
  usuario: string = '';
  clave: string = '';

  protected readonly loginService = inject(LoginService);
  protected readonly notificationService = inject(NotificationService);
  constructor(private router: Router) {}

  ngOnInit(): void {
    const token = localStorage.getItem('authToken');
    if (token) {
      localStorage.clear();
    }
  }

  onLogin(): void {
    if (this.usuario.trim().length == 0 || this.clave.trim().length == 0) {
      this.notificationService.showNotification(
        NotificationType.Warning,
        'Por favor competar los datos!'
      );
      return;
    }

    const loginRequest: LoginRequest = {
      usuario: this.usuario,
      password: btoa(this.clave), // codificás en base64 si la API lo espera así
    };

    this.loginService.login(loginRequest).subscribe({
      next: (data) => {
        localStorage.setItem('usuario', data.usuario);
        localStorage.setItem('email', data.email);
        localStorage.setItem('authToken', data.token);
        this.router.navigate(['/products']);
      },
      error: (err) => {
        // Se Intenta tomar el mensaje del body si existe
        const mensaje =
          err?.error || 'Credenciales inválidas o error en el servidor';
        this.notificationService.showNotification(
          NotificationType.Warning,
          mensaje
        );
      },
    });
  }
}
