import { Component, inject, Inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Producto } from '../../interfaces/product.interface';
import { ProductService } from '../../service/product-list.service';
import { NotificationService } from '../../service/notification-dialog.service';
import { NotificationType } from '../../interfaces/notification-dialog-data.interface';
import { SharedMaterial } from '../../shared/shared-material.module';

@Component({
  selector: 'app-product-detail-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    SharedMaterial
  ],
  templateUrl: './product-detail-dialog.component.html',
  styleUrls: ['./product-detail-dialog.component.scss']
})
export class ProductDetailDialogComponent implements OnInit {
  form!: FormGroup;
  isCreateMode = false;

  protected readonly fb = inject(FormBuilder);
  protected readonly dialogRef = inject(MatDialogRef<ProductDetailDialogComponent>);
  protected readonly productService = inject(ProductService);
  protected readonly notificationService = inject(NotificationService);

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: { producto?: Producto },
  ) {}

  ngOnInit() {
    const producto = this.data?.producto;
    this.isCreateMode = !producto?.id; // If no 'id', we assume it's a new product

    this.form = this.fb.group({
      nombre: [producto?.nombre ?? '', [Validators.required]],
      descripcion: [producto?.descripcion ?? ''],
      sku: [producto?.sku ?? '', [Validators.required]],
      estado: [producto?.estado ?? 'A', [Validators.required]],
      //cost: [
      //  product?.cost ?? 0, 
      //  [Validators.required, Validators.min(0.01), Validators.max(9999999)]
      //],
    });

    // If editing an existing product, we disable 'sku' field if you don't allow changes:
    if (!this.isCreateMode) {
      this.form.controls['sku'].disable();
    }
  }

  onSave() {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    const formData = this.form.getRawValue();

    if (this.isCreateMode) {
      this.createProduct(formData);
    } else {
      this.updateProduct(formData);
    }
  }

  private createProduct(formData: Producto) {
    this.productService.createProduct(formData).subscribe({
      next: () => {
        this.notificationService.showNotification(
          NotificationType.Success,
          'Producto creado con éxito',
          'Creado'
        );
        this.dialogRef.close(true);
      },
      error: (error) => {
        debugger
        this.notificationService.showNotification(
          NotificationType.Error,
          'No se pudo crear el producto: ' + error.error,
          'Error'
        );
      }
    });
  }

  private updateProduct(formData: Producto) {
    if (!this.data.producto?.id) return;

    const updateData = {
      id : this.data.producto.id,
      nombre: formData.nombre,
      descripcion: formData.descripcion,
      estado: formData.estado,
      sku: formData.sku
    };

    this.productService.updateProduct(updateData).subscribe({
      next: () => {
        this.notificationService.showNotification(
          NotificationType.Success,
          'Producto actualizado con éxito',
          'Actualizado'
        );
        this.dialogRef.close(true);
      },
      error: (error) => {
        this.notificationService.showNotification(
          NotificationType.Error,
          'No se ha actualizado el producto: ' + error.error,
          'Error'
        );
      }
    });
  }

  onCancel() {
    this.dialogRef.close(false); // user cancels
  }

  // A helper getter to easily check validation
  get f() {
    return {
      nombre: this.form.get('nombre'),
      descripcion: this.form.get('descripcion'),
      sku: this.form.get('sku'),
      estado: this.form.get('estado')
    };
  }
}