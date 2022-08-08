using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;
using Project.BillingProcessing.Charge.Api.Application.Interface;
using Project.BillingProcessing.Charge.Domain.Service.Interface;
using Project.BillingProcessing.Charge.Test.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Xunit;

namespace Project.BillingProcessing.Customer.Test;
public class ChargeScenariosTest : IClassFixture<ChargeScenariosBaseTest<Program>>
{
    private readonly Mock<IChargeService> _chargeSericeMock;
    private readonly Mock<IChargeAppService> _chargeAppService;
    private readonly Mock<ILogger> _logger;
    private List<Charge.Domain.ChargeEntity.Charge> Charges;
    private readonly HttpClient _client;

    public ChargeScenariosTest(ChargeScenariosBaseTest<Program> factory)
    {
        _client = factory.CreateClient();
        _chargeSericeMock = new Mock<IChargeService>();
        _chargeAppService = new Mock<IChargeAppService>();
        _logger = new Mock<ILogger>();

        Charges = new List<Charge.Domain.ChargeEntity.Charge>()
            {
              new Charge.Domain.ChargeEntity.Charge(){ Id = "", Identification = 12345678954, DueDate = DateTime.Now},
              new Charge.Domain.ChargeEntity.Charge(){ Id = "", Identification = 12346678954, DueDate = DateTime.Now},
              new Charge.Domain.ChargeEntity.Charge(){ Id = "", Identification = 37514289097, DueDate = DateTime.Now},
              new Charge.Domain.ChargeEntity.Charge(){ Id = "", Identification = 15407855054, DueDate = DateTime.Now},
              new Charge.Domain.ChargeEntity.Charge(){ Id = "", Identification = 62710176068, DueDate = DateTime.Now},
            };
    }

    [Theory]
    [InlineData("375.142.890-97", "2017-3-1")]
    [InlineData("627.101.760-68", "2018-3-1")]
    [InlineData("154.078.550-54", "2019-3-1")]
    public async void Test_Get_Charge_NotFound(string identification, string dateTime)
    {
        //A
        var response = await _client.GetAsync($"api/v1/charge/GetChargeByParameter?identification={identification}&dueDate={DateTime.Parse(dateTime).Date}");
        var status = response.StatusCode;
        var responseString = await response.Content.ReadAsStringAsync();
        //A
        Assert.Equal(System.Net.HttpStatusCode.NotFound, status);
        Assert.Equal("Cobrança não localizada", responseString);
    }

    [Fact]
    public async void Test_Create_Charge_Success()
    {
        //A
        var identification = 62710176068;
        var charge = new Charge.Domain.ChargeEntity.Charge() { ChargeValue = 200.0M, DueDate = DateTime.Now, Identification = identification };
        var jsonContent = JsonSerializer.Serialize(charge);
        var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
        contentString.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        //A
        HttpResponseMessage response = await _client.PostAsync("api/v1/charge/CreateCharge", contentString);
        //A
        Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);       
    }
}
