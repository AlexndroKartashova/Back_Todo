using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Update;
using Services.Dtos;
using Services.Services;
using Services.Services.Contacts;

namespace Api.Controllers
{
    [Route("api/item")]
    [ApiController]
    [EnableCors("Cors")]
    [Authorize]
    public class ItemController : ControllerBase
    {

        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet((""))]
        public async Task<ActionResult> GetCategories()
        {
            var claim = User.Claims.FirstOrDefault(x => x.Type.Equals("id"));

            if (claim == null)
            {
                return BadRequest();
            }

            var itemDtos = await _itemService.GetList(claim.Value);
            var itemModels = itemDtos.Select(item => new ItemModel { Id = item.Id, IsDone = item.IsDone, Name = item.Name });
            return Ok(itemModels);
        }

        [HttpGet(("{parentItemId}"))]
        public async Task<ActionResult> GetCategories(int parentItemId)
        {
            var claim = User.Claims.FirstOrDefault(x => x.Type.Equals("id"));

            if (claim == null)
            {
                return BadRequest();
            }

            var itemDtos = await _itemService.GetList(claim.Value, parentItemId);
            var itemModels = itemDtos.Select(item => new ItemModel { Id = item.Id, IsDone = item.IsDone, Name = item.Name });
            return Ok(itemModels);
        }
    
        [HttpPost("add")]
        public async Task<ActionResult> Add([FromBody] AddItemModel addItemModel)
        {
            var claim = User.Claims.FirstOrDefault(x => x.Type.Equals("id"));

            if (claim == null)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var itemDto = new ItemDto
                {
                    Name = addItemModel.Name,
                    ParentItemId = addItemModel.ParentItemId
                };

                await _itemService.Add(itemDto, claim.Value);
                return Ok();
            }

            return BadRequest();
        }

        [HttpPost("edit")]
        public async Task<ActionResult> EditItem([FromBody] ItemModel editItemModel)
        {
            var claim = User.Claims.FirstOrDefault(x => x.Type.Equals("id"));

            if (claim == null)
            {
                return BadRequest();
            }

            var itemDto = new ItemDto
            {
                Name = editItemModel.Name,
                IsDone = editItemModel.IsDone,
                Id = editItemModel.Id
            };

            await _itemService.EditItem(itemDto, claim.Value);
            return Ok();
        }

        [HttpPost("delete")]
        public async Task<ActionResult> IsDeleted([FromBody] ItemModel isDeletedModel) {
            
            var claim = User.Claims.FirstOrDefault(x => x.Type.Equals("id"));

            if (claim == null)
            {
                return BadRequest();
            }

            var itemDto = new ItemDto
            {
                Name = isDeletedModel.Name,
                Id = isDeletedModel.Id,
                isDeleted = true
            };

            await _itemService.IsDeleted(itemDto, claim.Value);
            return Ok();
        }
    }
}
