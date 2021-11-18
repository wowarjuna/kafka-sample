using Kafka.Common;
using Kafka.Producer.Api.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kafka.Producer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KafkaController : ControllerBase
    {
        private readonly KafkaProducerService _producerService;

        public KafkaController(KafkaProducerService producerService)
        {
            _producerService = producerService;
        }


        [HttpPost]
        public async Task<IActionResult> Post()
        {
            await _producerService.SendMessage(new User("Arjuna", "Dratharashtra"));
            return Ok();
        }
    }
}
