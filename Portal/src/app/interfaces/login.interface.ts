export interface LoginRequest {
    usuario: string;
    password: string;
  }

  export interface LoginResponse {
    usuario: string;
    email: string;
    rol: string;
    token: string;
  }