using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _30_Lambda
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Action _aStepCheck = null;

        private void btnChange_Click(object sender, EventArgs e)
        {
            btnChange.BackColor = Color.Aqua;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _aStepCheck = () => lblStepCheck.Text = string.Format(" - 다음 Step은 {0} {1}",
                iNowStep, ((enumLamdaCase)iNowStep).ToString());
            _aStepCheck();
            ButtonColorChange();
        }

        private void ButtonColorChange()
        {
            // 이벤트 함수에서 색상 변경
            //btnChange.Click += btnChange_Click;

            // 무명 메서드
            btnColorChange_2.Click += delegate (object sender, EventArgs e)
            {
                btnColorChange_2.BackColor = Color.Brown;
            };

            // 람다식
            btnColorChange_3.Click += (sender, e) =>
            {
                btnColorChange_3.BackColor = Color.Coral;
            };
        }

        int iNowStep = 0;
        delegate int delIntFunc(int a, int b);
        delegate string delStringFunc();

        private void btnNext_Click(object sender, EventArgs e)
        {
            Lambda(iNowStep);
            iNowStep++;
            _aStepCheck();
        }

        private void Lambda(int iCase)
        {
            switch(iCase)
            {
                case (int)enumLamdaCase.식형식_람다식:
                    delIntFunc dint = (a, b) => a + b;
                    int iRet = dint(4, 5);
                    lboxResult.Items.Add(iRet.ToString());
                    delStringFunc dString = () => string.Format("Lambda Sample 식형식");
                    string strRet = dString();
                    lboxResult.Items.Add(strRet);
                    break;
                case (int)enumLamdaCase.문형식_람다식: // 명시적으로 반환을 해주어야 한다.
                    delStringFunc dstrSeqment = () =>
                    {
                        return string.Format("Lambda Sample 문형식");
                    };
                    strRet = dstrSeqment();
                    lboxResult.Items.Add(strRet);
                    break;
                case (int)enumLamdaCase.제네릭_형태의_무명메서드_Func:
                    // func : 반환값이 있는 형태
                    // 마지막 int가 반환값임..
                    Func<int, int, int> fInt = (a, b) => a + b;
                    int fIntRet = fInt(4, 5);
                    lboxResult.Items.Add(fIntRet.ToString());
                    break;
                case (int)enumLamdaCase.제네릭_형태의_무명메서드_Action:
                    // 리턴이 필요없음...
                    Action<string, string> aString = (a, b) =>
                    {
                        string strText = String.Format("인자값 {0}와 {1}을 받았습니다.", a, b);
                        lboxResult.Items.Add(strText.ToString());
                    };
                    aString("시간", "금");
                    break;
                case (int)enumLamdaCase.제네릭_형태의인자__반환_예제:
                    int[] iGroup = { 1, 3, 5, 7, 9 };
                    int iNumSum = iGroup.Sum(x => x); // 위의값을 빼고 계산하고 넣고 하는 과정...
                    lboxResult.Items.Add(iNumSum.ToString());

                    string[] strGroup = { "XXXX", "TTT", "YYY" };
                    int ilengthSum = strGroup.Sum(x => x.Length);
                    lboxResult.Items.Add(ilengthSum.ToString());
                    break;
                default:
                    break;
            }
        }

        enum enumLamdaCase
        {
            식형식_람다식 = 0,
            문형식_람다식 = 1,
            제네릭_형태의_무명메서드_Func = 2,
            제네릭_형태의_무명메서드_Action = 3,
            제네릭_형태의인자__반환_예제 = 4,
        }
    }
}
