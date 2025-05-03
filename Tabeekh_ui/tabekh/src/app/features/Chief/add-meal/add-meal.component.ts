import { AsyncPipe } from '@angular/common';
import { Component } from '@angular/core';
import {
  AbstractControl,
  FormArray,
  FormBuilder,
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { MultiSelectModule } from 'primeng/multiselect';
import { SelectModule } from 'primeng/select';
import { Observable } from 'rxjs';
import DaysAR from '../../../core/global/Days.data';
import UnitsAR from '../../../core/global/Units.data';
import ICategory from '../../../core/models/ICategory.model';
import { LookupsService } from '../../../core/services/lookups/lookups.service';
import { MealCardComponent } from '../../../shared/components/meal-card/meal-card.component';
import { ChiefService } from './../../../core/services/chief/chief.service';
import { TokenService } from './../../../core/services/token/token.service';
import { IMealForm } from './models/IMealForm.model';

@Component({
  selector: 'app-add-meal',
  imports: [
    AsyncPipe,
    MealCardComponent,
    ReactiveFormsModule,
    SelectModule,
    MultiSelectModule,
  ],
  templateUrl: './add-meal.component.html',
  styleUrl: './add-meal.component.scss',
})
export class AddMealComponent {
  initialMeal?: IMealForm;

  mealForm!: FormGroup;
  categories!: Observable<ICategory[]>;

  get ingredients() {
    return this.mealForm.get('ingredients') as FormArray;
  }

  get recipe() {
    return this.mealForm.get('recipe') as FormArray;
  }

  get daysOptions() {
    return DaysAR;
  }

  get units() {
    return UnitsAR;
  }

  get statuses() {
    return [
      { label: 'متواجد', value: true },
      { label: 'غير متواجد', value: false },
    ];
  }

  constructor(
    private fb: FormBuilder,
    private _LookupsService: LookupsService,
    private _chiefService: ChiefService,
    private _TokenService: TokenService,
    private _Router: Router
  ) {}

  ngOnInit(): void {
    this.initForm();
    if (this.initialMeal) {
      this.setFormData(this.initialMeal);
    }
    this.categories = this._LookupsService.getCategories();
  }

  initForm(): void {
    this.mealForm = this.fb.group({
      name: ['', Validators.required],
      category: [null, Validators.required],
      price: [null, Validators.required],
      available: [false, Validators.required],
      measure_unit: [null, Validators.required],
      chief_id: [this._TokenService.getUserId()],
      day: [[], Validators.required],
      prepration_Time: [null, Validators.required],
      ingredients: this.fb.array([this.fb.control('', Validators.required)]),
      recipe: this.fb.array([this.fb.control('', Validators.required)]),
      photo: [''],
    });
  }

  addIngredient(): void {
    this.ingredients.push(this.fb.control('', Validators.required));
  }

  removeIngredient(index: number): void {
    this.ingredients.removeAt(index);
  }

  addRecipeStep(): void {
    this.recipe.push(this.fb.control('', Validators.required));
  }

  removeRecipeStep(index: number): void {
    this.recipe.removeAt(index);
  }

  onPhotoChange(event: any): void {
    const file = event.target.files?.[0];
    if (!file) return;

    const reader = new FileReader();
    reader.onload = () => {
      this.mealForm.patchValue({
        photo: (reader.result as string).split(',')[1],
      });
    };
    reader.readAsDataURL(file);
  }

  setFormData(meal: IMealForm): void {
    this.mealForm.patchValue({
      ...meal,
      ingredients: [],
      recipe: [],
    });

    this.ingredients.clear();
    meal.ingredients.forEach((ing) =>
      this.ingredients.push(this.fb.control(ing, Validators.required))
    );

    this.recipe.clear();
    meal.recipe.forEach((step) =>
      this.recipe.push(this.fb.control(step, Validators.required))
    );
  }

  onSubmit(): void {
    if (this.mealForm.invalid) return;
    const mealData: Partial<IMealForm> = this.mealForm.value;
    console.log('Submitted Meal:', mealData);
    this._chiefService
      .addMeal(this.mealForm.value['chief_id'], {
        ...this.mealForm.value,
        ingredients: this.mealForm.value['ingredients'].join('^'),
        recipe: this.mealForm.value['recipe'].join('^'),
        day: this.mealForm.value['day'][0],
      })
      .subscribe({
        next: () => {},
        error: (err) => {},
        complete: () => {
          this._Router.navigate(['/chief/dashboard']);
        },
      });
    // Submit to backend
  }

  castFormControl(control: AbstractControl): FormControl {
    return control as FormControl;
  }
}
