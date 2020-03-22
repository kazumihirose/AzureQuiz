using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlazorSignalRApp.Shared;
using Microsoft.Azure.Storage.Auth;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace BlazorSignalRApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly QuizDBContext _context;
        IConfiguration _configuration;

        public ImagesController(QuizDBContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: api/Images
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Image>>> GetImages()
        {
            return await _context.Images.ToListAsync();
        }

        // GET: api/Images/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Image>> GetImage(int id)
        {
            var image = await _context.Images.FindAsync(id);

            if (image == null)
            {
                return NotFound();
            }

            return image;
        }

        // PUT: api/Images/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutImage(int id, Image image)
        {
            if (id != image.ImageID)
            {
                return BadRequest();
            }

            _context.Entry(image).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImageExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Images
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Image>> PostImage(Image image)
        {
            StorageCredentials storageCredentials = new StorageCredentials(_configuration["blobStorageAccountName"], _configuration["blobStorageAccountKey"]);
            CloudStorageAccount cloudStorageAccount = new CloudStorageAccount(storageCredentials, true);
            CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = cloudBlobClient.GetContainerReference(_configuration["blobStorageContainer"]);
            try
            {
                bool result = await container.CreateIfNotExistsAsync();
                BlobContainerPermissions permissions = await container.GetPermissionsAsync();
                permissions.PublicAccess = BlobContainerPublicAccessType.Container;
                await container.SetPermissionsAsync(permissions);
                if (result == true)
                {
                    Console.WriteLine("Created container {0}", container.Name);
                }
            }
            catch (StorageException e)
            {
                Console.WriteLine("HTTP error code {0}: {1}",
                                    e.RequestInformation.HttpStatusCode,
                                    e.RequestInformation.ErrorCode);
                Console.WriteLine(e.Message);
            }

            var extention = Path.GetExtension(image.ImageFileName).ToLower();
            string fileName = (Guid.NewGuid().ToString() + extention).ToLower();
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileName);
            byte[] binary = Convert.FromBase64String(image.ImageData);
            await blockBlob.UploadFromByteArrayAsync(binary, 0, binary.Length);
            if (extention == ".svg")
            {
                blockBlob.Properties.ContentType = "image/svg+xml";
            }
            else if (extention == ".png")
            {
                blockBlob.Properties.ContentType = "image/png";
            }
            else if (extention == ".jpeg" || extention == ".jpg")
            {
                blockBlob.Properties.ContentType = "image/jpeg";
            }


            await blockBlob.SetPropertiesAsync();

            image.ImageUrl = blockBlob.Uri.AbsoluteUri;
            _context.Images.Add(image);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetImage), new { id = image.ImageID }, image);
        }

        // DELETE: api/Images/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Image>> DeleteImage(int id)
        {
            var image = await _context.Images.FindAsync(id);
            if (image == null)
            {
                return NotFound();
            }

            _context.Images.Remove(image);
            await _context.SaveChangesAsync();

            return image;
        }

        private bool ImageExists(int id)
        {
            return _context.Images.Any(e => e.ImageID == id);
        }
    }
}
