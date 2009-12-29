PATH=C:\Program Files\Mono-1.2.5.2;%PATH%

mkdir bin
mkdir bin\mono
gmcs -out:bin\mono\sgfconvert.exe -optimize+ *.cs Utility\*.cs
