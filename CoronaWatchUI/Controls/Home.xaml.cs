using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CoronaWatchUI.Controls
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : UserControl
    {
        public Home()
        {
            InitializeComponent();
        }
        void OnLoad(object sender, RoutedEventArgs e)
        {
            doTypeWrite();
        }
        private void doTypeWrite()
        {
            List<string> toWrite = new List<string>()
            {
                "Lorem ipsum dolor sit amet",
                "consectetur adipiscing elit",
                "sed do eiusmod tempor incididunt ut labore et dolore magna aliqua",
                "Ut enim ad minim veniam",
                "quis nostrud exercitation ullamco laboris",
                "nisi ut aliquip ex ea commodo consequat",
                "Duis aute irure dolor in reprehenderit in voluptate",
                "velit esse cillum dolore eu fugiat nulla pariatur"
            };
            TextBlock tb = (TextBlock)this.FindName("typewriterTextBlock");
            Int64 timeToWrite = Convert.ToInt64(((double)String.Join("", toWrite).Length * 10000000.0)*(60.0/350.0));
            Typewriter(toWrite, tb, new TimeSpan(timeToWrite));
        }
        private void Typewriter(List<string> sAnim, TextBlock txt, TimeSpan span)
        {
            Storyboard story = new Storyboard();
            story.FillBehavior = FillBehavior.HoldEnd;
            story.RepeatBehavior = RepeatBehavior.Forever;

            DiscreteStringKeyFrame discreteStringKeyFrame;
            StringAnimationUsingKeyFrames stringAnimationUsingKeyFrames = new StringAnimationUsingKeyFrames();
            stringAnimationUsingKeyFrames.Duration = new Duration(span);
            for (int i = 0; i < 15; i++)
            {
                discreteStringKeyFrame = new DiscreteStringKeyFrame();
                discreteStringKeyFrame.KeyTime = KeyTime.Uniform;
                discreteStringKeyFrame.Value = string.Empty;
                stringAnimationUsingKeyFrames.KeyFrames.Add(discreteStringKeyFrame);
            }
            foreach (string s in sAnim)
            {
                string tmp = string.Empty;
                foreach (char c in s)
                {
                    discreteStringKeyFrame = new DiscreteStringKeyFrame();
                    discreteStringKeyFrame.KeyTime = KeyTime.Uniform;
                    tmp += c;
                    discreteStringKeyFrame.Value = tmp;
                    stringAnimationUsingKeyFrames.KeyFrames.Add(discreteStringKeyFrame);
                    stringAnimationUsingKeyFrames.KeyFrames.Add(discreteStringKeyFrame);
                }
                for (int i = 0; i < 10; i++)
                {
                    discreteStringKeyFrame = new DiscreteStringKeyFrame();
                    discreteStringKeyFrame.KeyTime = KeyTime.Uniform;
                    discreteStringKeyFrame.Value = tmp;
                    stringAnimationUsingKeyFrames.KeyFrames.Add(discreteStringKeyFrame);
                }
                for (int i = 0; i < s.Length; i++)
                {
                    discreteStringKeyFrame = new DiscreteStringKeyFrame();
                    discreteStringKeyFrame.KeyTime = KeyTime.Uniform;
                    tmp = tmp.Remove(tmp.Length - 1, 1);
                    discreteStringKeyFrame.Value = tmp;
                    stringAnimationUsingKeyFrames.KeyFrames.Add(discreteStringKeyFrame);
                }
                discreteStringKeyFrame = new DiscreteStringKeyFrame();
                discreteStringKeyFrame.KeyTime = KeyTime.Uniform;
                discreteStringKeyFrame.Value = string.Empty;
                stringAnimationUsingKeyFrames.KeyFrames.Add(discreteStringKeyFrame);
            }
            Storyboard.SetTargetName(stringAnimationUsingKeyFrames, txt.Name);
            Storyboard.SetTargetProperty(stringAnimationUsingKeyFrames, new PropertyPath(TextBlock.TextProperty));
            story.Children.Add(stringAnimationUsingKeyFrames);

            story.Begin(txt);
        }
    }
}
