using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Forum.Data;
using Forum.Dtos;
using Forum.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Predicate = System.Linq.Expressions.Expression<System.Func<Forum.Models.Discussion, bool>>;

namespace Forum.Controllers
{
    [Route("api/discussions")]
    [ApiController]
    public class DiscussionsController : ControllerBase
    {
        private readonly IDiscussionRepository _repository;
        private readonly IMapper _mapper;

        public DiscussionsController(IDiscussionRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        //GET api/discussions
        [HttpGet]
        public ActionResult<IEnumerable<DiscussionReadDto>> GetAllDiscussions(
            string query = "", string sort = "date", string order = "asc", string disctype = null, string career = null,
            int page = 1, int pagesize = 10)
        {
            try 
            {
                VerifySortParameters(sort, order);
            }
            catch(ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            
            var discussions = _repository.GetDiscussionsUsingParameters(
                filters: QueryParametersToPredicates(query, disctype, career),
                sortParam: sort,
                orderAscending: order.ToLower().Equals("asc") ? true : false,
                page: page,
                pageSize: pagesize
            );
            
            var meta = new 
            {
                discussions.TotalCount,
                discussions.PageSize,
                discussions.CurrentPage,
                discussions.PageCount,
                discussions.HasNext,
                discussions.HasPrevious
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(meta));
            
            return Ok(_mapper.Map<IEnumerable<DiscussionReadDto>>(discussions));
        }

        //GET api/discussions/{id}
        [HttpGet("{id}", Name = "GetDiscussionById")]
        public ActionResult<DiscussionReadDto> GetDiscussionById(int id)
        {
            var discussion = _repository.GetById(id);
            if(discussion is null) 
            {
                return NotFound();
            }
            return Ok(_mapper.Map<DiscussionReadDto>(discussion));
        }

        //POST api/discussions
        [Authorize]
        [HttpPost]
        public ActionResult<DiscussionReadDto> CreateDiscussion(DiscussionCreateDto discussionCreateDto)
        {
            var discussion = _mapper.Map<Discussion>(discussionCreateDto);
            discussion.Username = this.User.Identity.Name;
            discussion.DiscussionDateTime = System.DateTime.Now;
            
            _repository.Add(discussion);
            _repository.SaveChanges();
            
            var discussionReadDto = _mapper.Map<DiscussionReadDto>(_repository.GetById(discussion.DiscussionId));

            return CreatedAtRoute(nameof(GetDiscussionById), new {Id = discussionReadDto.DiscussionId}, discussionReadDto);
        }

        //PUT api/discussions/{id}
        [Authorize]
        [HttpPut("{id}")]
        public ActionResult UpdateDiscussion(int id, DiscussionCreateDto discussionUpdateDto)
        {
            var discussionModel = _repository.GetById(id);
            
            if(discussionModel is null)
            {
                return NotFound();
            }

            if(!this.User.Identity.Name.Equals(discussionModel.Username))
            {
                return Unauthorized();
            }
            
            _mapper.Map(discussionUpdateDto, discussionModel);
            _repository.SaveChanges();
            
            return NoContent();
        }

        //PATCH api/discussions/{id}
        [Authorize]
        [HttpPatch("{id}")]
        public ActionResult PartialUpdateDiscussion(int id, JsonPatchDocument<DiscussionCreateDto> patchDocument)
        {
            var discussionModel = _repository.GetById(id);
            
            if(discussionModel is null)
            {
                return NotFound();
            }

            if(!this.User.Identity.Name.Equals(discussionModel.Username))
            {
                return Unauthorized();
            }

            var discussionToPatch = _mapper.Map<DiscussionCreateDto>(discussionModel);
            patchDocument.ApplyTo(discussionToPatch, ModelState);
            if(!TryValidateModel(discussionToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(discussionToPatch, discussionModel);
            _repository.SaveChanges();

            return NoContent();
        }

        //DELETE api/discussions/{id}
        [Authorize]
        [HttpDelete("{id}")]
        public ActionResult DeleteDiscussion(int id)
        {
            var discussion = _repository.GetById(id);
            
            if(discussion is null) 
            {
                return NotFound();
            }
            
            if(!this.User.Identity.Name.Equals(discussion.Username))
            {
                return Unauthorized();
            }

            _repository.Delete(discussion);
            _repository.SaveChanges();

            return NoContent();
        }

        [NonAction]
        private IEnumerable<Predicate> QueryParametersToPredicates(string query, string disctype, string career)
        {
            var filters = new List<Predicate>();
            
            if(!string.IsNullOrWhiteSpace(query))
            {
                filters.Add(QueryStringToPredicate(query));
            }
            if(!string.IsNullOrWhiteSpace(disctype))
            {
                filters.Add(DiscussionTypeParameterToPredicate(disctype));
            }
            if(!string.IsNullOrWhiteSpace(career))
            {
                filters.Add(CareerParameterToPredicate(career));
            }

            return filters;
        }
        [NonAction]
        private Predicate QueryStringToPredicate(string query)
        {
            return d => (d.Title.ToLower().Contains(query) || d.Description.ToLower().Contains(query));
        }
        [NonAction]
        private Predicate DiscussionTypeParameterToPredicate(string disctype)
        {
            var discussionTypes = string.IsNullOrWhiteSpace(disctype) ? new List<int>() 
                                    : disctype.Split(',').Select(int.Parse).ToList();
            
            return d => discussionTypes.Contains(d.DiscussionTypeId);
        }
        [NonAction]
        private Predicate CareerParameterToPredicate(string career)
        {
            var careers = string.IsNullOrWhiteSpace(career) ? new List<int>() 
                            : career.Split(',').Select(int.Parse).ToList();
            
            return d => careers.Contains(d.CareerId);
        }
        [NonAction]
        private void VerifySortParameters(string sortBy, string order)
        {
            if(!sortBy.ToLower().Equals("views") && !sortBy.ToLower().Equals("date"))
            {
                throw new ArgumentException("Invalid sort by argument. Must be 'views' or 'date'.");
            }
            if(!order.ToLower().Equals("asc") && !order.ToLower().Equals("desc"))
            {
                throw new ArgumentException("Invalid order argument. Must be 'asc' or 'desc'");
            }
        }
    }   
}