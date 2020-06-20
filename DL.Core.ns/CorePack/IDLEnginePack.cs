using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Core.ns.CorePack
{
    public interface IDLEnginePack
    {
        /// <summary>
        /// 获取所有PACK
        /// </summary>
        List<PackBase> LoadPacks { get; }

        /// <summary>
        /// 加载引擎内置Pack
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        IServiceCollection AddEnginePack(IServiceCollection services);
    }
}