using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore;

public class QuizService : IQuizService
{
    private readonly QuizContext _context;

    public QuizService(QuizContext context)
    {
        _context = context;
    }

    public async Task<Quiz> CreateQuizAsync(Quiz quiz)
    {

        _context.Quizzes.Add(quiz);
        await _context.SaveChangesAsync();
        return quiz;
    }

    public async Task<Quiz> GetQuizAsync(int id)
    {
      var ts = new Quiz();
        return await _context.Quizzes
                             .Include(q => q.Questions)
                             .FirstOrDefaultAsync(q => q.Id == id)?? ts;
    }

    public async Task<Result> GetResultAsync(int quizId, IResultStrategy resultStrategy)
    {
        var quiz = await _context.Quizzes
                                 .Include(q => q.Questions)
                                 .FirstOrDefaultAsync(q => q.Id == quizId);

        if (quiz == null)
            return null;

        return resultStrategy.CalculateResult(quiz);
    }

    public async Task<Answer> SubmitAnswerAsync(int quizId, Answer answer)
    {
        var quiz = await _context.Quizzes.Include(q => q.Questions).FirstOrDefaultAsync(q => q.Id == quizId);
        if (quiz == null) return null;

        var question = quiz.Questions.FirstOrDefault(q => q.Id == answer.QuestionId);
        if (question == null) return null;

        var isCorrect = question.CorrectOption == answer.SelectedOption;
        answer.IsCorrect = isCorrect;

        return answer;
    }
}
