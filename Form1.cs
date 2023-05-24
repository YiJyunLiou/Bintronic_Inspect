using Bintronic_rs485BOX_inspect;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Management; // need to add System.Management to your project references.
using System.IO;
using System.Reflection.Emit;

using System.Text.Json;
using System.Net.Http;

using System.Configuration; //讀取App.config使用
using System.Security.Policy;
using System.Text.Json.Nodes;
using Bintronic_Inspect.Repositories;

namespace Bintronic_Inspect
{
    public partial class formMain : Form
    {
        //IniManager _iniManager = new IniManager(ConfigurationManager.AppSettings["iniPath"] + "\\parameter.ini"); //用來讀取INI的程式，並從App.config抓取INI的儲存位置
        IniManager _iniManager = new IniManager(System.Windows.Forms.Application.StartupPath + "\\parameter.ini"); //用來讀取INI的程式，並以執行檔的位置作為INI的儲存位置
        const string CONFIG_FILE_NAME = "com_port_device.cfg";

        bool _bolIsDebug = true; //全OFF跳過PASS
        bool _IsAllSuccess = true; //是否全部成功，只要有一個失敗則改成false，且初始化改回true

        //bool _bolIsReciproca = true; //是否啟用到數 ()

        int _intReciprocaSet = 0; //使用者設定的倒數秒數
        int _intReciproca = 0; //倒數秒數
        int _intLedState = 0;

        int _intSteup = 0;
        int _intTimerCount = 0;

        string _strSw1Value, _strSw2Value;

        List<string> lstModelType = new List<string>();

        //DateTime _dtStartDetection;

        DetectionSend _detectionSend;

        public formMain() //視窗載入
        {
            string err_info;
            string info = "";

            InitializeComponent();

            EventAdd(); //事件新增

            lblNowDateTime.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");


            BT_rs485_ctrl.init();//程式一開始先初始化"BT_rs485_ctrl"

            if (!read_config(out err_info)) //載入COM PORT 設定檔
                info = err_info + Environment.NewLine;

            lstModelType.Add("C5A"); //添加選項到List
            lstModelType.Add("C5B"); //添加選項到List

            //區塊2
            //cboModel.Items.Add("C5A"); //添加選項
            //cboModel.Items.Add("C5B"); //添加選項
            foreach (string strItem in lstModelType) cboModel.Items.Add(strItem); //添加選項
            cboModel.Text = cboModel.Items[0].ToString(); //預設顯示第一個選項的內容

            step0(); //恢復原本初始化狀態
        }

        //-------------------------------------------------- ↓控制項事件的部分↓ --------------------------------------------------

        public void EventAdd() //事件新增
        {
            //區塊1
            mnsSetting.Click += mnsSetting_Click; //設定倒數秒數按鈕

            //區塊2
            tmrNowDateTime.Tick += tmrNowDateTime_Tick; //每秒更新當前時間

            //區塊3
            btnDetection.Click += btnDetection_Click; //檢測按鈕
            btnSkipToNextTest.Click += btnSkipToNextTest_Click; //跳至下項檢測按鈕

            //區塊4
            btnLedOK.Click += btnLedOK_Click; //LED檢測OK按鈕
            btnLedNG.Click += btnLedNG_Click; //LED檢測OK按鈕
            tmrLedState.Tick += tmrLedState_Tick; //LED閃爍 (0.5s)
            tmrLedStateFailDelay.Tick += tmrLedStateFailDelay_Tick; //LED亮滅失敗延遲 (1s) (這樣才能在亮滅失敗時不會讓程式當掉，又可以重新閃爍LED)

            //區塊5
            tmrReciproca.Tick += tmrReciproca_Tick; //倒數計時

            //區塊6
            tmrDetection.Tick += tmrDetection_Tick; //SW1的倒數計時
            btnLine1.Click += btnLine_Click;
            btnLine2.Click += btnLine_Click;
            btnLine3.Click += btnLine_Click;
            btnLine4.Click += btnLine_Click;
            btnLine5.Click += btnLine_Click;
            btnLine6.Click += btnLine_Click;
            btnLine7.Click += btnLine_Click;
            btnLine8.Click += btnLine_Click;
        }

        public void mnsSetting_Click(object sender, EventArgs e) //設定倒數秒數按鈕
        {
            formCountdownSetting frmCountdownSetting = new formCountdownSetting(); //產生新的 formCountdownSetting 物件
            frmCountdownSetting.Show(this); //以主畫面為基礎，讓設定畫面在主畫面上層
        }

        public void tmrNowDateTime_Tick(object sender, EventArgs e) //每秒更新當前時間
        {
            lblNowDateTime.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"); //用 lblNowDateTime 來顯示當前時間
        }

        public void btnDetection_Click(object sender, EventArgs e) //檢測按鈕
        {
            _intReciprocaSet = Convert.ToInt32(_iniManager.ReadIniFile("CountdownSetting", "CountdownSeconds", "5")); //抓取檔案中的倒數秒數
            step1(); //開始檢測
        }

        public void btnSkipToNextTest_Click(object sender, EventArgs e) //跳至下項檢測按鈕
        {
            if (_intReciproca > 0) //如果_intReciproca 大於0則表示目前正在逕行 SW的倒數，則直接跳到下一個檢測項目不要到數
            {
                _intReciproca = 0;

                tmrReciproca.Enabled = false; //倒數計時器不啟用
                lblReciprocal.Visible = false; //倒數Label隱藏

                runNextStep(); //執行下一步驟
            }
            else //否則表示目前正在某個項目的檢測中
            {
                tmrDetection.Enabled = false; //關閉計時器

                switch (_intSteup)
                {
                    case 2:
                        //_strSw1Value = readSw1(); //抓取六個腳位的狀態
                        viewSw1(_strSw1Value); //把SW1的6個腳位顯示在區域6中

                        if (_strSw1Value != "111111") //如果依舊不是全部等於0
                        {
                            changeLabelStyle(lblTestSW1Result, "FAILED"); //沒有測試通過
                            _IsAllSuccess = false; //有檢測失敗發生

                            //寫入資料
                            _detectionSend.data.sw1ONData = _strSw1Value;
                            _detectionSend.data.sw1ONResult = "FAILED";
                        }
                        else
                        {
                            changeLabelStyle(lblTestSW1Result, "PASS"); //測試通過

                            //寫入資料
                            _detectionSend.data.sw1ONData = _strSw1Value;
                            _detectionSend.data.sw1ONResult = "PASS";
                        }
                        setReciprocal(); //下一個檢測之前的倒數設定
                        break;

                    case 3:
                        //_strSw1Value = readSw1(); //抓取六個腳位的狀態
                        viewSw1(_strSw1Value); //把SW1的6個腳位顯示在區域6中

                        if (_strSw1Value != "000000") //如果依舊不是全部等於0
                        {
                            changeLabelStyle(lblTestSW1Result, "FAILED"); //沒有測試通過
                            _IsAllSuccess = false; //有檢測失敗發生

                            //寫入資料
                            _detectionSend.data.sw1OFFData = _strSw1Value;
                            _detectionSend.data.sw1OFFResult = "FAILED";
                        }
                        else
                        {
                            changeLabelStyle(lblTestSW1Result, "PASS"); //測試通過

                            //寫入資料
                            _detectionSend.data.sw1OFFData = _strSw1Value;
                            _detectionSend.data.sw1OFFResult = "PASS";
                        }
                        setReciprocal(); //下一個檢測之前的倒數設定
                        break;

                    case 4:
                        //_strSw2Value = readSw2(); //抓取兩個腳位的狀態
                        viewSw2(_strSw2Value); //把SW1的6個腳位顯示在區域6中

                        if (_strSw2Value != "11") //如果測試不通過，則透過Timer重新跑10次，每次間隔1秒
                        {
                            changeLabelStyle(lblTestSW1Result, "FAILED");
                            _IsAllSuccess = false; //有檢測失敗發生

                            //寫入資料
                            _detectionSend.data.sw2ONData = _strSw2Value;
                            _detectionSend.data.sw2ONResult = "FAILED";
                        }
                        else //測試通過
                        {
                            changeLabelStyle(lblTestSW1Result, "PASS");

                            //寫入資料
                            _detectionSend.data.sw2ONData = _strSw2Value;
                            _detectionSend.data.sw2ONResult = "PASS";

                        }
                        setReciprocal(); //下一個檢測之前的倒數設定
                        break;

                    case 5:
                        //_strSw2Value = readSw2(); //抓取兩個腳位的狀態
                        viewSw2(_strSw2Value); //把SW1的6個腳位顯示在區域6中

                        if (_strSw2Value != "00") //如果測試不通過，則透過Timer重新跑10次，每次間隔1秒
                        {
                            changeLabelStyle(lblTestSW1Result, "FAILED");
                            _IsAllSuccess = false; //有檢測失敗發生

                            //寫入資料
                            _detectionSend.data.sw2OFFData = _strSw2Value;
                            _detectionSend.data.sw2OFFResult = "FAILED";
                        }
                        else //測試通過
                        {
                            changeLabelStyle(lblTestSW1Result, "PASS");

                            //寫入資料
                            _detectionSend.data.sw2OFFData = _strSw2Value;
                            _detectionSend.data.sw2OFFResult = "PASS";

                        }
                        runNextStep(); //執行下一步驟 (檢查是否有FAILED)
                        break;

                    case 6: //回復出場預設值

                        viewSw1(_strSw1Value); //把SW1的6個腳位顯示在區域6中
                        viewSw2(_strSw2Value); //把SW2的2個腳位顯示在區域6中

                        if (_strSw1Value != "000001") //如果測試不通過，則透過Timer重新跑10次，每次間隔1秒
                        {
                            changeLabelStyle(lblTestSW1Result, "FAILED");
                            //changeLabelStyle(lblTestSW2Result, "FAILED");
                            _IsAllSuccess = false; //有檢測失敗發生

                            //寫入資料
                            _detectionSend.data.swInitialResult = "FAILED";
                        }
                        else //SW1測試通過
                        {
                            //changeLabelStyle(lblTestSW1Result, "PASS");

                            if (_strSw2Value != "01") //如果測試不通過，則透過Timer重新跑10次，每次間隔1秒
                            {
                                //changeLabelStyle(lblTestSW1Result, "FAILED");
                                changeLabelStyle(lblTestSW2Result, "FAILED");
                                _IsAllSuccess = false; //有檢測失敗發生

                                //寫入資料
                                _detectionSend.data.swInitialResult = "FAILED";
                            }
                            else //SW2測試通過
                            {
                                changeLabelStyle(lblTestSW1Result, "PASS");
                                changeLabelStyle(lblTestSW2Result, "PASS");

                                //寫入資料
                                _detectionSend.data.swInitialResult = "PASS";
                            }
                        }
                        runNextStep(); //執行下一步驟 (檢查是否有FAILED)
                        break;
                }
            }
        }

        public void btnLedOK_Click(object sender, EventArgs e) //LED檢測OK按鈕
        {
            //寫入資料
            _detectionSend.data.ledResult = "PASS";
            tmrLedState.Enabled = false; //LED閃爍計時器不啟用

            step12_1(); //檢測LED後，的最後整理
        }

        public void btnLedNG_Click(object sender, EventArgs e) //LED檢測NG按鈕
        {
            //寫入資料
            _detectionSend.data.ledResult = "FAILED";
            _IsAllSuccess = false; //有檢測失敗發生

            tmrLedState.Enabled = false; //LED閃爍計時器不啟用

            step12_1(); //檢測LED後，的最後整理
        }

        public void tmrLedState_Tick(object sender, EventArgs e) //LED亮滅測試
        {
            _intLedState = (_intLedState - 1) * -1;

            if (_intLedState > 0) //開啟LED
            {
                //ledOn(); //LED點亮
                if (!ledOn()) //如果點亮失敗
                {
                    tmrLedState.Enabled = false; //關閉LED閃爍計時器
                    tmrLedStateFailDelay.Enabled = true; //開啟量滅失敗延遲
                }
            }
            else
            {
                //ledOff(); //LED熄滅
                if (!ledOff()) //如果熄滅失敗
                {
                    tmrLedState.Enabled = false; //關閉LED閃爍計時器
                    tmrLedStateFailDelay.Enabled = true; //開啟量滅失敗延遲
                }
            }
        }

        public void tmrLedStateFailDelay_Tick(object sender, EventArgs e) //LED亮滅失敗延遲
        {
            tmrLedStateFailDelay.Enabled = false; //關閉量滅失敗延遲計時器
            tmrLedState.Enabled = true; //重新開啟LED亮滅
        }

        

        public void tmrReciproca_Tick(object sender, EventArgs e) //開始檢測的倒數秒數
        {
            if (_intReciproca <= 0) //判斷倒數秒數
            {
                tmrReciproca.Enabled = false; //倒數計時器不啟用
                lblReciprocal.Visible = false; //倒數Label隱藏

                runNextStep(); //執行下一步驟
            }

            //倒數
            _intReciproca = _intReciproca - 1;
            lblReciprocal.Text = _intReciproca.ToString();
        }

        public void tmrDetection_Tick(object sender, EventArgs e) //開始檢測腳位失敗之後的倒數秒數次數
        {
            //倒數
            switch (_intSteup)
            {
                case 2: //SW1的全ON 10次迴圈測試
                    if (_intTimerCount > 0)
                    {
                        _intTimerCount = _intTimerCount - 1; //計數-1

                        _strSw1Value = readSw1(); //抓取六個腳位的狀態
                        viewSw1(_strSw1Value); //把SW1的6個腳位顯示在區域6中

                        if (_strSw1Value != "111111") //如果依舊不是全部等於1
                        {
                            return;
                        }

                        //測試通過
                        tmrDetection.Enabled = false; //關閉計時器
                        changeLabelStyle(lblTestSW1Result, "PASS");

                        //寫入資料
                        _detectionSend.data.sw1ONData = _strSw1Value;
                        _detectionSend.data.sw1ONResult = "PASS";

                        setReciprocal(); //下一個檢測之前的倒數設定
                    }
                    else //計次歸零，仍然沒有測試通過
                    {
                        tmrDetection.Enabled = false; //關閉計時器
                        changeLabelStyle(lblTestSW1Result, "FAILED");
                        _IsAllSuccess = false; //有檢測失敗發生

                        //寫入資料
                        _detectionSend.data.sw1ONData = _strSw1Value;
                        _detectionSend.data.sw1ONResult = "FAILED";

                        setReciprocal(); //下一個檢測之前的倒數設定
                    }
                    break;
                case 3: //SW1的全OFF 10次迴圈測試
                    if (_intTimerCount > 0)
                    {
                        _intTimerCount = _intTimerCount - 1; //計數-1

                        _strSw1Value = readSw1(); //抓取六個腳位的狀態
                        viewSw1(_strSw1Value); //把SW1的6個腳位顯示在區域6中

                        if (_strSw1Value != "000000") //如果依舊不是全部等於0
                        {
                            return;
                        }

                        //測試通過
                        tmrDetection.Enabled = false; //關閉計時器
                        changeLabelStyle(lblTestSW1Result, "PASS");

                        //寫入資料
                        _detectionSend.data.sw1OFFData = _strSw1Value;
                        _detectionSend.data.sw1OFFResult = "PASS";

                        setReciprocal(); //下一個檢測之前的倒數設定
                    }
                    else //計次歸零，仍然沒有測試通過
                    {
                        tmrDetection.Enabled = false; //關閉計時器
                        changeLabelStyle(lblTestSW1Result, "FAILED");
                        _IsAllSuccess = false; //有檢測失敗發生

                        //寫入資料
                        _detectionSend.data.sw1OFFData = _strSw1Value;
                        _detectionSend.data.sw1OFFResult = "FAILED";

                        setReciprocal(); //下一個檢測之前的倒數設定
                    }
                    break;
                case 4: //SW2的全ON 10次迴圈測試
                    if (_intTimerCount > 0)
                    {
                        _intTimerCount = _intTimerCount - 1; //計數-1

                        _strSw2Value = readSw2(); //抓取兩個腳位的狀態
                        viewSw2(_strSw2Value); //把SW2的2個腳位顯示在區域2中

                        if (_strSw2Value != "11") //如果依舊不是全部等於1
                        {
                            return;
                        }

                        //測試通過
                        tmrDetection.Enabled = false; //關閉計時器
                        changeLabelStyle(lblTestSW2Result, "PASS");

                        //寫入資料
                        _detectionSend.data.sw2ONData = _strSw2Value;
                        _detectionSend.data.sw2ONResult = "PASS";

                        setReciprocal(); //下一個檢測之前的倒數設定
                    }
                    else //計次歸零，仍然沒有測試通過
                    {
                        tmrDetection.Enabled = false; //關閉計時器
                        changeLabelStyle(lblTestSW2Result, "FAILED");
                        _IsAllSuccess = false; //有檢測失敗發生

                        //寫入資料
                        _detectionSend.data.sw2ONData = _strSw2Value;
                        _detectionSend.data.sw2ONResult = "FAILED";

                        setReciprocal(); //下一個檢測之前的倒數設定
                    }
                    break;
                case 5: //SW2的全OFF 10次迴圈測試
                    if (_intTimerCount > 0)
                    {
                        _intTimerCount = _intTimerCount - 1; //計數-1

                        _strSw2Value = readSw2(); //抓取兩個腳位的狀態
                        viewSw2(_strSw2Value); //把SW2的2個腳位顯示在區域2中

                        if (_strSw2Value != "00") //如果依舊不是全部等於0
                        {
                            return;
                        }

                        //測試通過
                        tmrDetection.Enabled = false; //關閉計時器
                        changeLabelStyle(lblTestSW2Result, "PASS");

                        //寫入資料
                        _detectionSend.data.sw2OFFData = _strSw2Value;
                        _detectionSend.data.sw2OFFResult = "PASS";

                        setReciprocal(); //下一個檢測之前的倒數設定
                    }
                    else //計次歸零，仍然沒有測試通過
                    {
                        tmrDetection.Enabled = false; //關閉計時器
                        changeLabelStyle(lblTestSW2Result, "FAILED");
                        _IsAllSuccess = false; //有檢測失敗發生

                        //寫入資料
                        _detectionSend.data.sw2OFFData = _strSw2Value;
                        _detectionSend.data.sw2OFFResult = "FAILED";

                        setReciprocal(); //下一個檢測之前的倒數設定
                    }
                    break;
                case 6: //SW1與SW2回覆原廠設定
                    if (_intTimerCount > 0)
                    {
                        _intTimerCount = _intTimerCount - 1; //計數-1

                        _strSw1Value = readSw1(); //抓取六個腳位的狀態
                        viewSw1(_strSw1Value); //把SW1的6個腳位顯示在區域6中

                        _strSw2Value = readSw2(); //抓取兩個腳位的狀態
                        viewSw2(_strSw2Value); //把SW2的2個腳位顯示在區域6中

                        if (_strSw1Value != "000001") //如果依舊不是全部等於 原廠設定
                        {
                            return;
                        }

                        if (_strSw2Value != "01") //如果依舊不是全部等於 原廠設定
                        {
                            return;
                        }

                        //測試通過
                        tmrDetection.Enabled = false; //關閉計時器
                        changeLabelStyle(lblTestSW1Result, "PASS");
                        changeLabelStyle(lblTestSW2Result, "PASS");

                        //寫入資料
                        _detectionSend.data.swInitialResult = "PASS";

                        runNextStep(); //下一個檢測之前的倒數設定
                    }
                    else //計次歸零，仍然沒有測試通過
                    {
                        tmrDetection.Enabled = false; //關閉計時器
                        changeLabelStyle(lblTestSW1Result, "FAILED");
                        changeLabelStyle(lblTestSW2Result, "FAILED");
                        _IsAllSuccess = false; //有檢測失敗發生

                        //寫入資料
                        _detectionSend.data.swInitialResult = "FAILED";

                        runNextStep(); //下一個檢測之前的倒數設定
                    }
                    break;
                case 7:

                    break;
                default:

                    break;
            }
        }

        public void btnLine_Click(object sender, EventArgs e) //點擊線
        {
            LineColor lineColor = new LineColor();
            Button btn = (Button)sender;
            btn.BackColor = lineColor.getColor(btn.BackColor); //改色
        }

        //-------------------------------------------------- ↑控制項事件的部分↑ --------------------------------------------------



        //-------------------------------------------------- ↓主要跑各個流程的部分↓ --------------------------------------------------

        public void step0() //恢復原本初始化狀態
        {
            _intSteup = 0; //紀錄當前步驟

            _detectionSend = new DetectionSend(); //把要儲存檢測資料的物件new
            _IsAllSuccess = true; //是否全部成功，只要有一個失敗則改成false，且初始化改回true

            //區塊1
            mnsSetting.Enabled = true;  //工具列設定改為可操作

            //區塊2
            grbArea2.Enabled = true;  //整個區塊2改為可操作
            tbxSerialNumber.Focus(); //讓生產序號取得焦點，這樣對方使用條碼掃描或許會比較方便

            //區塊3
            btnDetection.Enabled = true;  //檢測按鈕改為可操作
            btnSkipToNextTest.Enabled = false; //跳至下項檢測按鈕改為不可操作

            //區塊4
            grbArea4.Visible = false; //LED檢測區塊隱藏
            tmrLedState.Enabled = false; //LED閃爍計時器不啟用
            ledOff(); //LED熄滅

            //區塊5
            lblDetectionProcessDescription.Text = "請將待測板放上治具，並治具USB接上電腦，開啟電源，並輸入生產序號、檢測人員及型號";
            lblReciprocal.Visible = false; //倒數秒數的

            //區塊6
            viewSw1Sw2Default(); //把SW1與SW2的顯示改為 -- 預設
            changeLabelStyle(lblTestSW1Result, "待測");
            changeLabelStyle(lblTestSW2Result, "待測");
            changeLabelStyle(lblTestModuleFirmwareVersionResult, "待測");
            changeLabelStyle(lblTest485CommunicationP4Result, "待測");
            changeLabelStyle(lblTest485CommunicationP5Result, "待測");
            changeLabelStyle(lblTestControlAP9Result, "待測");
            changeLabelStyle(lblTestControlBP10Result, "待測");
            changeLabelStyle(lblTestTxRxP3Result, "待測");
            changeLabelStyle(lblTestLedResult, "待測");
        }

        public void step1() //開始檢測
        {
            _intSteup = 1; //紀錄當前步驟

            //判斷資料是否有輸入
            if (tbxSerialNumber.Text == string.Empty) //生產序號為空白
            {
                MessageBox.Show("請輸入生產序號。");
                lblDetectionProcessDescription.Text = "請輸入生產序號、檢測人員及型號";
                return;
            }

            if (tbxInspector.Text == string.Empty) //生產人員為空白
            {
                MessageBox.Show("請輸入檢測人員。");
                lblDetectionProcessDescription.Text = "請輸入生產序號、檢測人員及型號";
                return;
            }

            if (cboModel.Text == string.Empty) //型號為空白
            {
                MessageBox.Show("請選擇型號。");
                lblDetectionProcessDescription.Text = "請輸入生產序號、檢測人員及型號";
                return;
            }

            bool bolIsModelType = false; //判斷型號選項是否跟選單的一樣
            foreach (string strItem in lstModelType)
            {
                if (cboModel.Text == strItem) bolIsModelType = true; //判斷型號選項是否跟選單的一樣
            }
            if (!bolIsModelType) //如果選單選項跟預設的不一樣，則表示有問題
            {
                MessageBox.Show("目前選擇的型號有誤，請再次確認型號。");
                lblDetectionProcessDescription.Text = "目前選擇的型號有誤，請再次確認型號";
                return;
            }

            if (!checkPorts(scan_port(false))) //檢查port，如果有錯誤，沒有三個port都抓到
            {
                step0(); //恢復原本初始化狀態
                lblDetectionProcessDescription.Text = "待測板連線錯誤，請將待測板放上治具，並治具USB接上電腦，開啟電源";
                MessageBox.Show("COM Port 有錯誤。");
                return;
            }

            if (!(readMeter())) //如果電壓電流溫度讀取失愛
            {
                step0(); //恢復原本初始化狀態
                lblDetectionProcessDescription.Text = "待測板連線錯誤，請將待測板放上治具，並治具USB接上電腦，開啟電源";
                MessageBox.Show("電壓、電流、溫度讀取失敗。");
                return;
            }

            //區塊1
            mnsSetting.Enabled = false;  //工具列設定改為不可操作

            //區塊2
            grbArea2.Enabled = false;  //整個區塊2改為不可操作

            //區塊3
            btnDetection.Enabled = false;  //檢測按鈕改為不可操作
            btnSkipToNextTest.Enabled = true; //跳至下項檢測按鈕改為可操作

            //區塊4
            grbArea4.Visible = false; //LED檢測區塊隱藏
            tmrLedState.Enabled = false; //LED閃爍計時器不啟用
            ledOff(); //LED熄滅

            //區塊5
            lblDetectionProcessDescription.Text = "開始檢測";

            //區塊6
            changeLabelStyle(lblTestSW1Result, "待測");
            changeLabelStyle(lblTestSW2Result, "待測");
            changeLabelStyle(lblTestModuleFirmwareVersionResult, "待測");
            changeLabelStyle(lblTest485CommunicationP4Result, "待測");
            changeLabelStyle(lblTest485CommunicationP5Result, "待測");
            changeLabelStyle(lblTestControlAP9Result, "待測");
            changeLabelStyle(lblTestControlBP10Result, "待測");
            changeLabelStyle(lblTestTxRxP3Result, "待測");
            changeLabelStyle(lblTestLedResult, "待測");

            //寫入資料
            _detectionSend.data.detectDateTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"); //把檢測時間記錄到物件中
            _detectionSend.data.prodSerialNum = tbxSerialNumber.Text; //把生產序號記錄到物件中
            _detectionSend.data.detectUserName = tbxInspector.Text; //把檢測人員記錄到物件中
            _detectionSend.data.prodType = cboModel.Text; //把型號記錄到物件中

            setReciprocal(); //下一個檢測之前的倒數設定
        }

        public void step2() //檢測1，SW1全部為ON
        {
            _intSteup = 2; //紀錄當前步驟

            //區塊1
            mnsSetting.Enabled = false;  //工具列設定改為不可操作

            //區塊2
            grbArea2.Enabled = false;  //整個區塊2改為不可操作

            //區塊3
            btnDetection.Enabled = false;  //檢測按鈕改為不可操作
            btnSkipToNextTest.Enabled = true; //跳至下項檢測按鈕改為可操作

            //區塊4
            grbArea4.Visible = false; //LED檢測區塊隱藏
            tmrLedState.Enabled = false; //LED閃爍計時器不啟用
            ledOff(); //LED熄滅

            //區塊5
            lblDetectionProcessDescription.Text = "將SW1全部撥到ON";

            //區塊6
            changeLabelStyle(lblTestSW1Result, "測試中");
            changeLabelStyle(lblTestSW2Result, "待測");
            changeLabelStyle(lblTestModuleFirmwareVersionResult, "待測");
            changeLabelStyle(lblTest485CommunicationP4Result, "待測");
            changeLabelStyle(lblTest485CommunicationP5Result, "待測");
            changeLabelStyle(lblTestControlAP9Result, "待測");
            changeLabelStyle(lblTestControlBP10Result, "待測");
            changeLabelStyle(lblTestTxRxP3Result, "待測");
            changeLabelStyle(lblTestLedResult, "待測");

            _strSw1Value = readSw1(); //抓取六個腳位的狀態

            if (_bolIsDebug) //由於難以做到全部OFF，所以Debug情況下先強制PASS
            {
                viewSw1(_strSw1Value); //把SW1的6個腳位顯示在區域6中
                changeLabelStyle(lblTestSW1Result, "PASS");

                //寫入資料
                _detectionSend.data.sw1ONData = _strSw1Value;
                _detectionSend.data.sw1ONResult = "PASS";

                setReciprocal(); //下一個檢測之前的倒數設定
            }

            if (_strSw1Value != "111111") //如果測試不通過，則透過Timer重新跑10次，每次間隔1秒
            {
                _intTimerCount = 10; //重新執行10次
                tmrDetection.Interval = 1000; //1000毫秒間隔
                tmrDetection.Enabled = true; //啟用計時器
            }
            else //測試通過
            {
                viewSw1(_strSw1Value); //把SW1的6個腳位顯示在區域6中
                changeLabelStyle(lblTestSW1Result, "PASS");

                //寫入資料
                _detectionSend.data.sw1ONData = _strSw1Value;
                _detectionSend.data.sw1ONResult = "PASS";

                setReciprocal(); //下一個檢測之前的倒數設定
            }
        }

        public void step3() //檢測2，SW1全部為OFF
        {
            _intSteup = 3; //紀錄當前步驟

            //區塊1
            mnsSetting.Enabled = false;  //工具列設定改為不可操作

            //區塊2
            grbArea2.Enabled = false;  //整個區塊2改為不可操作

            //區塊3
            btnDetection.Enabled = false;  //檢測按鈕改為不可操作
            btnSkipToNextTest.Enabled = true; //跳至下項檢測按鈕改為可操作

            //區塊4
            grbArea4.Visible = false; //LED檢測區塊隱藏
            tmrLedState.Enabled = false; //LED閃爍計時器不啟用
            ledOff(); //LED熄滅

            //區塊5
            lblDetectionProcessDescription.Text = "將SW1全部撥到OFF";

            //區塊6
            changeLabelStyle(lblTestSW1Result, "測試中");
            changeLabelStyle(lblTestSW2Result, "待測");
            changeLabelStyle(lblTestModuleFirmwareVersionResult, "待測");
            changeLabelStyle(lblTest485CommunicationP4Result, "待測");
            changeLabelStyle(lblTest485CommunicationP5Result, "待測");
            changeLabelStyle(lblTestControlAP9Result, "待測");
            changeLabelStyle(lblTestControlBP10Result, "待測");
            changeLabelStyle(lblTestTxRxP3Result, "待測");
            changeLabelStyle(lblTestLedResult, "待測");

            _strSw1Value = readSw1(); //抓取六個腳位的狀態

            if (_strSw1Value != "000000" || !_bolIsDebug) //如果測試不通過，則透過Timer重新跑10次，每次間隔1秒
            {
                _intTimerCount = 10; //重新執行10次
                tmrDetection.Interval = 1000; //1000毫秒間隔
                tmrDetection.Enabled = true; //啟用計時器
            }
            else //測試通過
            {
                viewSw1(_strSw1Value); //把SW1的6個腳位顯示在區域6中
                changeLabelStyle(lblTestSW1Result, "PASS");

                //寫入資料
                _detectionSend.data.sw1OFFData = _strSw1Value;
                _detectionSend.data.sw1OFFResult = "PASS";

                setReciprocal(); //下一個檢測之前的倒數設定
            }
        }

        public void step4() //檢測3，SW2全部為ON
        {
            _intSteup = 4; //紀錄當前步驟

            //區塊1
            mnsSetting.Enabled = false;  //工具列設定改為不可操作

            //區塊2
            grbArea2.Enabled = false;  //整個區塊2改為不可操作

            //區塊3
            btnDetection.Enabled = false;  //檢測按鈕改為不可操作
            btnSkipToNextTest.Enabled = true; //跳至下項檢測按鈕改為可操作

            //區塊4
            grbArea4.Visible = false; //LED檢測區塊隱藏
            tmrLedState.Enabled = false; //LED閃爍計時器不啟用
            ledOff(); //LED熄滅

            //區塊5
            lblDetectionProcessDescription.Text = "將SW2全部撥到ON";

            //區塊6
            changeLabelStyle(lblTestSW2Result, "測試中");
            changeLabelStyle(lblTestModuleFirmwareVersionResult, "待測");
            changeLabelStyle(lblTest485CommunicationP4Result, "待測");
            changeLabelStyle(lblTest485CommunicationP5Result, "待測");
            changeLabelStyle(lblTestControlAP9Result, "待測");
            changeLabelStyle(lblTestControlBP10Result, "待測");
            changeLabelStyle(lblTestTxRxP3Result, "待測");
            changeLabelStyle(lblTestLedResult, "待測");

            _strSw2Value = readSw2(); //抓取兩個腳位的狀態

            if (_bolIsDebug) //由於難以做到全部OFF，所以Debug情況下先強制PASS
            {
                viewSw2(_strSw2Value); //把SW1的6個腳位顯示在區域6中
                changeLabelStyle(lblTestSW2Result, "PASS");

                //寫入資料
                _detectionSend.data.sw2ONData = _strSw2Value;
                _detectionSend.data.sw2ONResult = "PASS";

                setReciprocal(); //下一個檢測之前的倒數設定
            }

            if (_strSw2Value != "11") //如果測試不通過，則透過Timer重新跑10次，每次間隔1秒
            {
                _intTimerCount = 10; //重新執行10次
                tmrDetection.Interval = 1000; //1000毫秒間隔
                tmrDetection.Enabled = true; //啟用計時器
            }
            else //測試通過
            {
                viewSw1(_strSw2Value); //把SW1的6個腳位顯示在區域6中
                changeLabelStyle(lblTestSW1Result, "PASS");

                //寫入資料
                _detectionSend.data.sw2ONData = _strSw2Value;
                _detectionSend.data.sw2ONResult = "PASS";

                setReciprocal(); //下一個檢測之前的倒數設定
            }
        }

        public void step5() //檢測4，SW2全部為OFF
        {
            _intSteup = 5; //紀錄當前步驟

            //區塊1
            mnsSetting.Enabled = false;  //工具列設定改為不可操作

            //區塊2
            grbArea2.Enabled = false;  //整個區塊2改為不可操作

            //區塊3
            btnDetection.Enabled = false;  //檢測按鈕改為不可操作
            btnSkipToNextTest.Enabled = true; //跳至下項檢測按鈕改為可操作

            //區塊4
            grbArea4.Visible = false; //LED檢測區塊隱藏
            tmrLedState.Enabled = false; //LED閃爍計時器不啟用
            ledOff(); //LED熄滅

            //區塊5
            lblDetectionProcessDescription.Text = "將SW2全部撥到OFF";

            //區塊6
            changeLabelStyle(lblTestSW2Result, "測試中");
            changeLabelStyle(lblTestModuleFirmwareVersionResult, "待測");
            changeLabelStyle(lblTest485CommunicationP4Result, "待測");
            changeLabelStyle(lblTest485CommunicationP5Result, "待測");
            changeLabelStyle(lblTestControlAP9Result, "待測");
            changeLabelStyle(lblTestControlBP10Result, "待測");
            changeLabelStyle(lblTestTxRxP3Result, "待測");
            changeLabelStyle(lblTestLedResult, "待測");

            _strSw2Value = readSw2(); //抓取兩個腳位的狀態

            if (_strSw2Value != "00") //如果測試不通過，則透過Timer重新跑10次，每次間隔1秒
            {
                _intTimerCount = 10; //重新執行10次
                tmrDetection.Interval = 1000; //1000毫秒間隔
                tmrDetection.Enabled = true; //啟用計時器
            }
            else //測試通過
            {
                viewSw1(_strSw2Value); //把SW1的6個腳位顯示在區域6中
                changeLabelStyle(lblTestSW2Result, "PASS");

                //寫入資料
                _detectionSend.data.sw2OFFData = _strSw2Value;
                _detectionSend.data.sw2OFFResult = "PASS";

                runNextStep(); //執行下一步驟
            }
        }

        public void step5_1() //判斷檢測1~4步驟是否有出現FAILED
        {
            _intSteup = 501; //紀錄當前步驟

            if (_detectionSend.data.sw1ONResult == "PASS" && _detectionSend.data.sw1OFFResult == "PASS" && _detectionSend.data.sw2ONResult == "PASS" && _detectionSend.data.sw2OFFResult == "PASS") //如果全部成功
            {
                setReciprocal(); //下一個檢測之前的倒數設定

            }
            else //如果沒有全部成功
            {
                sendDetection(); //傳送檢測紀錄

                step0(); //恢復原本初始化狀態

                //區塊5
                lblDetectionProcessDescription.Text = "SW1或SW2測試有問題，中斷測試。關閉電源，更換下一片";

                tbxSerialNumber.Text = string.Empty; //生產編號清空
            }
        }

        public void step6() //檢測5，SW1及SW2回到出場預設值
        {
            _intSteup = 6; //紀錄當前步驟

            //區塊1
            mnsSetting.Enabled = false;  //工具列設定改為不可操作

            //區塊2
            grbArea2.Enabled = false;  //整個區塊2改為不可操作

            //區塊3
            btnDetection.Enabled = false;  //檢測按鈕改為不可操作
            btnSkipToNextTest.Enabled = true; //跳至下項檢測按鈕改為可操作

            //區塊4
            grbArea4.Visible = false; //LED檢測區塊隱藏
            tmrLedState.Enabled = false; //LED閃爍計時器不啟用
            ledOff(); //LED熄滅

            //區塊5
            lblDetectionProcessDescription.Text = "將SW1-6撥到ON，其他OFF，SW2-1 OFF，SW2-2 ON，BAUD RATE 9600，ID: 1";

            //區塊6
            changeLabelStyle(lblTestSW1Result, "測試中");
            changeLabelStyle(lblTestSW2Result, "測試中");
            changeLabelStyle(lblTestModuleFirmwareVersionResult, "待測");
            changeLabelStyle(lblTest485CommunicationP4Result, "待測");
            changeLabelStyle(lblTest485CommunicationP5Result, "待測");
            changeLabelStyle(lblTestControlAP9Result, "待測");
            changeLabelStyle(lblTestControlBP10Result, "待測");
            changeLabelStyle(lblTestTxRxP3Result, "待測");
            changeLabelStyle(lblTestLedResult, "待測");


            _strSw1Value = readSw1(); //抓取六個腳位的狀態
            _strSw2Value = readSw2(); //抓取兩個腳位的狀態
            viewSw1(_strSw1Value); //把SW1的6個腳位顯示在區域6中
            viewSw2(_strSw2Value); //把SW1的6個腳位顯示在區域6中

            if (_bolIsDebug) //由於目前無法模擬，所以Debug情況下先強制PASS
            {
                //viewSw1(_strSw1Value); //把SW1的6個腳位顯示在區域6中
                changeLabelStyle(lblTestSW1Result, "PASS");
                changeLabelStyle(lblTestSW2Result, "PASS");

                //寫入資料
                _detectionSend.data.swInitialResult = "PASS";

                runNextStep(); //下一個檢測之前的倒數設定
            }

            if (_strSw1Value != "000001") //如果測試不通過，則透過Timer重新跑10次，每次間隔1秒
            {
                _intTimerCount = 10; //重新執行10次
                tmrDetection.Interval = 1000; //1000毫秒間隔
                tmrDetection.Enabled = true; //啟用計時器
            }
            else //SW1測試通過
            {
                //changeLabelStyle(lblTestSW1Result, "PASS");

                if (_strSw2Value != "01") //如果測試不通過，則透過Timer重新跑10次，每次間隔1秒
                {
                    _intTimerCount = 10; //重新執行10次
                    tmrDetection.Interval = 1000; //1000毫秒間隔
                    tmrDetection.Enabled = true; //啟用計時器
                }
                else //SW2測試通過
                {
                    changeLabelStyle(lblTestSW1Result, "PASS");
                    changeLabelStyle(lblTestSW2Result, "PASS");

                    //寫入資料
                    _detectionSend.data.swInitialResult = "PASS";

                    runNextStep(); //執行下一步驟
                }
            }
        }

        public void step6_1() //檢測SW1及SW2回到出場預設值是否有FAILED
        {
            _intSteup = 601; //紀錄當前步驟

            if (_detectionSend.data.swInitialResult == "PASS") //如果回復原廠設定全部成功
            {
                runNextStep(); //執行下一步驟

            }
            else //如果沒有全部成功
            {
                sendDetection(); //傳送檢測紀錄

                step0(); //恢復原本初始化狀態

                //區塊5
                lblDetectionProcessDescription.Text = "SW1與SW2回復出場測試有問題，中斷測試。關閉電源，更換下一片";

                tbxSerialNumber.Text = string.Empty; //生產編號清空
            }
        }

        public void step7() //檢測6，測試485通訊(P4)
        {
            _intSteup = 7; //紀錄當前步驟

            //區塊1
            mnsSetting.Enabled = false;  //工具列設定改為不可操作

            //區塊2
            grbArea2.Enabled = false;  //整個區塊2改為不可操作

            //區塊3
            btnDetection.Enabled = false;  //檢測按鈕改為不可操作
            btnSkipToNextTest.Enabled = false; //跳至下項檢測按鈕改為不可操作

            //區塊4
            grbArea4.Visible = false; //LED檢測區塊隱藏
            tmrLedState.Enabled = false; //LED閃爍計時器不啟用
            ledOff(); //LED熄滅

            //區塊5
            lblDetectionProcessDescription.Text = "測試485通訊(P4)";

            //區塊6
            changeLabelStyle(lblTestModuleFirmwareVersionResult, "測試中");
            changeLabelStyle(lblTest485CommunicationP4Result, "測試中");
            changeLabelStyle(lblTest485CommunicationP5Result, "待測");
            changeLabelStyle(lblTestControlAP9Result, "待測");
            changeLabelStyle(lblTestControlBP10Result, "待測");
            changeLabelStyle(lblTestTxRxP3Result, "待測");
            changeLabelStyle(lblTestLedResult, "待測");

            if (checkP4()) //測試485通訊(P4)，通過
            {
                changeLabelStyle(lblTest485CommunicationP4Result, "PASS");
                changeLabelStyle(lblTestModuleFirmwareVersionResult, _detectionSend.data.firmwareVersion); //顯示韌體版本

                //寫入資料
                _detectionSend.data.p4PortResult = "PASS";
            }
            else //測試485通訊(P4)，失敗
            {
                changeLabelStyle(lblTest485CommunicationP4Result, "FAILED");
                _IsAllSuccess = false; //有檢測失敗發生

                //寫入資料
                _detectionSend.data.p4PortResult = "FAILED";
            }

            runNextStep(); //執行下一步驟
        }

        public void step8() //檢測7，測試485通訊(P5)
        {
            _intSteup = 8; //紀錄當前步驟

            //區塊1
            mnsSetting.Enabled = false;  //工具列設定改為不可操作

            //區塊2
            grbArea2.Enabled = false;  //整個區塊2改為不可操作

            //區塊3
            btnDetection.Enabled = false;  //檢測按鈕改為不可操作
            btnSkipToNextTest.Enabled = false; //跳至下項檢測按鈕改為不可操作

            //區塊4
            grbArea4.Visible = false; //LED檢測區塊隱藏
            tmrLedState.Enabled = false; //LED閃爍計時器不啟用
            ledOff(); //LED熄滅

            //區塊5
            lblDetectionProcessDescription.Text = "測試485通訊(P5)";

            //區塊6
            changeLabelStyle(lblTest485CommunicationP5Result, "測試中");
            changeLabelStyle(lblTestControlAP9Result, "待測");
            changeLabelStyle(lblTestControlBP10Result, "待測");
            changeLabelStyle(lblTestTxRxP3Result, "待測");
            changeLabelStyle(lblTestLedResult, "待測");

            if (checkP5()) //測試485通訊(P5)，通過
            {
                changeLabelStyle(lblTest485CommunicationP5Result, "PASS");

                //寫入資料
                _detectionSend.data.p5PortResult = "PASS";
                changeLabelStyle(lblTestModuleFirmwareVersionResult, _detectionSend.data.firmwareVersion); //顯示韌體版本
            }
            else //測試485通訊(P5)，失敗
            {
                changeLabelStyle(lblTest485CommunicationP5Result, "FAILED");
                _IsAllSuccess = false; //有檢測失敗發生

                //寫入資料
                _detectionSend.data.p5PortResult = "FAILED";
            }

            runNextStep(); //執行下一步驟
        }

        public void step8_1() //檢測485通訊P4與P5是否都為FAILED
        {
            _intSteup = 801; //紀錄當前步驟

            if (_detectionSend.data.p4PortResult == "PASS" || _detectionSend.data.p5PortResult == "PASS") //如果其中一項為成功，則進行下一個步驟
            {
                if (_detectionSend.data.prodType == "C5A")
                {
                    //區塊6
                    changeLabelStyle(lblTestControlAP9Result, "--");
                    changeLabelStyle(lblTestControlBP10Result, "--");

                    step11(); //如果型號等於C5A則跳到第11步驟
                }
                else runNextStep(); //執行下一步驟
            }
            else //如果沒有全部成功
            {
                sendDetection(); //傳送檢測紀錄

                step0(); //恢復原本初始化狀態

                //區塊5
                lblDetectionProcessDescription.Text = "485通訊(P4)與 485通訊(P5)測試皆有問題，中斷測試，請更換待測板，並重新測試";

                tbxSerialNumber.Text = string.Empty; //生產編號清空
            }
        }

        public void step9() //檢測8，偵測Control-A (P9) (若型號選擇C5A，則略過此測試，區塊6此部分顯示--)
        {
            _intSteup = 9; //紀錄當前步驟

            //區塊1
            mnsSetting.Enabled = false;  //工具列設定改為不可操作

            //區塊2
            grbArea2.Enabled = false;  //整個區塊2改為不可操作

            //區塊3
            btnDetection.Enabled = false;  //檢測按鈕改為不可操作
            btnSkipToNextTest.Enabled = false; //跳至下項檢測按鈕改為不可操作

            //區塊4
            grbArea4.Visible = false; //LED檢測區塊隱藏
            tmrLedState.Enabled = false; //LED閃爍計時器不啟用
            ledOff(); //LED熄滅

            //區塊5
            lblDetectionProcessDescription.Text = "偵測Control-A(P9)";

            //區塊6
            changeLabelStyle(lblTestControlAP9Result, "測試中");
            changeLabelStyle(lblTestControlBP10Result, "待測");
            changeLabelStyle(lblTestTxRxP3Result, "待測");
            changeLabelStyle(lblTestLedResult, "待測");


            int port_idx;

            if (_detectionSend.data.p4PortResult == "PASS") port_idx = 0;
            else port_idx = 1;

            if (checkP9(port_idx, 200)) //偵測Control-A (P9)，通過
            {
                changeLabelStyle(lblTestControlAP9Result, "PASS");

                //寫入資料
                _detectionSend.data.p9PortResult = "PASS";
            }
            else //偵測Control-A (P9)，失敗，再檢測第二次
            {
                //第二次檢測，Delay時間拉長
                if (checkP9(port_idx, 500)) //偵測Control-A (P9)，通過
                {
                    changeLabelStyle(lblTestControlAP9Result, "PASS");

                    //寫入資料
                    _detectionSend.data.p9PortResult = "PASS";
                }
                else //偵測Control-A (P9)，第二次失敗，直接印出失敗訊息
                {
                    changeLabelStyle(lblTestControlAP9Result, "FAILED");
                    _IsAllSuccess = false; //有檢測失敗發生

                    //寫入資料
                    _detectionSend.data.p9PortResult = "FAILED";
                }
            }

            runNextStep(); //執行下一步驟
        }

        public void step10() //檢測9，偵測Control-B (P10) (若型號選擇C5A，則略過此測試，區塊6此部分顯示--)
        {
            _intSteup = 10; //紀錄當前步驟

            //區塊1
            mnsSetting.Enabled = false;  //工具列設定改為不可操作

            //區塊2
            grbArea2.Enabled = false;  //整個區塊2改為不可操作

            //區塊3
            btnDetection.Enabled = false;  //檢測按鈕改為不可操作
            btnSkipToNextTest.Enabled = false; //跳至下項檢測按鈕改為不可操作

            //區塊4
            grbArea4.Visible = false; //LED檢測區塊隱藏
            tmrLedState.Enabled = false; //LED閃爍計時器不啟用
            ledOff(); //LED熄滅

            //區塊5
            lblDetectionProcessDescription.Text = "偵測Control-B(P10)";

            //區塊6
            changeLabelStyle(lblTestControlBP10Result, "測試中");
            changeLabelStyle(lblTestTxRxP3Result, "待測");
            changeLabelStyle(lblTestLedResult, "待測");


            int port_idx;

            if (_detectionSend.data.p4PortResult == "PASS") port_idx = 0;
            else port_idx = 1;

            if (checkP10(port_idx, 200)) //偵測Control-B (P10)，通過
            {
                changeLabelStyle(lblTestControlBP10Result, "PASS");

                //寫入資料
                _detectionSend.data.p10PortResult = "PASS";
            }
            else //偵測Control-B (P10)，失敗，再檢測第二次
            {
                //第二次檢測，Delay時間拉長
                if (checkP10(port_idx, 500)) //偵測Control-B (P10)，通過
                {
                    changeLabelStyle(lblTestControlBP10Result, "PASS");

                    //寫入資料
                    _detectionSend.data.p10PortResult = "PASS";
                }
                else //偵測Control-B (P10)，第二次失敗，直接印出失敗訊息
                {
                    changeLabelStyle(lblTestControlBP10Result, "FAILED");
                    _IsAllSuccess = false; //有檢測失敗發生

                    //寫入資料
                    _detectionSend.data.p10PortResult = "FAILED";
                }
            }

            runNextStep(); //執行下一步驟
        }

        public void step11() //檢測10，偵測Tx Rx Port
        {
            _intSteup = 11; //紀錄當前步驟

            int port_idx;

            if (_detectionSend.data.p4PortResult == "PASS") port_idx = 0;
            else port_idx = 1;

            //區塊1
            mnsSetting.Enabled = false;  //工具列設定改為不可操作

            //區塊2
            grbArea2.Enabled = false;  //整個區塊2改為不可操作

            //區塊3
            btnDetection.Enabled = false;  //檢測按鈕改為不可操作
            btnSkipToNextTest.Enabled = false; //跳至下項檢測按鈕改為不可操作

            //區塊4
            grbArea4.Visible = false; //LED檢測區塊隱藏
            tmrLedState.Enabled = false; //LED閃爍計時器不啟用
            ledOff(); //LED熄滅

            //區塊5
            lblDetectionProcessDescription.Text = "偵測Tx Rx Port(P3)";

            //區塊6
            changeLabelStyle(lblTestTxRxP3Result, "測試中");
            changeLabelStyle(lblTestLedResult, "待測");

            if (checkP3(port_idx)) //偵測Tx Rx Port(P3)，通過
            {
                changeLabelStyle(lblTestTxRxP3Result, "PASS");

                //寫入資料
                _detectionSend.data.p3PortResult = "PASS";
            }
            else //偵測Tx Rx Port(P3)，失敗
            {
                changeLabelStyle(lblTestTxRxP3Result, "FAILED");
                _IsAllSuccess = false; //有檢測失敗發生

                //寫入資料
                _detectionSend.data.p3PortResult = "FAILED";
            }

            runNextStep(); //執行下一步驟
        }

        public void step12() //檢測11，偵測LED
        {
            _intSteup = 12; //紀錄當前步驟

            //區塊1
            mnsSetting.Enabled = false;  //工具列設定改為不可操作

            //區塊2
            grbArea2.Enabled = false;  //整個區塊2改為不可操作

            //區塊3
            btnDetection.Enabled = false;  //檢測按鈕改為不可操作
            btnSkipToNextTest.Enabled = false; //跳至下項檢測按鈕改為不可操作

            //區塊4
            grbArea4.Visible = true; //LED檢測區塊顯示
            tmrLedState.Enabled = true; //LED閃爍計時器啟用

            //區塊5
            lblDetectionProcessDescription.Text = "偵測LED，請確認LED是否閃爍，並選取檢測結果";

            //區塊6
            changeLabelStyle(lblTestLedResult, "測試中");
        }

        public void step12_1() //檢測LED後，的最後整理
        {
            _intSteup = 1201; //紀錄當前步驟

            sendDetection(); //傳送檢測紀錄

            step0(); //恢復原本初始化狀態

            //區塊5
            lblDetectionProcessDescription.Text = "測試完成, 關閉電源，更換下一片";

            tbxSerialNumber.Text = string.Empty; //生產編號清空
        }



        //-------------------------------------------------- ↓一些方便使用的Function↓ --------------------------------------------------

        public void setReciprocal() //下一個檢測之前的倒數設定
        {
            if (_intReciprocaSet > 0) //如果有啟用倒數功能
            {
                //switch (_intSteup) //判斷當前是在哪一個步驟，將倒數後的下一個步驟的訊息顯示出來
                //{
                //    case 2:
                //        lblDetectionProcessDescription.Text = "將SW1全部撥到OFF";
                //        break;
                //    case 3:
                //        lblDetectionProcessDescription.Text = "將SW2全部撥到ON";
                //        break;
                //    case 4:
                //        lblDetectionProcessDescription.Text = "將SW3全部撥到OFF";
                //        break;
                //    case 501:
                //        lblDetectionProcessDescription.Text = "將SW1-6撥到ON，其他OFF，SW2-1 OFF，SW2-2 ON，BAUD RATE 9600，ID: 1";
                //        break;
                //}

                _intReciproca = _intReciprocaSet; //設定檢測倒數秒數
                lblReciprocal.Visible = true;
                lblReciprocal.Text = _intReciproca.ToString(); //顯示倒數秒數
                tmrReciproca.Enabled = true; //開啟倒數計時器
            }
            else //沒有啟用倒數功能就直接跳到下一個步驟
            {
                runNextStep(); //執行下一步驟
            }
        }

        public void runNextStep() //執行下一步驟
        {
            switch (_intSteup) //判斷當前是在哪一個步驟
            {
                case 0:
                    step1(); //開始檢測
                    break;
                case 1:
                    step2(); //檢測1，SW1全部為ON
                    break;
                case 2:
                    step3(); //檢測2，SW1全部為OFF
                    break;
                case 3:
                    step4(); //檢測3，SW2全部為ON
                    break;
                case 4:
                    step5(); //檢測4，SW2全部為OFF
                    break;
                case 5:
                    step5_1(); //檢測判斷1-4步驟檢測有無FAILED
                    break;
                case 501:
                    step6(); //檢測5，SW1及SW2回到出場預設值
                    break;
                case 6:
                    step6_1(); //檢測SW1及SW2回到出場預設值是否有FAILED
                    break;
                case 601:
                    step7(); //檢測6，測試485通訊(P4)
                    break;
                case 7:
                    step8(); //檢測7，測試485通訊(P5)
                    break;
                case 8:
                    step8_1(); //檢測485通訊P4與P5是否都為FAILED
                    break;
                case 801:
                    step9(); //檢測8，偵測Control-A (P9) (若型號選擇C5A，則略過此測試，區塊6此部分顯示--)
                    break;
                case 9:
                    step10(); //檢測9，偵測Control-B (P10) (若型號選擇C5A，則略過此測試，區塊6此部分顯示--)
                    break;
                case 10:
                    step11(); //檢測10，偵測Tx Rx Port
                    break;
                case 11:
                    step12(); //檢測11	，偵測LED
                    break;
                default:
                    step0(); //恢復原本初始化狀態
                    break;
            }
        }


        public void changeLabelStyle(System.Windows.Forms.Label lblLabel, string strText) //一次改變Label的多個屬性，由於需要重複使用，所以獨立出來
        {
            switch (strText)
            {
                case "待測":
                    lblLabel.Text = strText;
                    lblLabel.ForeColor = ColorTranslator.FromHtml("#000000");
                    break;
                case "測試中":
                    lblLabel.Text = strText;
                    lblLabel.ForeColor = ColorTranslator.FromHtml("#880000");
                    break;
                case "PASS":
                    lblLabel.Text = strText;
                    lblLabel.ForeColor = ColorTranslator.FromHtml("#008800");
                    break;
                case "FAILED":
                    lblLabel.Text = strText;
                    lblLabel.ForeColor = ColorTranslator.FromHtml("#cc0000");
                    break;
                default: //其他
                    lblLabel.Text = strText;
                    lblLabel.ForeColor = ColorTranslator.FromHtml("#000000");
                    break;
            }
        }

        public bool checkPorts(List<string> listPorts) //判斷Port是否都有抓到
        {
            if (!(listPorts is null))
            {
                if (listPorts.Count == 3)
                {
                    foreach (string item in listPorts)
                    {
                        if (item == string.Empty)
                        {
                            return false;
                        }
                    }
                    return true;
                }
            }
            return false;
        }
        public void viewSw1Sw2Default() //把SW1與SW2的顯示改為 -- 預設
        {
            string strChat;
            System.Windows.Forms.Label[] lblSw = new System.Windows.Forms.Label[] { lblTestSW1_1V, lblTestSW1_2V, lblTestSW1_3V, lblTestSW1_4V, lblTestSW1_5V, lblTestSW1_6V, lblTestSW2_1V, lblTestSW2_2V };

            for (int i = 0; i < 8; i++)
            {
                lblSw[i].Text = convertState("--");
            }
        }

        public void viewSw1(string strSw1Value) //把SW1的6個腳位顯示在區域6中
        {
            string strChat;
            if (strSw1Value.Length == 6)
            {
                System.Windows.Forms.Label[] lblSw1 = new System.Windows.Forms.Label[] { lblTestSW1_1V, lblTestSW1_2V, lblTestSW1_3V, lblTestSW1_4V, lblTestSW1_5V, lblTestSW1_6V };

                for (int i = 0; i < 6; i++)
                {
                    strChat = strSw1Value.Substring(i, 1);
                    lblSw1[i].Text = convertState(strChat);
                }
            }
        }

        public void viewSw2(string strSw2Value) //把SW2的2個腳位顯示在區域6中
        {
            string strChat;
            if (strSw2Value.Length == 2)
            {
                System.Windows.Forms.Label[] lblSw2 = new System.Windows.Forms.Label[] { lblTestSW2_1V, lblTestSW2_2V };

                for (int i = 0; i < 2; i++)
                {
                    strChat = strSw2Value.Substring(i, 1);
                    lblSw2[i].Text = convertState(strChat);
                }
            }
        }

        public string convertState(string strState) //01轉換成ON或OFF
        {
            if (strState == "1") return "ON";
            else if (strState == "0") return "OFF";
            else return "--";
        }

        public void sendDetection() //傳送檢測紀錄，以及傳送前後需要做的動作
        {
            if (_IsAllSuccess) //如果沒有失敗發生
            {
                _detectionSend.data.finalResult = "PASS";
            }
            else
            {
                _detectionSend.data.finalResult = "FAILED";
            }

            string strMessage; //用來儲存資料送出的回傳訊息
            if (!(sendDetectionApi(out strMessage))) //如果資料送出失敗
            {
                MessageBox.Show("資料送出失敗，錯誤訊息：" + strMessage);
            }
        }

        public bool sendDetectionApi(out string strMessage) //實際透過API送出資料，回傳為是否成功的Bool，以及out錯誤訊息
        {
            strMessage = "";
            try
            {
                //把物件轉換成Json字串
                string strDetectionJson = JsonSerializer.Serialize(_detectionSend);

                string strApiPath = ConfigurationManager.AppSettings["ApiUrl_Create"];

                //建立 HttpClient
                HttpClient client = new HttpClient() { BaseAddress = new Uri(strApiPath) };

                var content = new StringContent(strDetectionJson, Encoding.UTF8, "application/json");
                var result = client.PostAsync(strApiPath, content).Result;

                DetectionResponse detectionResponse = JsonSerializer.Deserialize<DetectionResponse>(result.Content.ReadAsStringAsync().GetAwaiter().GetResult()); //單筆資料的JSON反序列化成物件

                //判斷API回傳的Success狀態，以此確定有沒有新增成功
                if (detectionResponse.success) //如果新增成功
                {
                    return true; //回傳字串給Controller
                }
                else //如果API回傳False
                {
                    strMessage = detectionResponse.message;
                    return false;
                }
            }
            catch (Exception ex)
            {
                strMessage = ex.ToString();
                return false;
            }
        }

        //-------------------------------------------------- ↑一些方便使用的Function↑ --------------------------------------------------



        //-------------------------------------------------- ↓主要跟硬體構通的部分↓ --------------------------------------------------

        private bool read_config(out string ErrorInfo)//載入設定檔
        {
            ErrorInfo = "";
            string info = "";
            //string debug_info = "";
            bool flag = false;
            string config_path = Path.GetDirectoryName(typeof(Program).Assembly.Location) + "/" + CONFIG_FILE_NAME;

            try
            {
                string SrcString = "";
                SrcString = File.ReadAllText(config_path, System.Text.Encoding.Default);
                string[] rawString = SrcString.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                info = "CONFIG FILE CONTENT: " + Environment.NewLine;

                for (int i = 0; i < 3; i++)
                {
                    BT_rs485_ctrl.g_com_port_info[i].device_id = rawString[i].Split(';')[0];
                    BT_rs485_ctrl.g_com_port_info[i].com_port = "";
                    info += "[" + i + "] device_id: " + BT_rs485_ctrl.g_com_port_info[i].device_id + Environment.NewLine;
                }
                flag = true;
                //DEBUG ----------------------------
                setText(info);
            }
            catch (Exception ee)
            {
                ErrorInfo += "ERROR OPEN CONFIG FILE " + Environment.NewLine;
                ErrorInfo += ee.ToString() + Environment.NewLine;
                flag = false;
            }

            return flag;
        }

        private List<string> scan_port(bool debug) //掃描COM PORT
        {
            string info = "";
            List<string> listPorts = new List<string>();

            using (ManagementClass i_Entity = new ManagementClass("Win32_PnPEntity"))
            {
                for (int i = 0; i < 3; i++)
                {
                    BT_rs485_ctrl.g_com_port_info[i].com_port = "";
                }

                foreach (ManagementObject i_Inst in i_Entity.GetInstances())
                {
                    Object o_Guid = i_Inst.GetPropertyValue("ClassGuid");
                    if (o_Guid == null || o_Guid.ToString().ToUpper() != "{4D36E978-E325-11CE-BFC1-08002BE10318}")
                        continue; // Skip all devices except device class "PORTS"

                    String s_Caption = i_Inst.GetPropertyValue("Caption").ToString();
                    String s_Manufact = i_Inst.GetPropertyValue("Manufacturer").ToString();
                    String s_DeviceID = i_Inst.GetPropertyValue("PnpDeviceID").ToString();

                    String s_RegPath = "HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Enum\\" + s_DeviceID + "\\Device Parameters";
                    String s_PortName = Registry.GetValue(s_RegPath, "PortName", "").ToString();

                    int s32_Pos = s_Caption.IndexOf(" (COM");
                    if (s32_Pos > 0) // remove COM port from description
                        s_Caption = s_Caption.Substring(0, s32_Pos);

                    //DEBUG =====================================================================
                    if (debug)
                    {
                        info += "Port Name:    " + s_PortName + Environment.NewLine;
                        info += "Description:  " + s_Caption + Environment.NewLine;
                        info += "Manufacturer: " + s_Manufact + Environment.NewLine;
                        info += "Device ID:    " + s_DeviceID + Environment.NewLine;

                        info += "-----------------------------------" + Environment.NewLine;
                    }
                    //DEBUG =====================================================================
                    for (int i = 0; i < BT_rs485_ctrl.g_com_port_info.Length; i++)
                    {
                        if (s_DeviceID.Contains(BT_rs485_ctrl.g_com_port_info[i].device_id))
                        {
                            BT_rs485_ctrl.g_com_port_info[i].com_port = s_PortName;
                            listPorts.Add(s_PortName); //把PortName放進List裡面
                            break;
                        }
                    }
                }
            }
            //DEBUG =====================================================================
            if (debug)
            {
                for (int i = 0; i < BT_rs485_ctrl.g_com_port_info.Length; i++)
                {
                    info += "[" + i + "] , ID: " + BT_rs485_ctrl.g_com_port_info[i].device_id + ", port: " + BT_rs485_ctrl.g_com_port_info[i].com_port + Environment.NewLine;
                }
            }
            //setText(info);
            //DEBUG =====================================================================

            return listPorts;
        }

        public bool checkP4() //測試485通訊(P4)
        {
            short ack_value;
            string ErrorInfo;
            //string info = "";
            /*
                T-6-1 測試485通訊(P4) 
	            T-6-2 呼叫程式測試P4通訊埠
	            T-6-3 若通訊PASS => 紀錄PASS => T-7-1
	            T-6-4 若通訊FAILED => 紀錄FAILED => T-7-1
             */

            if (BT_rs485_ctrl.read_box_P4(out ack_value, out ErrorInfo))
            {
                //info += "檢查P4成功，BOX 版本: " + ack_value.ToString("X4") + Environment.NewLine;
                _detectionSend.data.firmwareVersion = ack_value.ToString("X4");
                return true;
            }
            else
            {
                //info += "檢查P4 失敗" + Environment.NewLine + ErrorInfo + Environment.NewLine;
                return false;
            }
            //setText(info);
        }

        public bool checkP5() //測試485通訊(P5)
        {
            short ack_value;
            string ErrorInfo;
            //string info = "";
            /*
                T-6-1 測試485通訊(P4) 
	            T-6-2 呼叫程式測試P4通訊埠
	            T-6-3 若通訊PASS => 紀錄PASS => T-7-1
	            T-6-4 若通訊FAILED => 紀錄FAILED => T-7-1
             */

            if (BT_rs485_ctrl.read_box_P5(out ack_value, out ErrorInfo))
            {
                //info += "檢查P4成功，BOX 版本: " + ack_value.ToString("X4") + Environment.NewLine;
                _detectionSend.data.firmwareVersion = ack_value.ToString("X4");
                return true;
            }
            else
            {
                //info += "檢查P4 失敗" + Environment.NewLine + ErrorInfo + Environment.NewLine;
                return false;
            }
            //setText(info);
        }

        private bool checkP9(int port_idx, int intDelay) //檢查P9。 port_idx;//0: P4可用，1: P5 可用，預設用P4測試
        {
            string ErrorInfo;
            //string info = "";

            /*
                T-8-1 執行程式測試Control-A (P9)，選擇有PASS的 PORT，若P4、P5都PASS，選擇P4，預計執行時間2秒；C5A 不用測這項
	            T-8-2 程式回覆執行結果，紀錄執行結果 => T-9-1
             */

            if (BT_rs485_ctrl.check_P9(port_idx, intDelay, out ErrorInfo))
            {
                //info += "檢查P9成功" + Environment.NewLine;
                return true;
            }
            else
            {
                //info += "檢查P9失敗" + Environment.NewLine + ErrorInfo + Environment.NewLine;
                return false;
            }
            //setText(info);
        }

        private bool checkP10(int port_idx, int intDelay) //檢查P9。 port_idx;//0: P4可用，1: P5 可用，預設用P4測試
        {
            string ErrorInfo;
            //string info = "";
            //int port_idx = 0;//0: P4可用，1: P5 可用，預設用P4測試

            /*
                T-9-1 執行程式測試Control-B (P10)，選擇有PASS的 PORT，若P4、P5都PASS，選擇P4，預計執行時間2秒；C5A 不用測這項
	            T-9-2 程式回覆執行結果，紀錄執行結果 => T-10-1
             */

            if (BT_rs485_ctrl.check_P10(port_idx, intDelay, out ErrorInfo))
            {
                //info += "檢查P10成功" + Environment.NewLine;
                return true;
            }
            else
            {
                //info += "檢查P10失敗" + Environment.NewLine + ErrorInfo + Environment.NewLine;
                return false;
            }
            //setText(info);
        }

        private bool checkP3(int port_idx)//檢查P3。 0: P4可用，1: P5 可用，預設用P4測試
        {
            string ErrorInfo;
            //string info = "";

            /*
                T-10-1 執行程式測試TX/RX(P3)，預計執行時間1秒
	            T-10-2 程式回覆執行結果，紀錄執行結果 => T-11-1
             */

            if (BT_rs485_ctrl.check_P3(port_idx, out ErrorInfo))
            {
                //info += "檢查P3成功" + Environment.NewLine;
                return true;
            }
            else
            {
                //info += "檢查P3失敗" + Environment.NewLine + ErrorInfo + Environment.NewLine;
                return false;
            }
            //setText(info);
        }


        public bool ledOn() //LED點亮
        {
            string ErrorInfo;
            string info = "";

            int port_idx;

            if (_detectionSend.data.p4PortResult == "PASS") port_idx = 0;
            else port_idx = 1;

            /*
                T-11-1 執行程式點亮3顆LED
                T-11-2 由程式介面按壓OK或NG => 結束測試
             */

            if (BT_rs485_ctrl.LED_ctrl(port_idx, 1, out ErrorInfo))
            {
                //info += "LED點亮 成功" + Environment.NewLine;
                return true;
            }
            else
            {
                //info += "LED點亮 失敗" + Environment.NewLine + ErrorInfo + Environment.NewLine;
                return false;
            }
            //setText(info);
        }

        public bool ledOff() //LED熄滅
        {
            string ErrorInfo;
            string info = "";

            int port_idx;

            if (_detectionSend.data.p4PortResult == "PASS") port_idx = 0;
            else port_idx = 1;

            /*
                T-11-1 執行程式點亮3顆LED
                T-11-2 由程式介面按壓OK或NG => 結束測試
             */

            if (BT_rs485_ctrl.LED_ctrl(port_idx, 0, out ErrorInfo))
            {
                //info += "LED關閉 成功" + Environment.NewLine;
                return true;
            }
            else
            {
                //info += "LED關閉 失敗" + Environment.NewLine + ErrorInfo + Environment.NewLine;
                return false;
            }
            //setText(info);
        }

        public bool readMeter() //讀取電壓電流溫度，並且回傳是否出錯
        {
            string ErrorInfo = "";
            if (BT_rs485_ctrl.read_meter(out ErrorInfo))
            {
                //label1.Text = "電壓: " + BT_rs485_ctrl.g_meter_data.voltage;
                //label2.Text = "電流: " + BT_rs485_ctrl.g_meter_data.current;
                //label3.Text = "溫度: " + BT_rs485_ctrl.g_meter_data.temperature;
                //label4.Text = "資料時間: " + BT_rs485_ctrl.g_meter_data.data_time.ToString("yyyy-MM-dd hh:mm:ss");

                return true;
            }
            else
            {
                return false;
            }
        }

        private string readSw1() //讀SW1的6支PIN
        {
            string strAck_value = "";
            byte[] ack_value;
            string ErrorInfo = "";
            string info = "";
            /*
                T-1-1 提示"將SW1"全部撥到ON
	            T-1-2 執行程式持續讀取SW1狀態是否全部ON (持續呼叫程式&顯示數值)
	            T-1-3 讀到全部ON(ack_value[0-5] = 1 1 1 1 1 1 ) => 紀錄PASS => T-2-1
	            T-1-4 遲遲讀不到ON，使用者按壓"跳到下一項檢測" => 紀錄FAILED => T-2-1
	
	            T-2-1 提示"將SW1"全部撥到OFF
	            T-2-2 執行程式持續讀取SW1狀態是否全部OFF(持續呼叫程式&顯示數值)
	            T-2-3 讀到全部OFF(ack_value[0-5] = 0 0 0 0 0 0 ) => 紀錄PASS => T-3-1
	            T-2-4 遲遲讀不到OFF，使用者按壓"跳到下一項檢測" => 紀錄FAILED => T-3-1
             */
            if (BT_rs485_ctrl.query_sw_1(out ack_value, out ErrorInfo))
            {
                //SW1 = 0~5
                //info += "SW1的6支PIN: " + ack_value + Environment.NewLine;
                info += "SW1的6支PIN NUM: " + Environment.NewLine;
                for (int i = 1; i <= 6; i++)
                    info += i + " ";
                info += Environment.NewLine;
                for (int i = 0; i < 6; i++)
                {
                    info += ack_value[i] + " ";
                    strAck_value = strAck_value + ack_value[i]; //串接6個腳位的數值
                }
                info += Environment.NewLine;

                return strAck_value; //回傳6個腳位的數值
            }
            else
            {
                info += "讀SW1的6支PIN 失敗" + Environment.NewLine + ErrorInfo + Environment.NewLine;

                return ""; //回傳空字串表示錯誤
            }
            //setText(info);
        }

        private string readSw2() //讀SW2的2支PIN
        {
            string strAck_value = "";
            byte[] ack_value;
            string ErrorInfo = "";
            string info = "";
            /*
                T-3-1 提示"將SW2"全部撥到ON
	            T-3-2 執行程式持續讀取SW2狀態是否全部ON(持續呼叫程式&顯示數值)
	            T-3-3 讀到全部ON(ack_value [0-1]= 1 1) => 紀錄PASS => T-4-1
	            T-3-4 遲遲讀不到ON，使用者按壓"跳到下一項檢測" => 紀錄FAILED => T-4-1
	
	            T-4-1 提示"將SW2"全部撥到OFF
	            T-4-2 執行程式持續讀取SW2狀態是否全部OFF(持續呼叫程式&顯示數值)
	            T-4-3 讀到全部OFF (ack_value [0-1]= 0 0)=> 紀錄PASS => T-5-1
	            T-4-4 遲遲讀不到OFF，使用者按壓"跳到下一項檢測" => 紀錄FAILED => T-5-1

             */
            if (BT_rs485_ctrl.query_sw_2(out ack_value, out ErrorInfo))
            {
                //SW2 = 0~1
                //info += "SW2的2支PIN: " + ack_value + Environment.NewLine;
                info += "SW2的2支PIN NUM: " + Environment.NewLine;
                for (int i = 1; i <= 2; i++)
                    info += i + " ";
                info += Environment.NewLine;
                for (int i = 0; i < 2; i++)
                {
                    info += ack_value[i] + " ";
                    strAck_value = strAck_value + ack_value[i]; //串接6個腳位的數值
                }
                info += Environment.NewLine;
                return strAck_value; //回傳6個腳位的數值
            }
            else
            {
                info += "讀SW2的2支PIN 失敗" + Environment.NewLine + ErrorInfo + Environment.NewLine;

                return ""; //回傳空字串表示錯誤
            }
            //setText(info);
        }

        private void setText(string info)//在畫面上印出資訊
        {
            //richTextBox1.Text += info;
        }

        //-------------------------------------------------- ↑主要跟硬體構通的部分↑ --------------------------------------------------
    }
}
