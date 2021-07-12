using Catalog.Dtos;
using Catalog.Entities;
using Catalog.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Catalog.Controllers
{
    [ApiController]//It brings adicionals resorcers to the API
    [Route("items")]//Specify where it is going to response
    public class ItemsController : ControllerBase //Always inherit from the ControllerBase
    {

        //Declared a variable type Interface IItemsRepository to inject in the constroctor
        private readonly IItemsRepository repository;

        public ItemsController(IItemsRepository repository)
        {
            this.repository = repository;
        }

        //Get /items
        [HttpGet]
        public IEnumerable<ItemDto> GetItems()
        {
            //Calling the method GetItems from Repositories/InMemItemsRepository.cs and converting to ItemDto
            var items = repository.GetItems().Select(item => item.AsDto());
            return items;
        }

        //Get /items/id
        [HttpGet("{id}")]
        public ActionResult<ItemDto> GetItem(Guid id)//ActionResult allows it to return an item or outher type
        {
            //Calling the method GetItem from Repositories/InMemItemsRepository.cs
            var item = repository.GetItem(id);

            if (item is null)
            {
                return NotFound();
            }

            //It returns the item converted to ItemDto
            return item.AsDto();
        }

        //Post /items
        [HttpPost]
        public ActionResult<ItemDto> CreateItem(CreateItemDto itemDto)
        {
            Item item = new()
            {
                Id = Guid.NewGuid(),
                Name = itemDto.Name,
                Price = itemDto.Price,
                CreateDate = DateTimeOffset.UtcNow
            };

            repository.CreateItem(item);

            //Action to get informations about the new item
            return CreatedAtAction(nameof(GetItem), new { id = item.Id }, item.AsDto());//It returns the item converted to ItemDto
        }

        //Put /Items/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateItem(Guid id, UpdateItemDto itemDto)
        {
            var existingItem = repository.GetItem(id);

            if (existingItem is null)
            {
                return NotFound();
            }

            //Coping/Modifing the founded item
            Item UpdatedItem = existingItem with
            {
                Name = itemDto.Name,
                Price = itemDto.Price
            };

            repository.UpdateItem(UpdatedItem);

            return NoContent();
        }

        //Put /Items/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteItem(Guid id)
        {
            var existingItem = repository.GetItem(id);

            if (existingItem is null)
            {
                return NotFound();
            }

            //Deleting the founded item
            repository.DeleteItem(id);

            return NoContent();
        }
    }
}
