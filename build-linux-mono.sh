mkdir bin
mkdir bin\mono

gmcs /out:bin/mono/sgfconvert *.cs Utility\*.cs  /optimize+
