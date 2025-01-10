import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdLoginSignupComponent } from './ad-login-signup.component';

describe('AdLoginSignupComponent', () => {
  let component: AdLoginSignupComponent;
  let fixture: ComponentFixture<AdLoginSignupComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AdLoginSignupComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdLoginSignupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
