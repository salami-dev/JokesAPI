using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JokesAPI.Models;
using AutoMapper;
using JokesAPI.DTOs;
using JokesAPI.Interface;

namespace JokesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JokesController : ControllerBase
    {

        private readonly ILogger<JokesController> _logger;
        private readonly IMapper _mapper;
        private readonly IJokeService _jokeService;

        public JokesController(ILogger<JokesController> logger, IJokeService jokeService, IMapper mapper)
        {
            _logger = logger;
            _jokeService = jokeService;
            _mapper = mapper;
        }

        // GET: api/Jokes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JokeDto>>> GetJokes()
        {
            var jokes = await _jokeService.All();

            var jokesDto = _mapper.Map<IEnumerable<JokeDto>>(jokes);

            return Ok(jokesDto);
        }


        // GET: api/Jokes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FatJokeDto>> GetJoke(long id)
        {
            var joke = await _jokeService.Get(id);

            var jokeDto = _mapper.Map<FatJokeDto>(joke);

            return jokeDto;
        }


        // POST: api/Jokes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<JokeDto>> PostJoke(CreateJokeDto createJokeDto)
        {
            var joke = await _jokeService.Add(createJokeDto);

            var jokeDto = _mapper.Map<JokeDto>(joke);

            return CreatedAtAction("GetJoke", new { id = jokeDto.Id }, jokeDto);
        }

        // DELETE: api/Jokes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJoke(long id)
        {
            await _jokeService.Delete(id);

            return NoContent();
        }

        // GET: api/Jokes/Random
        /// <summary>
        /// Return a Random Joke.
        /// </summary>
        [HttpGet("Random")]
        public async Task<ActionResult<JokeDto>> GetRandomJoke()
        {
            var joke = await _jokeService.GetRandomJoke();

            var jokeDto = _mapper.Map<JokeDto>(joke);

            return jokeDto;
        }

        /// <summary>
        /// Like or Dislike a Joke.
        /// </summary>
        [HttpPost("{id}/React")]
        public async Task<ActionResult> ReactToJoke([FromRoute]long id, [FromBody]ReactToJokeDto reaction)
        {
            if (reaction.Action == ReactToJokeDto.ACTION_CHOICES.NONE)
            {
                return BadRequest(new { error = "Action must either be 1 (LIKE) or DISLIKE (2)" });
            }
            bool success = await _jokeService.ReactToJoke(id, reaction);

            if (!success)
            {
                return BadRequest();
            }

            return NoContent();

        }

    }
}
