using Microsoft.AspNetCore.Mvc;
using VoxData.VoxSurvey.Mongo.Models;
using VoxData.VoxSurvey.Mongo.Repository;

namespace APIInsert.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnswerResponseController : ControllerBase
    {
        private AnswerResponseRepository _repo;
        private AnswerSessionResponseRepository _repoAggregator;
        public AnswerResponseController(AnswerResponseRepository repo, AnswerSessionResponseRepository repoAggregator)
        {
            _repo = repo;
            _repoAggregator = repoAggregator;
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] AnswerResponse req)
        {
            try
            {
                await _repo.InsertRequest(req);
                return Ok();
            } catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] AnswerResponse req)
        {
            try
            {
                await _repo.Update(req);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Aggregate([FromQuery] GroupReq req)
        {
            try
            {
                var retorno = _repoAggregator.GetAnswerSessionResponseGrouped(req.DateStart, req.DateEnd, req.idEvaluation, "D");
                return Ok(retorno);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("new")]
        public async Task<IActionResult> NewAggregate([FromQuery] GroupReq req)
        {
            try
            {
                var retorno = await _repo.GetGroup(req.DateStart, req.DateEnd, req.idEvaluation, "D");
                return Ok(retorno);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }

    public class GroupReq
    {
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public long idEvaluation { get; set; }
    } 
}
