import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Answer } from './Answer';
import { Quiz } from './Quiz';

@Injectable()
export class QuizService {
  private apiUrl = 'http://localhost:5082/api/quiz';

  constructor(private http: HttpClient) {}

  createQuiz(quiz: Quiz): Observable<Quiz> {
    return this.http.post<Quiz>(this.apiUrl, quiz);
  }

  getQuiz(id: number): Observable<Quiz> {
    return this.http.get<Quiz>(`${this.apiUrl}/${id}`);
  }

  submitAnswer(quizId: number, answer: Answer): Observable<any> {
    return this.http.post(`${this.apiUrl}/${quizId}/answer`, answer);
  }

  getResults(quizId: number): Observable<any> {
    return this.http.get(`${this.apiUrl}/${quizId}/result`);
  }
}
