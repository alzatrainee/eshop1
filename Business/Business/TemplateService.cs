

using Alza.Core.Module.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Module.Business.Business
{
    class TemplateService
    {
        /*
        private IRazorViewEngine _viewEngine;
        private readonly IServiceProvider _serviceProvider;
        private readonly ITempDataProvider _tempDataProvider;

        public TemplateService(IRazorViewEngine viewEngine, IServiceProvider serviceProvider, ITempDataProvider tempDataProvider)
        {
            _viewEngine = viewEngine;
            _serviceProvider = serviceProvider;
            _tempDataProvider = tempDataProvider;
        }

        public async Task<string> RenderTemplateAsync<TViewModel>(string filename, TViewModel viewModel)
        {
            var httpContext = new DefaultHttpContext
            {
                RequestServices = _serviceProvider
            };

            var actionContext = new ActionContext(httpContext, new RouteData(), new ActionDescriptor());

            using (var outputWriter = new StringWriter())
            {
                var viewResult = _viewEngine.FindView(actionContext, filename, false);
                var viewDictionary = new ViewDataDictionary<TViewModel>(new EmptyModelMetadataProvider(), new ModelStateDictionary());
                var tempDataDictionary = new TempDataDictionary(httpContext, _tempDataProvider);

                if (!viewResult.Success)
                {
                    throw new Exception("Failed to render template {filename} because it was not found.");
                }

                try
                {
                    var viewContext = new ViewContext(actionContext, viewResult.View, viewDictionary,
                        tempDataDictionary, outputWriter, new HtmlHelperOptions());

                    await viewResult.View.RenderAsync(viewContext);
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed to render template due to a razor engine failure", ex);
                }

                return outputWriter.ToString();
            }
        }*/

    }
}