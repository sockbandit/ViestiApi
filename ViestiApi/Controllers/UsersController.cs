using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ViestiApi.Models;
using ViestiApi.Services;

namespace ViestiApi.Controllers
{
    [Route("api/[controller]")] // määritellään reitti, joka ohjaa pyyntöjä tähän kontrolleriin
    [ApiController]
    public class UsersController : ControllerBase // määritellään UsersController-luokka, joka perii ControllerBase-luokan
    {
        private readonly IUserService _userService; 

        public UsersController(IUserService userService) // injektoidaan IUserService riippuvuus konstruktorin kautta
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserDTO>>> GetUsers() // haetaan kaikki käyttäjät
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users); // palautetaan 200 OK ja käyttäjien lista
        }

        [HttpGet("{id}")] 
        public async Task<ActionResult<UserDTO>> GetUser(int id) // haetaan käyttäjä id:n perusteella
        {
            var user = await _userService.GetUserAsync(id);
            if (user == null)
            {
                return NotFound();  // palautetaan 404 Not Found, jos käyttäjää ei löydy
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<UserDTO>> PostUser(UserCreateDTO newUser) // luodaan uusi käyttäjäp
        {
            var user = await _userService.CreateUserAsync(newUser);
            if (user == null)
            {
                return BadRequest("Käyttäjätunnus on jo käytössä"); // palautetaan 400 Bad Request, jos käyttäjätunnus on jo käytössä
            }
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user); // palautetaan 201 Created ja uuden käyttäjän tiedot
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, UserDTO user)
        {
            if (id != user.Id)
            {
                return BadRequest(); // palautetaan 400 Bad Request, jos id ei täsmää
            }

            bool updated = await _userService.UpdateUserAsync(user);
            if (!updated) 
            {
                return NotFound(); // palautetaan 404 Not Found, jos käyttäjää ei löydy
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            bool deleted = await _userService.DeleteUserAsync(id);
            if (!deleted)
            {
                return NotFound(); // palautetaan 404 Not Found, jos käyttäjää ei löydy
            }
            return NoContent();
        }

    }
}
