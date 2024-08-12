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
import { ProductEditorData } from '../../models/productEditorData';
import { DialogEditAction, DialogEditResult } from '../../models/dialogEditResult';

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
  private dialogRef = inject(DynamicDialogRef);
  private dialogConfig = inject(DynamicDialogConfig) as DynamicDialogConfig<ProductEditorData>;

  editMode = false;
  product: Product | undefined = undefined;
  categories: ProductCategory[] = [];
  editForm = this.formBuilder.group({
    name: new FormControl<string>('', [Validators.required]),
    description: new FormControl<string>('', [Validators.required]),
    price: new FormControl<number | undefined>(undefined, [Validators.required]),
    note: new FormControl<string | undefined>(undefined),
    specialNote: new FormControl<string | undefined>(undefined),
    category: new FormControl<ProductCategory | undefined>(undefined, [Validators.required]),
  });

  ngOnInit(): void {
    if (!this.dialogConfig.data) {
      return;
    }

    const product = this.dialogConfig.data.product;
    this.categories = this.dialogConfig.data.categories;

    if (product) {
      this.editMode = true;
      this.product = this.dialogConfig.data.product;

      const product = this.product;
      const category = this.categories.find(c => c.id === product.categoryId);

      this.editForm.setValue({
        name: product.name,
        description: product.description,
        price: product.price,
        note: product.note,
        specialNote: product.specialNote,
        category: category,
      });
    }
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
