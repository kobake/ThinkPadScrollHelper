// CppPrototype.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include "pch.h"
#include <iostream>
#include <windows.h>
#include <psapi.h>
#include <CommCtrl.h>
HWND hwndCheck = (HWND)0x00010504;
HWND hwndButton = (HWND)0x000200BE;

void EnableThinkPadScroll(bool b) {
    wchar_t buf[256] = L"";
    GetWindowText(hwndCheck, buf, _countof(buf));
    // printf("%ls\n", buf);

    if (b) {
        PostMessage(hwndCheck, BM_SETCHECK, BST_CHECKED, 0);
        PostMessage(hwndButton, BM_CLICK, 0, 0);
    }
    else {
        PostMessage(hwndCheck, BM_SETCHECK, BST_UNCHECKED, 0);
        PostMessage(hwndButton, BM_CLICK, 0, 0);
    }
}

// TODO: "C:\Program Files (x86)\Lenovo\ThinkPad Compact Keyboard with TrackPoint driver\HScrollFun.exe" の自動復帰もあとで入れる.
// TODO: 相手プロセスが admin だとダメ

int main()
{
    setlocale(LC_ALL, "");

    // ウィンドウ名表示テスト
    if (false) {
        
        while (1) {
            ::Sleep(1000);

            // 現在のマウス配下ウィンドウ判定
            HWND hwnd = NULL;
            POINT p;
            GetCursorPos(&p);
            hwnd = ::WindowFromPoint(p);
            if (!hwnd) {
                hwnd = ::GetForegroundWindow();
            }

            // これだと自プロセスのものしか取得できない
            // wchar_t buf[256] = L"";
            // ::GetWindowModuleFileName(hwnd, buf, _countof(buf));
            // printf("%ls\n", buf);

            DWORD pid = 0;
            ::GetWindowThreadProcessId(hwnd, &pid);
            printf("pid = %d\n", pid);

            HANDLE hProcess = ::OpenProcess(PROCESS_QUERY_INFORMATION | PROCESS_VM_READ, FALSE, pid);
            if (hProcess && hProcess != INVALID_HANDLE_VALUE) {
                wchar_t buf[256] = L"";
                ::GetProcessImageFileName(hProcess, buf, _countof(buf));
                // ::GetModuleBaseName(hProcess, NULL, buf, _countof(buf));
                printf("%ls\n", buf);
                ::CloseHandle(hProcess);
            }

            /*
            if (hProcess && hProcess != INVALID_HANDLE_VALUE) {
                HMODULE hModules[1024] = { NULL };
                DWORD cbNeeded = 0;
                if (::EnumProcessModules(hProcess, hModules, sizeof(hModules), &cbNeeded)) {
                    for (int i = 0; i < cbNeeded / sizeof(HMODULE); i++) {
                        wchar_t buf[256] = L"";
                        ::GetModuleFileNameEx(hProcess, hModule, )

                    }
                }
                ::GetModuleFileName((HMODULE)hProcess, buf, _countof(buf));
                printf("%ls\n", buf);


                ::CloseHandle(hProcess);
            }
            */
        }
    }

    bool vs = false;
    while (1) {
        // 定期スリープ
        ::Sleep(80);
        // int n = TCM_SETCURFOCUS;
        int n = TCM_SETCURFOCUS;

        // バックグラウンドプロセスの監視
        // https://msdn.microsoft.com/ja-jp/library/z3w4xdc9(v=vs.110).aspx
        // #define BACKGROUND_PROCESS "C:\\Program Files(x86)\\Lenovo\\ThinkPad Compact Keyboard with TrackPoint driver\\HScrollFun.exe"
        // ::ShellExecuteA(NULL, "OPEN", BACKGROUND_PROCESS, "", NULL, 0);
        // ::CreateProcessA(BACKGROUND_PROCESS, "\"" BACKGROUND_PROCESS "\"", NULL, NULL, FALSE, 0, NULL, NULL, NULL, NULL);

        // 現在のマウス配下ウィンドウ判定
        HWND hwnd = NULL;
        // hwnd = ::GetForegroundWindow();
        POINT p;
        GetCursorPos(&p);
        hwnd = ::WindowFromPoint(p);
        if (!hwnd) {
            hwnd = ::GetForegroundWindow();
        }

        // ウィンドウ種別判定
        bool currentVs = false;
        wchar_t szWindowClass[256] = L"";
        ::RealGetWindowClass(hwnd, szWindowClass, _countof(szWindowClass));
        /*
        wchar_t buf[256];
        GetWindowText(hwnd, buf, _countof(buf));
        if (wcsstr(buf, L" - Microsoft Visual Studio")) {
            currentVs = true;
        }
        else if (wcsstr(buf, L" - Microsoft SQL Server Management Studio")) {
            currentVs = true;
        }
        else {
            currentVs = false;
        }
        */
        wchar_t buf[256] = L"";
        DWORD pid = 0;
        ::GetWindowThreadProcessId(hwnd, &pid);
        // printf("pid = %d\n", pid);
        HANDLE hProcess = ::OpenProcess(PROCESS_QUERY_INFORMATION | PROCESS_VM_READ, FALSE, pid);
        if (hProcess && hProcess != INVALID_HANDLE_VALUE) {
            ::GetProcessImageFileName(hProcess, buf, _countof(buf));
            // printf("%ls\n", buf);
            if (wcsstr(buf, L"\\Microsoft Visual Studio\\")) {
                currentVs = true;
                if (wcsstr(buf, L"\\VsDebugConsole.exe")) {
                    currentVs = false;
                }
            }
            else if (wcsstr(buf, L"\\Microsoft SQL Server\\")) {
                currentVs = true;
            }
            else if (wcsstr(buf, L"\\SourceTree.exe")) {
                currentVs = true;
            }
            else {
                currentVs = false;
            }
            ::CloseHandle(hProcess);
        }
        else {
            currentVs = true;

            // HSCROLLFUN そのものである場合もプロセス名取得には失敗する
            if (wcscmp(szWindowClass, L"mywnd_winclass") == 0) {
                currentVs = false;
            }
        }

        // ウィンドウに応じてモードを切り替える
        if (currentVs != vs) {
            vs = currentVs;
            printf(vs ? "vs: 0x%08X %ls %ls\n" : "not vs: 0x%08X %ls %ls\n", hwnd, szWindowClass, buf);
            EnableThinkPadScroll(!vs);
        }
    }
    // EnableThinkPadScroll(false);
}

// Run program: Ctrl + F5 or Debug > Start Without Debugging menu
// Debug program: F5 or Debug > Start Debugging menu

// Tips for Getting Started: 
//   1. Use the Solution Explorer window to add/manage files
//   2. Use the Team Explorer window to connect to source control
//   3. Use the Output window to see build output and other messages
//   4. Use the Error List window to view errors
//   5. Go to Project > Add New Item to create new code files, or Project > Add Existing Item to add existing code files to the project
//   6. In the future, to open this project again, go to File > Open > Project and select the .sln file
