import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChiefDashboardComponent } from './chief-dashboard.component';

describe('ChiefDashboardComponent', () => {
  let component: ChiefDashboardComponent;
  let fixture: ComponentFixture<ChiefDashboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ChiefDashboardComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ChiefDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
