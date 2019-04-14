import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CallOverviewComponent } from './call-overview.component';

describe('CallOverviewComponent', () => {
  let component: CallOverviewComponent;
  let fixture: ComponentFixture<CallOverviewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CallOverviewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CallOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
