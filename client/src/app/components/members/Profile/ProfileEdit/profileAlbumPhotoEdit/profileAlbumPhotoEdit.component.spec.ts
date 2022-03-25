/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { ProfileAlbumPhotoEditComponent } from './profileAlbumPhotoEdit.component';

describe('ProfilePhotoEditComponent', () => {
  let component: ProfileAlbumPhotoEditComponent;
  let fixture: ComponentFixture<ProfileAlbumPhotoEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProfileAlbumPhotoEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProfileAlbumPhotoEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
