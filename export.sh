dotnet publish --os win -c Release
dotnet publish --os linux -c Release
zip -r stellarthing_windows.zip stellarthing/bin/Release/net8.0/win-x64/publish
tar -czf stellarthing_linux.tar.gz stellarthing/bin/Release/net8.0/linux-x64/publish
echo "Stellarthing has been successfully exported!"