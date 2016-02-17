#! /bin/sh

UNITY_PACKAGE_URL="http://netstorage.unity3d.com/unity/e87ab445ead0/MacEditorInstaller/Unity-5.3.2f1.pkg"

echo 'Downloading from $UNITY_PACKAGE_URL: '
curl -o Unity.pkg $UNITY_PACKAGE_URL

echo 'Installing Unity.pkg'
sudo installer -dumplog -package Unity.pkg -target /
