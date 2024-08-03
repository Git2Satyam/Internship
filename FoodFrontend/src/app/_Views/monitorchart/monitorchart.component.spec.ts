import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MonitorchartComponent } from './monitorchart.component';

describe('MonitorchartComponent', () => {
  let component: MonitorchartComponent;
  let fixture: ComponentFixture<MonitorchartComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MonitorchartComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MonitorchartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
