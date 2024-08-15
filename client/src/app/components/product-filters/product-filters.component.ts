import { Component, inject, Input, output, Output } from '@angular/core';
import { FormBuilder, FormControl, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { FloatLabelModule } from 'primeng/floatlabel';
import { InputTextModule } from 'primeng/inputtext';
import { MultiSelectModule } from 'primeng/multiselect';
import { ProductCategory } from '../../models/productCategory';
import { ProductFilters } from '../../models/productFilters';

@Component({
  selector: 'app-product-filters',
  standalone: true,
  imports: [
    InputTextModule,
    FormsModule,
    ReactiveFormsModule,
    FloatLabelModule,
    ButtonModule,
    MultiSelectModule,
  ],
  templateUrl: './product-filters.component.html',
  styleUrl: './product-filters.component.scss'
})
export class ProductFiltersComponent {
  private formBuilder = inject(FormBuilder);
  
  @Input() categories: ProductCategory[] = [];
  onFiltersChange = output<ProductFilters>(); 

  filterForm = this.formBuilder.group({
    minPrice: new FormControl<number | undefined>(undefined),
    maxPrice: new FormControl<number | undefined>(undefined),
    categories: new FormControl<ProductCategory[]>([]),
  });

  apply() {
    this.filterForm.markAsPristine();

    const minPrice = this.filterForm.get("minPrice")!.value as number | undefined;
    const maxPrice = this.filterForm.get("maxPrice")!.value as number | undefined;
    const categories = this.filterForm.get("categories")!.value as ProductCategory[];
    const productFilters: ProductFilters = {
      minPrice,
      maxPrice,
      categories: categories.map(c => c.id),
    };
    
    this.onFiltersChange.emit(productFilters);
  }
}
