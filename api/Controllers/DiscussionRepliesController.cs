using System;
using System.Collections.Generic;
using AutoMapper;
using Forum.Data;
using Forum.Dtos;
using Forum.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Controllers
{
    [Route("api/discussionreplies")]
    [ApiController]
    public class DiscussionRepliesController : ControllerBase
    {
        private readonly IForumRepository _repository;
        private readonly IMapper _mapper;

        public DiscussionRepliesController(IForumRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        
        //GET api/discussionreplies
        [HttpGet]
        public ActionResult<IEnumerable<DiscussionReplyReadDto>> GetAllDiscusionReplies(int? did = null)
        {
            var replies = _repository.GetAllDiscussionReplies(did);
            return Ok(_mapper.Map<IEnumerable<DiscussionReplyReadDto>>(replies));
        }

        //GET api/discussionreplies/{id}
        [HttpGet("{id}", Name = "GetDiscussionReplyById")]
        public ActionResult GetDiscussionReplyById(int id)
        {
            var discussionReply = _repository.GetDiscussionReplyById(id);
            
            if(discussionReply is null)
            {
                return NotFound();
            } 

            return Ok(_mapper.Map<DiscussionReplyReadDto>(discussionReply));
        }

        //POST api/discussionreplies
        [Authorize]
        [HttpPost]
        public ActionResult<DiscussionReplyReadDto> CreateDiscussionReply(DiscussionReplyCreateDto discussionReplyCreateDto)
        {
            var discussionReplyModel = _mapper.Map<DiscussionReply>(discussionReplyCreateDto);
            discussionReplyModel.Username = this.User.Identity.Name;
            discussionReplyModel.DiscussionReplyDateTime = DateTime.Now;

            _repository.CreateDiscussionReply(discussionReplyModel);
            _repository.SaveChanges();

            var discussionReplyReadDto = _mapper.Map<DiscussionReplyReadDto>(discussionReplyModel);

            return CreatedAtRoute(nameof(GetDiscussionReplyById), 
                                new {Id = discussionReplyReadDto.DiscussionReplyId}, discussionReplyReadDto);
        }

        //PUT api/discussionreplies
        [Authorize]
        [HttpPut("{id}")]
        public ActionResult UpdateDiscussion(int id, DiscussionReplyUpdateDto updatedDiscussionReply)
        {
            var reply = _repository.GetDiscussionReplyById(id);

            if(reply is null)
            {
                return NotFound();
            }

            if(!reply.Username.Equals(this.User.Identity.Name))
            {
                return Unauthorized();
            }

            _mapper.Map(updatedDiscussionReply, reply);
            _repository.UpdateDiscussionReply(reply);
            _repository.SaveChanges();

            return NoContent();
        }

        //DELETE api/discussionreplies/{id}
        [Authorize]
        [HttpDelete("{id}")]
        public ActionResult DeleteDiscussionReply(int id)
        {
            var reply = _repository.GetDiscussionReplyById(id);
            
            if(reply is null)
            {
                return NotFound();
            }
            
            if(!this.User.Identity.Name.Equals(reply.Username))
            {
                return Unauthorized();
            }
           
            _repository.DeleteDiscussionReply(reply);
            _repository.SaveChanges();

            return NoContent();
        }
    }
}