using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AccompProject.Models;
using AccompProject.Models.EntityModel;
using System.Web.Mvc;

namespace AccompProject.Models
{
   

        public static class HtmlExtensions
        {
            public static MvcHtmlString Image(this HtmlHelper html, byte[] image)
            {
                var img = String.Format("data:image/jpg;base64,{0}", Convert.ToBase64String(image));
                return new MvcHtmlString("<img src='" + img + "'" + " width='200' height='200' />");
            }


            public static IHtmlString GetBytes<TModel, TValue>(this HtmlHelper<TModel> helper, System.Linq.Expressions.Expression<Func<TModel, TValue>> expression, byte[] array, string Id)
            {
                TagBuilder tb = new TagBuilder("img");
                tb.MergeAttribute("id", Id);
                var base64 = Convert.ToBase64String(array);
                var imgSrc = String.Format("data:image/gif;base64,{0}", base64);
                tb.MergeAttribute("src", imgSrc);
                return MvcHtmlString.Create(tb.ToString(TagRenderMode.SelfClosing));
            }
        }


    
}