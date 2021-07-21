using AccompProject.Models.DamModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace AccompProject.Services
{
    public class PhotoModel : List<Photos>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PhotoModel"/> class.
        /// </summary>
        public PhotoModel(string folder)
        {
            string path = HttpContext.Current.Server.MapPath(folder);

             try
             {

                 var di = new DirectoryInfo(path);

                 foreach (var file in di.EnumerateFiles("*.jpg", SearchOption.TopDirectoryOnly))
                 {
                     var p = new Photos(string.Concat(folder, file.Name), Path.GetFileNameWithoutExtension(file.Name));
                     Add(p);
                 }
             }
             catch (Exception e) { 
             
             
             }
        }

    }
}