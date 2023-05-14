import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SearchOportunityComponent } from './search-oportunity.component';

describe('SearchOportunityComponent', () => {
  let component: SearchOportunityComponent;
  let fixture: ComponentFixture<SearchOportunityComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SearchOportunityComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SearchOportunityComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
