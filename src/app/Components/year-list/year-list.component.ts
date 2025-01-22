import { Component, OnInit } from '@angular/core';
import { Year } from '../../Models/year';
import { SchoolService } from '../../Services/school.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-year-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './year-list.component.html',
  styleUrl: './year-list.component.css'
})
export class YearListComponent {

  constructor(public schoolService:SchoolService){}



}
