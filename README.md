# Cept! Auto Accept

Cept! is a lightweight Windows tray application that auto-accepts League of Legends matches after 8 seconds.


# Why? What's different about Cept!?

Have you ever faced the problem of clicking on Accept! and buttons grey out, but you're either running LoL on a low-end PC (like me :D), or you internet is bad (also like me), or it's just League of Legends client shenanigans, thus leading to match accept not registering, and your friends yell at you "WHY DIDN'T YOU ACCEPT WE HAVE BEEN IN QUEUE FOR 23 MINUTES"?

if yes then this app is supposed to prevent that by sending an accept command after 8 seconds (just enough before the timer runs out) of not accepting or declining a ready check.

It's super-lightweight, you can turn it on/off with only a few clicks, no extra screens, and works like other AutoAccepts too!


# How to use

0. Download the binary from [Releases](https://github.com/DKMiner/Cept/releases/latest) page

1. Double click the exe file to run the program. It has no strong GUI and goes into your system tray (The arrow in the taskbar)

2. Right click on the tray icon, and you'll see:

   * Auto Accept: Toggle on to start accepting the next ready check after 8 seconds. It will automatically revert to off after you have successfully entered the match (no dodge happened)
   * Keep on after joining: Toggle on to prevent Auto Accept from turning off after entering a match. Might use slightly more resources.
   * Quit: Close the app, duh.
  

## Developing Cept!

The application lives in the [Conduit](./conduit) directory. Open `Conduit.sln` in Visual Studio to restore dependencies and build the app.

It uses .NET 4.6.1 SDK.


## Credits

I have forked this from the Mimic project, because I had no idea how LoL handles the requests. It's a great project by itself, but I usually use my phone as a hotspot so I can't really move it all the time

I have also used AI (ChatGPT and its codex) to help me with Visual Studio, this repo, and the code (cause it's the first time I dive this deep into VS). Even though I have reviewed it myself, it might contain some bugs. If you encountered any, open an issue and I will check it out.


## License

Cept! is released under the [MIT](LICENSE) license.
