public interface IQuizService
{
    Task<Quiz> GetQuizAsync(int id);
    Task<Result> GetResultAsync(int quizId, IResultStrategy resultStrategy);
    Task<Answer> SubmitAnswerAsync(int quizId, Answer answer);
    Task<Quiz> CreateQuizAsync(Quiz quiz); 
}
