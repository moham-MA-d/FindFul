/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { PhotoUpload2Component } from './photo-upload.component';

describe('PhotoUpload2Component', () => {
  let component: PhotoUpload2Component;
  let fixture: ComponentFixture<PhotoUpload2Component>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PhotoUpload2Component ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PhotoUpload2Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
