using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Mvc;
using CasablancaMVC.Models;
using CasablancaMVC.ViewModel;

namespace CasablancaMVC.Filters
{
    [AttributeUsage(AttributeTargets.Method)]
    public class GenerateResultListFilterAttribute : FilterAttribute, IResultFilter
    {
        private readonly Type _sourceType;
        private readonly Type _destinatiopnType;

        public GenerateResultListFilterAttribute(Type sourceType,Type destinationType)
        {
            _sourceType = sourceType;
            _destinatiopnType = destinationType;
        }
        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
            
        }

        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            var model = filterContext.Controller.ViewData.Model;

            var resultListGenericType = typeof(ResultList<>).MakeGenericType(new Type[] { _destinatiopnType });

            var srcGenericType = typeof(List<>).MakeGenericType(new Type[] { _sourceType });

            var destGenericType = typeof(List<>).MakeGenericType(new Type[] { _destinatiopnType });

            AutoMapper.Mapper.Initialize(c => c.CreateMap(_sourceType, _destinatiopnType));

            var viewModel = AutoMapper.Mapper.Map(model, srcGenericType, destGenericType);

            var queryOptions = filterContext.Controller.ViewData.ContainsKey("QueryOptions") ? filterContext.Controller.ViewData["QueryOptions"] : new QueryOptions();

            var resultList = Activator.CreateInstance(resultListGenericType, viewModel, queryOptions);

            filterContext.Controller.ViewData.Model = resultList;

        }
    }
}