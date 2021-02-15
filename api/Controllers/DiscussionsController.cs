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
        ///<param name = "q"> title query string  </param>
        ///<param name = "s"> sort by param; should be "views" or "date" </param>
        ///<param name = "o"> sorting order; should be "asc" for ascending or "desc" for descending </param>
        ///<param name = "dt"> discussion type filter; for multiple types use "," to separate them; eg: dt=1,2 </param>
        ///<param name = "cr"> career filter; for multiple careers use "," to separate them; eg: cr=1,2 </param>
        ///<param name = "p"> page </param>
        ///<param name = "ps"> page size </param>
        ///<param name = "e"> whether or not to embed the discussion replies </param>
        public ActionResult<IEnumerable<DiscussionReadDto>> GetAllDiscussions(string q = "", string s = "views", string o = "asc", 
                                                            string dt = null, string cr = null, int p = 1, int ps = 10,
                                                            string e = "false")

        {
            var careers = string.IsNullOrWhiteSpace(cr) ? new List<int>() : cr.Split(',').Select(int.Parse).ToList();
            var discussionTypes = string.IsNullOrWhiteSpace(dt) ? new List<int>() : dt.Split(',').Select(int.Parse).ToList();

            var discussions = _repository.GetAllDiscussions(query: q, sortBy: s, sortOrder: o, 
                                                            discussionTypes: discussionTypes, careers: careers,
                                                            page: p, pageSize: ps, embed: e);
            
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