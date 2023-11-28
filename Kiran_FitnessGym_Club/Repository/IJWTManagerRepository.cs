using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kiran_FitnessGym_Club.Models;
namespace Kiran_FitnessGym_Club.Repository
{
    public interface IJWTManagerRepository
    {
        Tokens Authenticate(MemberRegt members);
    }
}
