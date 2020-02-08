using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lottery
{
    public partial class Form1 : Form
    {
        public Boolean Access = false;
        private Gioble oData = new Gioble() { };
        public Form1()
        {
            InitializeComponent();
        }
        
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == 13)
            {
                if (CheckWinListSignificant(textBox1.Text))
                    LockBtn(true);
                else
                    LockBtn(false);
            }
            else if (((int)e.KeyChar < 48 | (int)e.KeyChar > 57) & (int)e.KeyChar != 8 & (int)e.KeyChar != 44 & (int)e.KeyChar != 127)
                e.Handled = true;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (oData.isLock)
                    LockBtn(false);
                if (CheckWinListSignificant(textBox1.Text))
                    LockBtn(true);
                else
                    LockBtn(false);
            }
            catch (Exception _sEx)
            {
                MessageBox.Show(_sEx.Message);
            }
        }
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == 13)
            {
                e.Handled = true;
                string sStr = textBox2.Text;
                string sVal = "";
                List<string> oList1 = sStr.Split(new string[] {"\r\n" }, StringSplitOptions.None).ToList();
                foreach (string sTMP in oList1)
                {
                    string sTMP2 = sTMP.IndexOf(";") != -1 ? sTMP.Substring(0, sTMP.IndexOf(";")) : sTMP;
                    if (CheckListSignificant(sTMP2))
                        sVal += sTMP2 + ";檢核成功，" + ReportResult(sTMP2) + "\r\n";
                    else if (sTMP.Trim() == "")
                        sVal += sTMP2;
                    else
                        sVal += sTMP2 + ";檢核失敗，不做判斷" + "\r\n";
                }
                textBox2.Text = sVal;
                textBox2.SelectionStart = textBox2.Text.Length;
                textBox2.SelectionLength = textBox2.Text.Length;
                textBox2.ScrollToCaret();
            }
            else if (((int)e.KeyChar < 48 | (int)e.KeyChar > 57) & (int)e.KeyChar != 8 & (int)e.KeyChar != 44 & (int)e.KeyChar != 127)
                e.Handled = true;
        }
        void LockBtn(Boolean _Val)
        {
            if (_Val)
            {
                label1.Text = textBox1.Text;
                textBox2.Text = "";
                button1.Text = "解鎖";
                textBox1.Enabled = false;
                textBox2.Enabled = true;
                oData.isLock = true;
            }
            else
            {
                textBox1.Text = "";
                textBox2.Text = "";
                button1.Text = "鎖定";
                textBox1.Enabled = true;
                textBox2.Enabled = false;
                oData = new Gioble() { };
            }
        }
        string ReportResult(string _sVal)
        {
            List<Int32> oList = _sVal.Split(new string[] { "," }, StringSplitOptions.None).ToList().Select(x => Convert.ToInt32(x)).ToList();
            List<Int32> oResult = oData.oArr.Where(x=> oList.Contains(x)).ToList();
            if (oData.isSpecial == true)
            {
                if (oResult.Count == 6)
                    return "重複六個數字，恭喜你抽中紅包。";
                else
                    return "銘謝惠顧，下次再加油";
            }
            else
            {
                if (oResult.Count == 3)
                    return "重複三個數字，恭喜你有中獎。";
                else if (oResult.Count == 4)
                    return "重複四個數字，恭喜你中獎。";
                else if (oResult.Count == 5)
                    return "重複五個數字，恭喜您中大獎。";
                else if (oResult.Count == 6)
                    return "重複六個數字，你該搬家了。";
                else if (oResult.Count == 7)
                    return "重複七個數字，該看眼科了，樂透不會有七碼數字。";
                else
                    return "銘謝惠顧，下次再加油";
            }
        }
        Boolean CheckListSignificant(string _sVal, Boolean _First = false)
        {
            List<Int32> oList = new List<Int32>() { };
            List<string> oTMP = new List<string>() { };
            if (_sVal.Length == 0)
            {
                label1.Text = "您沒有輸入數字";
                return false;
            }
            string sVal = _sVal.Replace(" ", "");
            if (sVal.Length == 0)
            {
                label1.Text = "您沒有輸入數字或您輸入的都是空白";
                return false;
            }
            oTMP = sVal.Split(new string[] { "," }, StringSplitOptions.None).ToList();
            if (oTMP.Exists(x => isNum(x) == false))
            {
                label1.Text = "您輸入的並非全部都是數字，請確認是否有空白或非數字";
                return false;
            }
            oList = oTMP.Select(x => Convert.ToInt32(x)).ToList();
            if (oList.Exists(x => x >= 50 || x <= 0))
            {
                label1.Text = "您輸入的數字『大於49』或是『小於1』";
                return false;
            }
            if (_First)
            {
                if (oList.Count < 7)
                {
                    label1.Text = "您輸入的數字不滿七組(樂透預設是七組數字)；春節大紅包超過七組數字";
                    return false;
                }
                if (oList.GroupBy(x => x).Select(x=>x.First()).ToList().Count != oList.Count)
                {
                    label1.Text = "您輸入的數字有重副";
                    return false;
                }
                return true;
            }
            else
            {
                if (oList.Count != 6)
                {
                    label1.Text = "您輸入的數字不等於六組(樂透預設是六組數字)";
                    return false;
                }
                if (oList.GroupBy(x => x).ToList().Count < 6)
                {
                    label1.Text = "您輸入的數字有重副";
                    return false;
                }
            }
            return true;
        }
        Boolean CheckWinListSignificant(string _sVal)
        {
            if (CheckListSignificant(_sVal, true))
            {
                var oList = _sVal.Split(new string[] { "," }, StringSplitOptions.None).ToList().Select(x => Convert.ToInt32(x)).ToList();
                if (oList.Count == 7)
                    oData = new Gioble() { isLock = true, oArr = oList };
                else if (MessageBox.Show("請問您輸入的是春節大紅包嗎？", "請確認", MessageBoxButtons.YesNo)== DialogResult.Yes)
                    oData = new Gioble() { isLock = true, isSpecial = true, oArr = oList };
                else
                    return false;
                return true;
            }
            else
                return false;
        }
        Boolean isNum(string _sVal)
        {
            Int32 x = 0;
            if (Int32.TryParse(_sVal, out x) == false)
                return false;
            else
                return true;
        }
        class Gioble
        {
            public Gioble()
            {
                isLock = false;
                isSpecial = false;
                oArr = new List<int>() { };
            }
            public Boolean isLock { get; set; }
            public Boolean isSpecial { get; set; }
            public List<Int32> oArr { get; set; }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                FrmNews oFrm = new FrmNews() { };
                oFrm.ShowDialog();
                if (oFrm.Access == false)
                    this.Close();
            }
            catch(Exception _sEx)
            {
                MessageBox.Show(_sEx.Message);
            }
        }
    }
}
