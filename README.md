# Match Game
 
A WPF card-matching game built in C# as part of the *Head First C#* learning series (Andrew Stellman & Jennifer Greene).

 
## How It Works
 
All 16 emoji cards are displayed in a 4×4 grid. 
- Click a card to **hide** it.

- Click a **second card** to hide it too.
- **Match** → both stay hidden.
- **No match** → the first card flips back visible so you can memorize it.
Clear all 8 pairs as fast as you can. Click the timer text when the game ends to **play again**.

## Tech Stack
 
| | |
|---|---|
| Language | C# (.NET) |
| UI Framework | WPF (Windows Presentation Foundation) |
| IDE | Visual Studio 2022 |
| Platform | Windows |
 
## What's Inside the Code
 
**`SetUpGame()`** shuffles a `List<string>` of 16 emoji (8 pairs) using `Random`, then walks all `TextBlock` children of `mainGrid` filtered by `Tag == "emojiBlock"`. Each gets an emoji assigned and `Visibility.Visible` restored. Timer starts again here too.
 
**`TextBlock_MouseDown()`** is a two-state machine with `isFirstTextBlockChoosed`:
- First click → store reference in `guessedTextBlock`, hide it.
- Second click → compare `.Text`. Match: hide and increment `matchesFound`. No match: restore first card's visibility.

**`Timer_Tick()`** fires every 0.1 s via `DispatcherTimer`. Displays `time` and `matchesFound`. Stops and appends `" - Play again?"` when `matchesFound == 8`.
 
**`TimeTextBlock_MouseDown()`**
Calls `SetUpGame()` only if the game is finished (`matchesFound == 8`). Acts as the restart button.
 
## My Notes
 
- `Tag="emojiBlock"` on XAML elements is used instead of checking element names — cleaner filter when iterating `mainGrid.Children`.
- `sender as TextBlock` instead of a cast — returns `null` safely instead of throwing on type mismatch.
- `"as"` used for string tag comparison (`textBlock.Tag as string`) because `Tag` is `object`; avoids unsafe `.ToString()` on potentially null.
- Timer is started inside the `foreach` in `SetUpGame()` — works, but starts slightly early (on first child iteration, not after all cards are placed).
- `Visibility.Visible` is explicitly reset in `SetUpGame()` so "Play again" correctly restores hidden matched cards.

## Possible Extensions
 
- [ ] Difficulty levels (4×4, 6×6, 8×8)
- [ ] Reset button
- [ ] Card flip animation
- [ ] Best time persistence via JSON
- [ ] MVVM refactor — extract game state out of code-behind into a `ViewModel`

## Project Structure
 
```
MatchGame/
├── MainWindow.xaml        # 4×4 Grid of TextBlocks + timer row (5th row, ColumnSpan=4)
├── MainWindow.xaml.cs     # Game logic: shuffle, state machine, timer
├── App.xaml
├── App.xaml.cs
└── MatchGame.csproj
```
 
## Reference
 
- *Head First C#*, 4th Edition — Stellman & Greene (O'Reilly)
- [Official Head First C# GitHub](https://github.com/head-first-csharp/fourth-edition)
- [WPF Docs — Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/)
- [DispatcherTimer — Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/api/system.windows.threading.dispatchertimer)

---
# Old Notes
> Trying use buitin Git in VS2022  
> Learning some WPF, looks close enough to Blazor

## Notes
* Found *Readme* in **Solution Explorer** using **Switch between solutions** Button on top 
* Text Alignment in Text Paragraph menu not so important as Element Alignment in Layout
* Grid Span attribute can be used to cover several cells in Grid


## Progress
Write some text for myself here  
Make basis for this app, using TextBlocks, Emojis, Random, Lists and Loops  
Fixed Github commits mail  

Test