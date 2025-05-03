import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TopMealsComponent } from './top-meals.component';

describe('TopMealsComponent', () => {
  let component: TopMealsComponent;
  let fixture: ComponentFixture<TopMealsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TopMealsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TopMealsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
