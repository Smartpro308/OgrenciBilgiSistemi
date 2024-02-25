﻿using OgrenciBilgiSistemi.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OgrenciBilgiSistemi.Entity.Entitites;

public class Course : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Classroom { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}
