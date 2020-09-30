//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Discord.Api.Data;
//using Discord.Core.Models;
//using MongoDB.Driver;
//using System.Security.Claims;
//using Discord.Api.Data.Repositories;

//namespace Discord.Api.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class MessageController : ControllerBase
//    {
//        private readonly IMongoCollection<MongoMessages> _mongoMess;
//        private readonly DataContext _context;
//        private readonly IMongoDatabase database;
//        private readonly QueryRepository _query;

//        public MessageController(DataContext context, IMongoSettings settings, QueryRepository query)
//        {
//            _context = context;
//            _query = query;
//            var client = new MongoClient(settings.ConnectionString);
//            database = client.GetDatabase(settings.DatabaseName);

//            _mongoMess = database.GetCollection<MongoMessages>("DiscordMessages");
//        }

//        [HttpGet("getyourmessage/{id}")]
//        public async Task<ActionResult<Message>> GetYourMessage(string id)
//        {

//            //string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
//            string userId = "c9f9e43c-0a02-4909-b2b2-cf68a84dcdc0";
//            string query = "DiscordMessages:" + "_id:" + userId;
//            MongoMessages messages = await _query.GetSingle(new MongoMessages(), new MongoMessages(), query);
//            Message message = messages.Messages.FirstOrDefault(x => x.Id == id);

//            return message;
//        }
//        [HttpGet("getmessage/{id}")]
//        public async Task<ActionResult<Message>> GetMessage(string id)
//        {

//            //string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
//            string query = "DiscordMessages:" + "Messages._id:" + id;
//            MongoMessages messages = await _query.GetSingle(new MongoMessages(), new MongoMessages(), query);
//            Message message = messages.Messages.FirstOrDefault(x => x.Id == id);

//            return message;
//        }

//        [HttpGet("getmessages/{belongsTo}")]
//        public async Task<ActionResult<List<Message>>> GetMessages(string belongsTo)
//        {
//            var collection = database.GetCollection<MongoMessages>("DiscordMessages");
//            List<Message> messages = new List<Message>();
//            var filter = Builders<MongoMessages>.Filter.Eq("_id", belongsTo);
//            MongoMessages mMessages = await collection.Find(filter).FirstOrDefaultAsync();
//            messages = mMessages.Messages.OrderBy(x => x.CreatedAt).Take(20).ToList();

//            return messages;
//        }

//        //[HttpPut("{id}")]
//        //public async Task<IActionResult> PutMessage(string id, Message message)
//        //{
//        //    if (id != message.Id)
//        //    {
//        //        return BadRequest();
//        //    }

//        //    _context.Entry(message).State = EntityState.Modified;

//        //    try
//        //    {
//        //        await _context.SaveChangesAsync();
//        //    }
//        //    catch (DbUpdateConcurrencyException)
//        //    {
//        //        if (!MessageExists(id))
//        //        {
//        //            return NotFound();
//        //        }
//        //        else
//        //        {
//        //            throw;
//        //        }
//        //    }

//        //    return NoContent();
//        //}

//        [HttpPost]
//        public async Task<ActionResult<Message>> PostMessage(Message message)
//        {

//            var filter = Builders<MongoMessages>.Filter.Eq("_id", message.BelongsTo);

//            var record = await _mongoMess.Find(filter).FirstOrDefaultAsync();
//            if(record != null)
//            {
//                var update = Builders<MongoMessages>.Update.Push("DiscordMessages", message);
//                await _mongoMess.FindOneAndUpdateAsync(filter, update);
//            }
//            else
//            {
//                MongoMessages mMess = new MongoMessages()
//                {
//                    Id = message.BelongsTo
//                };
//                mMess.Messages.Add(message);

//                await _mongoMess.InsertOneAsync(mMess);
//            }

//            return CreatedAtAction("GetMessage", new { id = message.Id }, message);
//        }

//        //[HttpDelete("{id}")]
//        //public async Task<ActionResult<Message>> DeleteMessage(string id)
//        //{
//        //    var message = await _context.Messages.FindAsync(id);
//        //    if (message == null)
//        //    {
//        //        return NotFound();
//        //    }

//        //    _context.Messages.Remove(message);
//        //    await _context.SaveChangesAsync();

//        //    return message;
//        //}

//        //private bool MessageExists(string id)
//        //{
//        //    return _context.Messages.Any(e => e.Id == id);
//        //}
//    }
//}
