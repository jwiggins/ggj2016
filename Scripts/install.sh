#! /bin/sh

UNITY_PAKCAGE_URL="http://netstorage.unity3d.com/unity/5a2e8fe35a68/MacEditorInstaller/Unity.pkg"

echo 'Downloading from ${UNITY_PAKCAGE_URL}: '
curl -o Unity.pkg $UNITY_PAKCAGE_URL

echo 'Installing Unity.pkg'
sudo installer -dumplog -package Unity.pkg -target /
