﻿using System.ComponentModel.DataAnnotations;

namespace HRMS.Domain.Base;

public class BaseEntity<TPrimaryKey>
{
    [Key]
    public TPrimaryKey Id { get; set; } = default!;

    public bool IsActive { get; set; } = true;

    public Guid CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid? LastModifiedBy { get; set; }

    public DateTime? LastModifiedAt { get; set; }

    public bool IsDeleted { get; set; } = false;

    public Guid? DeletedBy { get; set; }

    public DateTime? DeletedAt { get; set; }
}