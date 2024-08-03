import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DefaltLayoutComponent } from './defalt-layout.component';

describe('DefaltLayoutComponent', () => {
  let component: DefaltLayoutComponent;
  let fixture: ComponentFixture<DefaltLayoutComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DefaltLayoutComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DefaltLayoutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
