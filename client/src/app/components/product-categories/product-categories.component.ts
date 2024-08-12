import { Component, inject, OnInit } from '@angular/core';
import { ProductCategoriesService } from '../../services/product-categories.service';
import { ProductCategory } from '../../models/productCategory';
import { ButtonModule } from 'primeng/button';
import { DialogService, DynamicDialogConfig, DynamicDialogModule, DynamicDialogRef } from 'primeng/dynamicdialog';
import { TableModule } from 'primeng/table';
import { ProductCategoryEditorComponent } from '../product-category-editor/product-category-editor.component';
import { DialogEditResult } from '../../models/dialogEditResult';

@Component({
  selector: 'app-product-categories',
  standalone: true,
  imports: [TableModule, ButtonModule, DynamicDialogModule],
  providers: [DialogService],
  templateUrl: './product-categories.component.html',
  styleUrl: './product-categories.component.scss'
})
export class ProductCategoriesComponent implements OnInit {
  private categoriesService = inject(ProductCategoriesService);
  private dialogRef: DynamicDialogRef | undefined;
  private dialogService = inject(DialogService);

  categories: ProductCategory[] = [];
  selectedCategory: ProductCategory | undefined;

  ngOnInit(): void {
    this.categoriesService.get()
      .subscribe(categories => {
        this.categories = categories;
      });
  }


  openEditModal() {
    const dialogConfig = this.getDialogConfig<void>();
    dialogConfig.header = 'Создание категории';

    this.dialogRef = this.dialogService.open(ProductCategoryEditorComponent, dialogConfig);

    this.dialogRef.onClose.subscribe((data: DialogEditResult<ProductCategory>) => {
      if (data) {
        this.categories.push(data.result);
      }
    });
  }

  onRowSelect() {
    const dialogConfig = this.getDialogConfig<ProductCategory>();
    dialogConfig.header = `Редактирование категории '${this.selectedCategory!.name}'`;
    dialogConfig.data = this.selectedCategory;

    this.dialogRef = this.dialogService.open(ProductCategoryEditorComponent, dialogConfig);

    this.dialogRef.onClose.subscribe((data: DialogEditResult<ProductCategory>) => {
      if (data) {
        const idx = this.categories.findIndex(c => c.id === data.result.id);

        if (data.action === 'update') {
          this.categories.splice(idx, 1, data.result);
        } else {
          this.categories.splice(idx, 1);
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
