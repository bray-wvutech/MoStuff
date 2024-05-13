using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;

public class StoreItem
{
    // this should have a private set
    // but I need to be able to set it for my in memory repository
    // which I made as a demo for students to see swapping out implementations
    public int Id { get; set; }

    // we really shouldn't have any attributes on domain models
    // we can control database options in the DbContext in the Data layer
    // and create view models in our UI layer for display attributes
    // and map between these models and those view models
    // but I'm adding this here for simplicity in the demo
    // and to keep the focus on the architecture and how the interfaces work
    [StringLength(60, MinimumLength = 1)]
    [Display(Name = "Item")]
    public string Name { get; set; } = "";

    public string? Description { get; set; }

    [Range(0, 1000)]
    [DataType(DataType.Currency)]
    [Column(TypeName = "decimal(18, 2)")]
    public decimal Price { get; set; }

    [Display(Name = "Picture")]
    public string? PictureUri { get; set; }
}
