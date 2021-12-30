using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EmployeeManagment.Models
{
    public static class ClaimsStore
    {
        public static List<Claim> AllClaims = new()
        {
            new Claim ("Create Role" ,"Create Role"),
            new Claim ("Delete Role" ,"Delete Role"),
            new Claim ("Edit Role" ,"Edit Role")

        };
    }
}
