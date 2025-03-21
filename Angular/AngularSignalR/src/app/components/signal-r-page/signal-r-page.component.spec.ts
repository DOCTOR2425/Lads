import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SignalRPageComponent } from './signal-r-page.component';

describe('SignalRPageComponent', () => {
  let component: SignalRPageComponent;
  let fixture: ComponentFixture<SignalRPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SignalRPageComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SignalRPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
