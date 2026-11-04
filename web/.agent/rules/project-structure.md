---
trigger: always_on
---

我们正在重构一个blazor项目, 重构为一个完全前后端分离的项目.

./blazor/ 目录是原始项目
./api/ 目录是我们正在重构的项目,其中 api\MementoMori.Api 是入口
./ 是前端项目


docs\提取的游戏内帮助文档.md 这个文档是我们的业务说明书

.\api\MementoMori.Ortega 这个项目是从游戏文件(il2cpp)提取出来的部分 C# 代码
.\api\MementoMori.Ortega\Share\Data\ApiInterface 里面是游戏的接口,我们在 api\MementoMori.Api\Controllers\OrtegaProxyController.cs 实现了自动化代理


项目使用 typegen 从C#代码生成typescript的类型定义
api\MementoMori.Api\tgconfig.json
pnpm run generate-types




所有生成的文档使用中文