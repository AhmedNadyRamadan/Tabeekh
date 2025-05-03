import { AbstractControl, ValidationErrors } from "@angular/forms";
import { IMealWithArr } from "../../../../core/models";

export interface IMealForm extends Pick<IMealWithArr, "available" | "day" | "measure_unit" | "ingredients" | "name" | "chief_id" | "prepration_Time" | "recipe" | "price" | "photo" | "category"> {}

export default IMealFormGroup;
type IMealFormGroup =  { [key in keyof IMealForm]: IMealForm[key] | ((control: AbstractControl) => ValidationErrors | null)[] }