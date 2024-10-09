using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class QuizController : ControllerBase
{
    private readonly IQuizService _quizService;
    private readonly ILogger<QuizController> _logger;

    public QuizController(IQuizService quizService, ILogger<QuizController> logger)
    {
        _quizService = quizService;
        _logger = logger;
    }

    [HttpPost]
    public async Task<ActionResult<Quiz>> CreateQuiz([FromBody] Quiz quiz)
    {
        if (quiz == null || !ModelState.IsValid)
        {
            _logger.LogWarning("Invalid model state for creating quiz.");
            return BadRequest(ModelState);
        }

        try
        {
            var createdQuiz = await _quizService.CreateQuizAsync(quiz);
            return CreatedAtAction(nameof(GetQuiz), new { id = createdQuiz.Id }, createdQuiz);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while creating quiz.");
            return StatusCode(500, "An error occurred while creating the quiz.");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Quiz>> GetQuiz(int id)
    {
        try
        {
            var quiz = await _quizService.GetQuizAsync(id);
            if (quiz == null)
            {
                _logger.LogInformation($"Quiz with id {id} not found.");
                return NotFound();
            }

            return Ok(quiz);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while retrieving quiz with id {id}.");
            return StatusCode(500, "An error occurred while retrieving the quiz.");
        }
    }

    [HttpGet("{quizId}/result")]
    public async Task<ActionResult<Result>> GetResults(int quizId)
    {
        try
        {
            var result = await _quizService.GetResultAsync(quizId, new DefaultResultStrategy());
            if (result == null)
            {
                _logger.LogInformation($"Results for quiz with id {quizId} not found.");
                return NotFound("Quiz not found.");
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while retrieving results for quiz with id {quizId}.");
            return StatusCode(500, "An error occurred while retrieving quiz results.");
        }
    }

    [HttpPost("{quizId}/answer")]
    public async Task<ActionResult<Answer>> SubmitAnswer(int quizId, [FromBody] Answer answer)
    {
        if (answer == null || !ModelState.IsValid)
        {
            _logger.LogWarning($"Invalid model state for submitting answer to quiz {quizId}.");
            return BadRequest(ModelState);
        }

        try
        {
            var submittedAnswer = await _quizService.SubmitAnswerAsync(quizId, answer);
            if (submittedAnswer == null)
            {
                _logger.LogInformation($"Quiz {quizId} or question {answer.QuestionId} not found.");
                return NotFound("Quiz or question not found.");
            }

            return Ok(submittedAnswer);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while submitting answer to quiz {quizId}.");
            return StatusCode(500, "An error occurred while submitting the answer.");
        }
    }
}
