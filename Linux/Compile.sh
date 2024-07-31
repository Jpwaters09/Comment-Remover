#!/bin/bash

set +x

/home/$USER/.local/bin/pyinstaller --noconsole --onefile "Comment_Remover-Linux.py"

rm "Comment_Remover-Linux.spec"
rm -rf build
