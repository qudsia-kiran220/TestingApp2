using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestingApp2.Models;

public partial class Project
{
   

    [Key]
   
    public int Id { get; set; }

    public int ProjectId { get; set; }

    public string ProjectName { get; set; } = null!;
}
