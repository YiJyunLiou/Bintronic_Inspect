using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bintronic_Inspect.Repositories;

namespace Bintronic_Inspect
{
    public partial class formCountdownSetting : Form
    {
        //IniManager _iniManager = new IniManager(ConfigurationManager.AppSettings["iniPath"] + "\\parameter.ini"); //用來讀取INI的程式，並從App.config抓取INI的儲存位置
        IniManager _iniManager = new IniManager(System.Windows.Forms.Application.StartupPath + "\\parameter.ini"); //用來讀取INI的程式，並以執行檔的位置作為INI的儲存位置

        int intCountdownSeconds = 0;

        public formCountdownSetting()
        {
            InitializeComponent(); //視窗初始化

            try //讀取INI
            {
                intCountdownSeconds = Convert.ToInt32(_iniManager.ReadIniFile("CountdownSetting", "CountdownSeconds", "5"));
            }
            catch //如果INI讀取失敗，則一樣預設5秒
            {
                intCountdownSeconds = 5;
            }

            if (intCountdownSeconds > 0) //如果讀取到的秒數大於0，則表示啟用倒數功能
            {
                rdbOpenCountdown.Checked = true; //設定勾選啟用倒數
                nbudCountdownSeconds.Text = intCountdownSeconds.ToString(); //寫入秒數到文字方塊中
                nbudCountdownSeconds.Enabled = true; //秒數文字方塊啟用編輯
            }
            else //如果讀取到的秒數小於等於0，則表示關閉倒數功能
            {
                rdbCloseCountdown.Checked = true; //設定勾選不啟用倒數
                nbudCountdownSeconds.Text = 0.ToString(); //設定倒數秒數為0
                nbudCountdownSeconds.Enabled = false; //秒數文字方塊不啟用編輯
            }

            EventAdd(); //事件新增
        }

        public void EventAdd() //事件新增
        {
            rdbOpenCountdown.CheckedChanged += rdbOpenCountdown_CheckedChanged; //開啟倒數勾選按鈕狀態發生改變
            rdbCloseCountdown.CheckedChanged += rdbCloseCountdown_CheckedChanged; //關閉倒數勾選按鈕狀態發生改變

            btnConfirm.Click += btnConfirm_Click; //確認按鈕
            btnCancel.Click += btnCancel_Click; //取消按鈕
        }

        public void rdbOpenCountdown_CheckedChanged(object sender, EventArgs e) //開啟倒數勾選按鈕狀態發生改變
        {
            if (rdbOpenCountdown.Checked) //如果開啟倒數
            {
                nbudCountdownSeconds.Enabled = true; //啟用輸入倒數秒數文字方塊
            }
            else //如果關閉倒數
            {
                nbudCountdownSeconds.Enabled = false; //不啟用輸入倒數秒數文字方塊
                nbudCountdownSeconds.Text = 0.ToString(); //倒數秒數設定成0秒
            }
        }

        public void rdbCloseCountdown_CheckedChanged(object sender, EventArgs e) //關閉倒數勾選按鈕狀態發生改變
        {

        }

        public void btnConfirm_Click(object sender, EventArgs e) //確認按鈕
        {
            try
            {
                _iniManager.WriteIniFile("CountdownSetting", "CountdownSeconds", nbudCountdownSeconds.Text); //把設定的秒數寫入檔案
            }
            catch (Exception ex)
            {
                MessageBox.Show("設定儲存失敗！" + '\n' + "錯誤訊息：" + ex.ToString());
            }
            
            this.Close(); //關閉視窗
        }

        public void btnCancel_Click(object sender, EventArgs e) //取消按鈕
        {
            this.Close(); //關閉視窗
        }
    }
}
