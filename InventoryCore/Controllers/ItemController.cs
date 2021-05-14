using InventoryCore.Repository;
using InventoryCore.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        IRepoItem repository;
        public ItemController(IRepoItem _IRepoItem)
        {
            repository = _IRepoItem;
        }

       

        [HttpGet]
        [Route("GetItems")]
        public async Task<IActionResult> GetItems()
        {
            try
            {
                var posts = await repository.GetItems();
                if (posts == null)
                {
                    return NotFound();
                }

                return Ok(posts);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("GetItem")]
        public async Task<IActionResult> GetItem(int? Item_ID)
        {
            if (Item_ID == null)
            {
                return BadRequest();
            }

            try
            {
                var post = await repository.GetItem(Item_ID);

                if (post == null)
                {
                    return NotFound();
                }

                return Ok(post);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("AddItem")]
        public async Task<IActionResult> AddItem([FromBody] ItemViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var postId = await repository.AddItem(model);
                    if (postId > 0)
                    {
                        return Ok(postId);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception)
                {

                    return BadRequest();
                }

            }
            else
            {
                return BadRequest(ModelState);
            }

        }

        [HttpPost]
        [Route("DeleteItem")]
        public async Task<IActionResult> DeleteItem(int? Item_ID)
        {
            int result = 0;

            if (Item_ID == null)
            {
                return BadRequest();
            }

            try
            {
                result = await repository.DeleteItem(Item_ID);
                if (result == 0)
                {
                    return NotFound();
                }
                return Ok();
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }


        [HttpPost]
        [Route("UpdateItem")]
        public async Task<IActionResult> UpdateItem([FromBody] ItemViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await repository.UpdateItem(model);

                    return Ok();
                }
                catch (Exception ex)
                {

                    return BadRequest(ex);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }

        }
    }
}
