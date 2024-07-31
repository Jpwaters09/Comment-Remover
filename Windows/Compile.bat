@echo off

pyinstaller --noconsole --onefile --icon="Comment_Remover-v1.1.0-Windows-Icon.ico" --version-file="Comment_Remover-v1.1.0-Windows-Version_File" "Comment_Remover-v1.1.0-Windows.py"

del "Comment_Remover-v1.1.0-Windows.spec"
rmdir /s /q build
