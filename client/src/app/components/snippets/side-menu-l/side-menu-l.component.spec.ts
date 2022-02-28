import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SideMenuLComponent } from './side-menu-l.component';

describe('SideMenuLComponent', () => {
  let component: SideMenuLComponent;
  let fixture: ComponentFixture<SideMenuLComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SideMenuLComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SideMenuLComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
