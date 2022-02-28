import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LandingRegisterComponent } from './landing-register.component';

describe('LandingRegisterComponent', () => {
  let component: LandingRegisterComponent;
  let fixture: ComponentFixture<LandingRegisterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LandingRegisterComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LandingRegisterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
