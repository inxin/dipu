@SET PATH=%PATH%;C:\Program Files\MSBuild\14.0\Bin;C:\Program Files (x86)\MSBuild\14.0\Bin;C:\Windows\Microsoft.NET\Framework64\v4.0.30319;C:\Windows\Microsoft.NET\Framework\v4.0.30319

rmdir /s /q .\Domain\Generated

msbuild Dipu.sln /target:Clean /verbosity:minimal

msbuild Import.proj /verbosity:minimal

msbuild Dipu.sln /target:Merge:Rebuild /p:Configuration="Debug" /verbosity:minimal
msbuild Resources/Merge.proj /verbosity:minimal

msbuild Dipu.sln /target:Meta:Rebuild /p:Configuration="Debug" /verbosity:minimal

msbuild Domain/Generate.proj /verbosity:minimal
msbuild Website/Generate.proj /verbosity:minimal

msbuild Diagrams/Generate.proj /verbosity:minimal
rem msbuild Website.Diagrams/Generate.proj /verbosity:minimal

@pause
