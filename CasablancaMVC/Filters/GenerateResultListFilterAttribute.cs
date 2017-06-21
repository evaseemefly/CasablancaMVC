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
        /// <summary>
        /// 原类型
        /// </summary>
        private readonly Type _sourceType;

        /// <summary>
        /// 目标类型
        /// </summary>
        private readonly Type _destinationType;

        /// <summary>
        /// 构造函数为原类型及目标类型初始化（赋值）
        /// </summary>
        /// <param name="sourceType"></param>
        /// <param name="destinationType"></param>
        public GenerateResultListFilterAttribute(Type sourceType,Type destinationType)
        {
            _sourceType = sourceType;
            _destinationType = destinationType;
        }
        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
            
        }

        /// <summary>
        /// 在视图生成前调用
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            var model = filterContext.Controller.ViewData.Model;
            /*
                    {Name = "ResultList`1" 
                    FullName = "CasablancaMVC.ViewModel.ResultList`1[[CasablancaMVC.ViewModel.AuthorViewModel, CasablancaMVC, 
                    Version=1.0.0.0,
                    Culture=neutral, 
                    PublicKeyToken=null]]"}
            */
            var resultListGenericType = typeof(ResultList<>).MakeGenericType(new Type[] { _destinationType });


            /*
            {Name = "List`1" 
            FullName = "System.Collections.Generic.List`1[[CasablancaMVC.Models.Author, CasablancaMVC, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]]"}
            */
            var srcGenericType = typeof(List<>).MakeGenericType(new Type[] { _sourceType });

            /*
            {Name = "List`1"
            FullName = "System.Collections.Generic.List`1[[CasablancaMVC.ViewModel.AuthorViewModel, CasablancaMVC, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]]"}
            */
            var destGenericType = typeof(List<>).MakeGenericType(new Type[] { _destinationType });

            AutoMapper.Mapper.Initialize(c => c.CreateMap(_sourceType, _destinationType));

            //将CasablancaMVC.Models.Author=》CasablancaMVC.ViewModel.AuthorViewModel
            var viewModel = AutoMapper.Mapper.Map(model, srcGenericType, destGenericType);

            //获取控制器传给视图的ViewData字典中是否包含QueryOptions
            //若包含则对QueryOptions赋对应的value，若不包含则创建新的QueryOptions
            var queryOptions = filterContext.Controller.ViewData.ContainsKey("QueryOptions") ? filterContext.Controller.ViewData["QueryOptions"] : new QueryOptions();

            //转成ResultList对象
            var resultList = Activator.CreateInstance(resultListGenericType, viewModel, queryOptions);

            //作为Model返回
            filterContext.Controller.ViewData.Model = resultList;

        }
    }
}