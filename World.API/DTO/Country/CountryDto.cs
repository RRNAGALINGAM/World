using System.ComponentModel.DataAnnotations;

namespace World.API.DTO.Country
{
    public class CountryDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ShortName { get; set; }

        public string CountryCode { get; set; }
    }
}
