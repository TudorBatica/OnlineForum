using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Forum.Data;
using Forum.Dtos;
using Forum.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Controllers
{
    [Route("api/discussions")]
    [ApiController]
    public class DiscussionsController : ControllerBase
    {
        private readonly IForumRepository _repository;
        private readonly IMapper _mapper;

        public DiscussionsController(IForumRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        //GET api/discussions
        [HttpGet]
        public ActionResult<IEnumerable<DiscussionReadDto>> GetAllDiscussions(string query = "", string sort = "views", string order = "asc", 
                                                            string disctype = null, string career = null, int page = 1, int pagesize = 10,
                                                            string embed = "false")

        {
            var careers = string.IsNullOrWhiteSpace(career) ? new List<int>() : career.Split(',').Select(int.Parse).ToList();
            var discussionTypes = string.IsNullOrWhiteSpace(disctype) ? new List<int>() : disctype.Split(',').Select(int.Parse).ToList();

            var discussions = _repository.GetAllDiscussions(query, sort, order, discussionTypes, careers,
                                                            page, pagesize, embed);
            
            return Ok(_mapper.Map<IEnumerable<DiscussionReadDto>>(discussions));
        }

        //GET api/discussions/{id}
        [HttpGet("{id}", Name = "GetDiscussionById")]
        public ActionResult<DiscussionReadDto> GetDiscussionById(int id, string e = "true")
        {
            var discussion = _repository.GetDiscussionById(id, embed: e);
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
            var discussionModel = _mapper.Map<Discussion>(discussionCreateDto);
            discussionModel.Username = this.User.Identity.Name;
            discussionModel.DiscussionDateTime = System.DateTime.Now;
            
            _repository.CreateDiscussion(discussionModel);
            _repository.SaveChanges();
            
            var discussionReadDto = _mapper.Map<DiscussionReadDto>(_repository.GetDiscussionById(discussionModel.DiscussionId, "false"));

            return CreatedAtRoute(nameof(GetDiscussionById), new {Id = discussionReadDto.DiscussionId}, discussionReadDto);
        }

        //PUT api/discussions/{id}
        [Authorize]
        [HttpPut("{id}")]
        public ActionResult UpdateDiscussion(int id, DiscussionCreateDto discussionUpdateDto)
        {
            var discussionModel = _repository.GetDiscussionById(id, "false");
            
            if(discussionModel is null)
            {
                return NotFound();
            }

            if(!this.User.Identity.Name.Equals(discussionModel.Username))
            {
                return Unauthorized();
            }
            
            _mapper.Map(discussionUpdateDto, discussionModel);
            _repository.UpdateDiscussion(discussionModel);
            _repository.SaveChanges();
            
            return NoContent();
        }

        //PATCH api/discussions/{id}
        [Authorize]
        [HttpPatch("{id}")]
        public ActionResult PartialUpdateDiscussion(int id, JsonPatchDocument<DiscussionCreateDto> patchDocument)
        {
            var discussionModel = _repository.GetDiscussionById(id, "false");
            
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
            _repository.UpdateDiscussion(discussionModel);
            _repository.SaveChanges();

            return NoContent();
        }

        //DELETE api/discussions/{id}
        [Authorize]
        [HttpDelete("{id}")]
        public ActionResult DeleteDiscussion(int id)
        {
            var discussion = _repository.GetDiscussionById(id, "false");
            
            if(discussion is null) 
            {
                return NotFound();
            }
            
            if(!this.User.Identity.Name.Equals(discussion.Username))
            {
                return Unauthorized();
            }

            _repository.DeleteDiscussion(discussion);
            _repository.SaveChanges();

            return NoContent();
        }
    }   
}