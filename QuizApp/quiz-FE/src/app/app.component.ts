import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule, RouterOutlet } from '@angular/router';
import { CreateQuizComponent } from "./components/create-quiz/create-quiz.component";
import { QuizService } from './services/quiz.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet,
    RouterModule,HttpClientModule
],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
  providers:[QuizService]
})
export class AppComponent {
}
