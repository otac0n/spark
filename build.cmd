%systemroot%\microsoft.net\framework\v3.5\msbuild.exe /t:Rebuild /property:Configuration=Release;OutDir=../../build/spark/ src/Spark/Spark.csproj
%systemroot%\microsoft.net\framework\v3.5\msbuild.exe /t:Rebuild /property:Configuration=Release;OutDir=../../build/castle/ src/Castle.MonoRail.Views.Spark/Castle.MonoRail.Views.Spark.csproj
%systemroot%\microsoft.net\framework\v3.5\msbuild.exe /t:Rebuild /property:Configuration=Release;OutDir=../../build/aspnetmvc/ src/MvcContrib.SparkViewEngine/MvcContrib.SparkViewEngine.csproj
pause