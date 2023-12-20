# Excel文件拆分工具

这个工具可以从一个Excel文件中拆分数据，并生成多个新的Excel文件，每个文件包含指定行数的数据。

## 使用者指南


### 下载和运行

下载release文件夹中的 DataProcess.exe 文件。由于打包了依赖项，该文件可以直接运行，代价是单文件比较大。

### 运行程序

打开命令行导航到保存该文件的目录 运行以下命令启动程序：

```bash
./DataProcess.exe <sourceFilePath> <targetDirectory> -r <rowsPerFile> -c <startCell> -e <endCell> -f <fileNameTemplate>
```
| 参数 | 说明 |
| --- | --- |
| sourceFilePath | 源Excel文件路径 |
| targetDirectory | 目标文件夹路径 |
| rowsPerFile | 每个文件包含的行数 |
| startCell | 开始单元格 |
| endCell | 结束单元格 |
| fileNameTemplate | 文件名模板 |
### 示例
```bash
 DataProcess  -s "C:\Users\Administrator\Desktop\DataProcess\data.xlsx" -t "C:\Users\Administrator\Desktop\DataProcess" -r 8 -c "A" -e "G" -f "{B2}.xlsx"
```
### 文件
[示例源文件](https://github.com/zhangjunL/DataProcess/files/13727316/data.xlsx)
#### 以下是分割后的文件
[张三.xlsx](https://github.com/zhangjunL/DataProcess/files/13727323/default.xlsx)
[王五.xlsx](https://github.com/zhangjunL/DataProcess/files/13727322/default.xlsx)
[里斯.xlsx](https://github.com/zhangjunL/DataProcess/files/13727318/default.xlsx)
### 截图

 1.分割前的总文件，此文件以8行为一个文件进行分割 所以填写参数  -r 8 ，开始列为A 结束列为G 以B2单元格中的姓名为分割后的文件名
 
![dataprocess示例1](https://github.com/zhangjunL/DataProcess/assets/22259618/bf73246d-554d-4df5-aaf9-480e714087a2)

 2.分割后生成的文件
 
![dataprocess示例2](https://github.com/zhangjunL/DataProcess/assets/22259618/364cc3a4-7bd4-4904-8508-bd252bc62b85)

 3.生成文件截图
 
![示例5](https://github.com/zhangjunL/DataProcess/assets/22259618/7c161bf3-fdfc-4f4d-8b59-24be9cc04d54)
![示例4](https://github.com/zhangjunL/DataProcess/assets/22259618/dda4cf77-8159-4a3e-af20-0e3f39546e1b)
![示例3](https://github.com/zhangjunL/DataProcess/assets/22259618/4acf0d5f-49c1-4121-800a-8602919999ee)


## 注意事项

- 本工具使用 [EPPlus](https://github.com/JanKallman/EPPlus) 库进行Excel文件的处理。
- 请确保源文件存在，目标目录存在，并提供正确的命令行参数。
- 在使用过程中如果遇到问题，请查看错误信息并调整参数。

## 贡献

如果你发现了任何问题，或者有改进的建议，欢迎提出 [issue](https://github.com/zhangjunL/DataProcess/issues) 或提交 [pull request](https://github.com/zhangjunL/DataProcess/pulls)。

## 许可证

这个工具是基于 MIT 许可证开源的。详细信息请参阅 [LICENSE](LICENSE) 文件。

