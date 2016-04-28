# NPP.PasteRelativePath
Notepad++ Plugin for pasting relative paths (written in C#)

To install, download `NPP.PasteRelativePath.dll` from the release folder and drop it in `C:\Program Files (x86)\Notepad++\plugins`.  If you have Notepad++ started, you'll have to restart it.

To use, copy a path into your clipboard, open a document, and press `Ctrl+Shift+V` (or `Plugins -> NPP.PasteRelativePath -> Paste Relative`), and the path pasted will be relative to the currently opened document.

By default, path matching is done with case insensitivity (for Windows users.)  If you need to support case sensitive paths, you can enable it by selecting `Plugins -> NPP.PasteRelativePath -> Use case sensitive paths`.