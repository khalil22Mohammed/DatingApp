using FirstApp.Entities;
using FirstApp.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FirstApp.Data
{
    public class MemberRepository(AppDbContext context) : IMemberRepository
    {
        public async Task<Member?> GetMemberByIdAsync(string id)
        {
            return await context.Members.FindAsync(id);
        }

        public async Task<IReadOnlyList<Member>> GetMembersAsync()
        {
           return await context.Members.ToListAsync();
        }

        public async Task<IReadOnlyList<Photo>> GetPhotosForMemberAsync(string memberId)
        {
            return await context.Photos
                .Where(p => p.MemberId == memberId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IReadOnlyList<object>> GetAllPhotoDetailsAsync()
        {
            return await context.Photos
                .Join(
                    context.Members,
                    p => p.MemberId,
                    m => m.Id,
                    (p, m) => new
                    {
                        Id = p.id,
                        Url = p.Url,
                        PoblicId = p.PoblicId,
                        MemberId = m.Id,
                        MemberDisplayName = m.DisplayName,
                        MemberImageUrl = m.ImageUrl
                    }
                )
                .AsNoTracking()
                .Cast<object>()
                .ToListAsync();
        }
        

        public async Task<bool> SaveAllAsync()
        {
            return await context.SaveChangesAsync() > 0;
        }

        public void Update(Member member)
        {
            context.Entry(member).State = EntityState.Modified;
        }
    }
}
