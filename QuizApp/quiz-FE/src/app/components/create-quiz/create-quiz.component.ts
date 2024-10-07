import { Component } from '@angular/core';
import { QuizService } from '../../services/quiz.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Quiz } from '../../services/Quiz';


@Component({
  standalone: true,
  selector: 'app-create-quiz',
  templateUrl: './create-quiz.component.html',
  styleUrls: ['./create-quiz.component.css'],
  imports:[CommonModule,FormsModule]
})
export class CreateQuizComponent {
  quiz: Quiz = {
    id: 0,
    title: '',
    questions: [{ id:0, text: '', options: ['', '', '', ''], correct_option: 0 }],
  };

  errorMessage?: string;
  successMessage?: string;

  constructor(private quizService: QuizService) {}

  addQuestion(): void {
    this.quiz.questions.push({ id:0, text: '', options: ['', '', '', ''], correct_option: 0 });
  }

  removeQuestion(index: number): void {
    this.quiz.questions.splice(index, 1);
  }

  createQuiz(): void {
 
    if (!this.quiz.title || this.quiz.questions.length === 0) {
      this.errorMessage = 'Quiz title and at least one question are required.';
      return;
    }

    this.quizService.createQuiz(this.quiz).subscribe({
      next: (response) => {
        this.successMessage = 'Quiz created successfully!';
        this.errorMessage = undefined; 
        this.resetQuiz(); // Reset the form after success
      },
      error: (error) => {
        this.errorMessage = 'Failed to create quiz!';
        this.successMessage = undefined; 
      },
    });
  }

  resetQuiz(): void {
    this.quiz = {
      id: 0,
      title: '',
      questions: [{ id:0, text: '', options: ['', '', '', ''], correct_option: 0 }],
    };
  }
}