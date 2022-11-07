using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using MvvmBaseballGame.DataModel;

namespace MvvmBaseballGame.ViewModel
{
    public class GameViewModel : Notifier
    {

        private int strikeCount;
        private int ballCount;
        private int outCount;
        int tryCount;
        string inputBox;

        public int StrikeCount 
        { 
            get { return strikeCount; }
            set
            {
                strikeCount = value;
                OnpropertyChanged("StrikeCount");
            }
        }
        public int BallCount
        {
            get { return ballCount; }
            set
            {
                ballCount = value;
                OnpropertyChanged("BallCount");
            }
        }
        public int OutCount
        {
            get { return outCount; }
            set
            {
                outCount = value;
                OnpropertyChanged("outCount");
            }
        }
        public int TryCount 
        { 
            get { return tryCount; }
            set
            {
                tryCount = value;
                OnpropertyChanged("TryCount");
            }
        }
        public string InputBox
        {
            get { return inputBox; }
            set
            {
                Regex regex = new Regex("[^0-9]+");
                if (!regex.IsMatch(value) && value.Length <= 3)
                {
                    inputBox = value;
                    OnpropertyChanged("InputBox");
                }
            }
        }
        string solNum;

        public Command SubBtn { get; set; }
        public Command SubmitCommand { get; set; }

        public GameViewModel()
        {
            Reset();

            SubBtn = new Command(Submission, CanSubmission);
            SubmitCommand = new Command(Submission,CanSubmission);
        }

        public string getNum()
        {
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            List<int> numList = new List<int>();
            for (int i = 0; i < 10; i++)
            {
                numList.Add(i);
            }
            string str = "";
            for (int i = 0; i < 3; i++)
            {
                int index = rand.Next(0, 10 - i);
                str += numList[index];
                numList.RemoveAt(index);
            }
            return str;
        }

        public void Submission(object param)
        {
            TryCount++;

            int s = 0;
            int b = 0;
            int o = 0;
            string ans = param.ToString();
            for(int i = 0; i < 3; i++)
            {
                if (solNum[i].Equals(ans[i])) s++;
                else if (solNum.Contains(ans[i])) b++;
                else o++;
            }
            StrikeCount = s;
            BallCount = b;
            OutCount = o;

            if (strikeCount == 3)
            {
                MessageBox.Show(tryCount + "회");
                Reset();
            }
        }

        public bool CanSubmission(object param)
        {
            if (param == null) return false;
            string ans = param.ToString();
            if (ans.Length != 3) return false;
            if (!Int32.TryParse(ans, out int i)) return false;
            if (ans[0].Equals(ans[1]) || ans[1].Equals(ans[2]) || ans[2].Equals(ans[0])) return false;
            return true;
        }

        public void Reset()
        {
            TryCount = 0;
            StrikeCount = 0;
            BallCount = 0;
            OutCount = 0;
            solNum = getNum();
        }


    }
}
