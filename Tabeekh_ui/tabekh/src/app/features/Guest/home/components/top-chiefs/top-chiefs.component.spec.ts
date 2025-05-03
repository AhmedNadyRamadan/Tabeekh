import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TopchiefsComponent } from './top-chiefs.component';

describe('TopchiefsComponent', () => {
  let component: TopchiefsComponent;
  let fixture: ComponentFixture<TopchiefsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TopchiefsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TopchiefsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
