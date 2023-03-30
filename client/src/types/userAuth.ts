export interface RegisterCredentials {
  username: string;
  email: string;
  password: string;
}

export interface LoginCredentials {
  username: string;
  password: string;
}

export interface JWTDeCode {
  UserId: string;
  Username: string;
  Email: string;
  Role: string;
  iat: number;
  exp: number;
  iss: string;
}
