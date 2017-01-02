using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyCredit.Models
{
    public abstract class EasyCreditEntity : IEasyCreditEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
    }
}
