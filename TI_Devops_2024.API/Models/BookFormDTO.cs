﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TI_Devops_2024.API.Validators;

namespace TI_Devops_2024.API.Models
{
    public class BookFormDTO
    {
        [DisplayName("ISBN")]
        [Required(ErrorMessage = "Champ requis")]
        [ISBNValidation]
        public string ISBN { get; set; } = null!;

        [DisplayName("Titre")]
        [Required]
        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        [DisplayName("Date de publication")]
        [DataType(DataType.DateTime)]
        public DateTime PublishDate { get; set; }

        [DisplayName("Auteur")]
        public int AuthorId { get; set; }
    }
}
