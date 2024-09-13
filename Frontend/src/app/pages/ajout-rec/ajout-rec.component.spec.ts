import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AjoutRecComponent } from './ajout-rec.component';

describe('AjoutRecComponent', () => {
  let component: AjoutRecComponent;
  let fixture: ComponentFixture<AjoutRecComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AjoutRecComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AjoutRecComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
