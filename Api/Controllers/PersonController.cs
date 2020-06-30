using System.Threading.Tasks;
using DevExpressGroupingExample.Persistence;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly ILogger<PersonController> _logger;

        public PersonController(AppDbContext appDbContext, ILogger<PersonController> logger)
        {
            _db = appDbContext;
            _logger = logger;
        }
        
        [HttpPost]
        public Task<LoadResult> GetAll([FromBody] Request request)
        {
            _logger.LogInformation($"Request received: {request.Options}");
            return DataSourceLoader.LoadAsync(_db.Persons, request.Options);
        }

        public class Request
        {
            public DataSourceLoadOptions Options { get; set; }
        }
    }
}