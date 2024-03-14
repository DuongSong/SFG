using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SFG.Core.Kafka;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SFG.WebApi.Controllers
{
    [ApiController]
    [Route("sfg/test")]
    public class TestController : ControllerBase
    {
        public TestController() { }

        [HttpGet("test")]
        public IActionResult Abc()
        {

            //KafkaProducer kafkaProducer = new KafkaProducer();

            //for (int i = 0; i < 1000; i++)
            //{
            //    await kafkaProducer.ProduceAsync("DuongSong-Topic",  i.ToString());
            //    //await kafkaProducer.ProduceAsync("DuongSong-Topic", i.ToString());
            //    await Task.Delay(5000);
            //}
                

            
            return Ok("fsf");
        }
    }
}

