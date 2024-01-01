import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BeerViewComponent } from './beer-view.component';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';

describe('BeerViewComponent', () => {
  let component: BeerViewComponent;
  let fixture: ComponentFixture<BeerViewComponent>;
  
  const mockComponent = {
    close: jasmine.createSpy('close')
  }

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BeerViewComponent, HttpClientModule],
      providers: [HttpClientModule, HttpClient, {
        provide: ActivatedRoute,
        useValue: mockComponent
      }]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(BeerViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create the component', () => {
    expect(component).toBeTruthy();
  });
});
