import { ComponentFixture, TestBed } from '@angular/core/testing';

import { chiefsComponent } from './chiefs.component';

describe('chiefsComponent', () => {
  let component: chiefsComponent;
  let fixture: ComponentFixture<chiefsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [chiefsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(chiefsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
