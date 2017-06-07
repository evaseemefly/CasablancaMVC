using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CasablancaMVC.Models;


    public static class HtmlHelperExtensions
    {
        public static HtmlString HtmlConvertToJson(this HtmlHelper htmlHelper,object model)
        {
            
            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Formatting = Formatting.Indented
            };
            return new HtmlString(JsonConvert.SerializeObject(model, settings));
        }

    /// <summary>
    /// 构建合适的链接
    /// 
    /// </summary>
    /// <param name="htmlHelper"></param>
    /// <param name="filedName"></param>
    /// <param name="actionName"></param>
    /// <param name="sortField">模型的排序字段名称</param>
    /// <param name="queryOptions"></param>
    /// <returns></returns>
    public static MvcHtmlString BuildSortableLink(this HtmlHelper htmlHelper,string fieldName,string actionName,string sortField,QueryOptions queryOptions)
    {
        /*
            eg:
            fieldName:FirstName
            actionName:Index
            sortField:FirstName
            queryOptions
        */
        var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);

        var isCurrentSortField = queryOptions.SortField == sortField;

        //<a href=\"/Authors?SortField =FirstName&SortOrder =ASC\">FirstName<span class=\"glyphicon glyphicon-sort\"></span></a>
        var result = string.Format("<a href=\"{0}\">{1}{2}</a>",
            urlHelper.Action(
                actionName,
                new
                {
                    SortField = sortField,
                    SortOrder = (isCurrentSortField && queryOptions.SortOrder == SortOrder.ASC) ? SortOrder.DESC : SortOrder.ASC
                }),
                fieldName,
                BuildSortIcon(isCurrentSortField, queryOptions));

        return new MvcHtmlString(result);
    }


    /// <summary>
    /// 为排序table的表头添加图标样式
    /// （通过<span>拼接的方式实现</span>）
    /// </summary>
    /// <param name="isCurrentSortField"></param>
    /// <param name="queryOptions"></param>
    /// <returns></returns>
    private static string BuildSortIcon(bool isCurrentSortField,QueryOptions queryOptions)
    {
        string sortIcon = "sort";

        if (isCurrentSortField)
        {
            sortIcon += "-by-alphabet";
            if (queryOptions.SortOrder == SortOrder.DESC)
                sortIcon += "-alt";
            //glyphicon glyphicon-sort-by-alphabet-alt
            //"sort-by-alphabet-alt"
        }

        //<span class="glyphiconglyphicon-sort-by-alphabet-alt"></span>
        return string.Format("<span class=\"{0} {1}{2}\"></span>", "glyphicon", "glyphicon-", sortIcon);
    }

    public static MvcHtmlString BuildNextPreviousLinks(this HtmlHelper htmlhelper,QueryOptions queryOptions,string actionName)
    {
        var urlHelper = new UrlHelper(htmlhelper.ViewContext.RequestContext);

        string result_html = string.Format(
            "<nav>" +
            "<ul class=\"pager\">" +
                "<li class=\"previous {0}\">{1}</li>" +
                "<li class=\"next {2}\">{3}</li>" +
            "</ul>" +
            "</nav>",
            IsPreviousDisabled(queryOptions),
            BuildPreviousLink(urlHelper, queryOptions, actionName),
            IsNextDisabled(queryOptions),
            BuildNextLink(urlHelper, queryOptions, actionName));
        return new MvcHtmlString(result_html);
        //return new MvcHtmlString(string.Format(
        //    "<nav>" +
        //    "  <ul class=\"pager\">" +
        //        "<li class=\"perious {0}\">{1}</li>" +
        //        "<li class=\"next {2}\">{3}</li>" +
        //       "</ul>" +
        //    "</nav>",
        //    IsPreviousDisabled(queryOptions),
        //    BuildPreviousLink(urlHelper, queryOptions, actionName),
        //    IsNextDisabled(queryOptions),
        //    BuildNextLink(urlHelper, queryOptions, actionName)
        //    ));
    }

    private static string IsPreviousDisabled(QueryOptions queryOptions)
    {
        return (queryOptions.CurrentPage == 1) ? "disabled" : string.Empty;
    }

    private static string IsNextDisabled(QueryOptions queryOptions)
    {
        return (queryOptions.CurrentPage == queryOptions.TotalPages) ? "disabled" : string.Empty;
    }

    private static string BuildPreviousLink(UrlHelper urlHelper,QueryOptions queryOptions,string actionName)
    {
        return string.Format(
            "<a href=\"{0}\"><span aria-hidden=\"true\">&larr;</span> 下一页</a>", urlHelper.Action(actionName,
            //此种声明方式是匿名类？
            new
            {
                SortOrder = queryOptions.SortOrder,
                SortField = queryOptions.SortField,
                CurrentPage = queryOptions.CurrentPage - 1,
                PageSize = queryOptions.PageSize
            }
            ));
    }

    private static string BuildNextLink(UrlHelper urlHelper,QueryOptions queryOptions,string actionName)
    {
        return string.Format(
            "<a href=\"{0}\">上一页<span aria-hidden=\"true\">&rarr;</span> </a>", urlHelper.Action(actionName,
            //此种声明方式是匿名类？
            new
            {
                SortOrder = queryOptions.SortOrder,
                SortField = queryOptions.SortField,
                CurrentPage = queryOptions.CurrentPage + 1,
                PageSize = queryOptions.PageSize
            }
            ));
    }
    }
