
双击 publish.bat 

复制项目 至linux服务中，

```bash
root@VM-37-104-ubuntu:/home/ubuntu/# sudo su
root@VM-37-104-ubuntu:/home/ubuntu/# cd qiniu-web
root@VM-37-104-ubuntu:/home/ubuntu/qiniu-web# ls
appsettings.Development.json  Microsoft.VisualStudio.Web.CodeGeneration.dll Qiniu.Web.deps.json Swashbuckle.AspNetCore.SwaggerGen.dll
...
```
前置条件，在ubuntu上安装好了docker。并且正常运行。

-d 代表后台运行，此时将对外显露5000端口运行，5000是运行后，docker对外的端口，80是这个服务对外的端口，其中Dockerfile 存在语句EXPOSE 80

```bash
docker build -t igeekfan/qiniu .     #生成images
docker run -d -p 5000:80 igeekfan/qiniu  # 生成 container 并运行在5000端口
```
此时打开 浏览器， ip+端口5000即可访问服务，请加/swagger。

本项目已部署至服务器 [http://122.152.192.161:5000/swagger/index.html](http://122.152.192.161:5000/swagger/index.html)