################################################
## Comment Remover v1.0.0 - Linux             ##
## Language: Python                           ##
## Author: Jacob Waters                       ##
## Github: github.com/jpwaters09              ##
## Copyright (c) 2024 Jacob Waters            ##
## Contact me: jpwaters.github@gmail.com      ##
################################################

from PyQt5.QtWidgets import QApplication, QPushButton, QWidget, QLabel, QComboBox, QFileDialog, QLineEdit, QProgressBar
import sys

class Window(QWidget):
    def __init__(self):
        super().__init__()

        self.setWindowTitle("Comment Remover v1.0.0")
        self.setGeometry(0, 0, 600, 400)
        self.setFixedSize(600, 400)
        self.startMenu()

    def startMenu(self):
        self.startLabel = QLabel("Comment Remover:", self)
        self.startLabel.setStyleSheet("font-size: 25px;")
        self.startLabel.move(190, 0)
        self.startLabel.setVisible(True)

        self.startBtn = QPushButton("Click To Start", self)
        self.startBtn.clicked.connect(self.selectLang)
        self.startBtn.setMaximumWidth(130)
        self.startBtn.setMinimumHeight(50)
        self.startBtn.move(262, 150)
        self.startBtn.setVisible(True)

    def selectLang(self):
        self.startLabel.setVisible(False)
        self.startBtn.setVisible(False)

        self.selectLangLabel = QLabel("Select Coding Language:", self)
        self.selectLangLabel.setStyleSheet("font-size: 25px;")
        self.selectLangLabel.move(163, 0)
        self.selectLangLabel.setVisible(True)

        self.codeLangSelector = QComboBox(self)
        self.codeLangSelector.addItems(["Coding Language", "Python", "JavaScript", "C", "C++", "Lua", "Asm"])
        self.codeLangSelector.currentIndexChanged.connect(self.checkSelector)
        self.codeLangSelector.move(245, 150)
        self.codeLangSelector.setVisible(True)

        self.langBackBtn = QPushButton("Back", self)
        self.langBackBtn.setMaximumWidth(130)
        self.langBackBtn.setMinimumHeight(50)
        self.langBackBtn.clicked.connect(self.backToStartMenu)
        self.langBackBtn.move(25, 325)
        self.langBackBtn.setVisible(True)

        self.langNextBtn = QPushButton("Next", self)
        self.langNextBtn.setMaximumWidth(130)
        self.langNextBtn.setMinimumHeight(50)
        self.langNextBtn.clicked.connect(self.selectFile)
        self.langNextBtn.move(500, 325)
        self.langNextBtn.setVisible(False)

    def selectFile(self):
        self.selectLangLabel.setVisible(False)
        self.codeLangSelector.setVisible(False)
        self.langBackBtn.setVisible(False)
        self.langNextBtn.setVisible(False)

        self.selectFileLabel = QLabel("Select File:", self)
        self.selectFileLabel.setStyleSheet("font-size: 25px;")
        self.selectFileLabel.move(240, 0)
        self.selectFileLabel.setVisible(True)

        self.fileTextBox = QLineEdit(self)
        self.fileTextBox.setPlaceholderText("File Path:")
        self.fileTextBox.setReadOnly(True)
        self.fileTextBox.setMinimumWidth(500)
        self.fileTextBox.move(50, 100)
        self.fileTextBox.setVisible(True)

        self.selectFileBtn = QPushButton("Select File", self)
        self.selectFileBtn.setMaximumWidth(130)
        self.selectFileBtn.setMinimumHeight(50)
        self.selectFileBtn.clicked.connect(self.showFileDialog)
        self.selectFileBtn.move(262, 150)
        self.selectFileBtn.setVisible(True)

        self.fileBackBtn = QPushButton("Back", self)
        self.fileBackBtn.setMaximumWidth(130)
        self.fileBackBtn.setMinimumHeight(50)
        self.fileBackBtn.clicked.connect(self.backToSelectLang)
        self.fileBackBtn.move(25, 325)
        self.fileBackBtn.setVisible(True)

        self.fileNextBtn = QPushButton("Next", self)
        self.fileNextBtn.setMaximumWidth(130)
        self.fileNextBtn.setMinimumHeight(50)
        self.fileNextBtn.clicked.connect(self.removeStart)
        self.fileNextBtn.move(500, 325)
        self.fileNextBtn.setVisible(False)

    def removeStart(self):
        self.selectFileLabel.setVisible(False)
        self.fileTextBox.setVisible(False)
        self.selectFileBtn.setVisible(False)
        self.fileBackBtn.setVisible(False)
        self.fileNextBtn.setVisible(False)

        self.removeLabel = QLabel("Comment Remover:", self)
        self.removeLabel.setStyleSheet("font-size: 25px;")
        self.removeLabel.move(190, 0)
        self.removeLabel.setVisible(True)

        self.codeLabel = QLabel("Coding Language:", self)
        self.codeLabel.move(50, 84)
        self.codeLabel.setVisible(True)

        self.codeLangTextBox = QLineEdit(self)
        self.codeLangTextBox.setText(self.lang)
        self.codeLangTextBox.setReadOnly(True)
        self.codeLangTextBox.setMinimumWidth(100)
        self.codeLangTextBox.move(50, 100)
        self.codeLangTextBox.setVisible(True)

        self.fileLabel = QLabel("File Path:", self)
        self.fileLabel.move(50, 132)
        self.fileLabel.setVisible(True)

        self.filePathTextBox = QLineEdit(self)
        self.filePathTextBox.setText(self.file)
        self.filePathTextBox.setReadOnly(True)
        self.filePathTextBox.setMinimumWidth(500)
        self.filePathTextBox.move(50, 148)
        self.filePathTextBox.setVisible(True)

        self.removeBackBtn = QPushButton("Back", self)
        self.removeBackBtn.setMaximumWidth(130)
        self.removeBackBtn.setMinimumHeight(50)
        self.removeBackBtn.clicked.connect(self.backToSelectFile)
        self.removeBackBtn.move(25, 325)
        self.removeBackBtn.setVisible(True)

        self.removeBtn = QPushButton("Remove Comments", self)
        self.removeBtn.setMaximumWidth(130)
        self.removeBtn.setMinimumHeight(50)
        self.removeBtn.clicked.connect(self.startRemove)
        self.removeBtn.move(475, 325)
        self.removeBtn.setVisible(True)

    def startRemove(self):
        self.removeLabel.setVisible(False)
        self.codeLabel.setVisible(False)
        self.codeLangTextBox.setVisible(False)
        self.fileLabel.setVisible(False)
        self.filePathTextBox.setVisible(False)
        self.removeBackBtn.setVisible(False)
        self.removeBtn.setVisible(False)

        self.loadingTitleLabel = QLabel("Comment Remover:", self)
        self.loadingTitleLabel.setStyleSheet("font-size: 25px;")
        self.loadingTitleLabel.move(190, 0)
        self.loadingTitleLabel.setVisible(True)

        self.loadingLabel = QLabel("Removing Comments:", self)
        self.loadingLabel.move(244, 130)
        self.loadingLabel.setVisible(True)

        self.loadingBar = QProgressBar(self)
        self.loadingBar.setGeometry(130, 150, 350, 20)
        self.loadingBar.setValue(0)
        self.loadingBar.setVisible(True)

        QApplication.processEvents()

        commentH = ""
        commentS = ""
        commentSC = ""

        if self.lang == "Python":
            commentH = "#"

        if self.lang == "JavaScript":
            commentS = "//"

        if self.lang == "C":
            commentS = "//"

        if self.lang == "C++":
            commentS = "//"

        if self.lang == "Lua":
            commentH = "#"

        if self.lang == "Asm":
            commentSC = ";"

        def is_char_escaped(line, index):
            backslashes = 0

            while index > 0 and line[index - 1] == '\\':
                backslashes += 1
                index -= 1

            return backslashes % 2 == 1

        comments_removed = """"""

        with open(self.file, "r") as rfile:
            lines = rfile.readlines()

        self.loadingBar.setMaximum(len(lines))

        for idx, line in enumerate(lines):
            inside_single_quote = False
            inside_double_quote = False
            inside_backtick = False

            i = 0

            while i < len(line):
                if line[i] == "'" and not is_char_escaped(line, i) and not inside_double_quote and not inside_backtick:
                    inside_single_quote = not inside_single_quote

                elif line[i] == '"' and not is_char_escaped(line, i) and not inside_single_quote and not inside_backtick:
                    inside_double_quote = not inside_double_quote

                elif line[i] == "`" and not is_char_escaped(line, i) and not inside_double_quote and not inside_single_quote:
                    inside_backtick = not inside_backtick

                elif commentS == "//":
                    if line[i:i+2] == commentS and not (inside_single_quote or inside_double_quote or inside_backtick):
                        line = line[:i].rstrip()
                        break
                
                elif commentH == "#":
                    if line[i] == commentH and not (inside_single_quote or inside_double_quote or inside_backtick):
                        line = line[:i].rstrip()
                        break

                elif commentSC == ";":
                    if line[i] == commentSC and not (inside_single_quote or inside_double_quote or inside_backtick):
                        line = line[:i].rstrip()
                        break

                i += 1

            comments_removed += line.rstrip()

            if idx < len(lines) - 1:
                comments_removed += "\n"

            self.loadingBar.setValue(idx + 1)
            QApplication.processEvents()

        with open(self.file, "w") as wfile:
            wfile.write(comments_removed)

        self.loadingTitleLabel.setVisible(False)
        self.loadingLabel.setVisible(False)
        self.loadingBar.setVisible(False)

        self.finishedTitleLabel = QLabel("Comment Remover:", self)
        self.finishedTitleLabel.setStyleSheet("font-size: 25px;")
        self.finishedTitleLabel.move(190, 0)
        self.finishedTitleLabel.setVisible(True)

        self.finishedLabel = QLabel("Removed Comments!", self)
        self.finishedLabel.move(250, 160)
        self.finishedLabel.setVisible(True)

        self.removeBtn = QPushButton("Close", self)
        self.removeBtn.setMaximumWidth(130)
        self.removeBtn.setMinimumHeight(50)
        self.removeBtn.clicked.connect(self.closeWindow)
        self.removeBtn.move(262, 200)
        self.removeBtn.setVisible(True)

    def closeWindow(self):
        sys.exit()

    def backToSelectFile(self):
        self.removeLabel.setVisible(False)
        self.codeLabel.setVisible(False)
        self.codeLangTextBox.setVisible(False)
        self.fileLabel.setVisible(False)
        self.filePathTextBox.setVisible(False)
        self.removeBackBtn.setVisible(False)

        self.selectFile()

    def showFileDialog(self):
        if self.lang == "Python":
            file_path, _ = QFileDialog.getOpenFileName(self, "Open File", '', "Python Files (*.py)")

        if self.lang == "JavaScript":
            file_path, _ = QFileDialog.getOpenFileName(self, "Open File", '', "JavaScript Files (*.js)")

        if self.lang == "C":
            file_path, _ = QFileDialog.getOpenFileName(self, "Open File", '', "C Files (*.c)")

        if self.lang == "C++":
            file_path, _ = QFileDialog.getOpenFileName(self, "Open File", '', "C++ Files (*.cpp)")

        if self.lang == "Lua":
            file_path, _ = QFileDialog.getOpenFileName(self, "Open File", '', "Lua Files (*.lua)")

        if self.lang == "Asm":
            file_path, _ = QFileDialog.getOpenFileName(self, "Open File", '', "Asm Files (*.asm)")

        if file_path:
            self.fileTextBox.setText(file_path)
            self.file = file_path
            self.fileNextBtn.setVisible(True)

    def backToStartMenu(self):
        self.selectLangLabel.setVisible(False)
        self.codeLangSelector.setVisible(False)
        self.langBackBtn.setVisible(False)
        self.langNextBtn.setVisible(False)

        self.lang = ""

        self.startMenu()

    def backToSelectLang(self):
        self.selectFileLabel.setVisible(False)
        self.fileTextBox.setVisible(False)
        self.selectFileBtn.setVisible(False)
        self.fileBackBtn.setVisible(False)
        self.fileNextBtn.setVisible(False)

        self.selectLang()

    def checkSelector(self):
        selected_text = self.codeLangSelector.currentText()

        if selected_text == "Coding Language":
            self.lang = ""
            self.langNextBtn.setVisible(False)

        if selected_text == "Python":
            self.lang = "Python"
            self.langNextBtn.setVisible(True)

        if selected_text == "JavaScript":
            self.lang = "JavaScript"
            self.langNextBtn.setVisible(True)

        if selected_text == "C":
            self.lang = "C"
            self.langNextBtn.setVisible(True)

        if selected_text == "C++":
            self.lang = "C++"
            self.langNextBtn.setVisible(True)

        if selected_text == "Lua":
            self.lang = "Lua"
            self.langNextBtn.setVisible(True)

        if selected_text == "Asm":
            self.lang = "Asm"
            self.langNextBtn.setVisible(True)

app = QApplication(sys.argv)
window = Window()
window.show()

sys.exit(app.exec_())