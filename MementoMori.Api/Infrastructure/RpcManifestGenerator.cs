using System.Text;
using MementoMori.Api.Services;

namespace MementoMori.Api.Infrastructure
{
    public static class RpcManifestGenerator
    {
        public static void Generate(OrtegaApiDiscoveryService discoveryService, string outputDir)
        {
            var apis = discoveryService.GetAllApis();
            var sortedApis = apis.Values.OrderBy(a => a.Uri).ToList();
            
            // 收集所有需要导入的类型名
            var typesToImport = new HashSet<string>();
            foreach (var api in apis.Values)
            {
                typesToImport.Add(api.RequestType.Name);
                typesToImport.Add(api.ResponseType.Name);
            }
            var sortedTypes = typesToImport.OrderBy(t => t).ToList();

            // === 1. 生成 ortega-rpc-manifest.ts (纯类型) ===
            GenerateManifest(sortedApis, sortedTypes, Path.Combine(outputDir, "ortega-rpc-manifest.ts"));

            // === 2. 生成 ortega-client.ts (运行时辅助对象) ===
            GenerateClient(sortedApis, Path.Combine(outputDir, "ortega-client.ts"));
        }

        private static void GenerateManifest(List<OrtegaApiInfo> sortedApis, List<string> sortedTypes, string outputPath)
        {
            var sb = new StringBuilder();
            sb.AppendLine("// @ts-nocheck");
            sb.AppendLine("// 该文件由 C# 后端自动生成，仅包含类型定义，请勿手动修改");
            sb.AppendLine("/* eslint-disable */");
            sb.AppendLine("");
            sb.AppendLine("import type {");
            foreach (var typeName in sortedTypes)
            {
                sb.AppendLine($"    {typeName},");
            }
            sb.AppendLine("} from './generated';");
            sb.AppendLine("");
            sb.AppendLine("export interface OrtegaRpcMap {");
            foreach (var api in sortedApis)
            {
                sb.AppendLine($"    \"{api.Uri}\": {{");
                sb.AppendLine($"        request: {api.RequestType.Name};");
                sb.AppendLine($"        response: {api.ResponseType.Name};");
                sb.AppendLine("    };");
            }
            sb.AppendLine("}");

            WriteFile(outputPath, sb.ToString());
        }

        private static void GenerateClient(List<OrtegaApiInfo> sortedApis, string outputPath)
        {
            var sb = new StringBuilder();
            sb.AppendLine("// @ts-nocheck");
            sb.AppendLine("// 该文件由 C# 后端自动生成，包含运行时辅助对象，请勿手动修改");
            sb.AppendLine("/* eslint-disable */");
            sb.AppendLine("");
            sb.AppendLine("import rpcClient from './rpc-client';");
            sb.AppendLine("import type { OrtegaRpcMap } from './ortega-rpc-manifest';");
            sb.AppendLine("");
            sb.AppendLine("export const ortegaApi = {");

            var groups = sortedApis.GroupBy(a => a.Uri.Split('/')[0]);
            foreach (var group in groups)
            {
                var category = group.Key;
                sb.AppendLine($"    {category}: {{");
                foreach (var api in group)
                {
                    var action = api.Uri.Split('/').Length > 1 ? api.Uri.Split('/')[1] : "index";
                    action = action.Trim();
                    sb.AppendLine($"        /** {api.Uri} */");
                    sb.AppendLine($"        {action}: (request: OrtegaRpcMap[\"{api.Uri}\"][\"request\"]) =>");
                    sb.AppendLine($"            rpcClient.call<OrtegaRpcMap[\"{api.Uri}\"][\"response\"]>(\"{api.Uri}\", request),");
                }
                sb.AppendLine("    },");
            }
            
            sb.AppendLine("");
            sb.AppendLine("    /** 通用调用接口 */");
            sb.AppendLine("    call: <K extends keyof OrtegaRpcMap & string>(");
            sb.AppendLine("        uri: K,");
            sb.AppendLine("        request: OrtegaRpcMap[K]['request']");
            sb.AppendLine("    ) => rpcClient.call<OrtegaRpcMap[K]['response']>(uri, request),");
            sb.AppendLine("};");
            sb.AppendLine("");
            sb.AppendLine("export default ortegaApi;");

            WriteFile(outputPath, sb.ToString());
        }

        private static void WriteFile(string path, string content)
        {
            try
            {
                var directory = Path.GetDirectoryName(path);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                File.WriteAllText(path, content, Encoding.UTF8);
                Console.WriteLine($"[RpcManifestGenerator] Successfully exported to {path}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[RpcManifestGenerator] Error writing to {path}: {ex.Message}");
                throw;
            }
        }
    }
}
