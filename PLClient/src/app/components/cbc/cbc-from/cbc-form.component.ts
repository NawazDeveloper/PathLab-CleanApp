import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CbcTest } from '../../../models/cbc-test.model';

@Component({
  selector: 'app-cbc-form',
  templateUrl: './cbc-form.component.html',
  styleUrls: ['./cbc-form.component.css']
})
export class CbcFormComponent {
  @Input() test: CbcTest = {
    cbcTestId: 0,
    hemoglobin: 0,
    rbcCount: 0,
    wbcCount: 0,
    plateletCount: 0,
    hematocrit: 0,
    mcv: 0,
    mch: 0,
    mchc: 0
  };

  @Output() save = new EventEmitter<CbcTest>();
  @Output() cancel = new EventEmitter<void>();

  submitForm() {
    this.save.emit(this.test);
  }

  cancelForm() {
    this.cancel.emit();
  }
}
