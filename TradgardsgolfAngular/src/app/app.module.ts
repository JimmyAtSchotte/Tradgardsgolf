import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterModule } from '@angular/router';
import { MaterialModule } from './material.module';
import { CourseListComponent } from './course-list/course-list.component';
import { SetupPlayersComponent } from './setup-players/setup-players.component';
import { ScorecardComponent } from './scorecard/scorecard.component';

@NgModule({
   declarations: [
      AppComponent,
      CourseListComponent,
      SetupPlayersComponent,
      ScorecardComponent,
      CourseListComponent,
      SetupPlayersComponent,
      ScorecardComponent
   ],
   imports: [
      BrowserModule,
      AppRoutingModule,
      BrowserAnimationsModule,
      RouterModule,
      MaterialModule
   ],
   providers: [],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
