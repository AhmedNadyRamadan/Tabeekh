import { ComponentFixture, TestBed } from '@angular/core/testing';

import { chiefCardComponent } from './chief-card.component';

describe('chiefCardComponent', () => {
  let component: chiefCardComponent;
  let fixture: ComponentFixture<chiefCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [chiefCardComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(chiefCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
