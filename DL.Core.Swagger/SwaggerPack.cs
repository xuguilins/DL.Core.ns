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
using DL.Core.utility.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

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
                var swaggerInfo = ConfigerManager.Instance.Configuration.GetSwaggerSetting();
                if (swaggerInfo != null)
                {
                    if (swaggerInfo != null)
                    {
                        var swg = swaggerInfo;
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
                                if (swg.Authorization)
                                {
                                    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                                    {
                                        Description = "请求头中需要添加Jwt授权Token：Bearer Token",
                                        Name = "Authorization",
                                        In = ParameterLocation.Header,
                                        Type = SecuritySchemeType.ApiKey
                                    });
                                    options.AddSecurityRequirement(new OpenApiSecurityRequirement
                                    {
                                        {
                                            new OpenApiSecurityScheme
                                            {
                                                Reference = new OpenApiReference {
                                                    Type = ReferenceType.SecurityScheme,
                                                    Id = "Bearer"
                                                }
                                            },
                                            new string[] { }
                                        }
                                    });


                                   
                                }
                                if (string.IsNullOrWhiteSpace(swg.XmlAssmblyName))
                                    throw new Exception("无效的xml文件,请在配置文件中配置所需的xml文件");
                                var xmlList = swg.XmlAssmblyName.Split(',');
                                foreach (var xml in xmlList)
                                {
                                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xml);
                                    options.IncludeXmlComments(xmlPath);
                                }
                            });
                            if (swg.Authorization) {
                                #region [添加JWT认证]
                                // 添加验证服务
                                services.AddAuthentication(x =>
                                {
                                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                                }).AddJwtBearer(o =>
                                {
                                    o.TokenValidationParameters = new TokenValidationParameters
                                    {
                                        // 是否开启签名认证
                                        ValidateIssuerSigningKey = true,
                                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(swg.JwtSecret)), //密钥
                                        // 发行人验证，这里要和token类中Claim类型的发行人保持一致
                                        ValidateIssuer = true,
                                        ValidIssuer = swg.Issuer,//发行人
                                        ValidateAudience = false,
                                        ValidAudience = swg.Issuer,//接收人
                                        ValidateLifetime = true,
                                        ClockSkew = TimeSpan.Zero,
                                    };
                                });
                                #endregion

                            }



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