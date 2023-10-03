using Soccer.Models;

namespace Soccer.Services
{
    public class ServiceManagement : IServiceManagement
    {
        ResultBuilder resultBuilder;

        public ServiceManagement()
        {
            resultBuilder = new ResultBuilder("https://bti-results.bsportsasia.com/?ns=prod20082-23705321.bti-sports.io&locale=en&tzoffset=8");
        }
        public void GenerateResult()
        {
            List<Result> results = resultBuilder.GenerateResults();
            foreach (Result result in results)
            {
                Console.WriteLine("------------------------------------");
                Console.WriteLine(result.GameTime);
                Console.WriteLine(result.Leagues);
                Console.WriteLine(result.HomeTeam + " vs " + result.AwayTeam);
                if (result.Status == 1)
                {
                    Console.WriteLine(result.HomeScore);
                    Console.WriteLine(result.AwayScore);
                    Console.WriteLine("");
                    Console.WriteLine("Detail:");
                    Console.WriteLine(result.Detail.Teams[0] + " teams " + result.Detail.Teams[1]);
                    Console.WriteLine(result.Detail.FirstHalf[0] + " firstHalf " + result.Detail.FirstHalf[1]);
                    Console.WriteLine(result.Detail.SecondHalf[0] + " secondHalf " + result.Detail.SecondHalf[1]);
                    Console.WriteLine(result.Detail.RegularTime[0] + " regularTime " + result.Detail.RegularTime[1]);
                    Console.WriteLine(result.Detail.Corners[0] + " corners " + result.Detail.Corners[1]);
                    Console.WriteLine(result.Detail.Penalties[0] + " penalties " + result.Detail.Penalties[1]);
                    Console.WriteLine(result.Detail.YellowCards[0] + " yellowCards " + result.Detail.YellowCards[1]);
                    Console.WriteLine(result.Detail.RedCards[0] + " redCards " + result.Detail.RedCards[1]);
                    Console.WriteLine(result.Detail.FirstHalf[0] + " firstET " + result.Detail.FirstHalf[1]);
                    Console.WriteLine(result.Detail.SecondHalf[0] + " secondET " + result.Detail.SecondHalf[1]);
                    Console.WriteLine(result.Detail.PenaltiesShootout[0] + " penaltiesShootout " + result.Detail.PenaltiesShootout[1]);
                }
                else if (result.Status == 0)
                {
                    Console.WriteLine("");
                    Console.WriteLine("Cancelled");
                }
                Console.WriteLine("------------------------------------");
            }
        }

        public void UpdateDatabase()
        {
            Console.WriteLine("UpdateDatabase.....");
        }
    }
}
