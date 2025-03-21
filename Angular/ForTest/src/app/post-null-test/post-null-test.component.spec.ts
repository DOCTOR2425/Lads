import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PostNullTestComponent } from './post-null-test.component';

describe('PostNullTestComponent', () => {
  let component: PostNullTestComponent;
  let fixture: ComponentFixture<PostNullTestComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PostNullTestComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PostNullTestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
