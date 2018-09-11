# ThinkPadScrollHelper

![](https://raw.githubusercontent.com/kobake/ThinkPadScrollHelper/master/img/thinkpad.jpg)

## Solver for ThinkPad Keyboard driver problem
This software ThinkPadScrollHelper solves the problem of the ThinkPad USB Keyboard driver.

Specifically, it behaves as follows.
- Avoid crashing of the driver, and restart the driver even if the driver crashed.
- Automatically switch scrolling mode according to the software you are using.

## About ThinkPad Keyboard driver
You can get the driver of the ThinkPad Compact USB Keyboard from https://support.lenovo.com/us/en/solutions/pd026745,
and you installed it then **HScrollFun.exe** process is always running as background.

## About the driver process problem
Unfortunately **HScrollFun.exe** process crashes when you try to scroll by "ThinkPad Preferred Scrolling" mode on some softwares, for example, on Visual Studio or SourceTree, etc.

This software ThinkPadScrollHelper solves the problem above.
