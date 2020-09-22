using Charge.Repository.Service.Factories;
using Charge.Repository.Service.Repository;
using Charge.Repository.Service.Repository.Entity.Models;
using NSubstitute;
using System;
using System.Data;

namespace Charge.Repository.Service.Controller.Test.Mocks {
    public class RepositoriesFactoryMock {
        public static RepositoriesFactory Instance { get; }

        static RepositoriesFactoryMock() {
            Instance = Substitute.For<RepositoriesFactory>();
        }

        public IChargeRepository GetRespository() {
            return Instance.GetRespository();            
        }

        internal static void CreateAddRepository(ChargeRepositoryEntity chargeRepositoryEntity) {
            Instance.GetRespository().Returns(chargeRepositoryEntity);
        }
    }
}
