﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using AccompProject.Models.EntityModel;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using AccompProject.Resources.Constants;


namespace AccompProject.Helpers
{
    public static class MyHelper
    {





      
        
        
        
        
        
        public static MvcHtmlString NoEncodeActionLink(this HtmlHelper htmlHelper,
                                            string text, string title, string action,
                                            string controller,
                                            object routeValues = null,
                                            object htmlAttributes = null)
        {
            UrlHelper urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);

            TagBuilder builder = new TagBuilder("a");
            builder.InnerHtml = text;
            builder.Attributes["title"] = title;
            builder.Attributes["href"] = urlHelper.Action(action, controller, routeValues);
            builder.MergeAttributes(new RouteValueDictionary(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes)));

            return MvcHtmlString.Create(builder.ToString());
        }

    }
}