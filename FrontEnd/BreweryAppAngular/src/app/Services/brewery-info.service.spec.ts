import { TestBed } from '@angular/core/testing';

import { BreweryInfoService } from './brewery-info.service';
import { HttpClient, HttpHandler } from '@angular/common/http';

describe('BreweryInfoService', () => {
  let service: BreweryInfoService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [],
      providers: [HttpClient, HttpHandler]
    });
    service = TestBed.inject(BreweryInfoService);
  });


  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
