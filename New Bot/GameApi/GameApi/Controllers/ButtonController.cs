using GameApi.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class ButtonClickController : ControllerBase
{
    private readonly MongoDBService _mongoDBService;

    public ButtonClickController(MongoDBService mongoDBService)
    {
        _mongoDBService = mongoDBService;
    }

    [HttpPost]
    public async Task<IActionResult> RegisterClick([FromBody] ButtonClickDto clickDto)
    {
        try
        {
            if (string.IsNullOrEmpty(clickDto.EventId))
                return BadRequest("ID do evento é obrigatório");

            if (string.IsNullOrEmpty(clickDto.ButtonName))
                return BadRequest("Nome do botão é obrigatório");

            var clickRequest = clickDto.ToRequest();


            await _mongoDBService.SaveButtonClick(clickRequest);

            return Ok(new
            {
                success = true,
                message = "Evento registrado com sucesso",
                eventId = clickRequest.EventId
            });
        }
        catch (ValidationException ex)
        {
            return BadRequest(new { success = false, error = ex.Message });
        }
        catch (FormatException)
        {
            return BadRequest(new { success = false, error = "Formato de data/hora inválido" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { success = false, error = ex.Message });
        }
    }
}