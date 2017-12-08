import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UnsecuredComponent } from './unsecured.component';

describe('UnsecuredComponent', () => {
  let component: UnsecuredComponent;
  let fixture: ComponentFixture<UnsecuredComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UnsecuredComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UnsecuredComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
