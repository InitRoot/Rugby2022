using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RC4Editor
{
    public partial class frmGlobalEditor : Form
    {
        public frmGlobalEditor()
        {
            InitializeComponent();
            
        }

        public bool updateDone = false;
        /* #####################################################################################################
         * #####################################################################################################
         * Various byte manipulations as required for the app.
         * 
         * 
         * #####################################################################################################
         * #####################################################################################################
         */

        public string ConvertStringToHex(string asciiString, int bytesize)
        {
            string hex = "";


            foreach (char c in asciiString)
            {
                int tmp = c;
                hex += String.Format("{0:x2}", (uint)System.Convert.ToUInt32(tmp.ToString()));
            }
            int wordLen = hex.Length;

            for (int i = 0; i < (bytesize - wordLen); i++)
            {
                hex += "0";
            }

            return hex;
        }

        public int GetLittleEndianIntegerFromByteArray(byte[] data)
        {
            int length = data.Length;
            int result = 0;

            for (int i = length - 1; i >= 0; i--)
            {
                result |= data[i] << i * 8;
            }
            return result;
        }

        public string ToHexString(byte[] hex)
        {
            if (hex == null) return null;
            if (hex.Length == 0) return string.Empty;

            var s = new StringBuilder();
            foreach (byte b in hex)
            {
                s.Append(b.ToString("x2"));
            }
            return s.ToString();
        }

        private string HexString2Ascii(string hexString)
        {
            int NumberChars = hexString.Length / 2;
            byte[] dbytes = new byte[NumberChars];
            using (var sr = new StringReader(hexString))
            {
                for (int i = 0; i < NumberChars; i++)
                    dbytes[i] =
                      Convert.ToByte(new string(new char[2] { (char)sr.Read(), (char)sr.Read() }), 16);
            }
            string ASCIIresult = System.Text.Encoding.ASCII.GetString(dbytes);

            string utf8result = System.Text.Encoding.UTF8.GetString(dbytes);
            return utf8result;

            //String convertedutf = "";
            //  StringBuilder sb = new StringBuilder();
            //  for (int i = 0; i <= hexString.Length - 2; i += 2)
            //   {
            //       sb.Append(Convert.ToString(Convert.ToChar(Int32.Parse(hexString.Substring(i, 2), System.Globalization.NumberStyles.HexNumber))));
            //    }

            //     convertedutf = sb.ToString();
            //     Encoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(convertedutf));
            //  return convertedutf;
        }

        public byte[] StringToByteArray(String hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }

        public string decimalToHexLittleEndian(int _iValue, int _iBytes)
        {
            string sBigEndian = String.Format("{0:x" + (2 * _iBytes).ToString() + "}", _iValue);
            string sLittleEndian = "";

            for (int i = _iBytes - 1; i >= 0; i--)
            {
                sLittleEndian += sBigEndian.Substring(i * 2, 2);
            }

            return sLittleEndian;
        }

        private int Search(byte[] src, byte[] pattern)
        {
            int c = src.Length - pattern.Length + 1;
            int j;
            for (int i = 0; i < c; i++)
            {
                if (src[i] != pattern[0]) continue;
                for (j = pattern.Length - 1; j >= 1 && src[i + j] == pattern[j]; j--) ;
                if (j == 0) return i;

            }
            return -1;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (DataRow dr in tblPlayersHexTableAdapter1.GetData())
            {
                //We need to read each players current stats then calculate new ones based on required changes

                int fitness = 0;
                int speed = 0;
                int accl = 0;
                int agil = 0;
                int star = 0;
                int aggr = 0;
                int btckl = 0;
                int tackle = 0;
                int passing = 0;
                int offloading = 0;
                int playkick = 0;
                int goalkick = 0;
                int catching = 0;
                int strenght = 0;
                int mental = 0;
                int jumping = 0;
                int discpl = 0;

                //Initialise bytes for each variable
                byte[] source = StringToByteArray(dr.ItemArray[1].ToString());
                byte[] btFIT = new byte[2];
                byte[] btSPD = new byte[2];
                byte[] btACC = new byte[2];
                byte[] btAGL = new byte[2];
                byte[] btStar = new byte[2];
                byte[] btAgrr = new byte[2];
                byte[] btbtckl = new byte[2];
                byte[] btTckle = new byte[2];
                byte[] btpass = new byte[2];
                byte[] btoffload = new byte[2];
                byte[] btplaykick = new byte[2];
                byte[] btgoalkick = new byte[2];
                byte[] btcatch = new byte[2];
                byte[] btstrenght = new byte[2];
                byte[] btmental = new byte[2];
                byte[] btjump = new byte[2];
                byte[] btDiscpl = new byte[2];


                Buffer.BlockCopy(source, 108, btFIT, 0, 2);
                Buffer.BlockCopy(source, 112, btSPD, 0, 2);
                Buffer.BlockCopy(source, 114, btACC, 0, 2);
                Buffer.BlockCopy(source, 110, btAGL, 0, 2);
                Buffer.BlockCopy(source, 142, btStar, 0, 2);
                Buffer.BlockCopy(source, 116, btAgrr, 0, 2);
                Buffer.BlockCopy(source, 120, btbtckl, 0, 2);
                Buffer.BlockCopy(source, 118, btTckle, 0, 2);
                Buffer.BlockCopy(source, 122, btpass, 0, 2);
                Buffer.BlockCopy(source, 124, btoffload, 0, 2);
                Buffer.BlockCopy(source, 126, btplaykick, 0, 2);
                Buffer.BlockCopy(source, 128, btgoalkick, 0, 2);
                Buffer.BlockCopy(source, 130, btcatch, 0, 2);
                Buffer.BlockCopy(source, 132, btstrenght, 0, 2);
                Buffer.BlockCopy(source, 134, btmental, 0, 2);
                Buffer.BlockCopy(source, 136, btjump, 0, 2);
                Buffer.BlockCopy(source, 138, btDiscpl, 0, 2);

                fitness = GetLittleEndianIntegerFromByteArray(btFIT);
                speed = GetLittleEndianIntegerFromByteArray(btSPD);
                accl = GetLittleEndianIntegerFromByteArray(btACC);
                agil = GetLittleEndianIntegerFromByteArray(btAGL);
                aggr = GetLittleEndianIntegerFromByteArray(btAgrr);
                btckl = GetLittleEndianIntegerFromByteArray(btbtckl);
                tackle = GetLittleEndianIntegerFromByteArray(btTckle);
                passing = GetLittleEndianIntegerFromByteArray(btpass);
                offloading = GetLittleEndianIntegerFromByteArray(btoffload);
                playkick = GetLittleEndianIntegerFromByteArray(btplaykick);
                goalkick = GetLittleEndianIntegerFromByteArray(btgoalkick);
                catching = GetLittleEndianIntegerFromByteArray(btcatch);
                strenght = GetLittleEndianIntegerFromByteArray(btstrenght);
                mental = GetLittleEndianIntegerFromByteArray(btmental);
                jumping = GetLittleEndianIntegerFromByteArray(btjump);
                discpl = GetLittleEndianIntegerFromByteArray(btDiscpl);


                if (txtFitness.Text != null)
                {
                    if (txtFitness.Text.Contains('+'))
                    {
                        fitness = fitness + (Convert.ToInt32(txtFitness.Text.Replace('+', ' ')) * 100);
                        if (fitness > 9999) fitness = 9900;
                    }
                    if (txtFitness.Text.Contains('-'))
                    {
                        fitness = fitness + (Convert.ToInt32(txtFitness.Text.Replace('-', ' ')) * 100);
                        if (fitness < 1000) fitness = 1000;
                    }

                }
                if (txtSpeed.Text != null)
                {
                    if (txtSpeed.Text.Contains('+'))
                    {
                        speed = speed + (Convert.ToInt32(txtSpeed.Text.Replace('+', ' ')) * 100);
                        if (speed > 9999) speed = 9900;
                    }
                    if (txtSpeed.Text.Contains('-'))
                    {
                        speed = speed + (Convert.ToInt32(txtSpeed.Text.Replace('-', ' ')) * 100);
                        if (speed < 1000) speed = 1000;
                    }

                }
                if (txtAccel.Text != null)
                {
                    if (txtAccel.Text.Contains('+'))
                    {
                        accl = accl + (Convert.ToInt32(txtAccel.Text.Replace('+', ' ')) * 100);
                        if (accl > 9999) accl = 9900;
                    }
                    if (txtAccel.Text.Contains('-'))
                    {
                        accl = accl + (Convert.ToInt32(txtAccel.Text.Replace('-', ' ')) * 100);
                        if (accl < 1000) accl = 1000;
                    }
                }
                if (txtAggr.Text != null)
                {
                    if (txtAggr.Text.Contains('+'))
                    {
                        aggr = aggr + (Convert.ToInt32(txtAggr.Text.Replace('+', ' ')) * 100);
                        if (aggr > 9999) aggr = 9900;
                    }
                    if (txtAggr.Text.Contains('-'))
                    {
                        aggr = aggr + (Convert.ToInt32(txtAggr.Text.Replace('-', ' ')) * 100);
                        if (aggr < 1000) aggr = 1000;
                    }
                }
                if (txtbrckTackle.Text != null)
                {
                    if (txtbrckTackle.Text.Contains('+'))
                    {
                        btckl = btckl + (Convert.ToInt32(txtbrckTackle.Text.Replace('+', ' ')) * 100);
                        if (btckl > 9999) btckl = 9900;
                    }
                    if (txtbrckTackle.Text.Contains('-'))
                    {
                        btckl = btckl + (Convert.ToInt32(txtbrckTackle.Text.Replace('-', ' ')) * 100);
                        if (btckl < 1000) btckl = 1000;
                    }
                }
                if (txtTackle.Text != null)
                {
                    if (txtTackle.Text.Contains('+'))
                    {
                        tackle = tackle + (Convert.ToInt32(txtTackle.Text.Replace('+', ' ')) * 100);
                        if (tackle > 9999) tackle = 9900;
                    }
                    if (txtTackle.Text.Contains('-'))
                    {
                        tackle = tackle + (Convert.ToInt32(txtTackle.Text.Replace('-', ' ')) * 100);
                        if (tackle < 1000) tackle = 1000;
                    }
                }
                if (txtPass.Text != null)
                {
                    if (txtPass.Text.Contains('+'))
                    {
                        passing = passing + (Convert.ToInt32(txtPass.Text.Replace('+', ' ')) * 100);
                        if (passing > 9999) passing = 9900;
                    }
                    if (txtPass.Text.Contains('-'))
                    {
                        passing = passing + (Convert.ToInt32(txtPass.Text.Replace('-', ' ')) * 100);
                        if (passing < 1000) passing = 1000;
                    }
                }
                if (txtAgility.Text != null)
                {
                    if (txtAgility.Text.Contains('+'))
                    {
                        agil = agil + (Convert.ToInt32(txtAgility.Text.Replace('+', ' ')) * 100);
                        if (agil > 9999) agil = 9900;
                    }
                    if (txtAgility.Text.Contains('-'))
                    {
                        agil = agil + (Convert.ToInt32(txtAgility.Text.Replace('-', ' ')) * 100);
                        if (agil < 1000) agil = 1000;
                    }
                }
                if (txtOffload.Text != null)
                {
                    if (txtOffload.Text.Contains('+'))
                    {
                        offloading = offloading + (Convert.ToInt32(txtOffload.Text.Replace('+', ' ')) * 100);
                        if (offloading > 9999) offloading = 9900;
                    }
                    if (txtOffload.Text.Contains('-'))
                    {
                        offloading = offloading + (Convert.ToInt32(txtOffload.Text.Replace('-', ' ')) * 100);
                        if (offloading < 1000) offloading = 1000;
                    }
                }
                if (txtKick.Text != null)
                {
                    if (txtKick.Text.Contains('+'))
                    {
                        playkick = playkick + (Convert.ToInt32(txtKick.Text.Replace('+', ' ')) * 100);
                        if (playkick > 9999) playkick = 9900;
                    }
                    if (txtKick.Text.Contains('-'))
                    {
                        playkick = playkick + (Convert.ToInt32(txtKick.Text.Replace('-', ' ')) * 100);
                        if (playkick < 1000) playkick = 1000;
                    }
                }
                if (txtGoalKick.Text != null)
                {
                    if (txtGoalKick.Text.Contains('+'))
                    {
                        goalkick = goalkick + (Convert.ToInt32(txtGoalKick.Text.Replace('+', ' ')) * 100);
                        if (goalkick > 9999) goalkick = 9900;
                    }
                    if (txtGoalKick.Text.Contains('-'))
                    {
                        goalkick = goalkick + (Convert.ToInt32(txtGoalKick.Text.Replace('-', ' ')) * 100);
                        if (goalkick < 1000) goalkick = 1000;
                    }
                }
                if (txtCatch.Text != null)
                {
                    if (txtCatch.Text.Contains('+'))
                    {
                        catching = catching + (Convert.ToInt32(txtCatch.Text.Replace('+', ' ')) * 100);
                        if (catching > 9999) catching = 9900;
                    }
                    if (txtCatch.Text.Contains('-'))
                    {
                        catching = catching + (Convert.ToInt32(txtCatch.Text.Replace('-', ' ')) * 100);
                        if (catching < 1000) catching = 1000;
                    }
                }
                if (txtStrenght.Text != null)
                {
                    if (txtStrenght.Text.Contains('+'))
                    {
                        strenght = strenght + (Convert.ToInt32(txtStrenght.Text.Replace('+', ' ')) * 100);
                        if (strenght > 9999) strenght = 9900;
                    }
                    if (txtStrenght.Text.Contains('-'))
                    {
                        strenght = strenght + (Convert.ToInt32(txtStrenght.Text.Replace('-', ' ')) * 100);
                        if (strenght < 1000) strenght = 1000;
                    }
                }
                if (txtMental.Text != null)
                {
                    if (txtMental.Text.Contains('+'))
                    {
                        mental = mental + (Convert.ToInt32(txtMental.Text.Replace('+', ' ')) * 100);
                        if (mental > 9999) mental = 9900;
                    }
                    if (txtMental.Text.Contains('-'))
                    {
                        mental = mental + (Convert.ToInt32(txtMental.Text.Replace('-', ' ')) * 100);
                        if (mental < 1000) mental = 1000;
                    }
                }
                if (txtJump.Text != null)
                {
                    if (txtJump.Text.Contains('+'))
                    {
                        jumping = jumping + (Convert.ToInt32(txtJump.Text.Replace('+', ' ')) * 100);
                        if (jumping > 9999) jumping = 9900;
                    }
                    if (txtJump.Text.Contains('-'))
                    {
                        jumping = jumping + (Convert.ToInt32(txtJump.Text.Replace('-', ' ')) * 100);
                        if (jumping < 1000) jumping = 1000;
                    }
                }
                if (txtDiscp.Text != null)
                {
                    if (txtDiscp.Text.Contains('+'))
                    {
                        discpl = discpl + (Convert.ToInt32(txtDiscp.Text.Replace('+', ' ')) * 100);
                        if (discpl > 9999) discpl = 9900;
                    }
                    if (txtDiscp.Text.Contains('-'))
                    {
                        discpl = discpl + (Convert.ToInt32(txtDiscp.Text.Replace('-', ' ')) * 100);
                        if (discpl < 1000) discpl = 1000;
                    }
                }

                string text1 = dr.ItemArray[1].ToString(); //original

                string hexLittleEndian3 = this.decimalToHexLittleEndian((int)Convert.ToInt16(fitness), 2);
                string hexLittleEndian4 = this.decimalToHexLittleEndian((int)Convert.ToInt16(speed), 2);
                string hexLittleEndian5 = this.decimalToHexLittleEndian((int)Convert.ToInt16(accl), 2);
                string hexLittleEndian6 = this.decimalToHexLittleEndian((int)Convert.ToInt16(agil), 2);
                string hexLittleEndian7 = this.decimalToHexLittleEndian((int)Convert.ToInt16(aggr), 2);
                string hexLittleEndian8 = this.decimalToHexLittleEndian((int)Convert.ToInt16(btckl), 2);
                string hexLittleEndian9 = this.decimalToHexLittleEndian((int)Convert.ToInt16(tackle), 2);
                string hexLittleEndian10 = this.decimalToHexLittleEndian((int)Convert.ToInt16(passing), 2);
                string hexLittleEndian11 = this.decimalToHexLittleEndian((int)Convert.ToInt16(offloading), 2);
                string hexLittleEndian12 = this.decimalToHexLittleEndian((int)Convert.ToInt16(playkick), 2);
                string hexLittleEndian13 = this.decimalToHexLittleEndian((int)Convert.ToInt16(goalkick), 2);
                string hexLittleEndian14 = this.decimalToHexLittleEndian((int)Convert.ToInt16(catching), 2);
                string hexLittleEndian15 = this.decimalToHexLittleEndian((int)Convert.ToInt16(strenght), 2);
                string hexLittleEndian16 = this.decimalToHexLittleEndian((int)Convert.ToInt16(mental), 2);
                string hexLittleEndian17 = this.decimalToHexLittleEndian((int)Convert.ToInt16(jumping), 2);
                string hexLittleEndian18 = this.decimalToHexLittleEndian((int)Convert.ToInt16(discpl), 2);

                string str5 =
                    text1.Remove(216, 4).Insert(216, hexLittleEndian3) //fitness
                    .Remove(224, 4).Insert(224, hexLittleEndian4) //speed
                    .Remove(228, 4).Insert(228, hexLittleEndian5) //acceleration
                    .Remove(220, 4).Insert(220, hexLittleEndian6) //agility
                    .Remove(232, 4).Insert(232, hexLittleEndian7) //aggresion
                    .Remove(240, 4).Insert(240, hexLittleEndian8) //break tackle
                    .Remove(236, 4).Insert(236, hexLittleEndian9) //tackle
                    .Remove(244, 4).Insert(244, hexLittleEndian10) //passing
                    .Remove(248, 4).Insert(248, hexLittleEndian11) //offloading
                    .Remove(252, 4).Insert(252, hexLittleEndian12) //general kicking
                    .Remove(256, 4).Insert(256, hexLittleEndian13) //goal kicking
                    .Remove(260, 4).Insert(260, hexLittleEndian14) //catching
                    .Remove(264, 4).Insert(264, hexLittleEndian15) //strengh
                    .Remove(268, 4).Insert(268, hexLittleEndian16) //mental agility
                    .Remove(272, 4).Insert(272, hexLittleEndian17) //jumping
                    .Remove(276, 4).Insert(276, hexLittleEndian18); //discipline

                if (str5.Length != text1.Length)
                {
                    int num = (int)MessageBox.Show("Error - Hex sizes do not match.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    tblPlayersHexTableAdapter1.UpdateQueryModHex(str5, text1);
                    updateDone = true;

                }



            }
            // MessageBox.Show(selectedCellsAsText);
            //string playerHEX = DBGetPlayerHEXbyID(Convert.ToInt32(selectedCellsAsText));




        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();

        }
    }
}

