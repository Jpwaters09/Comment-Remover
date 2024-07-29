rmdir /s /q dist

pyinstaller --noconsole --onefile --icon="Comment Remover Icon.ico" --version-file="Comment Remover Version File" "Comment Remover.py"

del "Comment Remover.spec"
rmdir /s /q build
