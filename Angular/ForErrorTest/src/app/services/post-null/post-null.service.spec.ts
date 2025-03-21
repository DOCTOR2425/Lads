import { TestBed } from '@angular/core/testing';

import { PostNullService } from './post-null.service';

describe('PostNullService', () => {
  let service: PostNullService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PostNullService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
