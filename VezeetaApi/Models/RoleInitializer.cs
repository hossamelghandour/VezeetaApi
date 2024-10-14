using Microsoft.AspNetCore.Identity;

namespace VezeetaApi.Models
{
    public class RoleInitializer
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleInitializer(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task InitializerRoles()
        {
            var roleExists = await _roleManager.RoleExistsAsync("Patient");
            if (!roleExists) 
            { 
                // create the role
                var role=new IdentityRole("PATIENT");
                await _roleManager.CreateAsync(role);
            
            }
        }
    }
}
