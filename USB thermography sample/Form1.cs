using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using OaktreeLab.IO;

namespace USB_thermography_sample {
    public partial class Form1 : Form {
        // USBサーモグラフィ
        private USBThermography device = null;

        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load( object sender, EventArgs e ) {
            try {
                // USBサーモグラフィに接続する（接続できなかった場合は例外が発生）
                device = new USBThermography();
                // リフレッシュレートを1Hzに設定
                device.SetRefreshRate( USBThermography.RefreshRate._1Hz );
                // 放射率を0.95に設定
                device.SetEmissivity( 0.95 );
            } catch {
                MessageBox.Show( "ERROR\nDevice not found" );
                this.Close();
                return;
            }

            // 結果表示用のDataGridViewを初期化
            InitializeDataGridView( dataGridView1, device.Width, device.Height );
            InitializeDataGridView( dataGridView2, device.Width, device.Height );
            InitializeDataGridView( dataGridView3, 16, 16 );
        }

        // DataGridViewを指定された行数・桁数で初期化する
        private static void InitializeDataGridView( DataGridView ctrl, int cols, int rows ) {
            ctrl.ColumnCount = cols;
            for ( int y = 0 ; y < rows ; y++ ) {
                DataGridViewRow row = new DataGridViewRow();
                for ( int x = 0 ; x < cols ; x++ ) {
                    row.Cells.Add( new DataGridViewTextBoxCell() );
                }
                ctrl.Rows.Add( row );
            }
            ctrl[0, 0].Selected = false;
        }

        private void Form1_FormClosed( object sender, FormClosedEventArgs e ) {
            if ( device != null ) {
                // 不要になったら必ずDisposeする
                device.Dispose();
                device = null;
            }
        }

        private void buttonRead_Click( object sender, EventArgs e ) {
            // 測定結果を取得
            USBThermography.Pixel[,] Data = device.Read();

            double sum = 0;
            int count = 0;
            for ( int y = 0 ; y < device.Height ; y++ ) {
                for ( int x = 0 ; x < device.Width ; x++ ) {
                    // 測定結果の1ピクセル
                    USBThermography.Pixel pixel = Data[x, y];
                    string text = "";
                    if ( pixel.Status == USBThermography.PixelStatus.Normal ) {
                        // 変換成功なら温度を取り出す
                        text = pixel.Temperature.ToString();
                        sum += pixel.Temperature;
                        count++;
                    } else if ( pixel.Status == USBThermography.PixelStatus.Error ) {
                        // 変換エラー
                        text = "ERROR";
                    } else if ( pixel.Status == USBThermography.PixelStatus.Underflow ) {
                        // アンダーフロー
                        text = "UNDER";
                    } else if ( pixel.Status == USBThermography.PixelStatus.Overflow ) {
                        // オーバーフロー
                        text = "OVER";
                    }
                    // 測定結果をグリッドに表示
                    dataGridView1[x, y].Value = text;
                }
            }
            // 平均値を表示
            textBoxAverage.Text = ( sum / count ).ToString( "f1" );

        }

        private void buttonReadEEPROM_Click( object sender, EventArgs e ) {
            // EEPROMの内容を取得
            byte[] eeprom = device.ReadEEPROM();
            // グリッドに表示
            for ( int i = 0 ; i < 256 ; i++ ) {
                dataGridView3[i % 16, i / 16].Value = eeprom[i].ToString( "X2" );
            }
        }

        private void buttonReadRawData_Click( object sender, EventArgs e ) {
            // 生データを取得（前回Readを実行した時のもの）
            USBThermography.RawData RawData = device.ReadRawData();
            // グリッドに表示
            for ( int y = 0 ; y < device.Height ; y++ ) {
                for ( int x = 0 ; x < device.Width ; x++ ) {
                    dataGridView2[x, y].Value = RawData.Vir[x, y].ToString( "X4" );
                }
            }
            textBoxPTAT.Text = RawData.PTAT.ToString( "X4" );
            textBoxVcp.Text = RawData.Vcp.ToString( "X4" );
        }
    }
}
