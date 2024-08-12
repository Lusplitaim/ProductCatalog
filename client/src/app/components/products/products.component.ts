import { Component, inject, OnInit } from '@angular/core';
import { TableModule, TableRowSelectEvent } from 'primeng/table';
import { Product } from '../../models/product';
import { ProductsService } from '../../services/products.service';
import { ButtonModule } from 'primeng/button';
import { DialogService, DynamicDialogConfig, DynamicDialogModule, DynamicDialogRef } from 'primeng/dynamicdialog';
import { ProductEditorComponent } from '../product-editor/product-editor.component';
import { ProductCategoriesService } from '../../services/product-categories.service';
import { ProductCategory } from '../../models/productCategory';
import { ProductEditorData } from '../../models/productEditorData';
import { DialogEditResult } from '../../models/dialogEditResult';

@Component({
  selector: 'app-products',
  standalone: true,
  imports: [TableModule, ButtonModule, DynamicDialogModule],
  providers: [DialogService],
  templateUrl: './products.component.html',
  styleUrl: './products.component.scss'
})
export class ProductsComponent implements OnInit {
  private productsService = inject(ProductsService);
  private categoriesService = inject(ProductCategoriesService);
  
  dialogRef: DynamicDialogRef | undefined;
  dialogService = inject(DialogService);
  
  categories: ProductCategory[] = [];
  products: Product[] = [];
  selectedProduct: Product | undefined;

  ngOnInit(): void {
    this.productsService.get()
      .subscribe(products => {
        this.products = products;
      });

    this.categoriesService.get()
      .subscribe((categories: ProductCategory[]) => {
        this.categories = categories;
      });
  }

  getCategoryName(id: number): string {
    return this.categories.find(c => c.id === id)!.name;
  }

  openCreationModal() {
    const dialogConfig = this.getDialogConfig<ProductEditorData>();
    dialogConfig.header = 'Создание продукта';
    dialogConfig.data = {
      categories: this.categories,
    } as ProductEditorData;

    this.dialogRef = this.dialogService.open(ProductEditorComponent, dialogConfig);

    this.dialogRef.onClose.subscribe((data: DialogEditResult<Product>) => {
      if (data) {
        this.products.push(data.result);
      }
    });
  }

  onRowSelect(_: TableRowSelectEvent) {
    const dialogConfig = this.getDialogConfig<ProductEditorData>();
    dialogConfig.header = `Продукт '${this.selectedProduct?.name}'`;
    dialogConfig.data = {
      product: this.selectedProduct,
      categories: this.categories,
    } as ProductEditorData;

    this.dialogRef = this.dialogService.open(ProductEditorComponent, dialogConfig);

    this.dialogRef.onClose.subscribe((data: DialogEditResult<Product>) => {
      if (data) {
        const idx = this.products.findIndex(p => p.id === data.result.id);

        if (data.action === 'update') {
          this.products.splice(idx, 1, data.result);
        } else {
          this.products.splice(idx, 1);
        }
      }
    });
  }

  private getDialogConfig<T>(): DynamicDialogConfig<T> {
    return {
      header: ``,
      width: '20vw',
      modal: true,
      closeOnEscape: true,
      breakpoints: {
          '960px': '75vw',
          '640px': '90vw'
      },
    };
  }
}
