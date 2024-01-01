import { TestBed } from '@angular/core/testing';

import { BreweryPostService } from './brewery-post.service';
import { HttpClient, HttpClientModule, HttpHandler } from '@angular/common/http';

describe('BreweryPostService', () => {
  let service: BreweryPostService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [],
      providers: [HttpClient, HttpHandler]
    });
    service = TestBed.inject(BreweryPostService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
