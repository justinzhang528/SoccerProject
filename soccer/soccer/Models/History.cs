namespace Soccer.Models
{
    public class History
    {
        public string ResultId { get; set; }
        public string FirstHalf_H { get; set; }
        public string FirstHalf_A { get; set; }
        public string SecondHalf_H { get; set; }
        public string SecondHalf_A { get; set; }
        public string RegularTime_H { get; set; }
        public string RegularTime_A { get; set; }
        public string Corners_H { get; set; }
        public string Corners_A { get; set; }
        public string Penalties_H { get; set; }
        public string Penalties_A { get; set; }
        public string YellowCards_H { get; set; }
        public string YellowCards_A { get; set; }
        public string RedCards_H { get; set; }
        public string RedCards_A { get; set; }
        public string FirstET_H { get; set; }
        public string FirstET_A { get; set; }
        public string SecondET_H { get; set; }
        public string SecondET_A { get; set; }
        public string PenaltiesShootout_H { get; set; }
        public string PenaltiesShootout_A { get; set; }

        public History(string resultId, string firstHalf_H, string firstHalf_A, string secondHalf_H, string secondHalf_A, string regularTime_H, string regularTime_A, string corners_H, string corners_A, string penalties_H, string penalties_A,
            string yellowCards_H, string yellowCards_A, string redCards_H, string redCards_A, string firstET_H, string firstET_A, string secondET_H, string secondET_A, string penaltiesShootout_H, string penaltiesShootout_A)
        {
            ResultId = resultId;
            FirstHalf_H = firstHalf_H;
            FirstHalf_A = firstHalf_A;
            SecondHalf_H = secondHalf_H;
            SecondHalf_A = secondHalf_A;
            RegularTime_H = regularTime_H;
            RegularTime_A = regularTime_A;
            Corners_H = corners_H;
            Corners_A = corners_A;
            Penalties_H = penalties_H;
            Penalties_A = penalties_A;
            YellowCards_H = yellowCards_H;
            YellowCards_A = yellowCards_A;
            RedCards_H = redCards_H;
            RedCards_A = redCards_A;
            FirstET_H = firstET_H;
            FirstET_A = firstET_A;
            SecondET_H = secondET_H;
            SecondET_A = secondET_A;
            PenaltiesShootout_H = penaltiesShootout_H;
            PenaltiesShootout_A = penaltiesShootout_A;
        }
    }
}
