import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CbcListComponent } from './cbc-list.component';

describe('CbcListComponent', () => {
  let component: CbcListComponent;
  let fixture: ComponentFixture<CbcListComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CbcListComponent]
    });
    fixture = TestBed.createComponent(CbcListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
