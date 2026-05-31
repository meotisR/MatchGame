using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MatchGame
{
    using System.Windows.Threading;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        int tenthsOfSecondsElapsed;
        int matchesFound;
        public MainWindow()
        {
            InitializeComponent();

            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += Timer_Tick;// add event delegate

            SetUpGame();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            tenthsOfSecondsElapsed++;
            timeTextBlock.Text = "Time: " + (tenthsOfSecondsElapsed / 10F).ToString("0.0") + "s" + ", Pairs: " + matchesFound.ToString();
            // tenthsOfSecondsElapsed.ToString("0.0s")
            // don't use "+ matchesFound as string", works only because of cancatinaton
            // for non derived from string better not to use "as" keyword
            if (matchesFound == 8)
            {
                timer.Stop();
                timeTextBlock.Text = timeTextBlock.Text + " - Play again?";
            }
        }

        private void SetUpGame()
        {
            List<string> animalEmoji = new List<string>()
            {
                "🐇","🐇",
                "🐒","🐒",
                "🐍","🐍",
                "🦔","🦔",
                "🐁","🐁",
                "🐢","🐢",
                "🐘","🐘",
                "🦇","🦇"
            };
            Random random = new Random();
            foreach (var textBlock in mainGrid.Children.OfType<TextBlock>())
            {
                if ((textBlock.Tag as string) == "emojiBlock")// "==" to compare as both strings, using "as" here
                                                              // no Tag on element equals Null (safe because of comparing)
                // "textBlock.Tag is string comparedTag" can be more useful for other types, because of "as" just returns null if not same type
                // not textBlock.Tag.ToString() == "emojiBlock"
                // for obj's still unsafe -> "Equals(textBlock.Tag.ToString() , "emojiBlock")"
                //old implementation is -> if (textBlock.Name != "timeTextBlock")
                {
                    textBlock.Visibility = Visibility.Visible;// Was a little hidden in the book, but better to set it here, because of the "Play again?" text
                    int index = random.Next(animalEmoji.Count);
                    string nextEmoji = animalEmoji[index];// exception here
                    textBlock.Text = nextEmoji;
                    animalEmoji.RemoveAt(index);
                }

                timer.Start();
                tenthsOfSecondsElapsed = 0;
                matchesFound = 0;
            }
        }

        TextBlock guessedTextBlock = new TextBlock();
        bool isFirstTextBlockChoosed = false;
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock? currentTextBlock = sender as TextBlock;// useful like temp for later usage;
            // "as" if not same type returns Null, doesn't crash like cast "(TextBlock)"

            if (currentTextBlock == null)
                return;
            if (!isFirstTextBlockChoosed)
            {
                guessedTextBlock = currentTextBlock;// guessedTextBlock = (TextBlock)sender
                guessedTextBlock.Visibility = Visibility.Hidden;
                isFirstTextBlockChoosed = true;
            }
            else if (guessedTextBlock.Text == currentTextBlock.Text)// guessedTextBlock.Text == ((TextBlock)sender).Text;
                // also possible to use pattern matching "is" ("sender is TextBlock name"; then name can be used in other if part)
            {
                currentTextBlock.Visibility = Visibility.Hidden;
                isFirstTextBlockChoosed = false;
                matchesFound++;
            }

            else
            {
                guessedTextBlock.Visibility = Visibility.Visible;
                isFirstTextBlockChoosed = false;
            }
        }

        private void TimeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (matchesFound == 8)
            {
                SetUpGame();
            }
        }
    }
}