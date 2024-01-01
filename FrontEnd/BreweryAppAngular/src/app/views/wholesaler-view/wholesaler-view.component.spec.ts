import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WholesalerViewComponent } from './wholesaler-view.component';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';

describe('WholesalerViewComponent', () => {
  let component: WholesalerViewComponent;
  let fixture: ComponentFixture<WholesalerViewComponent>;

  const mockComponent = {
    close: jasmine.createSpy('close')
  }

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [WholesalerViewComponent, HttpClientModule],
      providers: [HttpClientModule, HttpClient, {
        provide: ActivatedRoute,
        useValue: mockComponent
      }]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(WholesalerViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
