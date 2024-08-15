import { Component, inject, OnInit } from '@angular/core';
import { TableModule, TableRowSelectEvent } from 'primeng/table';
import { Product } from '../../models/product';
import { ProductsService } from '../../services/products.service';
import { ButtonModule } from 'primeng/button';
import { DialogService, DynamicDialogConfig, DynamicDialogModule, DynamicDialogRef } from 'primeng/dynamicdialog';
import { ProductEditorComponent } from '../product-editor/product-editor.component';
import { DialogEditResult } from '../../models/dialogEditResult';
import { PermissionsService } from '../../services/permissions.service';
import { AppArea } from '../../models/appArea';
import { AreaAction } from '../../models/areaAction';
import { SidebarModule } from 'primeng/sidebar';
import { ProductFiltersComponent } from '../product-filters/product-filters.component';
import { ProductFiltersContext } from '../../models/productFiltersContext';
import { ProductFilters } from '../../models/productFilters';

@Component({
  selector: 'app-products',
  standalone: true,
  imports: [TableModule, ButtonModule, DynamicDialogModule, SidebarModule, ProductFiltersComponent],
  providers: [DialogService],
  templateUrl: './products.component.html',
  styleUrl: './products.component.scss'
})
export class ProductsComponent implements OnInit {
  private productsService = inject(ProductsService);
  private permissionsService = inject(PermissionsService);
  private dialogRef: DynamicDialogRef | undefined;
  private dialogService = inject(DialogService);
  
  products: Product[] = [];
  selectedProduct: Product | undefined;
  canCreate = false;
  canEdit = false;
  filtersVisible = false;
  filtersContext!: ProductFiltersContext;

  ngOnInit(): void {
    const areaActions = this.permissionsService.getAllowedActionsByArea(AppArea.Products);
    this.canCreate = areaActions.some(a => a === AreaAction.Create);
    this.canEdit = areaActions.some(a => a === AreaAction.Update);
    
    this.getProducts();

    this.productsService.getFiltersContext()
      .subscribe(filtersContext => {
        this.filtersContext = filtersContext;
      });
  }

  private getProducts(filters: ProductFilters | null = null) {
    this.productsService.get(filters)
      .subscribe(products => {
        this.products = products;
      });
  }

  applyFilters(filters: ProductFilters) {
    this.filtersVisible = false;
    this.getProducts(filters);
  }

  openCreationModal() {
    const dialogConfig = this.getDialogConfig<number>();
    dialogConfig.header = 'Создание продукта';

    this.dialogRef = this.dialogService.open(ProductEditorComponent, dialogConfig);

    this.dialogRef.onClose.subscribe((data: DialogEditResult<Product>) => {
      if (data) {
        this.products.push(data.result);
      }
    });
  }

  onRowSelect(_: TableRowSelectEvent) {
    if (!this.canEdit) {
      return;
    }

    const dialogConfig = this.getDialogConfig<number>();
    dialogConfig.header = `Продукт '${this.selectedProduct?.name}'`;
    dialogConfig.data = this.selectedProduct?.id;

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
