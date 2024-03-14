using AutoMapper;
using JokesAPI.Models;
using JokesAPI.DTOs;

namespace JokesAPI.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            // Jokes
            CreateMap<Joke, JokeDto>();
            //CreateMap<JokeDto, Joke>();
            CreateMap<CreateJokeDto, Joke>();
            CreateMap<Joke, FatJokeDto>();

            // Joke Categories
            CreateMap<JokeCategory, JokeCategoryDto>();
            CreateMap<JokeCategory, SlimJokeCategoryDto>();
            CreateMap<CreateJokeCategoryDto, JokeCategory>();

        }
    }
}
