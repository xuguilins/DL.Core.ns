﻿using System;
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
using DL.Core.utility.Logging;

namespace DL.Core.Swagger
{
    public static class SwaggerPack
    {
        private static string version = string.Empty;
        private static string title = string.Empty;
        private static bool engalbe = false;
        private static string docName = string.Empty;
        private static ILogger logger = LogManager.GetLogger();

        /// <summary>
        /// DLCore针对Swagger引用的扩展
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSwaggerPack(this IServiceCollection services)
        {
            try
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
                                if (string.IsNullOrWhiteSpace(swg.XmlAssmblyName))
                                    throw new Exception("无效的xml文件,请在配置文件中配置所需的xml文件");
                                var xmlList = swg.XmlAssmblyName.Split(',');
                                foreach (var xml in xmlList)
                                {
                                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xml);
                                    options.IncludeXmlComments(xmlPath);
                                }
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error($"创建swagger文件发生异常:${ex.Message}");
            }
            return services;
        }

        /// <summary>
        ///  DLCore针对Swagger引用的扩展
        /// </summary>
        /// <param name="app"></param>
        public static void UseSwaggerPack(this IApplicationBuilder app)
        {
            try
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
            catch (Exception ex)
            {
                logger.Error($"Use Swagger发生异常，ex:{ex.Message}");
            }
        }
    }
}