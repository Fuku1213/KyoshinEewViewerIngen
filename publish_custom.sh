#!/bin/bash

~/.dotnet/dotnet publish src/KyoshinEewViewer/KyoshinEewViewer.csproj -r $2 -c release -o tmp/$2_$3 -p:PublishReadyToRun=false -p:PublishSingleFile=true -p:PublishTrimmed=$4 --self-contained $4
chmod +x tmp/$2_$3/KyoshinEewViewer
zip -r tmp/KyoshinEewViewer_ingen_$2_$3.zip tmp/$2_$3
