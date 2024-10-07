import { Routes } from '@angular/router';
import { ResultsComponent } from './components/results/results.component';
import { TakeQuizComponent } from './components/take-quiz/take-quiz.component';
import { CreateQuizComponent } from './components/create-quiz/create-quiz.component';
import { HomeComponent } from './components/home/home.component';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';

export const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'create-quiz', component: CreateQuizComponent },
  { path: 'quiz/:id', component: TakeQuizComponent },
  { path: 'quiz/:id/results', component: ResultsComponent },
  { path: 'no-data', component: PageNotFoundComponent }];
