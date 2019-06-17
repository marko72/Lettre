using System;
using System.Collections.Generic;
using System.Linq;
using Lettre.Application.Commands.User;
using Lettre.Application.DTO.User;
using Lettre.Application.Exceptions;
using Lettre.Application.Searches;
using Lettre.EfDataAccess;
using Microsoft.EntityFrameworkCore;

namespace Lettre.EfCommands.UserCommand
{
    public class EfGetUsersCommand : EfBaseCommand, IGetUsersCommand
    {
        public EfGetUsersCommand(LettreDbContext context) : base(context)
        {
        }

        public IEnumerable<GetUserDto> Execute(UserSearch request)
        {
            var users = Context.Users.AsQueryable();
            if (request.Name != null)
            {
                users = users.Where(u => u.Name == request.Name && u.IsDeleted == false);
            }
            if (request.Email != null)
            {
                users = users.Where(u => u.Email == request.Email && u.IsDeleted == false);
            }
            if (request.Surname != null)
            {
                users = users.Where(u => u.Surname == request.Surname && u.IsDeleted == false);
            }
            if (request.RoleId != 0)
            {
                users = users.Where(u => u.RoleId == request.RoleId && u.IsDeleted == false);
            }
            if (users == null)
            {
                throw new EntityNotFoundException("Korisnik sa tim email-om");
            }

            var totalCount = users.Where(u => u.IsDeleted == false).Count();

            users = users.Skip((request.PageNumber - 1) * request.PerPage)
                .Take(request.PerPage);
            int pageCount = (int)Math.Ceiling((double)totalCount / request.PerPage);

            var usr = users.Include(r => r.Role).Where(u => u.IsDeleted == false).Select(u => new GetUserDto
            {
                Name = u.Name,
                Surname = u.Surname,
                Email = u.Email,
                Id = u.Id,
                RoleName = u.Role.Name
            });

            return usr;
        }
    }
}
