using AssignmateFunctional.API.Entities;
using AssignmateFunctional.Common.DTO;

namespace AssignmateFunctional.DAL.DAL.Factory
{
    public static class EntityToDtoFactory
    {

        public static UserStoreDto GetUserStore(this AssignmateUser entity)
        {
            return new UserStoreDto()
            {
                id = entity.Id,
                email = entity.Email,
                name = $"{entity.FirstName}{entity.LastName}",
                role = entity.Role.ToString(),
            };
        }
    }
}
