using Microsoft.AspNetCore.Mvc.Filters;
using MultiTenancy_ODEV1.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.Json;
using static MultiTenancy_ODEV1.Attribute.FilterEnum;

namespace MultiTenancy_ODEV1.Attribute
{
    public class FilterParameterAttribute: ActionFilterAttribute
    {
        public string value;
        public string KeyValue = "TenantId";
        public FilterType filterType = FilterType.NormalVariable;
        public FilterParameterAttribute(string EntityClass, FilterType filterType)
        {
            this.value = EntityClass;
            this.filterType = filterType;
        }
        public FilterParameterAttribute(string NormalVariable)
        {
            this.value = NormalVariable;
            this.filterType = filterType;
        }

        public override async void OnActionExecuting(ActionExecutingContext context)
        {
            if (filterType == FilterType.ClassVariable)
            {
                object Argument = (context.ActionArguments[value] as BaseEntity).TenantId;
                if (Argument == null || Argument == "" || Convert.ToInt16(Argument) == 0)
                {
                    var HeadersGetValue = context.HttpContext.Request.Headers.FirstOrDefault(x => x.Key == KeyValue).Value;
                    var QueryGetValue = context.HttpContext.Request.Query.FirstOrDefault(x => x.Key == KeyValue).Value;
                    var RouteGetValue = context.RouteData.Values.FirstOrDefault(x => x.Key == KeyValue).Value;
                    if (HeadersGetValue.Any())
                    {
                        (context.ActionArguments[value] as BaseEntity).TenantId = Convert.ToInt32(HeadersGetValue);
                    }
                    else if (QueryGetValue.Any())
                    {
                        (context.ActionArguments[value] as BaseEntity).TenantId = Convert.ToInt32(QueryGetValue);
                    }
                    else if (RouteGetValue != null)
                    {
                        (context.ActionArguments[value] as BaseEntity).TenantId = Convert.ToInt32(RouteGetValue);
                    }
                }
            }
            else if (filterType == FilterType.NormalVariable)
            {
                object Argument = context.ActionArguments[value];
                if (Argument == null || Argument == "")
                {
                    var HeadersGetValue = context.HttpContext.Request.Headers.FirstOrDefault(x => x.Key == KeyValue).Value;
                    var QueryGetValue = context.HttpContext.Request.Query.FirstOrDefault(x => x.Key == KeyValue).Value;
                    var RouteGetValue = context.RouteData.Values.FirstOrDefault(x => x.Key == KeyValue).Value;
                    if (HeadersGetValue.Any())
                    {
                        context.ActionArguments[value] = HeadersGetValue.ToString();
                    }
                    else if (QueryGetValue.Any())
                    {
                        context.ActionArguments[value] = QueryGetValue.ToString();
                    }
                    else if (RouteGetValue != null)
                    {
                        context.ActionArguments[value] = RouteGetValue.ToString();
                    }
                }

            }  
        }

    }
}
