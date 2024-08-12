import { Component, inject } from '@angular/core';
import { FormBuilder, FormControl, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';
import { DropdownModule } from 'primeng/dropdown';
import { FloatLabelModule } from 'primeng/floatlabel';
import { InputTextModule } from 'primeng/inputtext';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { ProductCategoriesService } from '../../services/product-categories.service';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { ProductCategory } from '../../models/productCategory';
import { CreateEditProductCategory } from '../../models/createEditProductCategory';
import { DialogEditAction, DialogEditResult } from '../../models/dialogEditResult';

@Component({
  selector: 'app-product-category-editor',
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
  templateUrl: './product-category-editor.component.html',
  styleUrl: './product-category-editor.component.scss'
})
export class ProductCategoryEditorComponent {
  private formBuilder = inject(FormBuilder);
  private categoriesService = inject(ProductCategoriesService);
  private dialogRef = inject(DynamicDialogRef);
  private dialogConfig = inject(DynamicDialogConfig) as DynamicDialogConfig<ProductCategory>;

  editMode = false;
  category: ProductCategory | undefined = undefined;
  editForm = this.formBuilder.group({
    name: new FormControl<string>('', [Validators.required]),
  });

  ngOnInit(): void {
    if (!this.dialogConfig.data) {
      return;
    }

    const category = this.dialogConfig.data;

    if (category) {
      this.editMode = true;
      this.category = this.dialogConfig.data;

      this.editForm.setValue({
        name: category.name,
      });
    }
  }
  
  save() {
    const model: CreateEditProductCategory = {
      name: this.editForm.get("name")!.value as string,
    };

    if (this.editMode) {
      this.categoriesService.update(this.category!.id, model)
        .subscribe(category => {
          this.dialogRef.close(this.getDialogResult(category, 'update'));
        });
    } else {
      this.categoriesService.create(model)
        .subscribe(category => {
          this.dialogRef.close(this.getDialogResult(category, 'create'));
        });
    }
  }

  delete() {
    if (this.editMode) {
      this.categoriesService.delete(this.category!.id)
        .subscribe(_ => {
          this.dialogRef.close(this.getDialogResult(this.category!, 'delete'));
        });
    }
  }

  private getDialogResult(category: ProductCategory, action: DialogEditAction): DialogEditResult<ProductCategory> {
    return {
      result: category,
      action: action
    };
  }
}
