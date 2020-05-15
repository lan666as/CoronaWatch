using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

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
                "We provide up to date data and chart",
                "Welcome to CoronaWatch",
                "#StayHealhty #StayAtHome",
                "Jangan lupa pyshical distancing",
                "#FlattenTheCurve",
                "Tetap semangat kita pasti bisa melewati ini",
                "Opportunities to find deeper powers within ourselves come when life seems most challenging.",
                "In the midst of chaos, there is also opportunity.",
                "The wound is the place where the light enters you."

            };
            TextBlock tb = (TextBlock)this.FindName("typewriterTextBlock");
            Int64 timeToWrite = Convert.ToInt64(((double)String.Join("", toWrite).Length * 10000000.0) * (60.0 / 350.0));
            Typewriter(toWrite, tb, new TimeSpan(timeToWrite));
        }
        private void Typewriter(List<string> sAnim, TextBlock txt, TimeSpan span)
        {
            Storyboard story = new Storyboard
            {
                FillBehavior = FillBehavior.HoldEnd,
                RepeatBehavior = RepeatBehavior.Forever
            };

            DiscreteStringKeyFrame discreteStringKeyFrame;
            StringAnimationUsingKeyFrames stringAnimationUsingKeyFrames = new StringAnimationUsingKeyFrames
            {
                Duration = new Duration(span)
            };
            for (int i = 0; i < 15; i++)
            {
                discreteStringKeyFrame = new DiscreteStringKeyFrame
                {
                    KeyTime = KeyTime.Uniform,
                    Value = string.Empty
                };
                stringAnimationUsingKeyFrames.KeyFrames.Add(discreteStringKeyFrame);
            }
            foreach (string s in sAnim)
            {
                string tmp = string.Empty;
                foreach (char c in s)
                {
                    discreteStringKeyFrame = new DiscreteStringKeyFrame
                    {
                        KeyTime = KeyTime.Uniform
                    };
                    tmp += c;
                    discreteStringKeyFrame.Value = tmp;
                    stringAnimationUsingKeyFrames.KeyFrames.Add(discreteStringKeyFrame);
                    stringAnimationUsingKeyFrames.KeyFrames.Add(discreteStringKeyFrame);
                }
                for (int i = 0; i < 10; i++)
                {
                    discreteStringKeyFrame = new DiscreteStringKeyFrame
                    {
                        KeyTime = KeyTime.Uniform,
                        Value = tmp
                    };
                    stringAnimationUsingKeyFrames.KeyFrames.Add(discreteStringKeyFrame);
                }
                for (int i = 0; i < s.Length; i++)
                {
                    discreteStringKeyFrame = new DiscreteStringKeyFrame
                    {
                        KeyTime = KeyTime.Uniform
                    };
                    tmp = tmp.Remove(tmp.Length - 1, 1);
                    discreteStringKeyFrame.Value = tmp;
                    stringAnimationUsingKeyFrames.KeyFrames.Add(discreteStringKeyFrame);
                }
                discreteStringKeyFrame = new DiscreteStringKeyFrame
                {
                    KeyTime = KeyTime.Uniform,
                    Value = string.Empty
                };
                stringAnimationUsingKeyFrames.KeyFrames.Add(discreteStringKeyFrame);
            }
            Storyboard.SetTargetName(stringAnimationUsingKeyFrames, txt.Name);
            Storyboard.SetTargetProperty(stringAnimationUsingKeyFrames, new PropertyPath(TextBlock.TextProperty));
            story.Children.Add(stringAnimationUsingKeyFrames);

            story.Begin(txt);
        }
    }
}
