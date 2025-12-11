// File: Utilities/SwaggerFileUploadOperationFilter.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Levavishwam_Backend.Utilities
{
    public class SwaggerFileUploadOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation == null || context == null) return;

            var fileParams = context.MethodInfo
                .GetParameters()
                .Where(p => p.ParameterType == typeof(IFormFile) || p.ParameterType == typeof(IFormFile[]))
                .ToArray();

            if (!fileParams.Any()) return;

            var props = new Dictionary<string, OpenApiSchema>();
            foreach (var p in fileParams)
            {
                if (p.ParameterType == typeof(IFormFile))
                {
                    props[p.Name] = new OpenApiSchema { Type = "string", Format = "binary" };
                }
                else // IFormFile[]
                {
                    props[p.Name] = new OpenApiSchema
                    {
                        Type = "array",
                        Items = new OpenApiSchema { Type = "string", Format = "binary" }
                    };
                }
            }

            operation.RequestBody = new OpenApiRequestBody
            {
                Content =
                {
                    ["multipart/form-data"] = new OpenApiMediaType
                    {
                        Schema = new OpenApiSchema
                        {
                            Type = "object",
                            Properties = props,
                            Required = new HashSet<string>(props.Keys)
                        }
                    }
                }
            };
        }
    }
}
