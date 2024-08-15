import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';
import { FloatLabelModule } from 'primeng/floatlabel';
import { InputTextModule } from 'primeng/inputtext';
import { CreateEditProduct } from '../../models/createEditProduct';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { ProductCategory } from '../../models/productCategory';
import { DropdownModule } from 'primeng/dropdown';
import { ProductsService } from '../../services/products.service';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { Product } from '../../models/product';
import { DialogEditAction, DialogEditResult } from '../../models/dialogEditResult';
import { PermissionsService } from '../../services/permissions.service';
import { AppArea } from '../../models/appArea';
import { AreaAction } from '../../models/areaAction';

@Component({
  selector: 'app-product-editor',
  standalone: true,
  imports: [
    InputTextModule,
    FormsModule,
    ReactiveFormsModule,
    FloatLabelModule,
    CardModule,
    ButtonModule,
    InputTextareaModule,
    DropdownModule,
  ],
  templateUrl: './product-editor.component.html',
  styleUrl: './product-editor.component.scss'
})
export class ProductEditorComponent implements OnInit {
  private formBuilder = inject(FormBuilder);
  private productsService = inject(ProductsService);
  private permissionsService = inject(PermissionsService);
  private dialogRef = inject(DynamicDialogRef);
  private dialogConfig = inject(DynamicDialogConfig) as DynamicDialogConfig<number>;

  editMode = false;
  canDelete = false;
  categories: ProductCategory[] = [];
  product: Product | undefined = undefined;
  editForm = this.formBuilder.group({
    name: new FormControl<string>('', [Validators.required]),
    description: new FormControl<string>('', [Validators.required]),
    price: new FormControl<number | undefined>(undefined, [Validators.required]),
    note: new FormControl<string | undefined>(undefined),
    specialNote: new FormControl<string | undefined>(undefined),
    category: new FormControl<ProductCategory | undefined>(undefined, [Validators.required]),
  });

  ngOnInit(): void {
    const productId = this.dialogConfig.data;

    this.productsService.getEditContext(productId)
      .subscribe(context => {
        this.categories = context.categories;

        if (context.product) {
          this.editMode = true;
          const areaActions = this.permissionsService.getAllowedActionsByArea(AppArea.Products);
          this.canDelete = this.editMode && areaActions.some(a => a === AreaAction.Delete);
          this.product = context.product;
    
          const product = this.product;
    
          this.editForm.setValue({
            name: product.name,
            description: product.description,
            price: product.price,
            note: product.note,
            specialNote: product.specialNote,
            category: product.category,
          });
        }
      });
  }
  
  save() {
    const model: CreateEditProduct = {
      name: this.editForm.get("name")!.value as string,
      description: this.editForm.get("description")!.value as string,
      price: this.editForm.get("price")!.value as number,
      note: this.editForm.get("note")?.value ?? '',
      specialNote: this.editForm.get("specialNote")?.value ?? '',
      categoryId: (this.editForm.get("category")!.value as ProductCategory).id,
    };

    if (this.editMode) {
      this.productsService.edit(this.product!.id, model)
        .subscribe(product => {
          this.dialogRef.close(this.getDialogResult(product, 'update'));
        });
    } else {
      this.productsService.create(model)
        .subscribe(product => {
          this.dialogRef.close(this.getDialogResult(product, 'create'));
        });
    }
  }

  delete() {
    if (this.editMode) {
      this.productsService.delete(this.product!.id)
        .subscribe(_ => {
          this.dialogRef.close(this.getDialogResult(this.product!, 'delete'));
        });
    }
  }

  private getDialogResult(product: Product, action: DialogEditAction): DialogEditResult<Product> {
    return {
      result: product,
      action: action
    };
  }
}
