
# GTAsuspender

> **Archived – for educational purposes only.**
> This project no longer works with current versions of GTA Online and is kept solely as a learning reference. It is not intended to be used.

A small C# console application that temporarily suspends the GTA V process in order to end up alone in a public GTA Online session.

---

## Background

GTA Online uses a peer-to-peer networking model. When the game process is frozen for a few seconds, it stops responding to the other players in the session. Once resumed, the game assumes it lost connection with everyone else and creates a new public session containing only the local player.

This was a well-known manual trick, usually done through the Windows Resource Monitor. This project simply automated it.

## How it works

1. Find the running `GTA5` process with `Process.GetProcessesByName`
2. Suspend every thread of the process
3. Display a console progress bar for about 8 seconds (78 steps × 100 ms)
4. Resume every thread

The suspend/resume logic is implemented as extension methods on `System.Diagnostics.Process` (`proc.Suspend()` / `proc.Resume()`), using P/Invoke calls to `kernel32.dll`:

| Function       | Role                                              |
|----------------|---------------------------------------------------|
| `OpenThread`   | Opens a handle to a thread with `SUSPEND_RESUME` access |
| `SuspendThread`| Increments the thread's suspend count             |
| `ResumeThread` | Decrements the thread's suspend count             |

## What you can learn from it

- Finding a running process by name with `System.Diagnostics.Process`
- Enumerating a process's threads through `Process.Threads`
- Calling native Win32 APIs from C# with `[DllImport]`
- Using a `[Flags]` enum to represent Win32 access rights
- Writing extension methods to add behavior to an existing .NET class
- Basic console UI tricks (ASCII art, colors, cursor positioning, a text progress bar)

## Known issues

The code was written quickly and has a few flaws that are worth studying:

- **Handle leak**: thread handles returned by `OpenThread` are never closed with `CloseHandle`.
- **No process check**: `p[0]` throws an `IndexOutOfRangeException` if GTA V is not running.
- **`break` instead of `continue`**: if a single thread cannot be opened, the loop stops and the remaining threads are left untouched, which can leave the process partially suspended.
- **Hardcoded cursor position**: `Console.SetCursorPosition(0, 24)` depends on the height of the ASCII art and breaks if it changes.
- **No error handling or privilege check**: the program may silently fail if it is not run with sufficient rights.

## Building

Requirements:
- Windows
- .NET 6 or later (the project relies on implicit usings)
- Visual Studio or the .NET SDK

```bash
git clone https://github.com/princzcsl/GTAsuspender.git
cd GTAsuspender/GTAsuspender
dotnet build
```

## Status

This method has been patched or made unreliable by updates to GTA Online and is no longer functional. No further updates will be made to this repository.

## Disclaimer

This project is not affiliated with or endorsed by Rockstar Games or Take-Two Interactive. It does not modify game files or inject code; it only relies on standard Windows thread-suspension functions. The code is shared for educational purposes only.

## License

<!-- add a license, e.g. MIT -->
