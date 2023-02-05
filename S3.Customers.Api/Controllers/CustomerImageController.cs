using System.Net;
using Amazon.S3;
using Microsoft.AspNetCore.Mvc;
using S3.Customers.Api.Services;

namespace S3.Customers.Api.Controllers;

[ApiController]
public class CustomerImageController : ControllerBase
{
    private readonly IImageService _imageService;

    public CustomerImageController(IImageService imageService)
    {
        _imageService = imageService;
    }

    [HttpPost("customers/{id:guid}/image")]
    public async Task<IActionResult> Upload([FromRoute] Guid id,
                                            [FromForm(Name = "Data")] IFormFile file)
    {
        var response = await _imageService.UploadImageAsync(id, file);


        return response.HttpStatusCode switch
        {
            HttpStatusCode.OK => Ok(),
            HttpStatusCode.NotFound => NotFound(),
            _ => BadRequest()
        };
    }

    [HttpGet("customers/{id:guid}/image")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        try
        {
            var ans = await _imageService.GetImageAsync(id);

            if (ans.HttpStatusCode == HttpStatusCode.OK)
                return File(ans.ResponseStream, ans.Headers.ContentType);

            return BadRequest();
        }
        catch (AmazonS3Exception ex) when (ex.Message is "The specified key does not exist.")
        {
            return NotFound();
        }
    }

    [HttpDelete("customers/{id:guid}/image")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var response = await _imageService.DeleteImageAsync(id);

        return response.HttpStatusCode switch
        {
            HttpStatusCode.OK => Ok(),
            HttpStatusCode.NoContent => NoContent(),
            HttpStatusCode.NotFound => NotFound(),
            _ => BadRequest()
        };
    }
}