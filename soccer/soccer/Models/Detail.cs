namespace Soccer.Models
{
    public class Detail
    {
        public string Id { get; set; }
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

        public Detail(string id, string[] firstHalf, string[] secondHalf, string[] regularTime, string[] corners, string[] penalties, string[] yellowCards, string[] redCards, string[] firstET, string[] secondET, string[] penaltiesShootout)
        {
            Id = id;
            FirstHalf_H = firstHalf[0];
            FirstHalf_A = firstHalf[1];
            SecondHalf_H = secondHalf[0];
            SecondHalf_A = secondHalf[1];
            RegularTime_H = regularTime[0];
            RegularTime_A = regularTime[1];
            Corners_H = corners[0];
            Corners_A = corners[1];
            Penalties_H = penalties[0];
            Penalties_A = penalties[1];
            YellowCards_H = yellowCards[0];
            YellowCards_A = yellowCards[1];
            RedCards_H = redCards[0];
            RedCards_A = redCards[1];
            FirstET_H = firstET[0];
            FirstET_A = firstET[1];
            SecondET_H = secondET[0];
            SecondET_A = secondET[1];
            PenaltiesShootout_H = penaltiesShootout[0];
            PenaltiesShootout_A = penaltiesShootout[1];
        }
    }
}
