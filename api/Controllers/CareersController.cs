using System.Collections.Generic;
using AutoMapper;
using Forum.Data;
using Forum.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Controllers
{
    [Route("api/careers")]
    [ApiController]
    public class CareersController : ControllerBase
    {
        private readonly ICareersRepository _repository;
        private readonly IMapper _mapper;

        public CareersController(ICareersRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        //GET api/careers
        [HttpGet]
        public ActionResult GetAllCareers()
        {
            return Ok(_mapper.Map<IEnumerable<CareerReadDto>>(_repository.GetAll()));
        }
    }
}