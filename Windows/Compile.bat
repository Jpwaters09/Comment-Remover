@echo off

pyinstaller --noconsole --onefile --icon="Comment_Remover-Windows-Icon.ico" --version-file="Comment_Remover-Windows-Version_File" "Comment_Remover-Windows.py"

del "Comment_Remover-Windows.spec"
rmdir /s /q build
