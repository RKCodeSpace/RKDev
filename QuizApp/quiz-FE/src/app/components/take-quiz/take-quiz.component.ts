import { Component } from '@angular/core';
import { QuizService } from '../../services/quiz.service';
import { Quiz } from '../../services/Quiz';
import { PageNotFoundComponent } from '../page-not-found/page-not-found.component';
import { CommonModule } from '@angular/common';
import { FormControl, FormsModule } from '@angular/forms';
import { Answer } from '../../services/Answer';
@Component({
  standalone: true,
  selector: 'app-take-quiz',
  templateUrl: './take-quiz.component.html',
  styleUrls: ['./take-quiz.component.css'],
  imports: [PageNotFoundComponent, CommonModule, FormsModule],
})
export class TakeQuizComponent {
  userQuizId: number = 1;
  quiz?: Quiz;
  selectedAnswers: { [questionId: number]: number } = {};
  error?: string;
  successMessage?: string;

  constructor(private quizService: QuizService) {}

  loadQuiz(): void {
    this.quizService.getQuiz(this.userQuizId).subscribe({
      next: (quiz) => {
        this.quiz = quiz;
        this.error = undefined;
        this.successMessage = undefined;
      },
      error: (err) => {
        this.error = 'Failed to load quiz!';
        this.quiz = undefined;
      },
    });
  }

  selectAnswer(questionId: number, optionIndex: number): void {
    this.selectedAnswers[questionId] = optionIndex;
  }

  submitQuiz(): void {
    const answers: Answer[] = Object.keys(this.selectedAnswers).map(
      (questionId) => ({
        question_id: +questionId,
        selected_option: this.selectedAnswers[+questionId],
        is_correct: true,
      })
    );

    this.quizService.submitAnswer(this.userQuizId, answers[0]).subscribe({
      next: (response) => {
        this.successMessage = 'Quiz submitted successfully!';
        this.error = undefined;
      },
      error: (err) => {
        this.error = 'Failed to submit quiz!';
      },
    });
  }
}
