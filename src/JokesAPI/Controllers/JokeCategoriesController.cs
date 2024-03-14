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
using JokesAPI.Exceptions;
using JokesAPI.Interface;

namespace JokesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JokeCategoriesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IJokeCategoryService _jokeCategoryService;

        public JokeCategoriesController(IMapper mapper, IJokeCategoryService jokeCategoryService)
        {
            _mapper = mapper;
            _jokeCategoryService = jokeCategoryService;
        }

        // GET: api/JokeCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JokeCategoryDto>>> GetJokeCategories()
        {
            var jokeCategories = await _jokeCategoryService.All();

            var jokeCategoriesDto = _mapper.Map<IEnumerable<JokeCategoryDto>>(jokeCategories);

            return Ok(jokeCategoriesDto);
        }

        // GET: api/JokeCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<JokeCategoryDto>> GetJokeCategory(long id)
        {
            var jokeCategory = await _jokeCategoryService.Get(id);

            var jokeCategoryDto = _mapper.Map<JokeCategoryDto>(jokeCategory);

            return Ok(jokeCategoryDto);
        }


        // POST: api/JokeCategories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<JokeCategory>> PostJokeCategory(CreateJokeCategoryDto createJokeCategoryDto)
        {
            var jokeCategory = await _jokeCategoryService.Add(createJokeCategoryDto);

            var jokeCategoryDto = _mapper.Map<JokeCategoryDto>(jokeCategory);

            return CreatedAtAction("GetJokeCategory", new { id = jokeCategoryDto.Id }, jokeCategoryDto);
        }

        // DELETE: api/JokeCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJokeCategory(long id)
        {

            await _jokeCategoryService.Delete(id);

            return NoContent();
        }

        // GET: api/JokeCategories/engineering/RandomJoke
        [HttpGet("{category}/RandomJoke")]
        public async Task<ActionResult<JokeDto>> GetRandomJoke(string category)
        {
            var joke = await _jokeCategoryService.GetRandomJoke(category);

            var jokeDto = _mapper.Map<JokeDto>(joke);

            return Ok(jokeDto);
        }


        // GET: api/JokeCategories/engineering/AllJokes
        /// <summary>
        /// Returns all the Jokes for a Paticular Category.
        /// </summary>
        [HttpGet("{category}/AllJokes")]
        public async Task<ActionResult<IEnumerable<JokeDto>>> GetAllJokesForCategory(string category)
        {
            var jokes = await _jokeCategoryService.AllJokes(category);

            var jokesDto = _mapper.Map<IEnumerable<JokeDto>>(jokes);

            return Ok(jokesDto);

        }

        // POST: api/JokeCategories/engineering/NewJoke
        /// <summary>
        /// Creates a New Joke for a Paticular Category.
        /// </summary>
        [HttpPost("{category}/NewJoke")]
        public async Task<ActionResult<JokeDto>> CreateNewJoke([FromRoute] string category, [FromBody] CreateJokeDto createJokeDto)
        {

            var joke = await _jokeCategoryService.AddNewJoke(category, createJokeDto);

            var jokeDto = _mapper.Map<JokeDto>(joke);

            return CreatedAtAction(nameof(JokesController.GetJoke), "Jokes", new { id = jokeDto.Id }, jokeDto);

        }

        // PATCH: api/JokeCategories/engineering/Add
        /// <summary>
        /// Adds a Category to an existing Joke.
        /// </summary>
        [HttpPatch("{category}/Joke/{jokeId}/Add")]

        public async Task<ActionResult<JokeDto>> AddCategoryToJoke([FromRoute] string category, [FromRoute] long jokeId)
        {
            var joke = await _jokeCategoryService.AddExistingJoke(category, jokeId);

            var jokeDto = _mapper.Map<JokeDto>(joke);

            return Ok(jokeDto);
        }

        // PATCH: api/JokeCategories/engineering/Remove
        /// <summary>
        /// Removes a Category from an existing Joke.
        /// </summary>
        [HttpPatch("{category}/Joke/{jokeId}/Remove")]
        public async Task<ActionResult<JokeDto>> RemoveCategoryFromJoke([FromRoute] string category, [FromRoute] long jokeId)
        {
            var joke = await _jokeCategoryService.RemoveJoke(category, jokeId);

            var jokeDto = _mapper.Map<JokeDto>(joke);

            return Ok(jokeDto);
        }

    }
}
