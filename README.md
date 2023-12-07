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
./DataProcess.exe -s "C:\Users\Administrator\Desktop\报表处理\input.xlsx" -t "C:\Users\Administrator\Desktop\报表处理" -r 10 -c A -e M -f "{C2}_{F2}_{E3}.xlsx"
```