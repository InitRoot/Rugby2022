using BrightIdeasSoftware;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Generic;

namespace RC4Editor
{

    public partial class frmMain : Form
    {
        String fileName;
        int PendingChanges = 0;
        ArrayList playerIDs = new ArrayList();
        private Thread workerThread = null;
        public Dictionary<int, string> TeamsList = new Dictionary<int, string>();
        public Dictionary<string, string> CompsList = new Dictionary<string, string>();

        public frmMain()
        {
            InitializeComponent();
            loadSettings();
            lockScreen();
            loadRCNations();
            loadRCPositions();
        }

        /* #####################################################################################################
         * #####################################################################################################
         * Manipulate UI and add static UI values to fields.
         * 
         * 
         * #####################################################################################################
         * #####################################################################################################
         */

        private void lockScreen()
        {
            pnlApp.Enabled = true;
            tbpPlayers.Hide();
            tbpLineups.Hide();
            tbpTeams.Hide();
            tbpKits.Hide();
            toolStripButton2.Enabled = false;
            toolStripButton3.Enabled = false;
            toolStripButton5.Enabled = false;
            toolStripButton6.Enabled = false;
        }

        private void unlockScreen()
        {
            pnlApp.Enabled = true;
            tbpPlayers.Show();
            tbpLineups.Show();
            tbpTeams.Show();
            tbpKits.Show();
            toolStripButton2.Enabled = true;
            toolStripButton3.Enabled = true;
            toolStripButton5.Enabled = true;
            toolStripButton6.Enabled = true;
        }

        //Adds nations to our combobox
        private void loadRCNations()
        {
            cmbNations.Items.Add("New Zealand");
            cmbNations.Items.Add("Australia");
            cmbNations.Items.Add("Hong Kong");
            cmbNations.Items.Add("Japan");
            cmbNations.Items.Add("Argentina");
            cmbNations.Items.Add("Canada");
            cmbNations.Items.Add("England");
            cmbNations.Items.Add("Fiji");
            cmbNations.Items.Add("France");
            cmbNations.Items.Add("Georgia");
            cmbNations.Items.Add("Ireland");
            cmbNations.Items.Add("Italy");
            cmbNations.Items.Add("Namibia");
            cmbNations.Items.Add("Romania");
            cmbNations.Items.Add("Russia");
            cmbNations.Items.Add("Samoa");
            cmbNations.Items.Add("Scotland");
            cmbNations.Items.Add("Tonga");
            cmbNations.Items.Add("USA");
            cmbNations.Items.Add("Wales");
            cmbNations.Items.Add("Uruguay");
            cmbNations.Items.Add("South Africa");
            cmbNations.Items.Add("China");
            cmbNations.Items.Add("Cameroon");
            cmbNations.Items.Add("Cook Islands");
            cmbNations.Items.Add("Spain");
            cmbNations.Items.Add("Morocco");
            cmbNations.Items.Add("Netherlands");
            cmbNations.Items.Add("Norway");
            cmbNations.Items.Add("Portugal");
            cmbNations.Items.Add("Vanuatu");
            cmbNations.Items.Add("Zimbabwe");
            cmbNations.Items.Add("Belgium");
            cmbNations.Items.Add("Ivory Coast");
            cmbNations.Items.Add("Switzerland");
            cmbNations.Items.Add("Brazil");
            cmbNations.Items.Add("Chile");
            cmbNations.Items.Add("Kenya");
            cmbNations.Items.Add("American Samoa");
        }

        private void loadRCPositions()
        {
            cmbPositions.Items.Clear();
            cmbPositions7.Items.Clear();
            cmbSecondaryPositions.Items.Clear();

            cmbPositions.Items.Add("LooseheadProp");
            cmbPositions.Items.Add("Hooker");
            cmbPositions.Items.Add("TighHeadProp");
            cmbPositions.Items.Add("FourLock");
            cmbPositions.Items.Add("FiveLock");
            cmbPositions.Items.Add("BlindsideFlank");
            cmbPositions.Items.Add("OpensideFlank");
            cmbPositions.Items.Add("Eightman");
            cmbPositions.Items.Add("Scrumhalf");
            cmbPositions.Items.Add("Flyhalf");
            cmbPositions.Items.Add("LeftWing");
            cmbPositions.Items.Add("InsideCentre");
            cmbPositions.Items.Add("OutsideCentre");
            cmbPositions.Items.Add("RightWing");
            cmbPositions.Items.Add("Fullback");
            cmbPositions.Items.Add("None");

            cmbPositions7.Items.Add("LooseheadProp");
            cmbPositions7.Items.Add("Hooker");
            cmbPositions7.Items.Add("TighHeadProp");
            cmbPositions7.Items.Add("FourLock");
            cmbPositions7.Items.Add("FiveLock");
            cmbPositions7.Items.Add("BlindsideFlank");
            cmbPositions7.Items.Add("OpensideFlank");
            cmbPositions7.Items.Add("Eightman");
            cmbPositions7.Items.Add("Scrumhalf");
            cmbPositions7.Items.Add("Flyhalf");
            cmbPositions7.Items.Add("LeftWing");
            cmbPositions7.Items.Add("InsideCentre");
            cmbPositions7.Items.Add("OutsideCentre");
            cmbPositions7.Items.Add("RightWing");
            cmbPositions7.Items.Add("Fullback");
            cmbPositions7.Items.Add("None");

            cmbSecondaryPositions.Items.Add("LooseheadProp");
            cmbSecondaryPositions.Items.Add("Hooker");
            cmbSecondaryPositions.Items.Add("TighHeadProp");
            cmbSecondaryPositions.Items.Add("FourLock");
            cmbSecondaryPositions.Items.Add("FiveLock");
            cmbSecondaryPositions.Items.Add("BlindsideFlank");
            cmbSecondaryPositions.Items.Add("OpensideFlank");
            cmbSecondaryPositions.Items.Add("Eightman");
            cmbSecondaryPositions.Items.Add("Scrumhalf");
            cmbSecondaryPositions.Items.Add("Flyhalf");
            cmbSecondaryPositions.Items.Add("LeftWing");
            cmbSecondaryPositions.Items.Add("InsideCentre");
            cmbSecondaryPositions.Items.Add("OutsideCentre");
            cmbSecondaryPositions.Items.Add("RightWing");
            cmbSecondaryPositions.Items.Add("Fullback");
            cmbSecondaryPositions.Items.Add("None");
        }

        /* #####################################################################################################
         * #####################################################################################################
         * Below code is meant for reading bytes from the files. Takes start and end positions as input.
         * 
         * 
         * #####################################################################################################
         * #####################################################################################################
         */

        private void loadFile()
        {
            // Create an instance of the open file dialog box.
            String fileShort = "";

            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            // Set filter options and filter index.
            openFileDialog1.Filter = "RC Database (.db)|*.db|All Files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;

            // Call the ShowDialog method to show the dialog box.
            bool? userClickedOK = (openFileDialog1.ShowDialog() == DialogResult.OK);

            // Process input if the user clicked OK.
            if (userClickedOK == true)
            {
                fileName = openFileDialog1.FileName;
                var onlyfilename = Path.GetFileName(openFileDialog1.FileName);

                //We first load each table from the database in their HEX values

                Properties.Settings.Default.KitsStart = txtPlayStartOff.Text;
                Properties.Settings.Default.KitsEnd = txtPlayEndOff.Text;
                Properties.Settings.Default.PlayStartInt = Convert.ToInt32(txtPlayStartOffInt.Text);
                Properties.Settings.Default.PlayEndInt = Convert.ToInt32(txtPlayEndOffInt.Text);

                //Teams
                Properties.Settings.Default.TeamsStart = txtTeamStartOff.Text;
                Properties.Settings.Default.TeamsEnd = txtTeamEndOff.Text;
                Properties.Settings.Default.TeamsStartInt = Convert.ToInt32(txtTeamStartOffInt.Text);
                Properties.Settings.Default.TeamsEndInt = Convert.ToInt32(txtTeamEndOffInt.Text);

                //Kits
                Properties.Settings.Default.KitsStart = txtKitsStartOff.Text;
                Properties.Settings.Default.KitsEnd = txtKitsEndOff.Text;
                Properties.Settings.Default.KitsStartInt = Convert.ToInt32(txtKitsStartOffInt.Text);
                Properties.Settings.Default.KitsEndint = Convert.ToInt32(txtKitsEndOffInt.Text);

                //Lineups
                Properties.Settings.Default.LinStart = txtLinStartOff.Text;
                Properties.Settings.Default.LinEnd = txtLinEndOff.Text;
                Properties.Settings.Default.LinStartInt = Convert.ToInt32(txtLinStartOffInt.Text);
                Properties.Settings.Default.LinEndInt = Convert.ToInt32(txtLinEndOffInt.Text);


                //Fixtures
                Properties.Settings.Default.FixStart = txtFixStart.Text;
                Properties.Settings.Default.FixEnd = txtFixEnd.Text;
                //Properties.Settings.Default.LinStartInt = Convert.ToInt32(txtLinStartOffInt.Text);
                //Properties.Settings.Default.LinEndInt = Convert.ToInt32(txtLinEndOffInt.Text);

                //Comps
                Properties.Settings.Default.CompStart = txtCompStart.Text;
                Properties.Settings.Default.CompEnd = txtCompEnd.Text;
                Properties.Settings.Default.CompStartInt = Convert.ToInt32(txtCompStartint.Text);
                Properties.Settings.Default.CompEndInt = Convert.ToInt32(txtCompEndInt.Text);


                byte[] bplayers = GetGlobalBytesFromFile(fileName, Properties.Settings.Default.PlayStart, Properties.Settings.Default.PlayEnd, Properties.Settings.Default.PlayStartInt, Properties.Settings.Default.PlayEndInt);
                byte[] bteams = GetGlobalBytesFromFile(fileName, Properties.Settings.Default.TeamsStart, Properties.Settings.Default.TeamsEnd, Properties.Settings.Default.TeamsStartInt, Properties.Settings.Default.TeamsEndInt);
                byte[] blineup = GetGlobalBytesFromFile(fileName, Properties.Settings.Default.LinStart, Properties.Settings.Default.LinEnd, Properties.Settings.Default.LinStartInt, Properties.Settings.Default.LinEndInt);
                byte[] btkits = GetGlobalBytesFromFile(fileName, Properties.Settings.Default.KitsStart, Properties.Settings.Default.KitsEnd, Properties.Settings.Default.KitsStartInt, Properties.Settings.Default.KitsEndint);
                byte[] btfixtures = GetGlobalBytesFromFile(fileName, Properties.Settings.Default.FixStart, Properties.Settings.Default.FixEnd, 0, 0);
                byte[] btcomps = GetGlobalBytesFromFile(fileName, Properties.Settings.Default.CompStart, Properties.Settings.Default.CompEnd, Properties.Settings.Default.CompStartInt, Properties.Settings.Default.CompEndInt);


                //We create a new byte lists of each record, includes the size per record in bytes
                List<byte[]> playerssp = splitDataToByteArray(bplayers, 1348);
                List<byte[]> lineupssp = splitDataToByteArray(blineup, 776);
                List<byte[]> teamssp = splitDataToByteArray(bteams, 212);
                List<byte[]> kitssp = splitDataToByteArray(btkits, 280);
                List<byte[]> fixssp = splitDataToByteArray(btfixtures, 16);
                List<byte[]> compssp = splitDataToByteArray(btcomps, 100);

                //We load each list of bytes into our database
                loadSplittedHEXtoDB(playerssp, "players");
                loadSplittedHEXtoDB(lineupssp, "lineups");
                loadSplittedHEXtoDB(teamssp, "teams");
                loadSplittedHEXtoDB(kitssp, "kits");
                loadSplittedHEXtoDB(fixssp, "fixtures");
                loadSplittedHEXtoDB(compssp, "comps");

                //Now we populate our lists using the data from the database
                populateViews();

                unlockScreen();
            }
        }

        private void loadSettings()
        {
            //Players
            txtPlayStartOff.Text = Properties.Settings.Default.PlayStart;
            txtPlayEndOff.Text = Properties.Settings.Default.PlayEnd;
            txtPlayStartOffInt.Text = Properties.Settings.Default.PlayStartInt.ToString();
            txtPlayEndOffInt.Text = Properties.Settings.Default.PlayEndInt.ToString();

            //Teams
            txtTeamStartOff.Text = Properties.Settings.Default.TeamsStart;
            txtTeamEndOff.Text = Properties.Settings.Default.TeamsEnd;
            txtTeamStartOffInt.Text = Properties.Settings.Default.TeamsStartInt.ToString();
            txtTeamEndOffInt.Text = Properties.Settings.Default.TeamsEndInt.ToString();

            //Kits
            txtKitsStartOff.Text = Properties.Settings.Default.KitsStart;
            txtKitsEndOff.Text = Properties.Settings.Default.KitsEnd;
            txtKitsStartOffInt.Text = Properties.Settings.Default.KitsStartInt.ToString();
            txtKitsEndOffInt.Text = Properties.Settings.Default.KitsEndint.ToString();

            //Lineups
            txtLinStartOff.Text = Properties.Settings.Default.LinStart;
            txtLinEndOff.Text = Properties.Settings.Default.LinEnd;
            txtLinStartOffInt.Text = Properties.Settings.Default.LinStartInt.ToString();
            txtLinEndOffInt.Text = Properties.Settings.Default.LinEndInt.ToString();

            //Fixtures
            txtFixStart.Text = Properties.Settings.Default.FixStart;
            txtFixEnd.Text = Properties.Settings.Default.FixEnd;

            //Comps
            txtCompStart.Text = Properties.Settings.Default.CompStart;
            txtCompEnd.Text = Properties.Settings.Default.CompEnd;
            txtCompStartint.Text = Properties.Settings.Default.CompStartInt.ToString();
            txtCompEndInt.Text = Properties.Settings.Default.CompEndInt.ToString();

        }

        private void saveSettings()
        {
            //Players
            Properties.Settings.Default.PlayStart = txtPlayStartOff.Text;
            Properties.Settings.Default.PlayEnd = txtPlayEndOff.Text;
            Properties.Settings.Default.PlayStartInt = Convert.ToInt32(txtPlayStartOffInt.Text);
            Properties.Settings.Default.PlayEndInt = Convert.ToInt32(txtPlayEndOffInt.Text);

            //Teams
            Properties.Settings.Default.TeamsStart = txtTeamStartOff.Text;
            Properties.Settings.Default.TeamsEnd = txtTeamEndOff.Text;
            Properties.Settings.Default.TeamsStartInt = Convert.ToInt32(txtTeamStartOffInt.Text);
            Properties.Settings.Default.TeamsEndInt = Convert.ToInt32(txtTeamEndOffInt.Text);

            //Kits
            Properties.Settings.Default.KitsStart = txtKitsStartOff.Text;
            Properties.Settings.Default.KitsEnd = txtKitsEndOff.Text;
            Properties.Settings.Default.KitsStartInt = Convert.ToInt32(txtKitsStartOffInt.Text);
            Properties.Settings.Default.KitsEndint = Convert.ToInt32(txtKitsEndOffInt.Text);

            //Lineups
            Properties.Settings.Default.LinStart = txtLinStartOff.Text;
            Properties.Settings.Default.LinEnd = txtLinEndOff.Text;
            Properties.Settings.Default.LinStartInt = Convert.ToInt32(txtLinStartOffInt.Text);
            Properties.Settings.Default.LinEndInt = Convert.ToInt32(txtLinEndOffInt.Text);

            //Fixtures
            Properties.Settings.Default.FixStart = txtFixStart.Text;
            Properties.Settings.Default.FixEnd = txtFixEnd.Text;


            //Comps
            Properties.Settings.Default.CompStart = txtCompStart.Text;
            Properties.Settings.Default.CompEnd = txtCompEnd.Text;
            Properties.Settings.Default.CompStartInt = Convert.ToInt32(txtCompStartint.Text);
            Properties.Settings.Default.CompEndInt = Convert.ToInt32(txtCompEndInt.Text);

            Properties.Settings.Default.Save();
            Properties.Settings.Default.Upgrade();
            MessageBox.Show("Saved Settings");

            loadSettings();
        }
        private void loadPostWrite()
        {
            //We first load each table from the database in their HEX values
            //We first load each table from the database in their HEX values
            //Players
            Properties.Settings.Default.KitsStart = txtPlayStartOff.Text;
            Properties.Settings.Default.KitsEnd = txtPlayEndOff.Text;
            Properties.Settings.Default.PlayStartInt = Convert.ToInt32(txtPlayStartOffInt.Text);
            Properties.Settings.Default.PlayEndInt = Convert.ToInt32(txtPlayEndOffInt.Text);

            //Teams
            Properties.Settings.Default.TeamsStart = txtTeamStartOff.Text;
            Properties.Settings.Default.TeamsEnd = txtTeamEndOff.Text;
            Properties.Settings.Default.TeamsStartInt = Convert.ToInt32(txtTeamStartOffInt.Text);
            Properties.Settings.Default.TeamsEndInt = Convert.ToInt32(txtTeamEndOffInt.Text);

            //Kits
            Properties.Settings.Default.KitsStart = txtKitsStartOff.Text;
            Properties.Settings.Default.KitsEnd = txtKitsEndOff.Text;
            Properties.Settings.Default.KitsStartInt = Convert.ToInt32(txtKitsStartOffInt.Text);
            Properties.Settings.Default.KitsEndint = Convert.ToInt32(txtKitsEndOffInt.Text);

            //Lineups
            Properties.Settings.Default.LinStart = txtLinStartOff.Text;
            Properties.Settings.Default.LinEnd = txtLinEndOff.Text;
            Properties.Settings.Default.LinStartInt = Convert.ToInt32(txtLinStartOffInt.Text);
            Properties.Settings.Default.LinEndInt = Convert.ToInt32(txtLinEndOffInt.Text);
            //Fixtures
            Properties.Settings.Default.FixStart = txtFixStart.Text;
            Properties.Settings.Default.FixEnd = txtFixEnd.Text;
            //Properties.Settings.Default.LinStartInt = Convert.ToInt32(txtLinStartOffInt.Text);
            //Properties.Settings.Default.LinEndInt = Convert.ToInt32(txtLinEndOffInt.Text);


            //Comps
            Properties.Settings.Default.CompStart = txtCompStart.Text;
            Properties.Settings.Default.CompEnd = txtCompEnd.Text;
            Properties.Settings.Default.CompStartInt = Convert.ToInt32(txtCompStartint.Text);
            Properties.Settings.Default.CompEndInt = Convert.ToInt32(txtCompEndInt.Text);


            byte[] bplayers = GetGlobalBytesFromFile(fileName, Properties.Settings.Default.PlayStart, Properties.Settings.Default.PlayEnd, Properties.Settings.Default.PlayStartInt, Properties.Settings.Default.PlayEndInt);
            byte[] bteams = GetGlobalBytesFromFile(fileName, Properties.Settings.Default.TeamsStart, Properties.Settings.Default.TeamsEnd, Properties.Settings.Default.TeamsStartInt, Properties.Settings.Default.TeamsEndInt);
            byte[] blineup = GetGlobalBytesFromFile(fileName, Properties.Settings.Default.LinStart, Properties.Settings.Default.LinEnd, Properties.Settings.Default.LinStartInt, Properties.Settings.Default.LinEndInt);
            byte[] btkits = GetGlobalBytesFromFile(fileName, Properties.Settings.Default.KitsStart, Properties.Settings.Default.KitsEnd, Properties.Settings.Default.KitsStartInt, Properties.Settings.Default.KitsEndint);
            byte[] btfixtures = GetGlobalBytesFromFile(fileName, Properties.Settings.Default.FixStart, Properties.Settings.Default.FixEnd, 0, 0);
            byte[] btcomps = GetGlobalBytesFromFile(fileName, Properties.Settings.Default.CompStart, Properties.Settings.Default.CompEnd, Properties.Settings.Default.CompStartInt, Properties.Settings.Default.CompEndInt);

            //We create a new byte lists of each record, includes the size per record in bytes
            List<byte[]> playerssp = splitDataToByteArray(bplayers, 1348);
            List<byte[]> lineupssp = splitDataToByteArray(blineup, 776);
            List<byte[]> teamssp = splitDataToByteArray(bteams, 212);
            List<byte[]> kitssp = splitDataToByteArray(btkits, 280);
            List<byte[]> fixssp = splitDataToByteArray(btfixtures, 16);
            List<byte[]> compssp = splitDataToByteArray(btcomps, 100);

            //We load each list of bytes into our database
            loadSplittedHEXtoDB(playerssp, "players");
            loadSplittedHEXtoDB(lineupssp, "lineups");
            loadSplittedHEXtoDB(teamssp, "teams");
            loadSplittedHEXtoDB(kitssp, "kits");
            loadSplittedHEXtoDB(fixssp, "fixtures");
            loadSplittedHEXtoDB(compssp, "comps");
            //Now we populate our lists using the data from the database
            populateViews();


        }

        public byte[] GetGlobalBytesFromFile(string fullFilePath, string posStartStr, string posEndStr, int ExtaStr, int ExtraEnd)
        {

            int posEnd = 0;
            int posStart = 0;
            byte[] buff = null;
            buff = File.ReadAllBytes(fullFilePath);
            try
            {
                posStart = Search(buff, StringToByteArray(posStartStr));
                posStart = posStart + ExtaStr;
            }
            catch (Exception exa)
            {
                MessageBox.Show("Error - Start!");
            }
            try
            {
                posEnd = Search(buff, StringToByteArray(posEndStr));
            // MessageBox.Show(posEnd.ToString());
            posEnd = posEnd + ExtraEnd;
            }
            catch (Exception exa)
            {
                MessageBox.Show("Error - End!");
            }
    byte[] slice = Extensions.ArraySlice(buff, posStart, posEnd);
            return slice;

        }
        public byte[] GetPlayerBytesFromFile(string fullFilePath)
        {

            int posEnd = 0;
            int posStart = 0;
            byte[] buff = null;
            buff = File.ReadAllBytes(fullFilePath);
            posStart = Search(buff, StringToByteArray("EA030000000827060200000000001000000010000200000000001000000010000000000000000000000000000000000000000000000080"));
            //posStart = posStart + 64;
            // MessageBox.Show(posStart.ToString());
            posEnd = Search(buff, StringToByteArray("E9030000ED9C78070000000000000000E903"));
            //MessageBox.Show(posEnd.ToString());
            byte[] slice = Extensions.ArraySlice(buff, posStart, posEnd);
            return slice;

        }

        public byte[] GetLineUpBytesFromFile(string fullFilePath)
        {

            int posEnd = 0;
            int posStart = 0;
            byte[] buff = null;
            buff = File.ReadAllBytes(fullFilePath);
            posStart = Search(buff, StringToByteArray("0A003C0049445F494E4A5552595F444553435F4E4F534500000000000000000000000000160000003C00960049445F494E4A5552595F444553435F53484F554C444552000000000000000000"));
            posStart = posStart + 76; //we have to adjust as we perform the lookup before the table starts
            //MessageBox.Show(posStart.ToString());
            //01000000A754785A020000008606795A030000000856775A0400000039077A5AE90300001E000000E803000014000000280000000500000032000000190000007D0000000A000000C80000000A0000007D000000
            posEnd = Search(buff, StringToByteArray("E90300001E000000E803000014000000280000000500000032000000190000007D0000000A000000C80000000A0000007D00000002000000320000000300000032000000030000003200000019000000E204000064000000102700000500000003000000EA0300001E000000E803000014000000280000000500000032000000190000007D0000000A000000C80000000A0000007D00000002000000320000000300000032000000030000003200000019000000E204000064000000102700000500000003000000EB0300001E000000E803000014000000280000000500000032000000190000007D0000000A000000C80000000A0000007D00000002000000320000000300000032000000030000003200000019000000E204000064000000102700000500000003000000EC0300001E000000EE0200001400000019000000050000001400000014000000C80000000F000000960000000A000000FA00000002000000320000000300000032000000030000003200000019000000E204000064000000102700000500000003000000ED0300001E000000EE02"));
            // posEnd = posEnd - 776;
            //MessageBox.Show(posEnd.ToString());
            byte[] slice = Extensions.ArraySlice(buff, posStart, posEnd);
            return slice;

        }

        public byte[] GetTeamsBytesFromFile(string fullFilePath)
        {
            int posEnd = 0;
            int posStart = 0;
            byte[] buff = null;
            buff = File.ReadAllBytes(fullFilePath);
            posStart = Search(buff, StringToByteArray("FF005553412057377300000000000000000000000000000000000000000000000000723773775F75736100000000"));
            posStart = posStart + 46; //we have to adjust as we perform the lookup before the table starts
                                      // MessageBox.Show(posStart.ToString());
            posEnd = Search(buff, StringToByteArray("EA030000000827060200000000001000000010000200000000001000000010000000000000000000000000000000"));
            //posEnd = posEnd - 212;
            // MessageBox.Show(posEnd.ToString());
            byte[] slice = Extensions.ArraySlice(buff, posStart, posEnd);
            return slice;

        }

        public byte[] GetKitsBytesFromFile(string fullFilePath)
        {
            int posEnd = 0;
            int posStart = 0;
            byte[] buff = null;
            buff = File.ReadAllBytes(fullFilePath);
            posStart = Search(buff, StringToByteArray("E3030000240124010100479C581342770000005F0A"));
            posStart = posStart; //we have to adjust as we perform the lookup before the table starts
                                 // MessageBox.Show(posStart.ToString());
            posEnd = Search(buff, StringToByteArray("E90300000018F50500005089E90300002D00000000000153696D6F6E2043756C68616E6500000000"));
            //posEnd = posEnd - 212;
            // MessageBox.Show(posEnd.ToString());
            byte[] slice = Extensions.ArraySlice(buff, posStart, posEnd);
            return slice;

        }

        /* #####################################################################################################
         * #####################################################################################################
         * Loading of data read from the bytes includes splitting into required offsets
         * 
         * 
         * #####################################################################################################
         * #####################################################################################################
         */
        private static List<byte[]> splitDataToByteArray(byte[] longString, int size)
        {
            byte[] source = longString;
            List<byte[]> result = new List<byte[]>();
            for (int i = 0; i < source.Length - 1; i += size)
            {
                byte[] buffer = new byte[size];
                Buffer.BlockCopy(source, i, buffer, 0, size);
                result.Add(buffer);
            }
            return result;
        }

        //The below requires the table adapter to be added onto the form.
        private void loadSplittedHEXtoDB(List<byte[]> bytelist, String dataType)
        {
            switch (dataType)
            {
                case "players":
                    tblPlayersHexTableAdapter.ClearTable();
                    for (int i = 0; i < bytelist.Count; i++)
                    {
                        tblPlayersHexTableAdapter.Insert(i, ToHexString(bytelist[i]), "", null);
                    }
                    break;
                case "lineups":
                    tblLineUpsHexTableAdapter.ClearTable();
                    for (int i = 0; i < bytelist.Count; i++)
                    {
                        tblLineUpsHexTableAdapter.Insert(i, ToHexString(bytelist[i]), "", null);
                    }
                    break;
                case "teams":
                    tblTeamsHexTableAdapter.ClearTable();
                    for (int i = 0; i < bytelist.Count; i++)
                    {
                        tblTeamsHexTableAdapter.Insert(i, ToHexString(bytelist[i]), "", null);
                    }
                    break;
                case "kits":
                    tblKitsHexTableAdapter.ClearTable();
                    for (int i = 0; i < bytelist.Count; i++)
                    {
                        tblKitsHexTableAdapter.Insert(i, ToHexString(bytelist[i]), "", null);
                    }
                    break;
                case "fixtures":
                    tblFixturesTableAdapter.ClearTable();
                    for (int i = 0; i < bytelist.Count; i++)
                    {
                        tblFixturesTableAdapter.Insert(i, ToHexString(bytelist[i]), "", null);
                    }
                    break;
                case "comps":
                    tblCompsTableAdapter1.ClearTable();
                    for (int i = 0; i < bytelist.Count; i++)
                    {
                        tblCompsTableAdapter1.Insert(i, ToHexString(bytelist[i]), "", null);
                    }
                    break;
                default:
                    MessageBox.Show("Error! Data not inserted, coding error!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }

        }

        //Reads the data from our tables then populates our listviews.
        public void populateViews()
        {
            //We assign arraylist for each itemset
            ArrayList playerIDs = new ArrayList();
            ArrayList playerNames = new ArrayList();


            //clear all list views
            objListViewPlayers.Items.Clear();
            objListViewPlayers2.Items.Clear();
            objListViewTeams.Items.Clear();
            objListViewTeams2.Items.Clear();
            objKitTeamView.Items.Clear();
            lstFixtures.Items.Clear();
            TeamsList.Clear();
            lstComp.Items.Clear();
            CompsList.Clear();

            //Let's load our type list with get:set methods
            List<Players> playersListEditor = new List<Players>();
            List<Players> linplayersListEditor = new List<Players>();
            List<Teams> teamsListEditor = new List<Teams>();
            List<Kits> kitsListEditor = new List<Kits>();

            //Get data from players
            if (tblPlayersHexTableAdapter.GetData().Count != 0)
            {
                //DataVariables

                int i = 0;
                int rwindex = 0;
                foreach (DataRow dr in tblPlayersHexTableAdapter.GetData())
                {
                    rwindex = i;
                    String fullName = "";
                    int nationID = 0;
                    string playerID = "";
                    String nation = "";
                    int star = 0;
                    String starStatus = "";
                    String prim15Position = "";
                    String prim7Position = "";

                    //Reads HEX datarow as source byte
                    byte[] source = StringToByteArray(dr.ItemArray[1].ToString());

                    //initialise bytes with correct sizes
                    byte[] bufferName = new byte[25];
                    byte[] bufferSurname = new byte[25];
                    byte[] btNationID = new byte[2];
                    byte[] btStar = new byte[2];
                    byte[] btplayID = new byte[2];
                    byte[] btPpos15ID = new byte[2];
                    byte[] btPpos7ID = new byte[2];


                    //Copy bytes out of the source
                    Buffer.BlockCopy(source, 220, bufferName, 0, 25);
                    Buffer.BlockCopy(source, 245, bufferSurname, 0, 25);
                    Buffer.BlockCopy(source, 0, btplayID, 0, 2);
                    Buffer.BlockCopy(source, 104, btNationID, 0, 2);
                    Buffer.BlockCopy(source, 142, btStar, 0, 2);
                    Buffer.BlockCopy(source, 8, btPpos15ID, 0, 2);
                    Buffer.BlockCopy(source, 20, btPpos7ID, 0, 2);

                    //Loads bytes to appropriate variables
                    fullName = HexString2Ascii(ToHexString(bufferName)).Replace("\0", string.Empty) + " " + HexString2Ascii(ToHexString(bufferSurname)).Replace("\0", string.Empty);
                    nationID = GetLittleEndianIntegerFromByteArray(btNationID);
                    star = GetLittleEndianIntegerFromByteArray(btStar);
                    if (star == 5000) { starStatus = "StarPlayer"; } else { starStatus = "Normal"; }
                    playerID = GetLittleEndianIntegerFromByteArray(btplayID).ToString();
                    nation = playerNation(nationID); //Converts the nation ID to a Name
                    prim15Position = getPosition(GetLittleEndianIntegerFromByteArray(btPpos15ID));
                    prim7Position = getPosition(GetLittleEndianIntegerFromByteArray(btPpos7ID));

                    //Populates our view arrays
                    playersListEditor.Add(new Players(playerID, fullName, nation, rwindex.ToString(), starStatus));
                    linplayersListEditor.Add(new Players(playerID, fullName, nation, rwindex.ToString(), prim15Position, prim7Position));

                    //Add the IDs to our dataset
                    dr[3] = playerID;
                    tblPlayersHexTableAdapter.Update(dr);

                    i++;
                }
                objListViewPlayers.SetObjects(playersListEditor);
                objListViewPlayers2.SetObjects(linplayersListEditor);
            }

            if (tblTeamsHexTableAdapter.GetData().Count != 0)
            {

                //DataVariables
                int i = 0;
                int rwindex = 0;
                foreach (DataRow dr in tblTeamsHexTableAdapter.GetData())
                {
                    rwindex = i;
                    String teamName = "";
                    string teamID = "";

                    //Reads HEX datarow as source byte
                    byte[] source = StringToByteArray(dr.ItemArray[1].ToString());

                    //initialise bytes with correct sizes
                    byte[] bufferName = new byte[32];
                    byte[] bttmID = new byte[2];


                    //Copy bytes out of the source
                    Buffer.BlockCopy(source, 148, bufferName, 0, 32);
                    Buffer.BlockCopy(source, 0, bttmID, 0, 2);

                    //Loads bytes to appropriate variables
                    teamName = HexString2Ascii(ToHexString(bufferName)).Replace("\0", string.Empty);
                    teamID = GetLittleEndianIntegerFromByteArray(bttmID).ToString();

                    //Populates our view arrays
                    teamsListEditor.Add(new Teams(teamID, teamName, rwindex));

                    //Add to our global dictionary
                    TeamsList.Add(Convert.ToInt16(teamID), teamName);

                    //Add the IDs to our dataset
                    dr[3] = teamID;
                    tblTeamsHexTableAdapter.Update(dr);

                    i++;
                }


                objListViewTeams.SetObjects(teamsListEditor);
                objListViewTeams2.SetObjects(teamsListEditor);
            }

            if (tblLineUpsHexTableAdapter.GetData().Count != 0)
            {
                //DataVariables
                int i = 0;
                int rwindex = 0;
                foreach (DataRow dr in tblLineUpsHexTableAdapter.GetData())
                {
                    rwindex = i;
                    string teamID = "";

                    //Reads HEX datarow as source byte
                    byte[] source = StringToByteArray(dr.ItemArray[1].ToString());

                    //initialise bytes with correct sizes
                    byte[] bttmID = new byte[2];


                    //Copy bytes out of the source
                    Buffer.BlockCopy(source, 0, bttmID, 0, 2);

                    //Loads bytes to appropriate variables
                    teamID = GetLittleEndianIntegerFromByteArray(bttmID).ToString();

                    //Add the IDs to our dataset
                    dr[3] = teamID;
                    tblLineUpsHexTableAdapter.Update(dr);

                    i++;
                }

            }

            if (tblKitsHexTableAdapter.GetData().Count != 0)
            {

                //DataVariables
                int i = 0;
                int rwindex = 0;
                foreach (DataRow dr in tblKitsHexTableAdapter.GetData())
                {
                    rwindex = i;
                    String kitName = "";
                    string kitID = "";

                    //Reads HEX datarow as source byte
                    byte[] source = StringToByteArray(dr.ItemArray[1].ToString());

                    //initialise bytes with correct sizes
                    byte[] bufferName = new byte[25];
                    byte[] btKitID = new byte[2];


                    //Copy bytes out of the source
                    Buffer.BlockCopy(source, 31, bufferName, 0, 25);
                    Buffer.BlockCopy(source, 0, btKitID, 0, 2);

                    //Loads bytes to appropriate variables
                    kitName = HexString2Ascii(ToHexString(bufferName)).Replace("\0", string.Empty);
                    kitID = GetLittleEndianIntegerFromByteArray(btKitID).ToString();


                    //Populates our view arrays
                    kitsListEditor.Add(new Kits(kitID, kitName, rwindex));

                    //Add the IDs to our dataset
                    dr[3] = kitID;
                    tblKitsHexTableAdapter.Update(dr);

                    i++;
                }

                objKitTeamView.SetObjects(kitsListEditor);

            }

            if (tblFixturesTableAdapter.GetData().Count != 0)
            {

                //DataVariables
                int i = 0;
                int rwindex = 0;
                foreach (DataRow dr in tblFixturesTableAdapter.GetData())
                {
                    rwindex = i;
                    String fixRound = "";
                    string fixID = "";
                    int home = 0;
                    int away = 0;
                    //Reads HEX datarow as source byte
                    byte[] source = StringToByteArray(dr.ItemArray[1].ToString());

                    //initialise bytes with correct sizes
                    //byte[] bufferName = new byte[25];
                    byte[] btFixID = new byte[2];
                    byte[] bffixTeamAway = new byte[2];
                    byte[] bffixTeamHome = new byte[2];

                    //Copy bytes out of the source
                    // Buffer.BlockCopy(source, 31, bufferName, 0, 25);
                    Buffer.BlockCopy(source, 0, btFixID, 0, 2);
                    Buffer.BlockCopy(source, 12, bffixTeamAway, 0, 2);
                    Buffer.BlockCopy(source, 10, bffixTeamHome, 0, 2);

                    //Converts extracted values to the required formats
                    fixID = GetLittleEndianIntegerFromByteArray(btFixID).ToString();
                    away = GetLittleEndianIntegerFromByteArray(bffixTeamAway);
                    home = GetLittleEndianIntegerFromByteArray(bffixTeamHome);

                    //Populates our view arrays
                    TeamsList.TryGetValue(home, out string teamhome);
                    TeamsList.TryGetValue(away, out string teamaway);

                    String addFix = fixID + " " + teamhome + " vs. " + teamaway;

                    lstFixtures.Items.Add(addFix);

                    //Add the IDs to our dataset
                    dr[3] = fixID;
                    tblFixturesTableAdapter.Update(dr);

                    i++;
                }

                //objKitTeamView.SetObjects(kitsListEditor);

            }

            if (tblCompsTableAdapter1.GetData().Count != 0)
            {

                //DataVariables
                int i = 0;
                int rwindex = 0;
                foreach (DataRow dr in tblCompsTableAdapter1.GetData())
                {
                    rwindex = i;
                    String Name = "";
                    string compID = "";
                    int edible = 0;

                    //Reads HEX datarow as source byte
                    byte[] source = StringToByteArray(dr.ItemArray[1].ToString());

                    //initialise bytes with correct sizes
                    //byte[] bufferName = new byte[25];
                    byte[] bfcompID = new byte[2];
                    byte[] bffCompEdb = new byte[1];
                    byte[] bfCompName = new byte[34];

                    //Copy bytes out of the source
                    // Buffer.BlockCopy(source, 31, bufferName, 0, 25);
                    Buffer.BlockCopy(source, 0, bfcompID, 0, 2);
                    Buffer.BlockCopy(source, 37, bffCompEdb, 0, 1);
                    Buffer.BlockCopy(source, 61, bfCompName, 0, 34);

                    //Converts extracted values to the required formats
                    compID = GetLittleEndianIntegerFromByteArray(bfcompID).ToString();
                    Name = HexString2Ascii(ToHexString(bfCompName)).Replace("\0", string.Empty);
                    edible = GetLittleEndianIntegerFromByteArray(bffCompEdb);

                    String addComp = compID + " " + Name;

                    lstComp.Items.Add(addComp);

                    //Add the IDs to our dataset
                    dr[3] = compID;
                    tblCompsTableAdapter1.Update(dr);

                    i++;
                }

                //objKitTeamView.SetObjects(kitsListEditor);

            }

        }





        /* #####################################################################################################
         * #####################################################################################################
         * Parses HEX stats from database to load data onto views
         * 
         * 
         * #####################################################################################################
         * #####################################################################################################
         */

        public void loadTeamLineUpfromHEX(string InHEX)
        {

            //put in check to see if HEX is empty
            //We clear our playerID holder
            playerIDs.Clear();


            //clear all list views
            lstLineup.Items.Clear();

            string playerID = "";
            String fullName = "";

            byte[] source = StringToByteArray(InHEX);
            //MessageBox.Show(InHEX);
            //initialise bytes with correct sizes
            byte[] lineup = new byte[90];
            byte[] currentPlayer = new byte[2];

            //Copy bytes out of the source
            Buffer.BlockCopy(source, 564, lineup, 0, 90);
            txtLineupOrgHEX.Text = InHEX;
            //Let's loop
            int byteset = 0;
            for (int i = 0; i < (lineup.Length) / 2; i++)
            {

                //MessageBox.Show(byteset.ToString());
                Buffer.BlockCopy(lineup, byteset, currentPlayer, 0, 2);
                byteset += 2;
                playerID = GetLittleEndianIntegerFromByteArray(currentPlayer).ToString();

                playerIDs.Add(playerID);

                i = i++;

            }
            int playernum = 1;
            foreach (string item in playerIDs)
            {
                string name = DBGetPlayerNamebyPlayerID(Convert.ToInt32(item));
                lstLineup.Items.Add(item + ":" + name + ": " + playernum.ToString());
                playernum = playernum + 1;

            }

        }

        private void defaultPlayers()
        {
            string playflName = Directory.GetCurrentDirectory() + @"\Default.tx";
            string[] lines = File.ReadAllLines(playflName);
            foreach (string line in lines)
            {
                string[] col = line.Split(',');
                DBReplacePlayerNamebyPlayerID(Convert.ToInt32(col[0]), col[1], col[2]);
                // process col[0], col[1], col[2]
            }
        }

        public void loadTeamDetails(string tmHEX)
        {

            // All variables
            String TeamName = "";
            String ShortName = "";
            String collarStr = "";
            int plateamIDyerID = 0;
            int deflinedepth = 0;
            int midlinedepth = 0;
            int attlinedepth = 0;
            int compcarID2 = 0;
            int compcarID1 = 0;
            int stripid1 = 0;
            int stripid2 = 0;
            int stripid3 = 0;
            int captID = 0;
            int goalkickId = 0;
            int playkickid = 0;
            int logoid = 0;
            int countid = 0;


            byte[] source = StringToByteArray(tmHEX);
            byte[] bfplateamIDyerID = new byte[2]; //Checked
            byte[] bfTeamName = new byte[32]; //Checked
            byte[] bfShortName = new byte[7]; //Checked
            byte[] bfcollar = new byte[1]; //Checked
            byte[] bfdeflinedepth = new byte[2]; //Checked
            byte[] bfmidlinedepth = new byte[2]; //Checked
            byte[] bfattlinedepth = new byte[2]; //Checked
            byte[] bfcompcarID2 = new byte[2]; //Checked
            byte[] bfcompcarID1 = new byte[2]; //Checked
            byte[] bfstripid1 = new byte[2]; //Checked
            byte[] bfstripid2 = new byte[2]; //Checked
            byte[] bfstripid3 = new byte[2]; //Checked
            byte[] bfcaptID = new byte[2]; //Checked
            byte[] bfgoalkickId = new byte[2]; //Checked
            byte[] bfplaykickid = new byte[2]; //Checked
            byte[] bflogoid = new byte[2]; //Checked
            byte[] bfcountid = new byte[2]; //Checked


            //Extracts the values from the hex into our byte values
            Buffer.BlockCopy(source, 0, bfplateamIDyerID, 0, 2);
            Buffer.BlockCopy(source, 148, bfTeamName, 0, 32);
            Buffer.BlockCopy(source, 205, bfShortName, 0, 7);
            Buffer.BlockCopy(source, 205, bfcollar, 0, 1);
            Buffer.BlockCopy(source, 66, bfdeflinedepth, 0, 2);
            Buffer.BlockCopy(source, 92, bfmidlinedepth, 0, 2);
            Buffer.BlockCopy(source, 118, bfattlinedepth, 0, 2);
            Buffer.BlockCopy(source, 136, bfcompcarID2, 0, 2);
            Buffer.BlockCopy(source, 134, bfcompcarID1, 0, 2);
            Buffer.BlockCopy(source, 14, bfstripid1, 0, 2);
            Buffer.BlockCopy(source, 16, bfstripid2, 0, 2);
            Buffer.BlockCopy(source, 18, bfstripid3, 0, 2);
            Buffer.BlockCopy(source, 24, bfcaptID, 0, 2);
            Buffer.BlockCopy(source, 26, bfgoalkickId, 0, 2);
            Buffer.BlockCopy(source, 28, bfplaykickid, 0, 2);
            Buffer.BlockCopy(source, 20, bflogoid, 0, 2);
            Buffer.BlockCopy(source, 22, bfcountid, 0, 2);


            string tmnamehex = ToHexString(bfTeamName);
            //Converts extracted values to the required formats
            plateamIDyerID = GetLittleEndianIntegerFromByteArray(bfplateamIDyerID);
            TeamName = HexString2Ascii(tmnamehex).Replace("\0", string.Empty);
            ShortName = HexString2Ascii(ToHexString(bfShortName)).Replace("\0", string.Empty);
            deflinedepth = GetLittleEndianIntegerFromByteArray(bfdeflinedepth);
            midlinedepth = GetLittleEndianIntegerFromByteArray(bfmidlinedepth);
            attlinedepth = GetLittleEndianIntegerFromByteArray(bfattlinedepth);
            compcarID2 = GetLittleEndianIntegerFromByteArray(bfcompcarID2);
            compcarID1 = GetLittleEndianIntegerFromByteArray(bfcompcarID1);

            stripid1 = GetLittleEndianIntegerFromByteArray(bfstripid1);
            stripid2 = GetLittleEndianIntegerFromByteArray(bfstripid2);
            stripid3 = GetLittleEndianIntegerFromByteArray(bfstripid3);
            captID = GetLittleEndianIntegerFromByteArray(bfcaptID);
            goalkickId = GetLittleEndianIntegerFromByteArray(bfgoalkickId);
            playkickid = GetLittleEndianIntegerFromByteArray(bfplaykickid);
            logoid = GetLittleEndianIntegerFromByteArray(bflogoid);
            countid = GetLittleEndianIntegerFromByteArray(bfcountid);

            deflinedepth = deflinedepth / 100;
            midlinedepth = midlinedepth / 100;
            attlinedepth = attlinedepth / 100;

            txtTeamID.Text = plateamIDyerID.ToString();
            txtTeamName.Text = TeamName;
            txtShortName.Text = ShortName;
            txtLinDef.Text = deflinedepth.ToString();
            txtLinMid.Text = midlinedepth.ToString();
            txtLinAtt.Text = attlinedepth.ToString();
            txtTeamHEX.Text = tmHEX;
            txtTeamCompID2.Text = compcarID2.ToString();
            txtTeamCompID1.Text = compcarID1.ToString();
            txtteamStripID1.Text = stripid1.ToString();
            txtteamStripID2.Text = stripid2.ToString();
            txtteamStripID3.Text = stripid3.ToString();
            txtPlCaptID.Text = captID.ToString();
            txtPLGoalKickID.Text = goalkickId.ToString();
            txtPLplaykickID.Text = playkickid.ToString();
            txtteamlogiID.Text = logoid.ToString();
            txtteamassoccountID.Text = countid.ToString();

        }

        public void loadFixtureData(string ktHEX)
        {

            // All variables
            int fixTeamHome = 0;
            int roundNumber = 0;
            int fixTeamAway = 0;
            int fixID = 0;
            int compID = 0;

            byte[] source = StringToByteArray(ktHEX);
            byte[] bffixTeamHome = new byte[2]; //Checked
            byte[] bfroundNumber = new byte[2]; //Checked
            byte[] bffixTeamAway = new byte[2]; //Checked
            byte[] bffixID = new byte[2]; //Checked
            byte[] bfCompID = new byte[2]; //Checked


            //Extracts the values from the hex into our byte values
            Buffer.BlockCopy(source, 0, bffixID, 0, 2);
            Buffer.BlockCopy(source, 12, bffixTeamAway, 0, 2);
            Buffer.BlockCopy(source, 8, bfCompID, 0, 2);
            Buffer.BlockCopy(source, 10, bffixTeamHome, 0, 2);
            Buffer.BlockCopy(source, 6, bfroundNumber, 0, 2);

            //Converts extracted values to the required formats
            fixID = GetLittleEndianIntegerFromByteArray(bffixID);
            fixTeamAway = GetLittleEndianIntegerFromByteArray(bffixTeamAway);
            fixTeamHome = GetLittleEndianIntegerFromByteArray(bffixTeamHome);
            roundNumber = GetLittleEndianIntegerFromByteArray(bfroundNumber);
            compID = GetLittleEndianIntegerFromByteArray(bfCompID);


            //Add labels
            TeamsList.TryGetValue(fixTeamHome, out string teamhome);
            TeamsList.TryGetValue(fixTeamAway, out string teamaway);

            //Populate gui
            txtFixID.Text = fixID.ToString();
            txtFixHome.Text = fixTeamHome.ToString();
            txtFixAway.Text = fixTeamAway.ToString();
            txtFixRound.Text = roundNumber.ToString();
            txtFixtureHEX.Text = ktHEX;
            lblAway.Text = teamaway;
            lblHome.Text = teamhome;
            txtFixCompID.Text = compID.ToString(); //This value is still unkown 04/02/2021

        }

        public void loadCompData(string cpHEX)
        {

            String CompName = "";
            int edible = 0;
            String compID = "";

            //initialise bytes with correct sizes
            //byte[] bufferName = new byte[25];
            byte[] bfcompID = new byte[2];
            byte[] bffCompEdb = new byte[1];
            byte[] bfCompName = new byte[34];

            //Copy bytes out of the source
            byte[] source = StringToByteArray(cpHEX);
            Buffer.BlockCopy(source, 0, bfcompID, 0, 2);
            Buffer.BlockCopy(source, 37, bffCompEdb, 0, 1);
            Buffer.BlockCopy(source, 61, bfCompName, 0, 34);

            //Converts extracted values to the required formats
            compID = GetLittleEndianIntegerFromByteArray(bfcompID).ToString();
            Name = HexString2Ascii(ToHexString(bfCompName)).Replace("\0", string.Empty);
            edible = GetLittleEndianIntegerFromByteArray(bffCompEdb);

            //Populate gui
            txtCompName.Text = Name.ToString();
            txtCompEdible.Text = edible.ToString();
            txtCompID.Text = compID.ToString();
            txtCompHEX.Text = cpHEX;
        }

        public void loadKitData(string ktHEX)
        {

            // All variables
            String KitName = "";
            int kitNumber = 0;
            String collarStr = "";
            int kitID = 0;


            byte[] source = StringToByteArray(ktHEX);
            byte[] bfKitID = new byte[2]; //Checked
            byte[] bfKitName = new byte[25]; //Checked
            byte[] bfcollar = new byte[1]; //Checked
            byte[] bfkitnumber = new byte[2]; //Checked


            //Extracts the values from the hex into our byte values
            Buffer.BlockCopy(source, 0, bfKitID, 0, 2);
            Buffer.BlockCopy(source, 31, bfKitName, 0, 25);
            Buffer.BlockCopy(source, 9, bfcollar, 0, 1);
            Buffer.BlockCopy(source, 6, bfkitnumber, 0, 2);

            string tmnamehex = ToHexString(bfKitName);
            //Converts extracted values to the required formats
            kitID = GetLittleEndianIntegerFromByteArray(bfKitID);
            KitName = HexString2Ascii(tmnamehex).Replace("\0", string.Empty);
            collarStr = ToHexString(bfcollar);
            kitNumber = GetLittleEndianIntegerFromByteArray(bfkitnumber);


            txtkitID.Text = kitID.ToString();
            txtkitName.Text = KitName;
            txtCollarID.Text = collarStr.ToString();
            txtNumberID.Text = kitNumber.ToString();
            txtKitHex.Text = ktHEX;

        }
        public void populatePlayerDetails(string playerHEX)
        {
            chkDummyPlayer.Checked = false;

            // All variables
            String name = "";
            String surName = "";
            int playerID = 0;
            int height = 0;
            int weight = 0;
            int locked = 0;
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
            int nationID = 0;
            int headID = 0;
            int faceID = 0;
            int hairID = 0;
            int facialID = 0;
            int PrimPosID = 0;
            int PrimPos7ID = 0;
            int bootIDint = 0;
            int bootIDclub = 0;
            int skinID = 0;
            int SecPosID = 0;

            //Initialise bytes for each variable
            byte[] source = StringToByteArray(playerHEX);
            byte[] playID = new byte[2]; //Checked
            byte[] bufferName = new byte[25]; //Checked
            byte[] bufferSurname = new byte[25]; //Checked
            byte[] btHeight = new byte[1];
            byte[] btWeight = new byte[1];
            // byte[] btLck = new byte[1];
            byte[] btFIT = new byte[2];
            byte[] btSPD = new byte[2];
            byte[] btACC = new byte[2];
            byte[] btAGL = new byte[2];
            byte[] btStar = new byte[2]; //Checked
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
            byte[] btNationID = new byte[2]; //Checked
            byte[] btHeadID = new byte[1];
            byte[] btFaceID = new byte[2];
            //    byte[] btHairID = new byte[2];
            //    byte[] btBeardID = new byte[2];
            byte[] btPposID = new byte[2];
            byte[] btPpos7ID = new byte[2];
            //    byte[] btBootint = new byte[1];
            //    byte[] btBootclub = new byte[1];
            //    byte[] btSkinID = new byte[1];
            byte[] btSposID = new byte[2];

            //Extracts the values from the hex into our byte values
            Buffer.BlockCopy(source, 0, playID, 0, 2);
            Buffer.BlockCopy(source, 220, bufferName, 0, 25);
            Buffer.BlockCopy(source, 245, bufferSurname, 0, 25);
            Buffer.BlockCopy(source, 181, btHeight, 0, 1);
            Buffer.BlockCopy(source, 182, btWeight, 0, 1);
            // Buffer.BlockCopy(source,, btLck, 0,);
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
            Buffer.BlockCopy(source, 104, btNationID, 0, 2);
            Buffer.BlockCopy(source, 152, btHeadID, 0, 1);
            Buffer.BlockCopy(source, 145, btFaceID, 0, 2);
            //   Buffer.BlockCopy(source,, btHairID, 0,);
            //   Buffer.BlockCopy(source,, btBeardID, 0,);
            Buffer.BlockCopy(source, 8, btPposID, 0, 2);
            Buffer.BlockCopy(source, 20, btPpos7ID, 0, 2);
            //   Buffer.BlockCopy(source,, btBootclub, 0,);
            //   Buffer.BlockCopy(source,, btBootint, 0,);
            //   Buffer.BlockCopy(source,, btSkinID, 0,);
            Buffer.BlockCopy(source, 12, btSposID, 0, 2);

            //Converts extracted values to the required formats
            playerID = GetLittleEndianIntegerFromByteArray(playID);
            name = HexString2Ascii(ToHexString(bufferName)).Replace("\0", string.Empty);
            surName = HexString2Ascii(ToHexString(bufferSurname)).Replace("\0", string.Empty);
            height = GetLittleEndianIntegerFromByteArray(btHeight);
            weight = GetLittleEndianIntegerFromByteArray(btWeight);
            //   locked = GetLittleEndianIntegerFromByteArray(btLck);
            fitness = GetLittleEndianIntegerFromByteArray(btFIT);
            speed = GetLittleEndianIntegerFromByteArray(btSPD);
            accl = GetLittleEndianIntegerFromByteArray(btACC);
            agil = GetLittleEndianIntegerFromByteArray(btAGL);
            star = GetLittleEndianIntegerFromByteArray(btStar);
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
            nationID = GetLittleEndianIntegerFromByteArray(btNationID);
            headID = GetLittleEndianIntegerFromByteArray(btHeadID);
            faceID = GetLittleEndianIntegerFromByteArray(btFaceID);
            //    hairID = GetLittleEndianIntegerFromByteArray(btHairID);
            //    facialID = GetLittleEndianIntegerFromByteArray(btBeardID);
            PrimPosID = GetLittleEndianIntegerFromByteArray(btPposID);
            PrimPos7ID = GetLittleEndianIntegerFromByteArray(btPpos7ID);
            //    bootIDclub = GetLittleEndianIntegerFromByteArray(btBootclub) + 1;
            //    bootIDint = GetLittleEndianIntegerFromByteArray(btBootint) + 1;
            //    skinID = GetLittleEndianIntegerFromByteArray(btSkinID) + 1;
            SecPosID = GetLittleEndianIntegerFromByteArray(btSposID);

            //Populates the interface with the values
            txtPlayID.Text = playerID.ToString();
            txtName.Text = name;
            txtSurname.Text = surName;
            txtHeight.Text = height.ToString();
            txtWeight.Text = weight.ToString();
            txtAgility.Text = agil.ToString();
            if (locked == 1) { chkLocked.Checked = true; } else { chkLocked.Checked = false; }
            txtFitness.Text = fitness.ToString();
            txtSpeed.Text = speed.ToString();
            txtAccel.Text = accl.ToString();
            txtAgility.Text = agil.ToString();
            if (star == 5000) { chkStar.Checked = true; } else { chkStar.Checked = false; }
            txtAggr.Text = aggr.ToString();
            txtbrckTackle.Text = btckl.ToString();
            txtTackle.Text = tackle.ToString();
            txtPass.Text = passing.ToString();
            txtOffload.Text = offloading.ToString();
            txtKick.Text = playkick.ToString();
            txtGoalKick.Text = goalkick.ToString();
            txtCatch.Text = catching.ToString();
            txtStrenght.Text = strenght.ToString();
            txtMental.Text = mental.ToString();
            txtJump.Text = jumping.ToString();
            txtDiscp.Text = discpl.ToString();
            selectNation(nationID);
            txtHeadID.Text = headID.ToString();
            txtFaceID.Text = faceID.ToString();
            //       txtHair.Text = hairID.ToString();
            //       txtFacialHair.Text = facialID.ToString();
            cmbPositions.SelectedItem = getPosition(PrimPosID);
            cmbPositions7.SelectedItem = getPosition(PrimPos7ID);
            //      txtClubBoot.Text = bootIDclub.ToString();
            //      txtIntBoot.Text = bootIDint.ToString();
            //      txtSkin.Text = skinID.ToString();
            cmbSecondaryPositions.SelectedItem = getPosition(SecPosID);
        }
        public String DBGetPlayerHEXbyID(int plID)
        {
            String HEX = "";

            if (tblPlayersHexTableAdapter.GetDataByRecordID(plID).Count < 2)
            {
                foreach (DataRow dr in tblPlayersHexTableAdapter.GetDataByRecordID(plID))
                {
                    HEX = (dr.ItemArray[1].ToString());
                }
            }
            else
            {
                MessageBox.Show("Mutliple records matched. Error! Contact developer!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return HEX;

        }
        public void DBReplacePlayerNamebyPlayerID(int plID, string fname, string lname)
        {
            String HEX = "";


            if (tblPlayersHexTableAdapter.GetDataByPlayerID(plID).Count < 2)
            {
                foreach (DataRow dr in tblPlayersHexTableAdapter.GetDataByPlayerID(plID))
                {
                    HEX = (dr.ItemArray[1].ToString());

                    string text1 = HEX; //original
                    string text2 = plID.ToString();

                    fname = fname.Replace("-", "");
                    lname = lname.Replace("-", "");

                    string hex1 = this.ConvertStringToHex(fname, 40);
                    string hex2 = this.ConvertStringToHex(lname, 40);

                    string str5 = "";

                    if (fname.Contains("Dummy"))
                    {
                        fname = "Fake";
                        lname = "Player";

                        string hexLittleEndian3 = this.decimalToHexLittleEndian((int)Convert.ToInt16("2000"), 2);
                        string hexLittleEndian4 = this.decimalToHexLittleEndian((int)Convert.ToInt16("2000"), 2);
                        string hexLittleEndian5 = this.decimalToHexLittleEndian((int)Convert.ToInt16("2000"), 2);
                        string hexLittleEndian6 = this.decimalToHexLittleEndian((int)Convert.ToInt16("2000"), 2);
                        string hexLittleEndian7 = this.decimalToHexLittleEndian((int)Convert.ToInt16("2000"), 2);
                        string hexLittleEndian8 = this.decimalToHexLittleEndian((int)Convert.ToInt16("2000"), 2);
                        string hexLittleEndian9 = this.decimalToHexLittleEndian((int)Convert.ToInt16("2000"), 2);
                        string hexLittleEndian10 = this.decimalToHexLittleEndian((int)Convert.ToInt16("2000"), 2);
                        string hexLittleEndian11 = this.decimalToHexLittleEndian((int)Convert.ToInt16("2000"), 2);
                        string hexLittleEndian12 = this.decimalToHexLittleEndian((int)Convert.ToInt16("2000"), 2);
                        string hexLittleEndian13 = this.decimalToHexLittleEndian((int)Convert.ToInt16("2000"), 2);
                        string hexLittleEndian14 = this.decimalToHexLittleEndian((int)Convert.ToInt16("2000"), 2);
                        string hexLittleEndian15 = this.decimalToHexLittleEndian((int)Convert.ToInt16("2000"), 2);
                        string hexLittleEndian16 = this.decimalToHexLittleEndian((int)Convert.ToInt16("2000"), 2);
                        string hexLittleEndian17 = this.decimalToHexLittleEndian((int)Convert.ToInt16("2000"), 2);
                        string hexLittleEndian18 = this.decimalToHexLittleEndian((int)Convert.ToInt16("2000"), 2);
                        string hexLittleEndian19 = this.decimalToHexLittleEndian((int)Convert.ToInt16(this.nationStrToID("Vanuatu")), 2);

                        str5 =
                        text1.Remove(440, 40).Insert(440, this.ConvertStringToHex(fname, 40)) //name
                        .Remove(490, 40).Insert(490, this.ConvertStringToHex(lname, 40)) //surname
                        .Remove(216, 4).Insert(216, hexLittleEndian3) //fitness
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
                        .Remove(276, 4).Insert(276, hexLittleEndian18) //discipline
                        .Remove(208, 4).Insert(208, hexLittleEndian19); //nationality


                    }
                    else
                    {
                        str5 =
                       text1.Remove(440, 40).Insert(440, hex1) //name
                       .Remove(490, 40).Insert(490, hex2);//surname
                    }

                    if (str5.Length != HEX.Length)
                    {
                        int num = (int)MessageBox.Show("Error - Hex sizes do not match.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        tblPlayersHexTableAdapter.UpdateQueryModHex(str5, text1);
                        PendingChanges = PendingChanges + 1;
                        statusToolstrip.Text = PendingChanges.ToString() + " changes pending to be written to DB!!";
                    }
                }
            }
            else
            {
                MessageBox.Show("Mutliple records matched. Error! Contact developer!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            }


        }
        public String DBGetPlayerNamebyPlayerID(int plID)
        {
            String HEX = "";
            String FullName = "";

            if (tblPlayersHexTableAdapter.GetDataByPlayerID(plID).Count < 2)
            {
                foreach (DataRow dr in tblPlayersHexTableAdapter.GetDataByPlayerID(plID))
                {
                    HEX = (dr.ItemArray[1].ToString());

                    //Reads HEX datarow as source byte
                    byte[] source = StringToByteArray(HEX);

                    //initialise bytes with correct sizes
                    byte[] bufferName = new byte[25];
                    byte[] bufferSurname = new byte[25];

                    //Copy bytes out of the source
                    Buffer.BlockCopy(source, 220, bufferName, 0, 25);
                    Buffer.BlockCopy(source, 245, bufferSurname, 0, 25);

                    //Loads bytes to appropriate variables
                    FullName = HexString2Ascii(ToHexString(bufferName)).Replace("\0", string.Empty) + " " + HexString2Ascii(ToHexString(bufferSurname)).Replace("\0", string.Empty);
                }
            }
            else
            {
                MessageBox.Show("Mutliple records matched. Error! Contact developer!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            }
            return FullName;

        }
        public String DBGetTeamHEXbyID(int plID)
        {
            String HEX = "";

            if (tblTeamsHexTableAdapter.GetDataByRecordID(plID).Count < 2)
            {
                foreach (DataRow dr in tblTeamsHexTableAdapter.GetDataByRecordID(plID))
                {
                    HEX = (dr.ItemArray[1].ToString());
                }
            }
            else
            {
                MessageBox.Show("Mutliple records matched. Error! Contact developer!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return HEX;

        }
        public String DBGetFixtureHEXbyID(int plID)
        {
            String HEX = "";

            if (tblFixturesTableAdapter.GetDataByRecordID(plID).Count < 2)
            {
                foreach (DataRow dr in tblFixturesTableAdapter.GetDataByRecordID(plID))
                {
                    HEX = (dr.ItemArray[1].ToString());
                }
            }
            else
            {
                MessageBox.Show("Mutliple records matched. Error! Contact developer!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return HEX;

        }

        public String DBGetCompHEXbyID(int plID)
        {
            String HEX = "";

            if (tblCompsTableAdapter1.GetDataByRecordID(plID).Count < 2)
            {
                foreach (DataRow dr in tblCompsTableAdapter1.GetDataByRecordID(plID))
                {
                    HEX = (dr.ItemArray[1].ToString());
                }
            }
            else
            {
                MessageBox.Show("Mutliple records matched. Error! Contact developer!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return HEX;

        }
        public String DBGetKitHEXbyID(int plID)
        {
            String HEX = "";

            if (tblKitsHexTableAdapter.GetDataByID(plID).Count < 2)
            {
                foreach (DataRow dr in tblKitsHexTableAdapter.GetDataByID(plID))
                {
                    HEX = (dr.ItemArray[1].ToString());
                }
            }
            else
            {
                MessageBox.Show("Mutliple records matched. Error! Contact developer!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return HEX;

        }
        public String DBGetLineUpsHEXbyTeamID(int tmID)
        {
            String HEX = "";

            if (tblLineUpsHexTableAdapter.GetDataByTeamID(tmID).Count < 2)
            {
                StringBuilder sb = new StringBuilder();
                foreach (DataRow dr in tblLineUpsHexTableAdapter.GetDataByTeamID(tmID))
                {


                    sb.AppendLine(string.Join(",", dr.ItemArray));


                    HEX = (dr.ItemArray[1].ToString());
                }

                //  MessageBox.Show(sb.ToString());
            }
            else
            {
                MessageBox.Show("Mutliple records matched. Error! Contact developer!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return HEX;

        }






        /* #####################################################################################################
         * #####################################################################################################
         * Saves modded HEX data to DB 
         * Writes changes
         * 
         * #####################################################################################################
         * #####################################################################################################
         */
        public void saveTeamsHEX()
        {
            string text1 = this.txtTeamHEX.Text; //original
            this.txtTeamName.Text = this.txtTeamName.Text.Replace("-", "");
            this.txtShortName.Text = this.txtShortName.Text.Replace("-", "");
            string hex1 = this.ConvertStringToHex(this.txtTeamName.Text, 32);
            string hex2 = this.ConvertStringToHex(this.txtShortName.Text, 7);
            string hexLittleEndian1 = this.decimalToHexLittleEndian((int)(Convert.ToInt16(this.txtLinDef.Text) * 100), 2);
            string hexLittleEndian2 = this.decimalToHexLittleEndian((int)(Convert.ToInt16(this.txtLinMid.Text) * 100), 2);
            string hexLittleEndian3 = this.decimalToHexLittleEndian((int)(Convert.ToInt16(this.txtLinAtt.Text) * 100), 2);
            string hexLittleEndian6 = this.decimalToHexLittleEndian((int)Convert.ToInt16(this.txtTeamCompID2.Text), 2);

            string hexLittleEndian7 = this.decimalToHexLittleEndian((int)Convert.ToInt16(this.txtteamStripID1.Text), 2);
            string hexLittleEndian8 = this.decimalToHexLittleEndian((int)Convert.ToInt16(this.txtteamStripID2.Text), 2);
            string hexLittleEndian9 = this.decimalToHexLittleEndian((int)Convert.ToInt16(this.txtteamStripID3.Text), 2);
            string hexLittleEndian10 = this.decimalToHexLittleEndian((int)Convert.ToInt16(this.txtPlCaptID.Text), 2);
            string hexLittleEndian11 = this.decimalToHexLittleEndian((int)Convert.ToInt16(this.txtPLGoalKickID.Text), 2);
            string hexLittleEndian12 = this.decimalToHexLittleEndian((int)Convert.ToInt16(this.txtPLplaykickID.Text), 2);
            string hexLittleEndian13 = this.decimalToHexLittleEndian((int)Convert.ToInt16(this.txtteamlogiID.Text), 2);
            string hexLittleEndian14 = this.decimalToHexLittleEndian((int)Convert.ToInt16(this.txtteamassoccountID.Text), 2);

            string str5 =
               text1.Remove(296, 32).Insert(296, hex1) //name
                .Remove(410, 7).Insert(410, hex2) //short
                .Remove(132, 4).Insert(132, hexLittleEndian1)//def
                .Remove(184, 4).Insert(184, hexLittleEndian2) //mid
                .Remove(236, 4).Insert(236, hexLittleEndian3) //att
                .Remove(272, 4).Insert(272, hexLittleEndian6) //compID
                .Remove(28, 4).Insert(28, hexLittleEndian7) //strip1
                .Remove(32, 4).Insert(32, hexLittleEndian8) //strip2
                .Remove(36, 4).Insert(36, hexLittleEndian9) //strip3
                .Remove(48, 4).Insert(48, hexLittleEndian10) //captid
                .Remove(52, 4).Insert(52, hexLittleEndian11) //goalid
                .Remove(56, 4).Insert(56, hexLittleEndian12) //playid
                .Remove(40, 4).Insert(40, hexLittleEndian13) //logoid
                .Remove(44, 4).Insert(44, hexLittleEndian14); //countid



            if (str5.Length != text1.Length)
            {
                int num = (int)MessageBox.Show("Error - Hex sizes do not match.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                tblTeamsHexTableAdapter.UpdateQueryModHex(str5, text1);
                PendingChanges = PendingChanges + 1;
                statusToolstrip.Text = PendingChanges.ToString() + " changes pending to be written to DB!!";

            }

        }

        public void saveKitsHex()
        {
            if (txtkitID.Text != "995")
            {
                string text1 = this.txtKitHex.Text; //original
                this.txtkitName.Text = this.txtkitName.Text.Replace("-", "");
                this.txtCollarID.Text = this.txtCollarID.Text.Replace("-", "");
                string hex1 = this.ConvertStringToHex(this.txtkitName.Text, 45);
                string hex2 = txtCollarID.Text;
                string hexLittleEndian6 = this.decimalToHexLittleEndian((int)Convert.ToInt16(this.txtNumberID.Text), 2);
                string str5 =
                   text1.Remove(62, 45).Insert(62, hex1) //name
                   .Remove(18, 2).Insert(18, hex2)
                   .Remove(12, 4).Insert(12, hexLittleEndian6);  //collar
                textBox3.Text = str5;


                if (str5.Length != text1.Length)
                {
                    int num = (int)MessageBox.Show("Error - Hex sizes do not match.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    tblKitsHexTableAdapter.UpdateQueryModHex(str5, text1);
                    PendingChanges = PendingChanges + 1;
                    statusToolstrip.Text = PendingChanges.ToString() + " changes pending to be written to DB!!";

                }

            }

        }

        public void saveFixturesHex()
        {
            if (txtFixID.Text != "1001")
            {
                string text1 = this.txtFixtureHEX.Text; //original

                string hexLittleEndian1 = this.decimalToHexLittleEndian((int)Convert.ToInt16(this.txtFixHome.Text), 2);
                string hexLittleEndian2 = this.decimalToHexLittleEndian((int)Convert.ToInt16(this.txtFixAway.Text), 2);
                string hexLittleEndian3 = this.decimalToHexLittleEndian((int)Convert.ToInt16(this.txtFixRound.Text), 2);
                string hexLittleEndian4 = this.decimalToHexLittleEndian((int)Convert.ToInt16(this.txtFixCompID.Text), 2);

                string str5 =
                   text1.Remove(20, 4).Insert(20, hexLittleEndian1) //home
                   .Remove(24, 4).Insert(24, hexLittleEndian2) //away
                   .Remove(16, 4).Insert(16, hexLittleEndian4) //compID
                   .Remove(12, 4).Insert(12, hexLittleEndian3);  //roundID
                textBox3.Text = str5;


                if (str5.Length != text1.Length)
                {
                    int num = (int)MessageBox.Show("Error - Hex sizes do not match.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    tblFixturesTableAdapter.UpdateQueryModHex(str5, text1);
                    PendingChanges = PendingChanges + 1;
                    statusToolstrip.Text = PendingChanges.ToString() + " changes pending to be written to DB!!";

                }

            }

        }

        public void saveCompsHex()
        {
            if (txtCompID.Text != "1001")
            {
                string text1 = this.txtCompHEX.Text; //original
                string hexLittleEndian1 = this.decimalToHexLittleEndian((int)Convert.ToInt16(this.txtCompID.Text), 2);
                string hexLittleEndian2 = this.decimalToHexLittleEndian((int)Convert.ToInt16(this.txtCompEdible.Text), 1);
                this.txtCompName.Text = this.txtCompName.Text.Replace("-", "");
                string hex1 = this.ConvertStringToHex(this.txtCompName.Text, 34);

                string str5 =
                   text1.Remove(74, 2).Insert(74, hexLittleEndian2) //edible
                   .Remove(122, 68).Insert(122, hex1); //name 
                txtCompModHEX.Text = str5;


                if (str5.Length != text1.Length)
                {
                    int num = (int)MessageBox.Show("Error - Hex sizes do not match.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    tblCompsTableAdapter1.UpdateQueryModHex(str5, text1);
                    PendingChanges = PendingChanges + 1;
                    statusToolstrip.Text = PendingChanges.ToString() + " changes pending to be written to DB!!";

                }

            }

        }

        public void saveLineUpsHex()
        {
            string orglineupHEX = this.txtLineupOrgHEX.Text;
            string playerID = "";
            string modlineupHEX = this.txtLineupOrgHEX.Text; //original
            string moddedHex = "";
            int byteset = 1128;   //start location of the playerlineup
            for (int i = 0; i < lstLineup.Items.Count; i++)
            {
                //need to check if player exists first!!
                playerID = (lstLineup.Items[i].ToString());
                string[] split = playerID.Split(':');
                playerID = split[0];
                //MessageBox.Show(playerID);
                if (playerID == "0")
                {
                    string hexLittleEndianplayID = "0000";
                    moddedHex = modlineupHEX.Remove(byteset, 4).Insert(byteset, hexLittleEndianplayID);
                }
                else
                {
                    string hexLittleEndianplayID = this.decimalToHexLittleEndian((int)Convert.ToInt16(playerID), 2);
                    moddedHex = modlineupHEX.Remove(byteset, 4).Insert(byteset, hexLittleEndianplayID);

                }
                byteset += 4;
                i = i++;
                modlineupHEX = moddedHex;

            }

            this.txtLineUpModHEX.Text = moddedHex;
            if (this.txtLineUpModHEX.TextLength != this.txtLineupOrgHEX.TextLength)
            {
                int num = (int)MessageBox.Show("Error - Hex sizes do not match.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                tblLineUpsHexTableAdapter.UpdateQueryModHex(moddedHex, orglineupHEX);
                PendingChanges = PendingChanges + 1;
                statusToolstrip.Text = PendingChanges.ToString() + " changes pending to be written to DB!!";
            }
        }

        public void savePlayerHEX()
        {
            if (txtPlayID.Text != "1002")
            {
                if (this.chkDummyPlayer.Checked)
                {
                    this.txtName.Text = "Fake";
                    this.txtSurname.Text = "Player";
                    this.txtHeight.Text = "170";
                    this.txtWeight.Text = "70";
                    this.txtAgility.Text = "2000";
                    this.chkLocked.Checked = false;
                    this.txtFitness.Text = "2000";
                    this.txtSpeed.Text = "2000";
                    this.txtAccel.Text = "2000";
                    this.txtAggr.Text = "2000";
                    this.chkStar.Checked = false;
                    this.txtAggr.Text = "2000";
                    this.txtbrckTackle.Text = "2000";
                    this.txtTackle.Text = "2000";
                    this.txtPass.Text = "2000";
                    this.txtOffload.Text = "2000";
                    this.txtKick.Text = "2000";
                    this.txtGoalKick.Text = "2000";
                    this.txtCatch.Text = "2000";
                    this.txtStrenght.Text = "2000";
                    this.txtMental.Text = "2000";
                    this.txtJump.Text = "2000";
                    this.txtDiscp.Text = "2000";
                    this.cmbNations.SelectedItem = (object)"Vanuatu";
                }
                string text1 = this.txtOrgHEX.Text; //original

                string text2 = this.txtPlayID.Text;
                this.txtName.Text = this.txtName.Text.Replace("-", "");
                this.txtSurname.Text = this.txtSurname.Text.Replace("-", "");
                string hex1 = this.ConvertStringToHex(this.txtName.Text, 45);
                string hex2 = this.ConvertStringToHex(this.txtSurname.Text, 45);
                string hexLittleEndian1 = this.decimalToHexLittleEndian((int)Convert.ToInt16(this.txtHeight.Text), 1);
                string hexLittleEndian2 = this.decimalToHexLittleEndian((int)Convert.ToInt16(this.txtWeight.Text), 1);
                //string str1 = !this.chkLocked.Checked ? "00" : "01";2
                string hexLittleEndian3 = this.decimalToHexLittleEndian((int)Convert.ToInt16(this.txtFitness.Text), 2);
                string hexLittleEndian4 = this.decimalToHexLittleEndian((int)Convert.ToInt16(this.txtSpeed.Text), 2);
                string hexLittleEndian5 = this.decimalToHexLittleEndian((int)Convert.ToInt16(this.txtAccel.Text), 2);
                string hexLittleEndian6 = this.decimalToHexLittleEndian((int)Convert.ToInt16(this.txtAgility.Text), 2);
                string str2 = !this.chkStar.Checked ? "0000" : "8813";
                string hexLittleEndian7 = this.decimalToHexLittleEndian((int)Convert.ToInt16(this.txtAggr.Text), 2);
                string hexLittleEndian8 = this.decimalToHexLittleEndian((int)Convert.ToInt16(this.txtbrckTackle.Text), 2);
                string hexLittleEndian9 = this.decimalToHexLittleEndian((int)Convert.ToInt16(this.txtTackle.Text), 2);
                string hexLittleEndian10 = this.decimalToHexLittleEndian((int)Convert.ToInt16(this.txtPass.Text), 2);
                string hexLittleEndian11 = this.decimalToHexLittleEndian((int)Convert.ToInt16(this.txtOffload.Text), 2);
                string hexLittleEndian12 = this.decimalToHexLittleEndian((int)Convert.ToInt16(this.txtKick.Text), 2);
                string hexLittleEndian13 = this.decimalToHexLittleEndian((int)Convert.ToInt16(this.txtGoalKick.Text), 2);
                string hexLittleEndian14 = this.decimalToHexLittleEndian((int)Convert.ToInt16(this.txtCatch.Text), 2);
                string hexLittleEndian15 = this.decimalToHexLittleEndian((int)Convert.ToInt16(this.txtStrenght.Text), 2);
                string hexLittleEndian16 = this.decimalToHexLittleEndian((int)Convert.ToInt16(this.txtMental.Text), 2);
                string hexLittleEndian17 = this.decimalToHexLittleEndian((int)Convert.ToInt16(this.txtJump.Text), 2);
                string hexLittleEndian18 = this.decimalToHexLittleEndian((int)Convert.ToInt16(this.txtDiscp.Text), 2);
                string hexLittleEndian19 = this.decimalToHexLittleEndian((int)Convert.ToInt16(this.nationStrToID(this.cmbNations.SelectedItem.ToString())), 2);
                string hexLittleEndian20 = this.decimalToHexLittleEndian((int)Convert.ToInt16(this.txtHeadID.Text), 1);
                string str3 = this.RCPOStoID(this.cmbPositions.SelectedItem.ToString());
                string str4 = this.RCPOStoID(this.cmbPositions7.SelectedItem.ToString());
                //string hexLittleEndian21 = this.decimalToHexLittleEndian((int)Convert.ToInt16(this.txtIntBoot.Text) - 1, 1);
                //string hexLittleEndian22 = this.decimalToHexLittleEndian((int)Convert.ToInt16(this.txtClubBoot.Text) - 1, 1);
                string hexLittleEndian23 = this.decimalToHexLittleEndian((int)Convert.ToInt16(this.txtFaceID.Text), 2);
                //string hexLittleEndian24 = this.decimalToHexLittleEndian(Convert.ToInt32(this.txtHair.Text), 2);
                //string hexLittleEndian25 = this.decimalToHexLittleEndian((int)Convert.ToInt16(this.txtFacialHair.Text), 2);
                //string hexLittleEndian26 = this.decimalToHexLittleEndian((int)Convert.ToInt16(this.txtSkin.Text), 1);
                string str9 = this.RCPOStoID(this.cmbSecondaryPositions.SelectedItem.ToString());

                string str5 =
                    text1.Remove(440, 45).Insert(440, hex1) //name
                    .Remove(490, 45).Insert(490, hex2) //surname
                    .Remove(216, 4).Insert(216, hexLittleEndian3) //fitness
                    .Remove(224, 4).Insert(224, hexLittleEndian4) //speed
                    .Remove(228, 4).Insert(228, hexLittleEndian5) //acceleration
                    .Remove(220, 4).Insert(220, hexLittleEndian6) //agility
                    .Remove(364, 2).Insert(364, hexLittleEndian2) //weight
                    .Remove(362, 2).Insert(362, hexLittleEndian1) //height
                    .Remove(284, 4).Insert(284, str2) //star
                    .Remove(304, 2).Insert(304, hexLittleEndian20) //headID
                                                                   //.Remove(290, 4).Insert(290, hexLittleEndian23) //faceID error at the moment
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
                    .Remove(276, 4).Insert(276, hexLittleEndian18) //discipline
                    .Remove(208, 4).Insert(208, hexLittleEndian19) //nationality
                    .Remove(16, 4).Insert(16, str3) //position15sp
                    .Remove(40, 4).Insert(40, str4) //position7sp
                    .Remove(24, 4).Insert(24, str9); //secondpos15s

                this.txtChangedHex.Text = str5;
                if (this.txtChangedHex.TextLength != this.txtOrgHEX.TextLength)
                {
                    int num = (int)MessageBox.Show("Error - Hex sizes do not match.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    tblPlayersHexTableAdapter.UpdateQueryModHex(str5, text1);
                    PendingChanges = PendingChanges + 1;
                    statusToolstrip.Text = PendingChanges.ToString() + " changes pending to be written to DB!!";

                }
            }
            else
            {
                MessageBox.Show("For the editor to work you cannot save over the first player in the DB. Sorry...");
            }
        }

        public void writeChangestoDB()
        {
            byte[] src = File.ReadAllBytes(this.fileName);
            String HEX = "";
            String modHEX = "";
            //we loop through each table and commit the modded hexes
            //Lets write players table
            HEX = "";
            modHEX = "";
            if (tblPlayersHexTableAdapter.GetDataByNotNullModHex().Count > 0)
            {
                foreach (DataRow dr in tblPlayersHexTableAdapter.GetDataByNotNullModHex())
                {
                    HEX = (dr.ItemArray[1].ToString());
                    modHEX = (dr.ItemArray[2].ToString());
                    int position = this.Search(src, this.StringToByteArray(HEX));
                    this.ReplaceData(this.fileName, position, this.StringToByteArray(modHEX));
                }

            }

            //Lets write teams table
            HEX = "";
            modHEX = "";
            if (tblTeamsHexTableAdapter.GetDataByNotNullModHex().Count > 0)
            {
                foreach (DataRow dr in tblTeamsHexTableAdapter.GetDataByNotNullModHex())
                {
                    HEX = (dr.ItemArray[1].ToString());
                    modHEX = (dr.ItemArray[2].ToString());
                    int position = this.Search(src, this.StringToByteArray(HEX));
                    this.ReplaceData(this.fileName, position, this.StringToByteArray(modHEX));
                }

            }

            //Lets write lineups table
            HEX = "";
            modHEX = "";
            if (tblLineUpsHexTableAdapter.GetDataByNotNullModHex().Count > 0)
            {
                foreach (DataRow dr in tblLineUpsHexTableAdapter.GetDataByNotNullModHex())
                {
                    HEX = (dr.ItemArray[1].ToString());
                    modHEX = (dr.ItemArray[2].ToString());
                    int position = this.Search(src, this.StringToByteArray(HEX));
                    this.ReplaceData(this.fileName, position, this.StringToByteArray(modHEX));
                }

            }
            //Kits
            HEX = "";
            modHEX = "";
            if (tblKitsHexTableAdapter.GetDataByNotNullModHex().Count > 0)
            {
                foreach (DataRow dr in tblKitsHexTableAdapter.GetDataByNotNullModHex())
                {
                    HEX = (dr.ItemArray[1].ToString());
                    modHEX = (dr.ItemArray[2].ToString());
                    int position = this.Search(src, this.StringToByteArray(HEX));
                    this.ReplaceData(this.fileName, position, this.StringToByteArray(modHEX));
                }

            }

            //Fixtures
            HEX = "";
            modHEX = "";
            if (tblFixturesTableAdapter.GetDataByNotNullModHex().Count > 0)
            {
                foreach (DataRow dr in tblFixturesTableAdapter.GetDataByNotNullModHex())
                {
                    HEX = (dr.ItemArray[1].ToString());
                    modHEX = (dr.ItemArray[2].ToString());
                    int position = this.Search(src, this.StringToByteArray(HEX));
                    this.ReplaceData(this.fileName, position, this.StringToByteArray(modHEX));
                }

            }


            //Comps
            HEX = "";
            modHEX = "";
            if (tblCompsTableAdapter1.GetDataByNotNullModHex().Count > 0)
            {
                foreach (DataRow dr in tblCompsTableAdapter1.GetDataByNotNullModHex())
                {
                    HEX = (dr.ItemArray[1].ToString());
                    modHEX = (dr.ItemArray[2].ToString());
                    int position = this.Search(src, this.StringToByteArray(HEX));
                    this.ReplaceData(this.fileName, position, this.StringToByteArray(modHEX));
                }

            }


            //bool @checked = this.chkLikeNessReplace.Checked;
            // if (@checked)
            // {
            //     int num = this.Search(src, this.StringToByteArray(this.textBox5.Text));
            //     bool flag = num > 0;
            //      if (flag)
            //       {
            //           this.ReplaceData(this.fileName, num, this.StringToByteArray(this.txtLikenessOriginal.Text));
            //       }
            //   }
            PendingChanges = 0;
            statusToolstrip.Text = "";
            MessageBox.Show("Changes Written!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ReplaceData(string filename, int position, byte[] data)
        {
            using (Stream stream = File.Open(filename, FileMode.Open))
            {
                stream.Position = position;
                stream.Write(data, 0, data.Length);
            }
        }

        /* #####################################################################################################
         * #####################################################################################################
         * Helper methdos for specific values in RC4 database
         * 
         * 
         * #####################################################################################################
         * #####################################################################################################
         */

        //Gets position based on decimcal value
        private string getPosition(int PosID)
        {
            Dictionary<string, int> dict = new Dictionary<string, int>();
            dict.Add("LooseheadProp", 1);
            dict.Add("Hooker", 2);
            dict.Add("TighHeadProp", 4);
            dict.Add("FourLock", 8);
            dict.Add("FiveLock", 16);
            dict.Add("BlindsideFlank", 32);
            dict.Add("OpensideFlank", 64);
            dict.Add("Eightman", 128);
            dict.Add("Scrumhalf", 256);
            dict.Add("Flyhalf", 512);
            dict.Add("LeftWing", 1024);
            dict.Add("InsideCentre", 2048);
            dict.Add("OutsideCentre", 4096);
            dict.Add("RightWing", 8192);
            dict.Add("Fullback", 16384);
            dict.Add("None", 0);

            foreach (KeyValuePair<string, int> pair in dict)
            {
                if (pair.Value == PosID) { return pair.Key.ToString(); }
            }

            return "error";

        }

        //Extracts HEX variable for Position
        private String RCPOStoID(String Position)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("LooseheadProp", "0100");
            dict.Add("Hooker", "0200");
            dict.Add("TighHeadProp", "0400");
            dict.Add("FourLock", "0800");
            dict.Add("FiveLock", "1000");
            dict.Add("BlindsideFlank", "2000");
            dict.Add("OpensideFlank", "4000");
            dict.Add("Eightman", "8000");
            dict.Add("Scrumhalf", "0001");
            dict.Add("Flyhalf", "0002");
            dict.Add("LeftWing", "0004");
            dict.Add("InsideCentre", "0008");
            dict.Add("OutsideCentre", "0010");
            dict.Add("RightWing", "0020");
            dict.Add("Fullback", "0040");
            dict.Add("None", "0100");

            foreach (KeyValuePair<string, string> pair in dict)
            {
                if (pair.Key == Position)
                {
                    return pair.Value;
                }
            }

            return "0100";
        }

        //Converts the nation ID to a Name
        private string playerNation(int nationID)
        {

            int caseSwitch = nationID;
            switch (caseSwitch)
            {
                case 1001:
                    return "New Zealand";
                    break;
                case 1002:
                    return ("Australia");
                    break;
                case 1003:
                    return ("Hong Kong");
                    break;
                case 1004:
                    return ("Japan");
                    break;
                case 1005:
                    return ("Argentina");
                    break;
                case 1006:
                    return ("Canada");
                    break;
                case 1007:
                    return ("England");
                    break;
                case 1008:
                    return ("Fiji");
                    break;
                case 1009:
                    return ("France");
                    break;
                case 1010:
                    return ("Georgia");
                    break;
                case 1011:
                    return ("Ireland");
                    break;
                case 1012:
                    return ("Italy");
                    break;
                case 1013:
                    return ("Namibia");
                    break;
                case 1014:
                    return ("Romania");
                    break;
                case 1015:
                    return ("Russia");
                    break;
                case 1016:
                    return ("Samoa");
                    break;
                case 1017:
                    return ("Scotland");
                    break;
                case 1018:
                    return ("Tonga");
                    break;
                case 1019:
                    return ("USA");
                    break;
                case 1020:
                    return ("Wales");
                    break;
                case 1021:
                    return ("Uruguay");
                    break;
                case 1022:
                    return ("South Africa");
                    break;
                case 1023:
                    return ("China");
                    break;
                case 1024:
                    return ("Cameroon");
                    break;
                case 1025:
                    return ("Cook Islands");
                    break;
                case 1026:
                    return ("Spain");
                    break;
                case 1027:
                    return ("Morocco");
                    break;
                case 1028:
                    return ("Netherlands");
                    break;
                case 1029:
                    return ("Norway");
                    break;
                case 1030:
                    return ("Portugal");
                    break;
                case 1031:
                    return ("Vanuatu");
                    break;
                case 1032:
                    return ("Zimbabwe");
                    break;
                case 1033:
                    return ("Belgium");
                    break;
                case 1034:
                    return ("Ivory Coast");
                    break;
                case 1035:
                    return ("Switzerland");
                    break;
                case 1036:
                    return ("Brazil");
                    break;
                case 1037:
                    return ("Chile");
                    break;
                case 1038:
                    return ("Kenya");
                    break;
                case 1039:
                    return ("American Samoa");
                    break;
                default:
                    return ("None Error");
                    break;
            }
        }

        private void selectNation(int nationID)
        {
            int caseSwitch = nationID;
            switch (caseSwitch)
            {
                case 1001:
                    cmbNations.SelectedItem = "New Zealand";
                    break;
                case 1002:
                    cmbNations.SelectedItem = ("Australia");
                    break;
                case 1003:
                    cmbNations.SelectedItem = ("Hong Kong");
                    break;
                case 1004:
                    cmbNations.SelectedItem = ("Japan");
                    break;
                case 1005:
                    cmbNations.SelectedItem = ("Argentina");
                    break;
                case 1006:
                    cmbNations.SelectedItem = ("Canada");
                    break;
                case 1007:
                    cmbNations.SelectedItem = ("England");
                    break;
                case 1008:
                    cmbNations.SelectedItem = ("Fiji");
                    break;
                case 1009:
                    cmbNations.SelectedItem = ("France");
                    break;
                case 1010:
                    cmbNations.SelectedItem = ("Georgia");
                    break;
                case 1011:
                    cmbNations.SelectedItem = ("Ireland");
                    break;
                case 1012:
                    cmbNations.SelectedItem = ("Italy");
                    break;
                case 1013:
                    cmbNations.SelectedItem = ("Namibia");
                    break;
                case 1014:
                    cmbNations.SelectedItem = ("Romania");
                    break;
                case 1015:
                    cmbNations.SelectedItem = ("Russia");
                    break;
                case 1016:
                    cmbNations.SelectedItem = ("Samoa");
                    break;
                case 1017:
                    cmbNations.SelectedItem = ("Scotland");
                    break;
                case 1018:
                    cmbNations.SelectedItem = ("Tonga");
                    break;
                case 1019:
                    cmbNations.SelectedItem = ("USA");
                    break;
                case 1020:
                    cmbNations.SelectedItem = ("Wales");
                    break;
                case 1021:
                    cmbNations.SelectedItem = ("Uruguay");
                    break;
                case 1022:
                    cmbNations.SelectedItem = ("South Africa");
                    break;
                case 1023:
                    cmbNations.SelectedItem = ("China");
                    break;
                case 1024:
                    cmbNations.SelectedItem = ("Cameroon");
                    break;
                case 1025:
                    cmbNations.SelectedItem = ("Cook Islands");
                    break;
                case 1026:
                    cmbNations.SelectedItem = ("Spain");
                    break;
                case 1027:
                    cmbNations.SelectedItem = ("Morocco");
                    break;
                case 1028:
                    cmbNations.SelectedItem = ("Netherlands");
                    break;
                case 1029:
                    cmbNations.SelectedItem = ("Norway");
                    break;
                case 1030:
                    cmbNations.SelectedItem = ("Portugal");
                    break;
                case 1031:
                    cmbNations.SelectedItem = ("Vanuatu");
                    break;
                case 1032:
                    cmbNations.SelectedItem = ("Zimbabwe");
                    break;
                case 1033:
                    cmbNations.SelectedItem = ("Belgium");
                    break;
                case 1034:
                    cmbNations.SelectedItem = ("Ivory Coast");
                    break;
                case 1035:
                    cmbNations.SelectedItem = ("Switzerland");
                    break;
                case 1036:
                    cmbNations.SelectedItem = ("Brazil");
                    break;
                case 1037:
                    cmbNations.SelectedItem = ("Chile");
                    break;
                case 1038:
                    cmbNations.SelectedItem = ("Kenya");
                    break;
                case 1039:
                    cmbNations.SelectedItem = ("American Samoa");
                    break;
                default:

                    break;
            }
        }

        //Converts nation Name to its ID
        private int nationStrToID(string selectednation)
        {
            int nation = 1001;

            string[] nations = new string[] { "New Zealand", "Australia", "Hong Kong", "Japan", "Argentina", "Canada", "England", "Fiji", "France", "Georgia", "Ireland", "Italy", "Namibia", "Romania", "Russia", "Samoa", "Scotland", "Tonga", "USA", "Wales", "Uruguay", "South Africa", "China", "Cameroon", "Cook Islands", "Spain", "Morocco", "Netherlands", "Norway", "Portugal", "Vanuatu", "Zimbabwe", "Belgium", "Ivory Coast", "Switzerland", "Brazil", "Chile", "Kenya", "American Samoa" };

            for (int i = 0; i < nations.Length; i++)
            {
                if (nations[i] == selectednation)
                {
                    nation = i + 1001;

                }

            }

            return nation;

        }

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

        /* #####################################################################################################
         * #####################################################################################################
         * Event handlers
         * 
         * 
         * #####################################################################################################
         * #####################################################################################################
         */

        private void frmMain_Load(object sender, EventArgs e)
        {


        }

        private void ObjListViewPlayers_SelectionChanged(object sender, EventArgs e)
        {
            var olvColumn = this.PlayIndex1; // whichever column you want
            var sb = new StringBuilder();
            foreach (object model in objListViewPlayers.SelectedObjects)
                sb.AppendLine(olvColumn.GetStringValue(model));
            var selectedCellsAsText = sb.ToString();
            if (selectedCellsAsText != "")
            {
                string playerHEX = DBGetPlayerHEXbyID(Convert.ToInt32(selectedCellsAsText));
                populatePlayerDetails(playerHEX);
                txtOrgHEX.Text = playerHEX;
            }
            // MessageBox.Show(selectedCellsAsText);

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            this.objListViewPlayers.ModelFilter = TextMatchFilter.Contains(this.objListViewPlayers, textBox7.Text);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

            loadFile();

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (tbcMain.SelectedTab == tbcMain.TabPages["tbpPlayers"])
            {
                savePlayerHEX();
            }
            else if (tbcMain.SelectedTab == tbcMain.TabPages["tbpLineups"])
            {
                saveLineUpsHex();
            }
            else if (tbcMain.SelectedTab == tbcMain.TabPages["tbpTeams"])
            {
                saveTeamsHEX();
            }
            else if (tbcMain.SelectedTab == tbcMain.TabPages["tbpKits"])
            {
                saveKitsHex();
            }
            else if (tbcMain.SelectedTab == tbcMain.TabPages["tbpFixtures"])
            {
                saveFixturesHex();
            }
            else if (tbcMain.SelectedTab == tbcMain.TabPages["tbpComps"])
            {
                saveCompsHex();
            }
        }


        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (PendingChanges > 0)
            {
                //let's backup first
               
                string sourceFile = this.fileName;
                string destinationFile = this.fileName + ".rc4editorold";
                try
                {
              
                    File.Copy(sourceFile, destinationFile, true);
                }
                catch (IOException iox)
                {
                  MessageBox.Show(iox.Message);
                }


                //WriteChanges
                writeChangestoDB();
                loadPostWrite();
            }
            else
            {
                MessageBox.Show("No changes pending, save after modifying and moving to a new screen!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }




    private void objListViewTeams2_SelectionChanged(object sender, EventArgs e)
        {
            var olvColumn = this.olvColumn7; // whichever column you want
            var sb = new StringBuilder();
            foreach (object model in objListViewTeams2.SelectedObjects)
                sb.AppendLine(olvColumn.GetStringValue(model));
            int selectedCellsAsText = Convert.ToInt32(sb.ToString());
         
            string lineupHEX = DBGetLineUpsHEXbyTeamID(selectedCellsAsText);
            loadTeamLineUpfromHEX(lineupHEX);

        }

        private void btnRMLineUpPlayer_Click(object sender, EventArgs e)
        {
            if (lstLineup.SelectedIndex < 23 )
            {
                MessageBox.Show("You cannot remove players from match 23, please use replace!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (lstLineup.SelectedIndex > 23)
            {
                while (lstLineup.SelectedItems.Count > 0)
                {
                    lstLineup.Items[lstLineup.SelectedIndex] = "0";
                    break;
                }
            }
            else
            {
                MessageBox.Show("You need to select a player first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


        }

        private void button7_Click(object sender, EventArgs e)
        {         
            if (objListViewPlayers2.SelectedObjects.Count > 0)
            {
                var olvColumn = this.olvColumn11; // whichever column you want
                var olvColumn1 = this.olvColumn10;
                var sb = new StringBuilder();
                var sb1 = new StringBuilder();
                foreach (object model in objListViewPlayers2.SelectedObjects)
                {
                    sb.AppendLine(olvColumn.GetStringValue(model));
                    sb1.AppendLine(olvColumn1.GetStringValue(model));
                }
                int curPlayerID = Convert.ToInt32(sb.ToString());
  
                string name = sb1.ToString();

                while (lstLineup.SelectedItems.Count > 0)
                {
                    if ((playerIDs.Contains(curPlayerID.ToString())) == false)
                    {
                        lstLineup.Items[lstLineup.SelectedIndex] = curPlayerID + ":" + name;
                        
                    }
                    else
                    {
                        MessageBox.Show("The player is already in the squad!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                }
            }
            else
                {
                MessageBox.Show("You have not selected the player to replace the current one with!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void txtPlayerSearch2_TextChanged(object sender, EventArgs e)
        {
            this.objListViewPlayers2.ModelFilter = TextMatchFilter.Contains(this.objListViewPlayers2, txtPlayerSearch2.Text);
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            frmGlobalEditor bge = new frmGlobalEditor();
            bge.ShowDialog();

            if (bge.updateDone == true)
            {
                writeChangestoDB();
                loadPostWrite();
            }

        }

        private void objListViewTeams_SelectionChanged(object sender, EventArgs e)
        {
            var olvColumn = this.TeamIndex2; // whichever column you want
            var sb = new StringBuilder();
            foreach (object model in objListViewTeams.SelectedObjects)
                sb.AppendLine(olvColumn.GetStringValue(model));
            int selectedCellsAsText = Convert.ToInt32(sb.ToString());

            string teamHEX = DBGetTeamHEXbyID(selectedCellsAsText);
            loadTeamDetails(teamHEX);



        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            defaultPlayers();
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            if (workerThread == null)
            {
                timer1.Stop();
                return;
            }

            // still works: exiting
            if (workerThread.IsAlive)
                return;

            // finished
            timer1.Stop();
            toolStripProgressBar1.Visible = false;
            workerThread = null;
        }

        private void objKitTeamView_SelectionChanged(object sender, EventArgs e)
        {
            var olvColumn = this.KitIndex; // whichever column you want
            var sb = new StringBuilder();
            foreach (object model in objKitTeamView.SelectedObjects)
                sb.AppendLine(olvColumn.GetStringValue(model));
            int selectedCellsAsText = Convert.ToInt32(sb.ToString());

            string kitHEX = DBGetKitHEXbyID(selectedCellsAsText);
            loadKitData(kitHEX);


        }

        private void groupBox6_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            saveSettings();
        }

        private void label66_Click(object sender, EventArgs e)
        {

        }



        private void tbpFixtures_Click(object sender, EventArgs e)
        {

        }

        private void lstFixtures_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedCellsAsText = Convert.ToInt32(lstFixtures.SelectedIndex);
            string fixHEX = DBGetFixtureHEXbyID(selectedCellsAsText);
            loadFixtureData(fixHEX);
        }

        private void lstComp_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedCellsAsText = Convert.ToInt32(lstComp.SelectedIndex);
            string cmpHEX = DBGetCompHEXbyID(selectedCellsAsText);
            loadCompData(cmpHEX);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Create an instance of the open file dialog box.
            String fileShort = "";

            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            // Set filter options and filter index.
            openFileDialog1.Filter = "RC Database (.db)|*.db|All Files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;

            // Call the ShowDialog method to show the dialog box.
            bool? userClickedOK = (openFileDialog1.ShowDialog() == DialogResult.OK);

            // Process input if the user clicked OK.
            if (userClickedOK == true)
            {
                fileName = openFileDialog1.FileName;
                var onlyfilename = Path.GetFileName(openFileDialog1.FileName);

                //We first load each table from the database in their HEX values

                Properties.Settings.Default.KitsStart = txtPlayStartOff.Text;
                Properties.Settings.Default.KitsEnd = txtPlayEndOff.Text;
                Properties.Settings.Default.PlayStartInt = Convert.ToInt32(txtPlayStartOffInt.Text);
                Properties.Settings.Default.PlayEndInt = Convert.ToInt32(txtPlayEndOffInt.Text);

                //Teams
                Properties.Settings.Default.TeamsStart = txtTeamStartOff.Text;
                Properties.Settings.Default.TeamsEnd = txtTeamEndOff.Text;
                Properties.Settings.Default.TeamsStartInt = Convert.ToInt32(txtTeamStartOffInt.Text);
                Properties.Settings.Default.TeamsEndInt = Convert.ToInt32(txtTeamEndOffInt.Text);

                //Kits
                Properties.Settings.Default.KitsStart = txtKitsStartOff.Text;
                Properties.Settings.Default.KitsEnd = txtKitsEndOff.Text;
                Properties.Settings.Default.KitsStartInt = Convert.ToInt32(txtKitsStartOffInt.Text);
                Properties.Settings.Default.KitsEndint = Convert.ToInt32(txtKitsEndOffInt.Text);

                //Lineups
                Properties.Settings.Default.LinStart = txtLinStartOff.Text;
                Properties.Settings.Default.LinEnd = txtLinEndOff.Text;
                Properties.Settings.Default.LinStartInt = Convert.ToInt32(txtLinStartOffInt.Text);
                Properties.Settings.Default.LinEndInt = Convert.ToInt32(txtLinEndOffInt.Text);


                //Fixtures
                Properties.Settings.Default.FixStart = txtFixStart.Text;
                Properties.Settings.Default.FixEnd = txtFixEnd.Text;
                //Properties.Settings.Default.LinStartInt = Convert.ToInt32(txtLinStartOffInt.Text);
                //Properties.Settings.Default.LinEndInt = Convert.ToInt32(txtLinEndOffInt.Text);

                //Comps
                Properties.Settings.Default.CompStart = txtCompStart.Text;
                Properties.Settings.Default.CompEnd = txtCompEnd.Text;
                Properties.Settings.Default.CompStartInt = Convert.ToInt32(txtCompStartint.Text);
                Properties.Settings.Default.CompEndInt = Convert.ToInt32(txtCompEndInt.Text);

                try
                {
                    MessageBox.Show("Testing - Players...");
                    byte[] bplayers = GetGlobalBytesFromFile(fileName, Properties.Settings.Default.PlayStart, Properties.Settings.Default.PlayEnd, Properties.Settings.Default.PlayStartInt, Properties.Settings.Default.PlayEndInt);
                    List<byte[]> playerssp = splitDataToByteArray(bplayers, 1348);
                }
                catch (Exception ze)
                {
                    MessageBox.Show("Failed - Players offsets not correct!");
                }
                try
                {
                    MessageBox.Show("Testing - Teams...");
                    byte[] bteams = GetGlobalBytesFromFile(fileName, Properties.Settings.Default.TeamsStart, Properties.Settings.Default.TeamsEnd, Properties.Settings.Default.TeamsStartInt, Properties.Settings.Default.TeamsEndInt);
                    List<byte[]> teamssp = splitDataToByteArray(bteams, 212); 

                }
                catch (Exception ze)
                {
                    MessageBox.Show("Failed - Teams offsets not correct!");
                }
                try
                    {
                    MessageBox.Show("Testing - Lineups...");
                    byte[] blineup = GetGlobalBytesFromFile(fileName, Properties.Settings.Default.LinStart, Properties.Settings.Default.LinEnd, Properties.Settings.Default.LinStartInt, Properties.Settings.Default.LinEndInt);
                        List<byte[]> lineupssp = splitDataToByteArray(blineup, 776);
                }
                catch (Exception ze)
                {
                    MessageBox.Show("Failed - Lineups offsets not correct!");
                }
                try
                        {
                    MessageBox.Show("Testing - Kits...");
                    byte[] btkits = GetGlobalBytesFromFile(fileName, Properties.Settings.Default.KitsStart, Properties.Settings.Default.KitsEnd, Properties.Settings.Default.KitsStartInt, Properties.Settings.Default.KitsEndint);
                    List<byte[]> kitssp = splitDataToByteArray(btkits, 280);
                }
                catch (Exception ze)
                {
                    MessageBox.Show("Failed - Kits offsets not correct!");
                }
                try
                {
                    MessageBox.Show("Testing - Fixtures...");
                    byte[] btfixtures = GetGlobalBytesFromFile(fileName, Properties.Settings.Default.FixStart, Properties.Settings.Default.FixEnd, 0, 0);
                    List<byte[]> fixssp = splitDataToByteArray(btfixtures, 16);
                }
                catch (Exception ze)
                {
                    MessageBox.Show("Failed - Fixtures offsets not correct!");
                }
                try
                {
                    MessageBox.Show("Testing - Competitions...");
                    byte[] btcomps = GetGlobalBytesFromFile(fileName, Properties.Settings.Default.CompStart, Properties.Settings.Default.CompEnd, Properties.Settings.Default.CompStartInt, Properties.Settings.Default.CompEndInt);
                    List<byte[]> compssp = splitDataToByteArray(btcomps, 100);
                }
                catch (Exception ze)
                {
                    MessageBox.Show("Failed - Competitions offsets not correct!");
                }

                //We create a new byte lists of each record, includes the size per record in bytes
                







            }


        }
    }
}
