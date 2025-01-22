import { Component } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { SchoolListComponent } from "../../Components/school-list/school-list.component";

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [SchoolListComponent],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent {

}
