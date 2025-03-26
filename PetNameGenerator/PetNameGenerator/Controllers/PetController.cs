using Microsoft.AspNetCore.Mvc;

namespace PetNameGenerator.Controllers
{
    [ApiController]
    [Route("api/petname")]
    public class PetController : ControllerBase
    {
        private readonly string[] dogNames = { "Buddy", "Max", "Charlie", "Rocky", "Rex" };
        private readonly string[] catNames = { "Whiskers", "Mittens", "Luna", "Simba", "Tiger" };
        private readonly string[] birdNames = { "Tweety", "Sky", "Chirpy", "Raven", "Sunny" };

        [HttpPost]
        public IActionResult Post([FromQuery] string animalType, [FromQuery] bool twoType = false)
        {
            if (string.IsNullOrEmpty(animalType))
                return BadRequest(new { message = "The 'animalType' field is required." });

            Random random = new Random();
            string petName;

            switch (animalType.ToLower())
            {
                case "dog":
                    petName = dogNames[random.Next(dogNames.Length)];
                    break;
                case "cat":
                    petName = catNames[random.Next(catNames.Length)];
                    break;
                case "bird":
                    petName = birdNames[random.Next(birdNames.Length)];
                    break;
                default:
                    return BadRequest(new { message = "Invalid animal type." });
            }

            if (twoType)
            {
                string secondPetName;
                if (animalType.ToLower() == "dog")
                    secondPetName = dogNames[random.Next(dogNames.Length)];
                else if (animalType.ToLower() == "cat")
                    secondPetName = catNames[random.Next(catNames.Length)];
                else
                    secondPetName = birdNames[random.Next(birdNames.Length)];

                petName += secondPetName;
            }

            return Ok(new { animalType, name = petName });
        }
    }
}