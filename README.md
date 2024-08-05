# Comment Remover
Removes comments from Python, JavaScript, Java, C, C++, C#, Lua, and Asm.
---
Hi, I'm Jacob,
Follow me for coding Tutorials / Projects.\
If you have any project ideas or suggestions please contact me at jpwaters.github@gmail.com \
Follow my Github: https://github.com/jpwaters09 \
Join my discord server: https://discord.gg/76dFqekSXz

# Running from release:
## For Windows: 
Double click on the Comment_Remover-v1.1.0-Windows.exe file. \
Or run in cmd:
```batch
./Comment_Remover-v1.1.0-Windows.exe
```

## For Linux: 
Run in terminal:
```bash
chmod +x Comment_Remover-v1.1.0-Linux
```
Then double click on Comment_Remover-v1.1.0-Linux file. \
Or run in terminal:
```bash
./Comment_Remover-v1.1.0-Linux
```

# Building:
1. Install python: \
   For Windows (64-Bit): [Python 3.12.3](https://www.python.org/ftp/python/3.12.3/python-3.12.3-amd64.exe) \
   For Windows (32-Bit): [Python 3.12.3](https://www.python.org/ftp/python/3.12.3/python-3.12.3.exe) \
   For Linux: [Python 3.12.3](https://www.python.org/ftp/python/3.12.3/Python-3.12.3.tgz)
   
2. Install required python packages: \
   For Windows open the terminal by typing 'cmd' in the Windows search and clicking enter:
   ```batch
   pip install pyqt5 pillow pyinstaller
   ```

   For Ubuntu / Debian Linux open the terminal by typing 'terminal' in the search:
   ```bash
   sudo apt update
   sudo apt install python3-pip -y
   pip install pyqt5 pyinstaller
   ```

4. Clone the repository: \
   For Windows: \
   Download the code: [Comment Remover](https://github.com/Jpwaters09/Comment-Remover/archive/refs/heads/main.zip). \
   Unzip the file, then in cmd run:
   ```batch
   cd "Comment-Remover-main\Comment-Remover-main\Windows"
   ```

   For Ubuntu / Debian Linux: \
   Download the code: [Comment Remover](https://github.com/Jpwaters09/Comment-Remover/archive/refs/heads/main.zip). \
   Unzip the file, then in terminal run:
   ```bash
   cd "Comment-Remover-main\Comment-Remover-main\Linux"
   ```

5. Build the application: \
   For Windows, in cmd:
   ```batch
   ./Compile.bat
   ```
   
   For Ubuntu / Debian Linux, in terminal:
   ```bash
   chmod +x Compile.sh
   ./Compile.sh
   ```

6. Running the application: \
   For Windows: \
   Double click on the Comment_Remover-v1.1.0-Windows.exe file. \
   Or run in cmd:
   ```batch
   ./"dist/Comment_Remover-v1.1.0-Windows.exe"
   ```

   For Ubuntu / Debian Linux: \
   Double click on the file. \
   Or run in terminal:
   ```bash
   ./"dist/Comment_Remover-v1.1.0-Linux"
   ```
