using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bintronic_Inspect
{
    internal class Model
    {
    }

    public class DetectionSend //主要傳送測試結果資料的Json物件
    {
        public string crud { get; set; } = "create"; //疑似資料寫入模式 (create)
        public DetectionResult data { get; set; } = new DetectionResult(); //主要測試結果的資料
    }

    public class DetectionResult //主要測試結果的資料
    {
        public string detectDateTime { get; set; } = string.Empty; //檢測時間，格式為 yyyy/MM/dd HH:mm:ss
        public string prodSerialNum { get; set; } = string.Empty; //生產序號
        public string detectUserName { get; set; } = string.Empty; //檢測人員
        public string prodType { get; set; } = string.Empty; //型號（C5A或C5B）
        public string sw1ONResult { get; set; } = "--"; //SW1全部ON的檢測結果（PASS、FAILED、--）
        public string sw1ONData { get; set; } = string.Empty; //SW1全部ON的檢測資料
        public string sw1OFFResult { get; set; } = "--"; //SW1全部OFF的檢測結果（PASS、FAILED、--）
        public string sw1OFFData { get; set; } = string.Empty; //SW1全部OFF的檢測資料
        public string sw2ONResult { get; set; } = "--"; //SW2全部ON的檢測結果（PASS、FAILED、--）
        public string sw2ONData { get; set; } = string.Empty; //SW2全部ON的檢測資料
        public string sw2OFFResult { get; set; } = "--"; //SW2全部OFF的檢測結果（PASS、FAILED、--）
        public string sw2OFFData { get; set; } = string.Empty; //SW2全部OFF的檢測資料
        public string swInitialResult { get; set; } = "--"; //SW1與SW2回復出場設定檢測結果（PASS、FAILED、--）
        public string firmwareVersion { get; set; } = string.Empty; //韌體版本
        public string p4PortResult { get; set; } = "--"; //485通訊(P4)檢測結果（PASS、FAILED、--）
        public string p5PortResult { get; set; } = "--"; //485通訊(P5)檢測結果（PASS、FAILED、--）
        public string p9PortResult { get; set; } = "--"; //Control-A(P9)檢測結果（PASS、FAILED、--）
        public string p10PortResult { get; set; } = "--"; //Control-B(P10)檢測結果（PASS、FAILED、--）
        public string p3PortResult { get; set; } = "--"; //Tx/Rx(P3)檢測結果（PASS、FAILED、--）
        public string ledResult { get; set; } = "--"; //LED檢測結果（PASS、FAILED、--）
        public string finalResult { get; set; } = string.Empty; //整體檢測結果（PASS、FAILED，只要上述測試有一個FAILED，整體檢測結果即為FAILED）
    }

    public class DetectionResponse //測試結果傳送的API回傳資料
    {
        public bool success { get; set; } = false; //bool；回傳API執行成功與否(true or false)
        public string message { get; set; } = string.Empty; //字串；執行錯誤時的訊息內容
    }

    public class InspectMemberSelectSend //要讀取檢測人員資料要傳給API的Json物件
    {
        public string crud { get; set; } = "read"; //疑似資料讀取 (read)
        public InspectMemberSelectSendData data { get; set; } = new InspectMemberSelectSendData(); //要讀取檢測人員資料所需要傳送的員工編號
    }

    public class InspectMemberSelectSendData //要讀取檢測人員資料所需要傳送的員工編號
    {
        public string EmpNum { get; set; } = string.Empty; //員工編號
    }

    public class InspectMemberResponse //讀取檢測人員的API回傳資料
    {
        public bool success { get; set; } = false; //bool；回傳API執行成功與否(true or false)
        public string message { get; set; } = string.Empty; //字串；執行錯誤時的訊息內容
        public InspectMemberData data { get; set; } = new InspectMemberData(); //測試人員資料
    }

    public class InspectMemberData //檢測人員資料
    {
        public string EmpNum { get; set; } = string.Empty; //員工編號
        public string Name { get; set; } = string.Empty; //人員名稱
        public string Telphone { get; set; } = string.Empty; //電話
        public string Remark { get; set; } = string.Empty; //備註
    }
}
