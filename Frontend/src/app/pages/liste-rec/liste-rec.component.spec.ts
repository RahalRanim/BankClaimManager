import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListeRecComponent } from './liste-rec.component';

describe('ListeRecComponent', () => {
  let component: ListeRecComponent;
  let fixture: ComponentFixture<ListeRecComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ListeRecComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ListeRecComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
