import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SaleEditComponent } from './sale-edit.component';
import { ActivatedRoute } from '@angular/router';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

describe('SaleEditComponent', () => {
  let component: SaleEditComponent;
  let fixture: ComponentFixture<SaleEditComponent>;

  const mockComponent = {
    close: jasmine.createSpy('close')
  }

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SaleEditComponent, HttpClientModule, BrowserAnimationsModule],
      providers: [HttpClientModule, HttpClient, {
        provide: ActivatedRoute,
        useValue: mockComponent
      }]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(SaleEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
