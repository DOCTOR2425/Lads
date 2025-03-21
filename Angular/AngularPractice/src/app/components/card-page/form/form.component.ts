import { Component, Output, EventEmitter } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';

@Component({
  selector: 'app-form',
  imports: [ReactiveFormsModule],
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.scss'],
})
export class FormComponent {
  @Output() formSubmit = new EventEmitter<any>();
  form: FormGroup;

  constructor(private fb: FormBuilder) {
    this.form = this.fb.group({
      cardName: ['', Validators.required],
      cardBalance: [null, [Validators.required, Validators.min(0)]],
      currency: ['BYN', Validators.required],
    });
  }

  public onSubmit() {
    this.formSubmit.emit(this.form.value);
  }
}
