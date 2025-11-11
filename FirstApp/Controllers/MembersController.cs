using FirstApp.Entities;
using FirstApp.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FirstApp.Controllers
{
    [Authorize]
    public class MembersController(IMemberRepository memberRepository) : BaesAPIController
    {
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Member>>> GetMembers()
        {
            return Ok(await memberRepository.GetMembersAsync());

        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<Member>> GetMember(string id)
        {
            var members = await memberRepository.GetMemberByIdAsync(id);
            if (members == null) return NotFound();
            return members;

        }

        [HttpGet("{id}/photos")]
        public async Task<ActionResult<IReadOnlyList<object>>> GetPhotosForMember(string id)
        {
            var member = await memberRepository.GetMemberByIdAsync(id);
            if (member == null) return NotFound();

            var photos = await memberRepository.GetPhotosForMemberAsync(id);
            var result = photos.Select(p => new
            {
                Id = p.id,
                Url = p.Url,
                PoblicId = p.PoblicId,
                MemberId = member.Id,
                MemberDisplayName = member.DisplayName,
                MemberImageUrl = member.ImageUrl
            }).ToList();

            return Ok(result);
        }

        // Returns all photo records with member info directly from SQL Server
        [HttpGet("photos")]
        public async Task<ActionResult<IReadOnlyList<object>>> GetAllPhotos()
        {
            var results = await memberRepository.GetAllPhotoDetailsAsync();
            return Ok(results);
        }
    }
}