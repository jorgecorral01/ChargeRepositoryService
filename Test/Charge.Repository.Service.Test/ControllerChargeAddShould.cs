using Charge.Repository.Service.Business.Dtos;
using Charge.Repository.Service.Controller.Test.Mocks;
using Charge.Repository.Service.Repository;
using FluentAssertions;
using Microsoft.AspNetCore.Razor.Language;
using Newtonsoft.Json;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Charge.Repository.Service.Controller.Test {
    [TestFixture]
    public class ControllerChargeAddShould {
        [Test]
        public async Task given_data_for_add_new_charge_we_obtein_an_ok_response() {
            RepositoryCharge newCharge = GivenARepositoryCharge();
            HttpClient client = TestFixture.HttpClient;
            var requestUri = "http://localhost:10001/api/charges/Add";
            var content = GivenAHttpContent(newCharge, requestUri);
            ChargeRepositoryEntity chargeRepositoryEntity = GivenArepositoryMock();
            ReturnTrueForRepositoryMock(newCharge, chargeRepositoryEntity);
            RepositoriesFactoryMock.CreateAddRepository(chargeRepositoryEntity);

            var result = await client.PostAsync(requestUri, content);

            result.StatusCode.Should().Be(HttpStatusCode.OK);
            await chargeRepositoryEntity.Received(1).Add(Arg.Is<RepositoryCharge>(item => item.Description == newCharge.Description && item.Amount == newCharge.Amount && item.identifier == newCharge.identifier));
        }

        private void ReturnTrueForRepositoryMock(RepositoryCharge newCharge, ChargeRepositoryEntity chargeRepositoryEntity) {
            chargeRepositoryEntity.Add(Arg.Is<RepositoryCharge>(item => item.Description == newCharge.Description && item.Amount == newCharge.Amount && item.identifier == newCharge.identifier)).Returns(true);
        }

        private static ChargeRepositoryEntity GivenArepositoryMock() {
            return Substitute.For<ChargeRepositoryEntity>(new object[] { null });
        }

        private static RepositoryCharge GivenARepositoryCharge() {
            return new RepositoryCharge { Description = "Nuevo cobro", Amount = 1000, identifier = "anyIdentifier" };
        }

        private static HttpContent GivenAHttpContent(RepositoryCharge repositoryCharge, string requestUri) {
            string jsonVideo = JsonConvert.SerializeObject(repositoryCharge, Formatting.Indented);
            HttpContent content = new StringContent(jsonVideo, Encoding.UTF8, "application/json");
            HttpRequestMessage request = new HttpRequestMessage {
                Method = HttpMethod.Post,
                RequestUri = new Uri(requestUri),
                Content = content
            };
            return content;
        }
    }
}

