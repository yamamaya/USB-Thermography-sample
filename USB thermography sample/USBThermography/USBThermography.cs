using System;
using OaktreeLab.USBDevice;

namespace OaktreeLab.IO {
    class USBThermography : IDisposable {
        public enum RefreshRate {
            /// <summary>リフレッシュレート 0.5Hz</summary>
            _0_5Hz = 0,
            /// <summary>リフレッシュレート 1Hz</summary>
            _1Hz = 1,
            /// <summary>リフレッシュレート 2Hz</summary>
            _2Hz = 2,
            /// <summary>リフレッシュレート 4Hz</summary>
            _4Hz = 4,
            /// <summary>リフレッシュレート 8Hz</summary>
            _8Hz = 8,
            /// <summary>リフレッシュレート 16Hz</summary>
            _16Hz = 16,
            /// <summary>リフレッシュレート 32Hz</summary>
            _32Hz = 32
        }

        /// <summary>
        /// IRアレイセンサーの変換処理のステータス
        /// </summary>
        public enum PixelStatus {
            /// <summary>変換成功</summary>
            Normal,
            /// <summary>アンダーフロー(-50℃以下)</summary>
            Underflow,
            /// <summary>オーバーフロー(+300/900℃以上)</summary>
            Overflow,
            /// <summary>変換処理でエラー発生(変換の結果が0ケルビン以下)</summary>
            Error
        };

        /// <summary>
        /// IRアレイセンサーの読み取り値を温度に変換した結果
        /// </summary>
        public struct Pixel {
            /// <summary>温度</summary>
            public double Temperature;
            /// <summary>変換ステータス</summary>
            public PixelStatus Status;
        }

        /// <summary>
        /// IRアレイセンサーの幅（ピクセル数）
        /// </summary>
        public int Width {
            get {
                return 16;
            }
        }

        /// <summary>
        /// IRアレイセンサーの高さ（ピクセル数）
        /// </summary>
        public int Height {
            get {
                return 4;
            }
        }

        /// <summary>
        /// ファームウェアのバージョン
        /// </summary>
        public Version FirmVersion {
            get;
            private set;
        }

        /// <summary>
        /// 測定可能な最高温度
        /// </summary>
        public double TempMax {
            get;
            private set;
        }

        /// <summary>
        /// 測定可能な最低温度
        /// </summary>
        public double TempMin {
            get;
            private set;
        }

        private HIDSimple device = null;
        private byte[] eeprom;
        private const int VID = 0x04d8;
        private const int PID = 0xfa87;

        /// <summary>
        /// インスタンスを作成しUSBサーモグラフィに接続する。
        /// </summary>
        /// <exception cref="SystemException">USBサーモグラフィに接続できない場合</exception>
        public USBThermography() {
            string[] paths = GetDevicePathList();
            if ( paths.Length == 0 ) {
                throw new SystemException( "device not found" );
            }
            Open( paths[ 0 ] );
        }

        /// <summary>
        /// インスタンスを作成しUSBサーモグラフィに接続する。
        /// </summary>
        /// <param name="path">デバイスのパス</param>
        /// <exception cref="SystemException">USBサーモグラフィに接続できない場合</exception>
        public USBThermography( string path ) {
            Open( path );
        }

        /// <summary>
        /// 接続されているUSBサーモグラフィのパスの一覧を取得する。
        /// </summary>
        /// <returns></returns>
        public static string[] GetDevicePathList() {
            return HIDSimple.EnumerateDevices( VID, PID );
        }

        private void Open( string path ) {
            device = new HIDSimple();
            if ( !device.Open( path ) ) {
                throw new SystemException( "device not found" );
            }
            device.Send( 0xff );
            byte[] res = device.Receive();
            string s = "";
            foreach ( byte b in res ) {
                if ( b == 0 ) {
                    break;
                }
                s += char.ConvertFromUtf32( b );
            }
            if ( s == "" ) {
                FirmVersion = new Version( "1.0.0" );
            } else {
                FirmVersion = new Version( s );
            }
            if ( FirmVersion >= new Version( "1.2.0" ) ) {
                TempMax = 900;
                TempMin = -50;
            } else {
                TempMax = 300;
                TempMin = -50;
            }

            eeprom = new byte[ 256 ];
            for ( int page = 0 ; page < 4 ; page++ ) {
                // read eepromコマンド（0～3ページ）
                device.Send( 0x80, (byte)page );
                res = device.Receive();
                Array.Copy( res, 0, eeprom, page * 64, 64 );
            }
        }

        public void Dispose() {
            if ( device != null ) {
                device.Dispose();
                device = null;
            }
        }

        /// <summary>
        /// 測定対象の放射率を設定する。
        /// </summary>
        /// <param name="emissivity">放射率 0＜ε≦1</param>
        /// <exception cref="InvalidOperationException">デバイスとの通信エラー</exception>
        public void SetEmissivity( double emissivity ) {
            if ( emissivity <= 0 || emissivity > 1 ) {
                throw new ArgumentOutOfRangeException();
            }
            UInt16 e = (UInt16)( emissivity * 1000 + 0.5 );
            device.Send( 0x04, (byte)( e & 0xff ), (byte)( e >> 8 ) );
            device.Receive();
        }

        /// <summary>
        /// リフレッシュレート（測定間隔）を設定する。
        /// 速いとノイズが増加する。
        /// </summary>
        /// <param name="refresh">リフレッシュレート(Hz)</param>
        /// <exception cref="InvalidOperationException">デバイスとの通信エラー</exception>
        public void SetRefreshRate( RefreshRate refresh ) {
            device.Send( 0x03, (byte)refresh );
            device.Receive();
        }

        /// <summary>
        /// 測定結果を取得する。
        /// </summary>
        /// <returns>取得した温度データ[横,縦]</returns>
        /// <exception cref="InvalidOperationException">デバイスとの通信エラー</exception>
        public Pixel[,] Read() {
            Pixel[,] Data = new Pixel[Width, Height];

            // snapshotコマンド
            device.Send( 0x00 );
            device.Receive();

            // readコマンド（0ページ）
            device.Send( 0x01, 0x00 );
            byte[] res1 = device.Receive();

            // readコマンド（1ページ）
            device.Send( 0x01, 0x01 );
            byte[] res2 = device.Receive();

            // 結果を変換
            StoreHalfFrame( res1, 0, Data );
            StoreHalfFrame( res2, 1, Data );

            return Data;
        }

        private static UInt16 unsigned16( byte[] data, int offset ) {
            return (UInt16)( data[offset] | ( (UInt16)data[offset + 1] << 8 ) );
        }

        private static Int16 signed16( byte[] data, int offset ) {
            int w = data[offset] | ( (UInt16)data[offset + 1] << 8 );
            if ( w >= 32768 ) {
                w = w - 65536;
            }
            return (Int16)w;
        }

        private void StoreHalfFrame( byte[] rawdata, int page, Pixel[,] Data ) {
            int i = 0;
            for ( int yy = 0 ; yy < 2 ; yy++ ) {
                int y = yy + page * 2;
                for ( int x = 0 ; x < 16 ; x++ ) {
                    int w = signed16( rawdata, i );
                    if ( w == -9991 || w == -9992 ) {
                        Data[x, y].Temperature = -50;
                        Data[x, y].Status = PixelStatus.Underflow;
                    } else if ( w == -9990 ) {
                        Data[x, y].Temperature = 300;
                        Data[x, y].Status = PixelStatus.Overflow;
                    } else {
                        Data[x, y].Temperature = (double)w / 10;
                        Data[x, y].Status = PixelStatus.Normal;
                    }
                    i += 2;
                }
            }
        }

        /// <summary>
        /// IRアレイセンサーの生データ
        /// </summary>
        public class RawData {
            /// <summary>環境温度センサーの読み取り値</summary>
            public UInt16 PTAT;
            /// <summary>補償ピクセルの読み取り値</summary>
            public Int16 Vcp;
            /// <summary>IRアレイの読み取り値</summary>
            public Int16[,] Vir;
        }

        /// <summary>
        /// 前回Read()を実行した時のIRアレイセンサーの生データを読み取る。
        /// </summary>
        /// <returns>生データ</returns>
        /// <exception cref="InvalidOperationException">デバイスとの通信エラー</exception>
        public RawData ReadRawData() {
            RawData data = new RawData();
            data.Vir = new Int16[16, 4];

            // read parametersコマンド
            device.Send( 0x82 );
            byte[] res = device.Receive();
            data.PTAT = unsigned16( res, 0 );
            data.Vcp = signed16( res, 2 );

            // read rawdataコマンド（0ページ）
            device.Send( 0x81, 0x00 );
            res = device.Receive();
            int i = 0;
            for ( int y = 0 ; y < 2 ; y++ ) {
                for ( int x = 0 ; x < 16 ; x++ ) {
                    data.Vir[x, y] = signed16( res, i );
                    i += 2;
                }
            }

            // read rawdataコマンド（1ページ）
            device.Send( 0x81, 0x01 );
            res = device.Receive();
            i = 0;
            for ( int y = 2 ; y < 4 ; y++ ) {
                for ( int x = 0 ; x < 16 ; x++ ) {
                    data.Vir[x, y] = signed16( res, i );
                    i += 2;
                }
            }

            return data;
        }

        /// <summary>
        /// IRアレイセンサーのEEPROMの内容を読み取る。
        /// </summary>
        /// <returns>EEPROMの内容</returns>
        public byte[] ReadEEPROM() {
            return eeprom;
        }

        /// <summary>
        /// デバイスの識別子を取得する。
        /// 識別子は32文字の文字列で、デバイス固有と考えて問題ない。
        /// </summary>
        public string DeviceID {
            get {
                byte[] hash;
                using ( System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create() ) {
                    hash = md5.ComputeHash( eeprom );
                }
                string result = "";
                foreach ( byte b in hash ) {
                    result += b.ToString( "X2" );
                }
                return result;
            }
        }
    }
}
