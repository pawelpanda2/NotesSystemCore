#!/usr/bin/osascript

tell application "Finder"
    if (count of windows) is 0 then
        set dir to (desktop as alias)
    else
        set dir to ((target of Finder window 1) as alias)
    end if
    return POSIX path of dir
end tell

# https://scriptingosx.com/2022/05/launching-scripts-4-applescript-from-shell-script/
