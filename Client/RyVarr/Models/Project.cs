using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RyVarr.Models;

public class Project
{
    public string Title { get; set; }
    public string Description { get; set; }
    public Project(string title, string description)
    {
        Title = title;
        Description = description;
    }
}
