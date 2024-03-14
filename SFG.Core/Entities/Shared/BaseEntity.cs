using System;
using System.ComponentModel.DataAnnotations;

namespace SFG.Database.Entities.Shared
{
	public class BaseEntity
	{
		[MaxLength(256)]
		public string? CreatedBy { get; set; }
		public DateTime? CreatedAt { get; set; }
        [MaxLength(256)]
        public string? UpdatedBy { get; set; }
		public DateTime? UpdatedAt { get; set; }
		public DateTime? DeletedAt { get; set; }

		public BaseEntity() { }
	}
}

