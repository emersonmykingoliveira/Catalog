using System;
using System.ComponentModel.DataAnnotations;

namespace Catalog.Dtos
{
    //Post will generate id and date
    public record CreateItemDto
    {   
        [Required]
        public string Name { get; init; }

        [Required]
        [Range(1, 1000)]
        public decimal Price { get; init; }
    }
}
