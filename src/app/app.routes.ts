import { Routes } from '@angular/router';
import { SchoolListComponent } from './Components/school-list/school-list.component';

export const routes: Routes = [

  {path:'School',component:SchoolListComponent,title:"School"},
  {path:'',redirectTo:'home',pathMatch:'full'},



];
