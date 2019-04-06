import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CallCountsComponent } from './call-counts.component';

describe('CallCountsComponent', () => {
  let component: CallCountsComponent;
  let fixture: ComponentFixture<CallCountsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CallCountsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CallCountsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
