namespace QuestionRepo.Dto
{
    public class CurrentRank
    {
        public IEnumerable<UserRanking> userRankings { get; set; }
        public IEnumerable<CountRightAnswer> countRightAnswer { get; set; }
        public string currentRankMoney { get; set; }
        public int money { get; set; }
        public string currentRankIQ { get; set; }
        public int rightAnswer { get; set; }
    }
}
