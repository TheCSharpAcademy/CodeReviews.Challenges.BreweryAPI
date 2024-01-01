import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BrewEditComponent } from './brew-edit.component';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule } from '@angular/forms';

describe('BrewEditComponent', () => {
  let component: BrewEditComponent;
  let fixture: ComponentFixture<BrewEditComponent>;

  const mockComponent = {
    close: jasmine.createSpy('close')
  }

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BrewEditComponent, HttpClientModule, BrowserAnimationsModule, FormsModule],
      providers: [HttpClientModule, HttpClient, {
        provide: ActivatedRoute,
        useValue: mockComponent
      }]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(BrewEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it("should have a valid form", () => {
    const form = fixture.nativeElement.querySelector('form');
    const name = fixture.nativeElement.querySelector('#name');
    const flavour = fixture.nativeElement.querySelector('#flavour');
    const age = fixture.nativeElement.querySelector('#age');
    const price = fixture.nativeElement.querySelector('#price');
    const brewery = fixture.nativeElement.querySelector('#brewery');

    name.value = 'testing';
    flavour.value = 'test';
    age.value = 'new';
    price.value = '10'
    brewery.value = 1;

    name.dispatchEvent(new Event('input'));
    flavour.dispatchEvent(new Event('input'));
    age.dispatchEvent(new Event('input'));
    price.dispatchEvent(new Event('input'));
    brewery.dispatchEvent(new Event('input'));

    fixture.detectChanges();

    expect(form.classList.contains('ng-valid')).toBeTruthy();


    name.value = '';
    flavour.value = 'test';
    age.value = 'new';
    price.value = '10'
    brewery.value = 1;

    name.dispatchEvent(new Event('input'));
    flavour.dispatchEvent(new Event('input'));
    age.dispatchEvent(new Event('input'));
    price.dispatchEvent(new Event('input'));
    brewery.dispatchEvent(new Event('input'));

    fixture.detectChanges();

    expect(form.classList.contains('ng-invalid')).toBeTruthy();

  })

});
