using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Charge.Repository.Service.Business.Dtos;
using Charge.Repository.Service.Factories;
using Charge.Repository.Service.swagger;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc.Versioning.Conventions;

namespace Charge.Repository.Service.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ChargesController : ControllerBase {
        private readonly RepositoriesFactory repositoriesFactory;

        public ChargesController(RepositoriesFactory repositoriesFactory) {
            this.repositoriesFactory = repositoriesFactory;
        }

        public static void Convention(ApiVersioningOptions options) {
            options.Conventions.Controller<ChargesController>().HasApiVersions(ApiVersioning.Versions());
        }
        // POST 
        [Route("Add")]
        [HttpPost]
        public async Task<ActionResult> Add(RepositoryCharge charge) {
            bool result = await  repositoriesFactory
                .GetRespository()
                .Add(charge);
            if(result) { return Ok(); }
            throw new Exception("FOR TODO");
        }
    }
}
