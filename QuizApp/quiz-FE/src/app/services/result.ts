import { Answer } from "./Answer";

export interface Result
    {
        quiz_id:number,
         user_id:number,
        score :number,
        answers : Answer [] 
    }