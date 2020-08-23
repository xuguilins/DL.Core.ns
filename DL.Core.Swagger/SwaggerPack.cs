using System;
using System.Collections.Generic;
using System.Text;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.Extensions.DependencyInjection;
using DL.Core.utility.Configer;
using System.Reflection;
using System.IO;
using Microsoft.AspNetCore.Builder;

namespace DL.Core.Swagger
{
    public static class SwaggerPack
    {
        private static string version = string.Empty;
        private static string title = string.Empty;
        private static bool engalbe = false;
        private static string docName = string.Empty;

        /// <summary>
        /// DLCore针对Swagger引用的扩展
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSwaggerPack(this IServiceCollection services)
        {
            var swaggerInfo = ConfigerManager.Instance.getCofiger();
            if (swaggerInfo != null)
            {
                if (swaggerInfo.CodeConfig.Swagger != null)
                {
                    var swg = swaggerInfo.CodeConfig.Swagger;
                    if (swg.Enable)
                    {
                        engalbe = swg.Enable;
                        title = swg.Title;
                        version = swg.Version;
                        docName = swg.SwaggerName;
                        services.AddSwaggerGen(options =>
                        {
                            options.SwaggerDoc(docName, new Microsoft.OpenApi.Models.OpenApiInfo
                            {
                                Description = swg.SwaggerDesc,
                                Title = title,
                                Version = version
                            });
                            //获取当前正在运行的程序集
                            var xmlFile = $"{swg.XmlAssmblyName}";
                            // 获取xml文件路径
                            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                            options.IncludeXmlComments(xmlPath);
                        });
                    }
                }
            }
            return services;
        }

        /// <summary>
        ///  DLCore针对Swagger引用的扩展
        /// </summary>
        /// <param name="app"></param>
        public static void UseSwaggerPack(this IApplicationBuilder app)
        {
            if (engalbe)
            {
                app.UseSwagger();
                app.UseSwaggerUI(option =>
                {
                    option.SwaggerEndpoint($"/swagger/{docName}/swagger.json", docName);
                });
            }
        }
    }
}