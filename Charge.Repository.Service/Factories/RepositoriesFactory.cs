using Charge.Repository.Service.Repository;
using Charge.Repository.Service.Repository.Entity.Models;
using System;

namespace Charge.Repository.Service.Factories {
    public class RepositoriesFactory {
        private ChargeRepositoryEntity chargeRepositoryEntity;

        public RepositoriesFactory() { }

        public RepositoriesFactory (ChargeRepositoryEntity chargeRepositoryEntity) {
            this.chargeRepositoryEntity = chargeRepositoryEntity;        
        }

        public virtual IChargeRepository GetRespository() {
            return chargeRepositoryEntity;
        }
    }
}
