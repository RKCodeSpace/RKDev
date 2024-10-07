
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class QuizController : ControllerBase
{
    private readonly QuizContext _context;

    public QuizController(QuizContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<ActionResult<Quiz>> CreateQuiz(Quiz quiz)
    {
        _context.Quizzes.Add(quiz);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetQuiz), new { id = quiz.Id }, quiz);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Quiz>> GetQuiz(int id)
    {
        var quiz = await _context.Quizzes.Include(q => q.Questions).FirstOrDefaultAsync(q => q.Id == id);

        if (quiz == null)
        {
            return NotFound();
        }

        // Hide correct options before returning
        quiz.Questions.ForEach(q => q.CorrectOption = -1);

        return quiz;
    }

    // [HttpPost("{quizId}/answer")]


    [HttpGet("{quizId}/result")]
    public ActionResult<Result> GetResults(int quizId)
    {
        var quiz = _context.Quizzes.Include(q => q.Questions).FirstOrDefault(q => q.Id == quizId);

        if (quiz == null)
        {
            return NotFound("Quiz not found.");
        }

        var result = new Result
        {
            QuizId = quizId,
            Score = 0,
            Answers = new List<Answer>()
        };


        foreach (var question in quiz.Questions)
        {
            var answer = new Answer
            {
                QuestionId = question.Id,
                IsCorrect = false
            };
            result.Answers.Add(answer);
        }

        return Ok(result);
    }

    [HttpPost("{quizId}/answer")]
    public ActionResult<Answer> SubmitAnswer(int quizId, [FromBody] Answer answer)
    {
        var quiz = _context.Quizzes.Include(q => q.Questions).FirstOrDefault(q => q.Id == quizId);

        if (quiz == null)
        {
            return NotFound("Quiz not found.");
        }

        // Find the question in the quiz
        var question = quiz.Questions.FirstOrDefault(q => q.Id == answer.QuestionId);
        if (question == null)
        {
            return NotFound("Question not found.");
        }


        var isCorrect = question.CorrectOption == answer.SelectedOption;


        var response = new Answer
        {
            QuestionId = answer.QuestionId,
            IsCorrect = isCorrect
        };

        return Ok(response);
    }

}
