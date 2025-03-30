import { Component, inject, OnInit, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { Producto } from '../../interfaces/product.interface';
import { ProductService } from '../../service/product-list.service';
import { SharedMaterial } from '../../shared/shared-material.module';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { MatDialog } from '@angular/material/dialog';
import { ProductDetailDialogComponent } from '../product-detail-dialog/product-detail-dialog.component';
import { NotificationService } from '../../service/notification-dialog.service';
import { NotificationType } from '../../interfaces/notification-dialog-data.interface';

@Component({
  selector: 'app-products-list',
  standalone: true,
  imports: [CommonModule, SharedMaterial],
  templateUrl: './products-list.component.html',
  styleUrls: ['./products-list.component.scss']
})
export class ProductsListComponent implements OnInit {
  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  
  dataSource = new MatTableDataSource<Producto>();
  displayedColumns: string[] = ['index', 'sku', 'nombre', 'descripcion', 'estado','actions'];

  protected readonly productService = inject(ProductService);
  protected readonly router = inject(Router);
  protected readonly dialog = inject(MatDialog);
  protected readonly notificationService = inject(NotificationService);

  ngOnInit(): void {
    const token = localStorage.getItem('authToken');

    if (!token) {
      this.router.navigate(['/']);
      return;
    }

    this.loadProducts();
  }

  loadProducts(): void {
    this.productService.getAllProducts().subscribe({
      next: (data) => {

        this.dataSource.data = data;

        this.dataSource.sort = this.sort;
        this.dataSource.paginator = this.paginator;
      },
      error: (err) => {
        if (err.status === 401) {
          this.notificationService.showNotification(
            NotificationType.Warning,
            'La sesion a caducado'
          );
           this.router.navigate(['/login']);
        } else {
          this.notificationService.showNotification(
            NotificationType.Error,
            'No se han cargado los productos: ' + err.error
          )
        }
      }
    });
  }

  onCreateNew() {
    const dialogRef = this.dialog.open(ProductDetailDialogComponent, {
      width: '900px',
      height: '700px',
      panelClass: 'custom-dialog-container',
      disableClose: true,
      autoFocus: false,
      data: { producto: {} }
    });

    dialogRef.afterClosed().subscribe((success: boolean) => {
      if (success) {
        this.loadProducts();
      }
    });
  }

  onEditProduct(producto: Producto) {
    const dialogRef = this.dialog.open(ProductDetailDialogComponent, {
      width: '900px',
      height: '700px',
      panelClass: 'custom-dialog-container',
      disableClose: true,
      autoFocus: false,
      data: { producto }
    });

    dialogRef.afterClosed().subscribe((success: boolean) => {
      if (success) {
        this.loadProducts();
      }
    });
  }

  onDeleteProduct(productId: number, productName: string) {
    this.notificationService
      .showConfirmation(
        `Está seguro de que desea eliminar el producto [${productName}]?`,
        'Confirmar Eliminar'
      )
      .subscribe((confirmed: boolean) => {
        if (confirmed) {
          this.productService.deleteProduct(productId).subscribe({
            next: () => {
              this.notificationService.showNotification(
                NotificationType.Success,
                'Producto eliminado con éxito',
                'Delete'
              );
              this.loadProducts();
            },
            error: (err) => {
              if (err.status === 401) {
                this.notificationService.showNotification(
                  NotificationType.Warning,
                  'La sesion a caducado'
                );
                 this.router.navigate(['/login']);
              } else {
                this.notificationService.showNotification(
                  NotificationType.Error,
                  'Eliminar fallido: ' + err.error,
                  'Delete'
                )
              }
            }
           
              
          }
        );
        }
      });
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    // mat-table uses lowercase for filter matching
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }
}
