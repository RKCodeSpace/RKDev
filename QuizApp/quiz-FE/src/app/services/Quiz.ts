import { Question } from './Question';


export interface Quiz {
  id: number;
  title?: string;
  questions: Question[];
}
