:: Copy third-party components to the compilation directory
".\thirdparties\7za.exe" x ".\thirdparties\aria2c.7z" -y -aos -o".\debug\aria2c"
".\thirdparties\7za.exe" x ".\thirdparties\ffmpeg.7z" -y -aos -o".\debug\ffmpeg"
".\thirdparties\7za.exe" x ".\thirdparties\firefox.7z" -y -aos -o".\debug\firefox"

".\thirdparties\7za.exe" x ".\thirdparties\aria2c.7z" -y -aos -o".\release\aria2c"
".\thirdparties\7za.exe" x ".\thirdparties\ffmpeg.7z" -y -aos -o".\release\ffmpeg"
".\thirdparties\7za.exe" x ".\thirdparties\firefox.7z" -y -aos -o".\release\firefox"

:: Delete debug files in the release directory
rmdir /s/q ".\release\ru-ru"
del ".\release\*.pdb"
del ".\release\*.xml"
del ".\release\*.config"

:: Package the program to the dist directory
".\thirdparties\7za.exe" a ".\dist\MoocDownloader v_._._._.zip" ".\release\*" -mx9