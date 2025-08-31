import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CbcFormComponent } from './cbc-form.component';

describe('CbcFromComponent', () => {
  let component: CbcFormComponent;
  let fixture: ComponentFixture<CbcFormComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CbcFormComponent]
    });
    fixture = TestBed.createComponent(CbcFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
