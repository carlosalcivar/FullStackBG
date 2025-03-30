import { inject, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Producto } from '../interfaces/product.interface';
import { environment } from '../environment/environment';


@Injectable({ providedIn: 'root' })
export class ProductService {
  private baseUrl = environment.apiProducto;

  protected readonly http = inject(HttpClient);

  private getAuthHeaders(): HttpHeaders {
    const token = localStorage.getItem('authToken') || '';
    return new HttpHeaders({
      Authorization: `Bearer ${token}`,
      'Content-Type': 'application/json'
    });
  }

  getAllProducts(): Observable<Producto[]> {
    return this.http.get<Producto[]>(this.baseUrl, {
      headers: this.getAuthHeaders()
    });
  }

  getProductById(id: number): Observable<Producto> {
    return this.http.get<Producto>(`${this.baseUrl}/${id}`, {
      headers: this.getAuthHeaders()
    });
  }

  createProduct(product: Producto): Observable<Producto> {
    return this.http.post<Producto>(this.baseUrl, product, {
      headers: this.getAuthHeaders()
    });
  }

  updateProduct(partialProduct: Partial<Producto>): Observable<Producto> {
    return this.http.put<Producto>(`${this.baseUrl}`, partialProduct, {
      headers: this.getAuthHeaders()
    });
  }

  deleteProduct(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`, {
      headers: this.getAuthHeaders()
    });
  }
}
