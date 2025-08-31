
import { Component, OnInit } from '@angular/core';
import { CbcService } from '../../../services/cbc.service';
import { CbcTest } from '../../../models/cbc-test.model';

@Component({
  selector: 'app-cbc-list',
  templateUrl: './cbc-list.component.html',
  styleUrls: ['./cbc-list.component.css']
})
export class CbcListComponent implements OnInit {
  cbcTests: CbcTest[] = [];
  selectedTest: CbcTest | null = null;

  constructor(private cbcService: CbcService) {}

  ngOnInit(): void {
    this.loadTests();
  }

  loadTests() {
    this.cbcService.getAll().subscribe(data => {
      this.cbcTests = data;
    });
  }

  editTest(test: CbcTest) {
    this.selectedTest = { ...test }; // copy for editing
  }

  onFormSubmit(test: CbcTest) {
    if (test.cbcTestId === 0) {
      this.cbcService.create(test).subscribe(() => this.loadTests());
    } else {
      this.cbcService.update(test.cbcTestId, test).subscribe(() => this.loadTests());
    }
    this.selectedTest = null;
  }

  deleteTest(id: number) {
    if (confirm('Are you sure to delete this test?')) {
      this.cbcService.delete(id).subscribe(() => this.loadTests());
    }
  }

  cancelEdit() {
    this.selectedTest = null;
  }
}
