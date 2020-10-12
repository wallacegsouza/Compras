#!/usr/bin/env bash

if [ -z $@ ]
then
    echo 'wait fo 40 seconds'
    sleep 40
else
    echo 'wait fo $@ seconds'
    sleep $@
fi

dotnet Compras.dll