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
using Services.Services.Contracts;

namespace Api.Controllers
{
    [Route("api/todo")]
    [ApiController]
    [Authorize]
    public class ItemController : ControllerBase
    {

        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        private string GetUserId()
        {
            var claim = User.Claims.FirstOrDefault(x => x.Type.Equals("id"));

            if (claim == null)
            {
                throw new ArgumentNullException(nameof(claim));
            }

            return claim.Value;
        }
   

        [HttpGet((""))]
        public async Task<ActionResult> GetCategories()
        {
            var itemDtos = await _itemService.GetList(GetUserId());
            var itemModels = itemDtos.Select(item => new ItemModel { 
                Id = item.Id, 
                IsDone = item.IsDone, 
                Name = item.Name, 
                ParentItemId = item.ParentItemId,
                ItemCount = item.ItemCount,
                DoneItemCount = item.DoneItemCount
            });
            return Ok(itemModels);
        }

        [HttpGet(("{parentItemId}"))]
        public async Task<ActionResult> GetCategories(int parentItemId)
        {
            var itemDtos = await _itemService.GetList(GetUserId(), parentItemId);
            var itemModels = itemDtos.Select(item => new ItemModel { 
                Id = item.Id, 
                IsDone = item.IsDone, 
                Name = item.Name, 
                ParentItemId = item.ParentItemId,
                ItemCount = item.ItemCount,
                DoneItemCount = item.DoneItemCount
            });
            return Ok(itemModels);
        }

        [HttpGet("id/{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var itemDto = await _itemService.Get(GetUserId(), id);
            var itemModel = new ItemModel
            {
                Id = itemDto.Id,
                ParentItemId = itemDto.ParentItemId,
                Name = itemDto.Name,
                IsDeleted = itemDto.IsDeleted,
                IsDone = itemDto.IsDone
            };

            return Ok(itemModel);
        }

        [HttpPost("add")]
        public async Task<ActionResult> Add([FromBody] AddItemModel addItemModel)
        {
            if (ModelState.IsValid)
            {
                var itemDto = new ItemDto
                {
                    Name = addItemModel.Name,
                    ParentItemId = addItemModel.ParentItemId
                };

                await _itemService.Add(itemDto, GetUserId());
                return Ok();
            }

            return BadRequest();
        }

        [HttpPost("edit")]
        public async Task<ActionResult> EditItem([FromBody] ItemModel editItemModel)
        {
            var itemDto = new ItemDto
            {
                Name = editItemModel.Name,
                IsDone = editItemModel.IsDone,
                Id = editItemModel.Id
            };

            await _itemService.EditItem(itemDto, GetUserId());
            return Ok();
        }

        [HttpGet("{id}/delete")]
        public async Task<ActionResult> DeleteItem(int id)
        {
            await _itemService.DeleteItem(id, GetUserId());

            return Ok();
        }

        [HttpPost("chenge_status")]
        public async Task ChangeStatus([FromBody] ItemModel changeStatus)
        {
            var itemDto = new ItemDto
            {
                IsDone = changeStatus.IsDone,
                Id = changeStatus.Id
            };

            await _itemService.ChangeStatus(itemDto, GetUserId());
        }
    }
}
