#!/bin/bash

set +x

/home/$USER/.local/bin/pyinstaller --noconsole --onefile "Comment_Remover-v1.1.0-Linux.py"

rm "Comment_Remover-v1.1.0-Linux.spec"
rm -rf build
