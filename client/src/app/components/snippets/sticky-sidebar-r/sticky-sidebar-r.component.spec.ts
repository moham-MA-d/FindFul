import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StickySidebarRComponent } from './sticky-sidebar-r.component';

describe('StickySidebarRComponent', () => {
  let component: StickySidebarRComponent;
  let fixture: ComponentFixture<StickySidebarRComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StickySidebarRComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(StickySidebarRComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
