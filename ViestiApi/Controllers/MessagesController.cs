using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ViestiApi.Models;
using ViestiApi.Services;

namespace ViestiApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessagesController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpGet("public")] // GET: api/messages/public
            public async Task<ActionResult<List<MessageDTO>>> GetPublicMessages()
        {
             var messages = await _messageService.GetPublicMessagesAsync();
            return Ok(messages);
        }

        [HttpGet("user/{userId}")] // GET: api/messages/user/{userId})]
        public async Task<ActionResult<List<MessageDTO>>> GetMessagesForUser(int userId)
        {
            var messages = await _messageService.GetMessagesForUserAsync(userId);
            return Ok(messages);
        }

        [HttpGet("{id}")] // GET: api/messages/{id})]
        public async Task<ActionResult<MessageDTO>> GetMessage(long id, int userId) // GET: api/messages/{id}?userId={userId}
        {
            var message = await _messageService.GetMessageAsync(id, userId);
            if (message == null)
            {
                return NotFound(); // viestiä ei ole tai käyttäjä ei saa nähdä sitä
            }
            return Ok(message);
        }

        [HttpPost]
        public async Task<ActionResult<MessageDTO>> PostMessage(MessageDTO newMessage) // POST: api/messages
        {
            var message = await _messageService.SendMessageAsync(newMessage);
            if (message == null)
            {
                return BadRequest("Edellistä viestiä ei löytynyt"); // Jos edellistä viestiä ei löytynyt, palautetaan BadRequest
            }
            return CreatedAtAction(nameof(GetMessage), new { id = message.Id, userId = message.SenderId }, message); // Palautetaan luotu viesti ja sen sijainti
        }

        [HttpPut("{id}")] // PUT: api/messages/{id}
        public async Task<IActionResult> PutMessage(long id, int userId, MessageDTO message)
        {
            if (id != message.Id)
            {
                return BadRequest(); // Jos id ei vastaa viestin id:tä, palautetaan BadRequest
            }

            bool updated = await _messageService.UpdateMessageAsync(message, userId); // Päivitetään viesti
            if (!updated)
            {
                return NotFound(); // Jos viestiä ei löytynyt tai käyttäjä ei saa muokata sitä, palautetaan NotFound
            }
            return NoContent(); // Palautetaan NoContent, koska päivitys onnistui
        }

        [HttpDelete("{id}")] // DELETE: api/messages/{id}
        public async Task<IActionResult> DeleteMessage(long id, int userId)
        {
            bool deleted = await _messageService.DeleteMessageAsync(id, userId); // Poistetaan viesti
            if (!deleted)
            {
                return NotFound(); // Jos viestiä ei löytynyt tai käyttäjä ei saa poistaa sitä, palautetaan NotFound
            }
            return NoContent(); // Palautetaan NoContent, koska poisto onnistui

        }


        }
}
