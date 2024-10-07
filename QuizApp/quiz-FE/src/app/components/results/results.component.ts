import { Component } from '@angular/core';
import { QuizService } from '../../services/quiz.service';
import { Result } from '../../services/result';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  standalone: true,
  selector: 'app-results',
  templateUrl: './results.component.html',
  styleUrls: ['./results.component.css'],
  imports: [FormsModule, CommonModule],
})
export class ResultsComponent {
  userQuizId: number = 1;
  result?: Result;
  error?: string;

  constructor(private quizService: QuizService) {}

  loadResults(): void {
    this.quizService.getResults(this.userQuizId).subscribe({
      next: (result) => {
        this.result = result;
        this.error = undefined;
      },
      error: (err) => {
        this.error = 'Failed to load results!';
      },
    });
  }
}
