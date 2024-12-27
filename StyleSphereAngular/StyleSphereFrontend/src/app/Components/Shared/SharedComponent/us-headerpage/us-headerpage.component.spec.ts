import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UsHeaderpageComponent } from './us-headerpage.component';

describe('UsHeaderpageComponent', () => {
  let component: UsHeaderpageComponent;
  let fixture: ComponentFixture<UsHeaderpageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UsHeaderpageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UsHeaderpageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
