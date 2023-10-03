namespace ConsoleApp
{
    internal class Detail
    {
        public string[] Teams { get; set; }
        public string[] FirstHalf { get; set; }
        public string[] SecondHalf { get; set; }
        public string[] RegularTime { get; set; }
        public string[] Corners { get; set; }
        public string[] Penalties { get; set; }
        public string[] YellowCards { get; set; }
        public string[] RedCards { get; set; }
        public string[] FirstET { get; set; }
        public string[] SecondET { get; set; }
        public string[] PenaltiesShootout { get; set; }

        public Detail(string[] teams, string[] firstHalf, string[] secondHalf, string[] regularTime, string[] corners, string[] penalties, string[] yellowCards, string[] redCards, string[] firstET, string[] secondET, string[] penaltiesShootout)
        {
            Teams = teams;
            FirstHalf = firstHalf;
            SecondHalf = secondHalf;
            RegularTime = regularTime;
            Corners = corners;
            Penalties = penalties;
            YellowCards = yellowCards;
            RedCards = redCards;
            FirstET = firstET;
            SecondET = secondET;
            PenaltiesShootout = penaltiesShootout;
        }
    }
}
