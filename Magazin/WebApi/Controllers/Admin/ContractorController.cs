using DataCore.Entities;
using DataCore.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.Models.Admin.Contractors;
using WebUiAdmin.Controllers;

namespace WebApi.Controllers.Admin
{
    /// <summary>
    /// Контроллер для работы с контрагентами
    /// </summary>
    [Authorize(Roles = "admin")]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "admin")]
    [ApiController]
    public class ContractorController : ControllerBase
    {
        private readonly IContractorService _contractorService;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="contractorService"></param>
        public ContractorController(IContractorService contractorService)
        {
            _contractorService = contractorService;
        }

        /// <summary>
        /// Получить список контрагентов
        /// </summary>
        /// <param name="take"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(ListRespone<Contractor>), 200)]
        public async Task<IActionResult> GetAll(int take = 20, int page = 1)
        {
            if (take == 0)
            {
                take = 20;
            }
            if (page <= 0)
            {
                page = 1;
            }

            var skip = (page - 1) * take;

            var all = _contractorService.GetAllAsNotracking();

            var entities = await all.OrderByDescending(x => x.Id).Skip(skip).Take(take).ToListAsync();

            var total = await all.CountAsync();

            var result = new ListRespone<Contractor>()
            {
                Data = entities,
                Total = total
            };

            return Ok(result);
        }

        /// <summary>
        /// Получить продукт для изменения
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(Contractor), 200)]
        public async Task<IActionResult> Get(long id)
        {
            var contractor = await _contractorService.GetAsync(id);

            return Ok(contractor);
        }

        /// <summary>
        /// Создание продукта, принмает multipart
        /// </summary>
        /// <param name="body">body</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(Contractor),200)]
        public async Task<IActionResult> Create([FromForm]ContractorPostModel body)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var contractor = JsonConvert.DeserializeObject<Contractor>(body.Model);

            await _contractorService.CreateAsync(contractor, body.Image);

            return Ok(contractor);
        }

        /// <summary>
        /// Update product
        /// </summary>
        /// <param name="body">body</param>
        /// <returns></returns>
        [HttpPatch]
        [ProducesResponseType(typeof(Contractor), 200)]
        public async Task<IActionResult> Update([FromForm]ContractorPostModel body)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var contractor = JsonConvert.DeserializeObject<Contractor>(body.Model);

            await _contractorService.UpdateAsync(contractor, body.Image);

            return Ok(contractor);
        }

        /// <summary>
        /// Удалить продукт
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> Remove(long id)
        {
            try
            {
                await _contractorService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception error)
            {
                return BadRequest(error.GetBaseException().Message);
            }
        }


    }
}
