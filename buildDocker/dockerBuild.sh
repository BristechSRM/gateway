#!/bin/sh -eu
DIR=$(CDPATH= cd -- "$(dirname -- "$0")" && pwd)
echo $DIR

rm -rf $DIR/source
rm -rf $DIR/binaries
rm -rf $DIR/context
mkdir -p $DIR/source
mkdir -p $DIR/binaries
mkdir $DIR/context

git clone --depth 1 https://github.com/BristechSRM/gateway.git $DIR/source

docker run -v $DIR/source:/source \
    -v $DIR/binaries:/binaries \
    bristechsrm/build-fsharp /source/buildDocker/fsharpBuild.sh

cp $DIR/Dockerfile $DIR/context/Dockerfile
cp -R $DIR/binaries/ $DIR/context/
cd $DIR/context/
docker build -t gateway . 
IMAGEID=$(docker images gateway:latest -q)
docker tag $IMAGEID bristechsrm/gateway:latest
