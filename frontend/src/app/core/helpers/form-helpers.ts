import {FormGroup} from "@angular/forms";

export class FormHelper {
  public static markAsDirty(form: FormGroup): void {
    Object.values(form.controls).forEach(control => {
      control.markAsTouched();
      control.markAsDirty();
    });
  }
}
