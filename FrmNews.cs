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
    public partial class FrmNews : Form
    {
        public Boolean Access = false;
        public FrmNews()
        {
            InitializeComponent();
            MaximizeBox = false;
            MinimizeBox = false;
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            Access = true;
            this.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void FrmNews_Load(object sender, EventArgs e)
        {
            textBox1.Text += @"1. 本系統為鹹魚工程師開發、鹹魚版權所有且鹹魚本人分享，請勿用於營利事業上。" + "\r\n";
            textBox1.Text += @"2. 本系統應用於『便利對獎』，不提供兌換或中獎之保證。" + "\r\n";
            textBox1.Text += @"3. 本系統號碼皆為手動輸入，若因輸入錯誤衍生的問題本系統一概不負任何責任。" + "\r\n";
            textBox1.Text += @"4. 本系統原始碼皆公開於Github：『https://github.com/FemhMis/Lottery』，有任何程式上的問題歡迎指正。" + "\r\n";
            textBox1.Text += @"5. 您需同意以上的描述才可開始使用本系統。" + "\r\n";
        }
    }
}
