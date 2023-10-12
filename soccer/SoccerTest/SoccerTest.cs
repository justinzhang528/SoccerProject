using Xunit.Abstractions;
using Soccer.Repository.Models;
using Microsoft.Extensions.Configuration;
using Soccer.Service.Implementation;
using Soccer.Service.Interface;

namespace SoccerTest
{
    public class SoccerTest
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly IConfiguration _configuration;
        private ISBOMatchResultBuilder _sBOMatchResultBuilder;

        public SoccerTest(ITestOutputHelper testOutputHelper)
        {
            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            _testOutputHelper = testOutputHelper;
            _sBOMatchResultBuilder = new SBOMatchResultBuilder(_configuration);
        }

        //[Fact]
        public void GenerateSBOMatchResultTest()
        {
            _sBOMatchResultBuilder.Build();
            List<SBOMatchResultModel> sBOMatchResultModels = _sBOMatchResultBuilder.GetSBOMatchResults();
            _testOutputHelper.WriteLine(sBOMatchResultModels.Count.ToString());
            foreach (var item in sBOMatchResultModels)
            {
                _testOutputHelper.WriteLine(item.Id);
                _testOutputHelper.WriteLine(item.League);
                _testOutputHelper.WriteLine(item.HomeTeam);
                _testOutputHelper.WriteLine(item.AwayTeam);
                _testOutputHelper.WriteLine(item.HomeFirstHalfScore);
                _testOutputHelper.WriteLine(item.AwayFirstHalfScore);
                _testOutputHelper.WriteLine(item.HomeFullTimeScore);
                _testOutputHelper.WriteLine(item.AwayFullTimeScore);
                _testOutputHelper.WriteLine("------------------------------------");
            }
            Console.WriteLine("Hello");
            Assert.True(true);
        }

        [Fact]
        public void GenerateSBOMatchDetailTest()
        {
            _sBOMatchResultBuilder.Build();
            List<SBOMatchDetailModel> sBOMatchDetailModels = _sBOMatchResultBuilder.GetSBOMatchDetails();
            _testOutputHelper.WriteLine(sBOMatchDetailModels.Count.ToString());
            foreach (var item in sBOMatchDetailModels)
            {
                _testOutputHelper.WriteLine(item.Id);
                _testOutputHelper.WriteLine(item.MarketType);
                _testOutputHelper.WriteLine(item.HomeFirstHalfScore);
                _testOutputHelper.WriteLine(item.AwayFirstHalfScore);
                _testOutputHelper.WriteLine(item.HomeFullTimeScore);
                _testOutputHelper.WriteLine(item.AwayFullTimeScore);
                _testOutputHelper.WriteLine(item.Code);
                _testOutputHelper.WriteLine("------------------------------------");
            }
            Assert.True(true);
        }
    }
}