public class DefaultResultStrategy : IResultStrategy
{
    public Result CalculateResult(Quiz quiz)
    {
        var result = new Result
        {
            QuizId = quiz.Id,
            Score = 0,
            Answers = new List<Answer>()
        };

        foreach (var question in quiz.Questions)
        {
            var answer = new Answer
            {
                QuestionId = question.Id,
                IsCorrect = false // Default strategy marks answers as incorrect
            };
            result.Answers.Add(answer);
        }

        return result;
    }
}