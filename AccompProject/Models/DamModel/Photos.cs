﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AccompProject.Models.DamModel
{
    public class Photos
    {
        public string Path { get; set; }

        public string Description { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Photos" /> class.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="description">The description.</param>
        public Photos(string path, string description)
        {
            Path = path;
            Description = description;
        }
    }
}