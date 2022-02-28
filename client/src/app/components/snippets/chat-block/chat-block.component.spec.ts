import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChatBlockComponent } from './chat-block.component';

describe('ChatBlockComponent', () => {
  let component: ChatBlockComponent;
  let fixture: ComponentFixture<ChatBlockComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ChatBlockComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ChatBlockComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
