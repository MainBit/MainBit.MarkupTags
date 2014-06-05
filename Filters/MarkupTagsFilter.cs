using Orchard;
using Orchard.Caching;
using Orchard.Mvc.Filters;
using Orchard.UI.Admin;
using MainBit.MarkupTags.Services;
using MainBit.MarkupTags.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MainBit.MarkupTags.Filters
{
    public class MarkupTagsFilter : FilterProvider, IResultFilter {
        private readonly IWorkContextAccessor _workContextAccessor;
        private readonly IMarkupTagService _markupTagService;
        private readonly ICacheManager _cacheManager;
        private readonly ISignals _signals;

        public MarkupTagsFilter(
            IWorkContextAccessor workContextAccessor,
            IMarkupTagService markupTagService,
            ICacheManager cacheManager,
            ISignals signals) {
            _workContextAccessor = workContextAccessor;
            _markupTagService = markupTagService;
            _cacheManager = cacheManager;
            _signals = signals;
        }

        public void OnResultExecuting(ResultExecutingContext filterContext) {
            // ignore tracker on admin pages
            if (AdminFilter.IsApplied(filterContext.RequestContext)) {
                return;
            }

            // should only run on a full view rendering result
            if (!(filterContext.Result is ViewResult))
                return;

            var tags = _cacheManager.Get("MainBit.MarkupTags", ctx => 
            {
                ctx.Monitor(_signals.When("MainBit.MarkupTags.MarkupTagRecordChanged"));
                List<MarkupTagRecord> markupTags = _markupTagService.Get(true);
                return markupTags;
            });

            if (tags == null || tags.Count == 0)
            {
                return;
            }

            //IResourceManager resourceManager = _workContextAccessor.GetContext().Resolve<IResourceManager>(); //получаем экземпляр ресурс менеджера, он нужен для работы с метой, скриптами и стилями
            var context = _workContextAccessor.GetContext();
            var head = context.Layout.Head;
            var beforeBody = context.Layout.BeforeBody;
            var tail = context.Layout.Tail;
            
            

            foreach (var item in tags)
            {
                //// dont work
                //var shape = context.Layout.GetType().GetProperty(item.Position).GetValue(context.Layout, null);
                //shape.Add(new MvcHtmlString(item.Content));
                
                switch (item.Zone)
                {
                    case "Head":
                        {
                            head.Add(new MvcHtmlString(item.Content), string.IsNullOrEmpty(item.Position) ? null : item.Position);
                        }
                        break;
                    case "BeforeBody":
                        {
                            beforeBody.Add(new MvcHtmlString(item.Content), string.IsNullOrEmpty(item.Position) ? null : item.Position);
                        }
                        break;
                    case "Tail":
                        {
                            tail.Add(new MvcHtmlString(item.Content), string.IsNullOrEmpty(item.Position) ? null : item.Position);
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        public void OnResultExecuted(ResultExecutedContext filterContext) {
        }
    }
}