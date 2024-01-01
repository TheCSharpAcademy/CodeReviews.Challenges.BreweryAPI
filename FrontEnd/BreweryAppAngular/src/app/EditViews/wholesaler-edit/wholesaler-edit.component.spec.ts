import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WholesalerEditComponent } from './wholesaler-edit.component';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule } from '@angular/forms';

describe('WholesalerEditComponent', () => {
  let component: WholesalerEditComponent;
  let fixture: ComponentFixture<WholesalerEditComponent>;

  const mockComponent = {
    close: jasmine.createSpy('close')
  }

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [WholesalerEditComponent, HttpClientModule, BrowserAnimationsModule, FormsModule],
      providers: [HttpClientModule, HttpClient, {
        provide: ActivatedRoute,
        useValue: mockComponent
      }]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(WholesalerEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it("should have a valid form", () => {
    const form = fixture.nativeElement.querySelector('form');
    const name = fixture.nativeElement.querySelector('#name');

    name.value = 'testing';
    name.dispatchEvent(new Event('input'));
    fixture.detectChanges();

    expect(form.classList.contains('ng-valid')).toBeTruthy();

    name.value = '';
    name.dispatchEvent(new Event('input'));
    fixture.detectChanges();

    expect(form.classList.contains('ng-invalid')).toBeTruthy();
  })
});
