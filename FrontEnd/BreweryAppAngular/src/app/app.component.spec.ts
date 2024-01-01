import { TestBed } from '@angular/core/testing';
import { AppComponent } from './app.component';
import { HttpClient, HttpHandler } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';

describe('AppComponent', () => {
  beforeEach(async () => {

    const mockComponent = {
      close: jasmine.createSpy('close')
    }

    await TestBed.configureTestingModule({
      imports: [AppComponent],
      providers: [HttpClient, HttpHandler, {
        provide: ActivatedRoute,
        useValue: mockComponent
      }]
    }).compileComponents();
  });

  it('should create the app', () => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.componentInstance;
    expect(app).toBeTruthy();
  });

  it(`should have the 'BrewApi' title`, () => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.componentInstance;
    expect(app.title).toEqual('BrewApi');
  });
});
