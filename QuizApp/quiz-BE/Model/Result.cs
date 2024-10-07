public class Result
    {
        public int QuizId { get; set; }
        public int UserId { get; set; } 
        public int Score { get; set; }
        public List<Answer> Answers { get; set; } = new List<Answer>();
    }