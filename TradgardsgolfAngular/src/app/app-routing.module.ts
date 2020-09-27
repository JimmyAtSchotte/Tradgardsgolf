import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CourseListComponent } from './course-list/course-list.component';
import { SetupPlayersComponent } from './setup-players/setup-players.component';
import { ScorecardComponent } from './scorecard/scorecard.component';

const routes: Routes = [
  {  path: '', component: CourseListComponent },
  {  path: 'courses', component: CourseListComponent },
  {  path: 'setup-players', component: SetupPlayersComponent },
  {  path: 'scorecard', component: ScorecardComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
