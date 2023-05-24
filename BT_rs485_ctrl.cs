using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentModbus;
using System.IO;
using System.IO.Ports;
using System.Threading;

/*2023/05/05 彬騰案 RS485 BOX 場內測試程式
 * 1. 要安裝FluentModbus
 * 2. NET FRAMEWORK 要用4.7.2以上
 */

namespace Bintronic_rs485BOX_inspect
{
    class BT_rs485_ctrl
    {
        public struct COM_PORT_INFO_STRUCT
        {
            public string device_id; //設備ID字串
            public string com_port; //com port 名稱
        };

        public static COM_PORT_INFO_STRUCT[] g_com_port_info;

        public struct METER_INFO_STRUCT
        {
            public double voltage; //電壓
            public double current; //電流
            public double temperature; //溫度
            public DateTime data_time; //資料時間
        };
        public static METER_INFO_STRUCT g_meter_data;

        /*init 初始化
         */
        public static void init()
        {
            g_com_port_info = new BT_rs485_ctrl.COM_PORT_INFO_STRUCT[3]; //3個USB DONGLE，P4、P5、電錶
            g_meter_data.voltage = 0;
            g_meter_data.current = 0;
            g_meter_data.temperature = 0;
            g_meter_data.data_time = new DateTime(1970, 1, 1);

        }

        /*query_sw_1 讀SW1的6支腳狀態
         * 回傳值: true(正常)、false(錯誤)
         * output: ack_value: 回傳的資料內容( byte array[6])
         * output: ErrorInfo: 若return false時，這裡紀錄錯誤內容
         */
        public static bool query_sw_1(out byte []ack_value, out string ErrorInfo)
        {
            byte ack = 0;
            ack_value = new byte[6];
            if (!RS485_BOX_READ_COILS(g_com_port_info[0].com_port, 64, 0x0020, 6, out ack, out ErrorInfo))
            {
                return false;
            }
            else
            {
                for (int i = 0; i < 6; i++)
                    ack_value[i] = (byte)((ack >> i) & 0x01);
                return true;
            }
        }

        /*query_sw_2 讀SW2的2支腳狀態
         * 回傳值: true(正常)、false(錯誤)
         * output: ack_value: 回傳的資料內容(byte array[2])
         * output: ErrorInfo: 若return false時，這裡紀錄錯誤內容
         */
        public static bool query_sw_2(out byte [] ack_value, out string ErrorInfo)
        {
            byte ack = 0;
            ack_value = new byte[2];
            if (!RS485_BOX_READ_COILS(g_com_port_info[0].com_port, 64, 0x0026, 2, out ack, out ErrorInfo))
            {
                return false;
            }
            else
            {
                for (int i = 0; i < 2; i++)
                    ack_value[i] = (byte)((ack >> i) & 0x01);
                return true;
            }
        }

        /*query_all_sw 讀SW1的6支腳狀態 & 讀SW2的2支腳狀態
         * 回傳值: true(正常)、false(錯誤)
         * output: ack_value_1: 回傳SW1的資料內容(byte array[6])
         * output: ack_value_2: 回傳SW2的資料內容(byte array[2])
         * output: ErrorInfo: 若return false時，這裡紀錄錯誤內容
         */
        public static bool query_all_sw(out byte []ack_value_1, out byte []ack_value_2, out string ErrorInfo)
        {
            byte ack_1, ack_2;
            bool flag;
            string info;

            ack_value_1 = new byte[6];
            ack_value_2 = new byte[2];

            flag = RS485_BOX_READ_COILS(g_com_port_info[0].com_port, 64, 0x0020, 6, out ack_1, out info);
            ErrorInfo = info + Environment.NewLine;
            Thread.Sleep(10);
            flag &= RS485_BOX_READ_COILS(g_com_port_info[0].com_port, 64, 0x0026, 2, out ack_2, out info);
            ErrorInfo += info + Environment.NewLine;
            if (flag)
            {
                for (int i = 0; i < 6; i++)
                    ack_value_1[i] = (byte)((ack_1 >> i) & 0x01);
                for (int i = 0; i < 2; i++)
                    ack_value_2[i] = (byte)((ack_2 >> i) & 0x01);
            }
            return flag;
        }

        /*read_box_version 讀控制盒P4
         * 回傳值: true(正常)、false(錯誤)
         * output: ack_value: 回傳控制盒版本(short)
         * output: ErrorInfo: 若return false時，這裡紀錄錯誤內容
         */
        public static bool read_box_P4(out short ack_value, out string ErrorInfo)
        {
            ack_value = 0;
            ErrorInfo = "";
            return RS485_BOX_READ(g_com_port_info[0].com_port, 1, 0x4258, out ack_value, out ErrorInfo);
        }

        /*read_box_version 讀控制盒P5
         * 回傳值: true(正常)、false(錯誤)
         * output: ack_value: 回傳控制盒版本(short)
         * output: ErrorInfo: 若return false時，這裡紀錄錯誤內容
         */
        public static bool read_box_P5(out short ack_value, out string ErrorInfo)
        {
            ack_value = 0;
            ErrorInfo = "";
            return RS485_BOX_READ(g_com_port_info[1].com_port, 1, 0x4258, out ack_value, out ErrorInfo);
        }

        /*check_P9 檢查P9，上、下、停三支解的開關狀態
         * input: port_idx，通訊埠序號，0: 用P4通訊，1: 用P5通訊
         * 回傳值: true(正常)、false(錯誤)
         * output: ack_value: 回傳控制盒版本(short)
         * output: ErrorInfo: 若return false時，這裡紀錄錯誤內容
         */
        public static bool check_P9(int port_idx, int delay, out string ErrorInfo)
        {
            byte ack_value;
            ErrorInfo = "";
            //int delay = 200;
            if (!RS485_BOX_WRITE(g_com_port_info[port_idx].com_port, 1, 0x4257, 0, out ErrorInfo))//先全關
            {
                return false;
            }
            Thread.Sleep(delay);
            if (!RS485_BOX_WRITE(g_com_port_info[port_idx].com_port, 1, 0x4257, 1, out ErrorInfo))//1. A組上拉
            {
                return false;
            }
            Thread.Sleep(delay);
            if (!RS485_BOX_READ_COILS(g_com_port_info[0].com_port, 64, 0x0028, 1, out ack_value, out ErrorInfo))//1. 檢查A組上拉回饋
            {
                return false;
            }
            if (ack_value!=1)
            {
                ErrorInfo = "P9 UP Pin Error";
                return false;
            }

            Thread.Sleep(delay);
            if (!RS485_BOX_WRITE(g_com_port_info[port_idx].com_port, 1, 0x4257, 2, out ErrorInfo))//2. A組停止
            {
                return false;
            }
            Thread.Sleep(delay);
            if (!RS485_BOX_READ_COILS(g_com_port_info[0].com_port, 64, 0x0029, 1, out ack_value, out ErrorInfo))//2. 檢查A組停止回饋
            {
                return false;
            }
            if (ack_value != 1)
            {
                ErrorInfo = "P9 STOP Pin Error";
                return false;
            }

            Thread.Sleep(delay);
            if (!RS485_BOX_WRITE(g_com_port_info[port_idx].com_port, 1, 0x4257, 4, out ErrorInfo))//3. A組下拉
            {
                return false;
            }
            Thread.Sleep(delay);
            if (!RS485_BOX_READ_COILS(g_com_port_info[0].com_port, 64, 0x002A, 1, out ack_value, out ErrorInfo))//2. 檢查A組下拉回饋
            {
                return false;
            }
            if (ack_value != 1)
            {
                ErrorInfo = "P9 DOWN Pin Error";
                return false;
            }
            Thread.Sleep(delay);
            if (!RS485_BOX_WRITE(g_com_port_info[port_idx].com_port, 1, 0x4257, 0, out ErrorInfo))//先全關
            {
                return false;
            }

            return true;
        }

        /*check_P10 檢查P10，上、下、停三支解的開關狀態
         * input: port_idx，通訊埠序號，0: 用P4通訊，1: 用P5通訊
         * 回傳值: true(正常)、false(錯誤)
         * output: ack_value: 回傳控制盒版本(short)
         * output: ErrorInfo: 若return false時，這裡紀錄錯誤內容
         */
        public static bool check_P10(int port_idx, int delay, out string ErrorInfo)
        {
            byte ack_value;
            ErrorInfo = "";
            //int delay = 200;
            if (!RS485_BOX_WRITE(g_com_port_info[port_idx].com_port, 1, 0x4257, 0, out ErrorInfo))//先全關
            {
                return false;
            }
            Thread.Sleep(delay);
            if (!RS485_BOX_WRITE(g_com_port_info[port_idx].com_port, 1, 0x4257, 16, out ErrorInfo))//1. B組上拉
            {
                return false;
            }
            Thread.Sleep(delay);
            if (!RS485_BOX_READ_COILS(g_com_port_info[0].com_port, 64, 0x002B, 1, out ack_value, out ErrorInfo))//1. 檢查B組上拉回饋
            {
                return false;
            }
            if (ack_value != 1)
            {
                ErrorInfo = "P10 UP Pin Error";
                return false;
            }

            Thread.Sleep(delay);
            if (!RS485_BOX_WRITE(g_com_port_info[port_idx].com_port, 1, 0x4257, 32, out ErrorInfo))//2. B組停止
            {
                return false;
            }
            Thread.Sleep(delay);
            if (!RS485_BOX_READ_COILS(g_com_port_info[0].com_port, 64, 0x002C, 1, out ack_value, out ErrorInfo))//2. 檢查B組停止回饋
            {
                return false;
            }
            if (ack_value != 1)
            {
                ErrorInfo = "P10 STOP Pin Error";
                return false;
            }

            Thread.Sleep(delay);
            if (!RS485_BOX_WRITE(g_com_port_info[port_idx].com_port, 1, 0x4257, 64, out ErrorInfo))//3. B組下拉
            {
                return false;
            }
            Thread.Sleep(delay);
            if (!RS485_BOX_READ_COILS(g_com_port_info[0].com_port, 64, 0x002D, 1, out ack_value, out ErrorInfo))//2. 檢查B組下拉回饋
            {
                return false;
            }
            if (ack_value != 1)
            {
                ErrorInfo = "P10 DOWN Pin Error";
                return false;
            }
            Thread.Sleep(delay);
            if (!RS485_BOX_WRITE(g_com_port_info[port_idx].com_port, 1, 0x4257, 0, out ErrorInfo))//先全關
            {
                return false;
            }

            return true;
        }

        /*check_P3 讀MOTOR A的狀態
         * input: port_idx，通訊埠序號，0: 用P4通訊，1: 用P5通訊
         * 回傳值: true(正常)、false(錯誤)
         * output: ErrorInfo: 若return false時，這裡紀錄錯誤內容
         */
        public static bool check_P3(int port_idx,out string ErrorInfo)
        {
            short ack_value = 0;
            int temp_value;
            if (!RS485_BOX_READ(g_com_port_info[port_idx].com_port, 1, 0x4252, out ack_value, out ErrorInfo))
            {
                return false;
            }
            temp_value = (ack_value >> 8) & 0xFF;
            if (temp_value == 0xEE)//模組未連線
            {
                ErrorInfo = "ERROR UNCONNECTED!!";
                return false;
            }
            return true;
        }

        /*LED_ctrl 控制LED 開關
         * input: port_idx，通訊埠序號，0: 用P4通訊，1: 用P5通訊
         * input: mode，0: 關，1: 開
         * 回傳值: true(正常)、false(錯誤)
         * output: ErrorInfo: 若return false時，這裡紀錄錯誤內容
         */
        public static bool LED_ctrl(int port_idx, short mode, out string ErrorInfo)
        {
            if (!RS485_BOX_WRITE(g_com_port_info[port_idx].com_port, 1, 0x4259, mode, out ErrorInfo))
            {
                return false;
            }
            return true;
        }

        /* read_meter 讀電流錶的數值
         * input: port_idx，通訊埠序號，0: 用P4通訊，1: 用P5通訊
         * 回傳值: true(正常)、false(錯誤)
         * output: ErrorInfo: 若return false時，這裡紀錄錯誤內容
         */
        public static bool read_meter(out string ErrorInfo)
        {
            ErrorInfo = "";
            
            var client = new ModbusRtuClient()
            {
                BaudRate = 9600,
                Parity = Parity.None,
                StopBits = StopBits.One,
                ReadTimeout = 1000
            };

            try
            {
                client.Connect(g_com_port_info[2].com_port, ModbusEndianness.BigEndian);

                var shortData = client.ReadHoldingRegisters<short>(1, 0, 3);

                g_meter_data.voltage = ((double)shortData[0]) / 100;
                g_meter_data.current = ((double)shortData[1]) / 1000;
                g_meter_data.temperature = shortData[2];
                g_meter_data.data_time = DateTime.Now;
                client.Close();
                return true;
            }
            catch (Exception ex)
            {
                ErrorInfo = "錯誤!!" + Environment.NewLine + "ERROR!! " + Environment.NewLine + ex.ToString();
                g_meter_data.voltage = 0;
                g_meter_data.current = 0;
                g_meter_data.temperature = 0;
                g_meter_data.data_time = new DateTime(1970,1,1);
                if (client.IsConnected)
                    client.Close();
                return false;
            }
        }

        //RS485 通訊控制 =================================================================================================
        /*RS485_BOX_READ 讀控制盒暫存器數值
         * 回傳值: true(正常)、false(錯誤)
         * input: port_name: 通訊埠名稱，EX: com1、com2...
         * input: ID: 控制盒ID
         * input: address: 要撈的資料位址
         * output: ack_value: 回傳的資料內容(short)
         * output: info: 若return false時，這裡紀錄錯誤內容
         */
        private static bool RS485_BOX_READ(string port_name, int ID, short address, out short ack_value, out string info)
        {
            ack_value = 0;
            info = "";

            var client = new ModbusRtuClient()
            {
                BaudRate = 9600,
                Parity = Parity.None,
                StopBits = StopBits.One,
                ReadTimeout = 400
            };

            try
            {
                client.Connect(port_name, ModbusEndianness.BigEndian);

                var shortData = client.ReadHoldingRegisters<short>(ID, address, 1);

                ack_value = shortData[0];
                client.Close();
                return true;
            }
            catch (Exception ex)
            {
                info = "錯誤!!" + Environment.NewLine + "ERROR!! " + Environment.NewLine + ex.ToString();
                if (client.IsConnected)
                    client.Close();
                return false;
            }
        }

        /*RS485_BOX_READ_COILS 讀控制盒線圈暫存器數值
         * 回傳值: true(正常)、false(錯誤)
         * input: port_name: 通訊埠名稱，EX: com1、com2...
         * input: ID: 控制盒ID
         * input: address: 要撈的資料位址
         * input: length: 資料長度(不得大於8)
         * output: ack_value: 回傳的資料內容(short)
         * output: info: 若return false時，這裡紀錄錯誤內容
         */
        private static bool RS485_BOX_READ_COILS(string port_name, int ID, short address, int length, out byte ack_value, out string info)
        {
            ack_value = 0;
            info = "";

            var client = new ModbusRtuClient()
            {
                BaudRate = 9600,
                Parity = Parity.None,
                StopBits = StopBits.One
            };
            try
            {
                client.Connect(port_name, ModbusEndianness.BigEndian);

                var shortData = client.ReadCoils(ID, address, length);

                ack_value = shortData[0];
                client.Close();
                return true;
            }
            catch (Exception ex)
            {
                info = "錯誤!!" + Environment.NewLine + "ERROR!! " + Environment.NewLine + ex.ToString();
                if (client.IsConnected)
                    client.Close();
                return false;
            }
        }

        /*RS485_BOX_WRITE 寫控制盒暫存器
         * 回傳值: true(正常)、false(錯誤)
         * input: port_name: 通訊埠名稱，EX: com1、com2...
         * input: ID: 控制盒ID
         * input: address: 要寫的資料位址
         * input: value: 資料內容
         * output: info: 若return false時，這裡紀錄錯誤內容
         */
        private static bool RS485_BOX_WRITE(string port_name, int ID, short address, short value, out string info)
        {
            info = "";

            var client = new ModbusRtuClient()
            {
                BaudRate = 9600,
                Parity = Parity.None,
                StopBits = StopBits.One
            };
            try
            {
                client.Connect(port_name, ModbusEndianness.BigEndian);

                //var shortData = client.ReadHoldingRegisters<short>(0x01, address, 1);
                client.WriteSingleRegister(ID, address, value);
                client.Close();
                return true;
            }
            catch (Exception ex)
            {
                info = "錯誤!!" + Environment.NewLine + "ERROR!! " + Environment.NewLine + ex.ToString();
                if (client.IsConnected)
                    client.Close();
                return false;
            }
        }
    }
}
