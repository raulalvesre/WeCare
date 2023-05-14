import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UsersIssuesComponent } from './users-issues.component';

describe('UsersIssuesComponent', () => {
  let component: UsersIssuesComponent;
  let fixture: ComponentFixture<UsersIssuesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UsersIssuesComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UsersIssuesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
