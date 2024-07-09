#pragma warning disable IDE1006 // Naming Styles

using System.Text;

namespace PoeFilterConfigure
{
    public partial class Form1 : Form
    {
        enum EquipmentClass
        {
            Rings,
            Amulets,
            Belts,
            Shields,
            Weapons,
            BodyArmors,
            Boots,
            Gloves,
            Helmets,
        }

        enum SelectionLevelRange
        {
            Off,
            Any,
            ChaosOnly,
            DivineOnly,
        }

        private const string c_PFCSTART = "#PFC-START";
        private const string c_PFCEND = "#PFC-END";
        private bool m_initializing = false;
        private Dictionary<EquipmentClass, SelectionLevelRange> m_configuration = new();
        private object m_lock = new();

        void Update(FileInfo fileInfo)
        {
            lock (m_lock)
            {
                string ourFilters = GenerateFilters();
                SplitFile(fileInfo, out string beforeLine, out string lineAndAfter);
                WriteFile(fileInfo, beforeLine, ourFilters, lineAndAfter);
            }
        }

        private void WriteFile(FileInfo fileInfo, string beforeLine, string ourFilters, string lineAndAfter)
        {
            using (StreamWriter sw = File.CreateText(fileInfo.FullName))
            {
                sw.WriteLine(beforeLine.TrimEnd());
                sw.WriteLine("");
                sw.WriteLine(c_PFCSTART);
                sw.WriteLine(ourFilters);
                sw.WriteLine(c_PFCEND);
                sw.WriteLine("");
                sw.Write(lineAndAfter.TrimStart());
            }
        }

        void Create(FileInfo fileInfo)
        {
            lock (m_lock)
            {
                string ourFilters = GenerateFilters();
                SplitFileAtLineContaining(fileInfo, "tier->raresendgame", out string beforeLine, out string lineAndAfter);
                WriteFile(fileInfo, beforeLine, ourFilters, lineAndAfter);
            }
        }

        private string GenerateFilters()
        {
            StringBuilder sb = new();
            AddFilter(sb, m_configuration[EquipmentClass.Rings], "\"Rings\"");
            AddFilter(sb, m_configuration[EquipmentClass.Amulets], "\"Amulets\"");
            AddFilter(sb, m_configuration[EquipmentClass.Belts], "\"Belts\"");
            AddFilter(sb, m_configuration[EquipmentClass.BodyArmors], "\"Body Armours\"");
            AddFilter(sb, m_configuration[EquipmentClass.Boots], "\"Boots\"");
            AddFilter(sb, m_configuration[EquipmentClass.Gloves], "\"Gloves\"");
            AddFilter(sb, m_configuration[EquipmentClass.Helmets], "\"Helmets\"");
            AddFilter(sb, m_configuration[EquipmentClass.Shields], "\"Shields\"");
            AddFilter(sb, m_configuration[EquipmentClass.Weapons], "\"Wands\" \"Daggers\" \"Claws\"");
            return sb.ToString();
        }

        private void AddFilter(StringBuilder sb, SelectionLevelRange selectionLevelRange, string classText)
        {
            if (selectionLevelRange == SelectionLevelRange.Off)
                return;

            sb.AppendLine("");
            sb.AppendLine("Show # PFC");
            sb.AppendLine("\tRarity Rare");
            sb.AppendLine($"\tClass == {classText}");

            if (selectionLevelRange == SelectionLevelRange.DivineOnly)
            {
                sb.AppendLine("\tItemLevel >= 75");
            }
            else if (selectionLevelRange == SelectionLevelRange.ChaosOnly)
            {
                sb.AppendLine("\tItemLevel >= 60");
                sb.AppendLine("\tItemLevel <= 74");
            }
            else if (selectionLevelRange == SelectionLevelRange.Any)
            {
                sb.AppendLine("\tItemLevel >= 60");
            }

            sb.AppendLine("\tSetFontSize 45");
            sb.AppendLine("\tMinimapIcon 2 Grey Diamond");
            sb.AppendLine("\tSetBackgroundColor 132 86 60 255");
        }

        private void SplitFile(FileInfo fileInfo, out string beforeLine, out string lineAndAfter)
        {
            using (StreamReader sr = new(fileInfo.FullName))
            {
                StringBuilder sbBefore = new();
                StringBuilder sbAfter = new();
                bool foundStart = false;
                bool foundEnd = false;
                string? line = sr.ReadLine();
                while (line != null)
                {
                    if (foundStart == false)
                    {
                        if (line.Contains(c_PFCSTART))
                        {
                            foundStart = true;
                        }
                        else
                        {
                            sbBefore.AppendLine(line);
                        }
                    }
                    else if (foundEnd == false)
                    {
                        if (line.Contains(c_PFCEND))
                        {
                            foundEnd = true;
                        }
                    }
                    else
                    {
                        sbAfter.AppendLine(line);
                    }

                    line = sr.ReadLine();
                }

                beforeLine = sbBefore.ToString();
                lineAndAfter = sbAfter.ToString();
            }
        }

        private void SplitFileAtLineContaining(FileInfo fileInfo, string match, out string beforeLine, out string lineAndAfter)
        {
            using (StreamReader sr = new(fileInfo.FullName))
            {
                StringBuilder sbBefore = new();
                StringBuilder sbAfter = new();
                bool foundMatch = false;
                string? line = sr.ReadLine();
                while (line != null)
                {
                    if (line.Contains(match))
                    {
                        foundMatch = true;
                    }

                    if (foundMatch)
                    {
                        sbAfter.AppendLine(line);
                    }
                    else
                    {
                        sbBefore.AppendLine(line);
                    }

                    line = sr.ReadLine();
                }

                beforeLine = sbBefore.ToString();
                lineAndAfter = sbAfter.ToString();
            }
        }

        void Backup(FileInfo fileInfo)
        {
            var filenameWithoutExtension = Path.GetFileNameWithoutExtension(fileInfo.Name);
            var backupPath =  $"{fileInfo.DirectoryName}/{filenameWithoutExtension}_pfc{fileInfo.Extension}";
            
            FileInfo backupInfo = new FileInfo(backupPath);
            if (!backupInfo.Exists)
            {
                File.Copy(fileInfo.FullName, backupInfo.FullName);
            }
        }

        void Update(EquipmentClass equipmentClass, SelectionLevelRange selectionLevelRange)
        {
            try
            {
                m_configuration[equipmentClass] = selectionLevelRange;
                if (m_initializing)
                    return;

                FileInfo fileInfo = new FileInfo(labelFilePath.Text);
                if (!fileInfo.Exists)
                {
                    throw new Exception($"{fileInfo.FullName} does not exist");
                }

                string fileContents;
                using (StreamReader sr = new(fileInfo.FullName))
                {
                    fileContents = sr.ReadToEnd();
                }

                if (fileContents.Length < 10)
                    throw new Exception($"{labelFilePath.Text} is pretty short");

                if (!fileContents.Contains(c_PFCSTART))
                {
                    Backup(fileInfo);
                    Create(fileInfo);
                }
                else
                {
                    Update(fileInfo);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public Form1()
        {
            m_initializing = true;
            InitializeComponent();

            rbRingOff.Checked = true;
            rbAmuletsOff.Checked = true;
            rbBeltsOff.Checked = true;
            rbShieldsOff.Checked = true;
            rbWeaponsOff.Checked = true;
            rbBodyOff.Checked = true;
            rbBootsOff.Checked = true;
            rbGlovesOff.Checked = true;
            rbHelmetsOff.Checked = true;

            m_initializing = false;
        }

        private void rbRings_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is RadioButton rb)
            {
                if (rb.Checked)
                {
                    if (rb == rbRingAny) { Update(EquipmentClass.Rings, SelectionLevelRange.Any); }
                    else if (rb == rbRingChaos) { Update(EquipmentClass.Rings, SelectionLevelRange.ChaosOnly); }
                    else if (rb == rbRingDivine) { Update(EquipmentClass.Rings, SelectionLevelRange.DivineOnly); }
                    else if (rb == rbRingOff) { Update(EquipmentClass.Rings, SelectionLevelRange.Off); }
                    else throw new Exception($"Bad radio button {rb}");
                }
            }
        }

        private void rbAmulets_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is RadioButton rb)
            {
                if (rb.Checked)
                {
                    if (rb == rbAmuletsAny) { Update(EquipmentClass.Amulets, SelectionLevelRange.Any); }
                    else if (rb == rbAmuletsChaos) { Update(EquipmentClass.Amulets, SelectionLevelRange.ChaosOnly); }
                    else if (rb == rbAmuletsDivine) { Update(EquipmentClass.Amulets, SelectionLevelRange.DivineOnly); }
                    else if (rb == rbAmuletsOff) { Update(EquipmentClass.Amulets, SelectionLevelRange.Off); }
                    else throw new Exception($"Bad radio button {rb}");
                }
            }
        }

        private void rbBelts_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is RadioButton rb)
            {
                if (rb.Checked)
                {
                    if (rb == rbBeltsAny) { Update(EquipmentClass.Belts, SelectionLevelRange.Any); }
                    else if (rb == rbBeltsChaos) { Update(EquipmentClass.Belts, SelectionLevelRange.ChaosOnly); }
                    else if (rb == rbBeltsDivine) { Update(EquipmentClass.Belts, SelectionLevelRange.DivineOnly); }
                    else if (rb == rbBeltsOff) { Update(EquipmentClass.Belts, SelectionLevelRange.Off); }
                    else throw new Exception($"Bad radio button {rb}");
                }
            }
        }

        private void rbShields_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is RadioButton rb)
            {
                if (rb.Checked)
                {
                    if (rb == rbShieldsAny) { Update(EquipmentClass.Shields, SelectionLevelRange.Any); }
                    else if (rb == rbShieldsChaos) { Update(EquipmentClass.Shields, SelectionLevelRange.ChaosOnly); }
                    else if (rb == rbShieldsDivine) { Update(EquipmentClass.Shields, SelectionLevelRange.DivineOnly); }
                    else if (rb == rbShieldsOff) { Update(EquipmentClass.Shields, SelectionLevelRange.Off); }
                    else throw new Exception($"Bad radio button {rb}");
                }
            }
        }

        private void rbWeapons_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is RadioButton rb)
            {
                if (rb.Checked)
                {
                    if (rb == rbWeaponsAny) { Update(EquipmentClass.Weapons, SelectionLevelRange.Any); }
                    else if (rb == rbWeaponsChaos) { Update(EquipmentClass.Weapons, SelectionLevelRange.ChaosOnly); }
                    else if (rb == rbWeaponsDivine) { Update(EquipmentClass.Weapons, SelectionLevelRange.DivineOnly); }
                    else if (rb == rbWeaponsOff) { Update(EquipmentClass.Weapons, SelectionLevelRange.Off); }
                    else throw new Exception($"Bad radio button {rb}");
                }
            }
        }

        private void rbChest_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is RadioButton rb)
            {
                if (rb.Checked)
                {
                    if (rb == rbBodyAny) { Update(EquipmentClass.BodyArmors, SelectionLevelRange.Any); }
                    else if (rb == rbBodyChaos) { Update(EquipmentClass.BodyArmors, SelectionLevelRange.ChaosOnly); }
                    else if (rb == rbBodyDivine) { Update(EquipmentClass.BodyArmors, SelectionLevelRange.DivineOnly); }
                    else if (rb == rbBodyOff) { Update(EquipmentClass.BodyArmors, SelectionLevelRange.Off); }
                    else throw new Exception($"Bad radio button {rb}");
                }
            }
        }

        private void rbBoots_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is RadioButton rb)
            {
                if (rb.Checked)
                {
                    if (rb == rbBootsAny) { Update(EquipmentClass.Boots, SelectionLevelRange.Any); }
                    else if (rb == rbBootsChaos) { Update(EquipmentClass.Boots, SelectionLevelRange.ChaosOnly); }
                    else if (rb == rbBootsDivine) { Update(EquipmentClass.Boots, SelectionLevelRange.DivineOnly); }
                    else if (rb == rbBootsOff) { Update(EquipmentClass.Boots, SelectionLevelRange.Off); }
                    else throw new Exception($"Bad radio button {rb}");
                }
            }
        }

        private void rbGloves_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is RadioButton rb)
            {
                if (rb.Checked)
                {
                    if (rb == rbGlovesAny) { Update(EquipmentClass.Gloves, SelectionLevelRange.Any); }
                    else if (rb == rbGlovesChaos) { Update(EquipmentClass.Gloves, SelectionLevelRange.ChaosOnly); }
                    else if (rb == rbGlovesDivine) { Update(EquipmentClass.Gloves, SelectionLevelRange.DivineOnly); }
                    else if (rb == rbGlovesOff) { Update(EquipmentClass.Gloves, SelectionLevelRange.Off); }
                    else throw new Exception($"Bad radio button {rb}");
                }
            }
        }

        private void rbHelmets_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is RadioButton rb)
            {
                if (rb.Checked)
                {
                    if (rb == rbHelmetsAny) { Update(EquipmentClass.Helmets, SelectionLevelRange.Any); }
                    else if (rb == rbHelmetsChaos) { Update(EquipmentClass.Helmets, SelectionLevelRange.ChaosOnly); }
                    else if (rb == rbHelmetsDivine) { Update(EquipmentClass.Helmets, SelectionLevelRange.DivineOnly); }
                    else if (rb == rbHelmetsOff) { Update(EquipmentClass.Helmets, SelectionLevelRange.Off); }
                    else throw new Exception($"Bad radio button {rb}");
                }
            }
        }
    }
}
