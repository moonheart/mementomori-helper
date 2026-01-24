using System;
using System.Linq;
using TypeGen.Core.SpecGeneration;
using MementoMori.Ortega.Share.Data.ApiInterface;

namespace MementoMori.Api.TypeGen
{
    /// <summary>
    /// TypeGen Generation Spec for Ortega API types
    /// 自动生成所有 Ortega Request 和 Response 类型的 TypeScript 定义
    /// </summary>
    public class OrtegaGenerationSpec : GenerationSpec
    {
        public override void OnBeforeGeneration(OnBeforeGenerationArgs args)
        {
            // 获取 Ortega 程序集
            var ortegaAssembly = typeof(ApiRequestBase).Assembly;

            // 生成所有 Request 类型
            var requestTypes = ortegaAssembly.GetTypes()
                .Where(t => t.IsClass 
                    && !t.IsAbstract 
                    && IsValidType(t)
                    && t.IsSubclassOf(typeof(ApiRequestBase))
                    && t.Namespace != null
                    && t.Namespace.StartsWith("MementoMori.Ortega.Share.Data.ApiInterface"));

            foreach (var type in requestTypes)
            {
                AddClass(type);
            }

            // 生成所有 Response 类型
            var responseTypes = ortegaAssembly.GetTypes()
                .Where(t => t.IsClass 
                    && !t.IsAbstract
                    && IsValidType(t)
                    && t.IsSubclassOf(typeof(ApiResponseBase))
                    && t.Namespace != null
                    && t.Namespace.StartsWith("MementoMori.Ortega.Share.Data.ApiInterface"));

            foreach (var type in responseTypes)
            {
                AddClass(type);
            }

            // 生成相关的数据类型（被 Request/Response 引用的类型）
            var dataTypes = ortegaAssembly.GetTypes()
                .Where(t => t.IsClass 
                    && !t.IsAbstract
                    && IsValidType(t)
                    && t.Namespace != null
                    && (t.Namespace.StartsWith("MementoMori.Ortega.Share.Data")
                        || t.Namespace.StartsWith("MementoMori.Ortega.Share.Enums")
                        || t.Namespace.StartsWith("MementoMori.Ortega.Share.Master.Data"))
                    && !t.IsSubclassOf(typeof(ApiRequestBase))
                    && !t.IsSubclassOf(typeof(ApiResponseBase)));

            foreach (var type in dataTypes)
            {
                // 排除一些不需要的类型
                if (type.Name.Contains("Attribute") || type.Name.Contains("Exception"))
                    continue;
                    
                AddClass(type);
            }

            // 生成所有 Ortega 枚举
            var enumTypes = ortegaAssembly.GetTypes()
                .Where(t => t.IsEnum
                    && t.Namespace != null
                    && t.Namespace.StartsWith("MementoMori.Ortega.Share"));

            foreach (var type in enumTypes)
            {
                AddEnum(type);
            }

            Console.WriteLine($"[OrtegaGenerationSpec] Added {requestTypes.Count()} Request types");
            Console.WriteLine($"[OrtegaGenerationSpec] Added {responseTypes.Count()} Response types");
            Console.WriteLine($"[OrtegaGenerationSpec] Added {dataTypes.Count()} Data types");
            Console.WriteLine($"[OrtegaGenerationSpec] Added {enumTypes.Count()} Enum types");
        }

        /// <summary>
        /// 检查类型是否有效（排除编译器生成的类型和特殊类型）
        /// </summary>
        private bool IsValidType(Type type)
        {
            // 排除编译器生成的类型（如闭包类）
            if (type.Name.Contains("<>") || type.Name.Contains("__"))
                return false;

            // 排除嵌套的编译器生成类型
            if (type.IsNested && type.DeclaringType != null)
            {
                if (type.DeclaringType.Name.Contains("<>"))
                    return false;
            }

            // 排除特殊类型
            if (type.IsPointer || type.IsByRef || type.IsGenericTypeDefinition)
                return false;

            return true;
        }
    }
}
