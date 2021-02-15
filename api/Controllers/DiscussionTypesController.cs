using System.Collections.Generic;
using AutoMapper;
using Forum.Data;
using Forum.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Controllers
{   
    [Route("api/discussiontypes")]
    [ApiController]
    public class DiscussionTypesController : ControllerBase
    {
        private readonly IDiscussionTypesRepository _repository;
        private readonly IMapper _mapper;

        public DiscussionTypesController(IDiscussionTypesRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        //GET api/discussiontypes
        [HttpGet]
        public ActionResult GetAllDiscussionTypes()
        {
            return Ok(_mapper.Map<IEnumerable<DiscussionTypeReadDto>>(_repository.GetAll()));
        }
    }
}