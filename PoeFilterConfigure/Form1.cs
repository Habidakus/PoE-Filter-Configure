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

        class CheckboxGroup
        {
            readonly Form1 _parent;
            readonly EquipmentClass _equipmentClass;
            readonly RadioButton _any;
            readonly RadioButton _chaos;
            readonly RadioButton _divine;
            readonly RadioButton _off;

            internal CheckboxGroup(Form1 parent, EquipmentClass equipmentClass, RadioButton any, RadioButton chaos, RadioButton divine, RadioButton off)
            {
                _parent = parent;
                _equipmentClass = equipmentClass;
                _any = any;
                _chaos = chaos;
                _divine = divine;
                _off = off;

                EnableButtons(false);
            }

            internal void OnCheck(RadioButton rb)
            {
                if (!rb.Checked)
                {
                    return;
                }

                if (rb == _any)
                {
                    _parent.Update(_equipmentClass, SelectionLevelRange.Any);
                }
                else if (rb == _chaos)
                {
                    _parent.Update(_equipmentClass, SelectionLevelRange.ChaosOnly);
                }
                else if (rb == _divine)
                {
                    _parent.Update(_equipmentClass, SelectionLevelRange.DivineOnly);
                }
                else if (rb == _off)
                {
                    _parent.Update(_equipmentClass, SelectionLevelRange.Off);
                }
                else
                {
                    throw new Exception($"Bad radio button {rb}");
                }
            }

            internal void SetSelectionLevelRange(SelectionLevelRange selectionLevelRange)
            {
                if (selectionLevelRange == SelectionLevelRange.Any)
                    _any.Checked = true;
                else if (selectionLevelRange == SelectionLevelRange.ChaosOnly)
                    _chaos.Checked = true;
                else if (selectionLevelRange == SelectionLevelRange.DivineOnly)
                    _divine.Checked = true;
                else
                    _off.Checked = true;
            }

            private void EnableButtons(bool enabled)
            {
                _any.Enabled = enabled;
                _chaos.Enabled = enabled;
                _divine.Enabled = enabled;
                _off.Enabled = enabled;
            }

            internal static void EnableButtons(CheckboxGroup[] groups)
            {
                foreach(CheckboxGroup group in groups)
                {
                    group.EnableButtons(true);
                }
            }
        };

        private Dictionary<EquipmentClass, CheckboxGroup> m_checkboxGroups;

        private const int c_version = 1;
        private const string c_PFCSTART = "#PFC-START";
        private const string c_SHOWTAG = "PFCSHOW";
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

        void Scan(FileInfo fileInfo)
        {
            if (m_initializing == false)
            {
                throw new Exception("Can only call Scan() when initializing");
            }

            rbRingOff.Checked = true;
            rbAmuletsOff.Checked = true;
            rbBeltsOff.Checked = true;
            rbShieldsOff.Checked = true;
            rbWeaponsOff.Checked = true;
            rbBodyOff.Checked = true;
            rbBootsOff.Checked = true;
            rbGlovesOff.Checked = true;
            rbHelmetsOff.Checked = true;
            labelFilePath.ForeColor = Color.Black;
            CheckboxGroup.EnableButtons(m_checkboxGroups.Values.ToArray());

            lock (m_lock)
            {
                using StreamReader sr = new(fileInfo.FullName);
                string? line = sr.ReadLine();
                while (line != null)
                {
                    if (line.Contains(c_SHOWTAG))
                    {
                        string[] lineparts = line.Split(c_SHOWTAG, StringSplitOptions.TrimEntries);
                        if (lineparts == null || lineparts.Length != 2)
                        {
                            throw new Exception($"Badly formed PFC line: {line}");
                        }

                        string[] defs = lineparts[1].Split(' ');
                        if (defs == null || defs.Length != 2)
                        {
                            throw new Exception($"Badly formed PFC comment: {line}");
                        }

                        if (!Enum.TryParse(defs[1], out SelectionLevelRange selectionLevelRange))
                        {
                            throw new Exception($"Badly formed PFC selection level range: {defs[1]}");
                        }

                        if (selectionLevelRange != SelectionLevelRange.Any && selectionLevelRange != SelectionLevelRange.ChaosOnly && selectionLevelRange != SelectionLevelRange.DivineOnly)
                        {
                            throw new Exception($"Badly formed PFC selecton level range: {line}");
                        }

                        if (!Enum.TryParse(defs[0], out EquipmentClass equipClass))
                        {
                            throw new Exception($"Badly formed PFC equipment class: {defs[0]}");
                        }

                        m_configuration[equipClass] = selectionLevelRange;
                        m_checkboxGroups[equipClass].SetSelectionLevelRange(selectionLevelRange);
                    }

                    line = sr.ReadLine();
                }
            }
        }

        private void WriteFile(FileInfo fileInfo, string beforeLine, string ourFilters, string lineAndAfter)
        {
            using (StreamWriter sw = File.CreateText(fileInfo.FullName))
            {
                sw.WriteLine(beforeLine.TrimEnd());
                sw.WriteLine("");
                sw.WriteLine($"{c_PFCSTART} v {c_version}");
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
                SplitFileAtLineContaining(fileInfo, "c6.rare.t4.all", out string beforeLine, out string lineAndAfter);
                WriteFile(fileInfo, beforeLine, ourFilters, lineAndAfter);
            }
        }

        private string GenerateFilters()
        {
            StringBuilder sb = new();
            AddFilter(sb, EquipmentClass.Rings, "\"Rings\"");
            AddFilter(sb, EquipmentClass.Amulets, "\"Amulets\"");
            AddFilter(sb, EquipmentClass.Belts, "\"Belts\"");
            AddFilter(sb, EquipmentClass.BodyArmors, "\"Body Armours\"");
            AddFilter(sb, EquipmentClass.Boots, "\"Boots\"");
            AddFilter(sb, EquipmentClass.Gloves, "\"Gloves\"");
            AddFilter(sb, EquipmentClass.Helmets, "\"Helmets\"");
            AddFilter(sb, EquipmentClass.Shields, "\"Shields\"");
            AddFilter(sb, EquipmentClass.Weapons, "\"Wands\" \"Daggers\" \"Claws\"");
            return sb.ToString();
        }

        private void AddFilter(StringBuilder sb, EquipmentClass equipClass, string classText)
        {
            if (!m_configuration.TryGetValue(equipClass, out SelectionLevelRange selectionLevelRange))
            {
                throw new Exception($"Uninitialized equipment class {equipClass}");
            }

            if (selectionLevelRange == SelectionLevelRange.Off)
                return;

            sb.AppendLine("");
            sb.AppendLine($"Show # {c_SHOWTAG} {equipClass} {selectionLevelRange}");
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

            m_checkboxGroups = new() {
                { EquipmentClass.Rings, new CheckboxGroup(this, EquipmentClass.Rings, rbRingAny, rbRingChaos, rbRingDivine, rbRingOff) },
                { EquipmentClass.Amulets, new CheckboxGroup(this, EquipmentClass.Amulets, rbAmuletsAny, rbAmuletsChaos, rbAmuletsDivine, rbAmuletsOff) },
                { EquipmentClass.Belts, new CheckboxGroup(this, EquipmentClass.Belts, rbBeltsAny, rbBeltsChaos, rbBeltsDivine, rbBeltsOff) },
                { EquipmentClass.Shields, new CheckboxGroup(this, EquipmentClass.Shields, rbShieldsAny, rbShieldsChaos, rbShieldsDivine, rbShieldsOff) },
                { EquipmentClass.Weapons, new CheckboxGroup(this, EquipmentClass.Weapons, rbWeaponsAny, rbWeaponsChaos, rbWeaponsDivine, rbWeaponsOff) },
                { EquipmentClass.BodyArmors, new CheckboxGroup(this, EquipmentClass.BodyArmors, rbBodyAny, rbBodyChaos, rbBodyDivine, rbBodyOff) },
                { EquipmentClass.Boots, new CheckboxGroup(this, EquipmentClass.Boots, rbBootsAny, rbBootsChaos, rbBootsDivine, rbBootsOff) },
                { EquipmentClass.Gloves, new CheckboxGroup(this, EquipmentClass.Gloves, rbGlovesAny, rbGlovesChaos, rbGlovesDivine, rbGlovesOff) },
                { EquipmentClass.Helmets, new CheckboxGroup(this, EquipmentClass.Helmets, rbHelmetsAny, rbHelmetsChaos, rbHelmetsDivine, rbHelmetsOff) },
            };


            AllowDrop = true;
            DragEnter += new DragEventHandler(DragEnterHandler);
            DragDrop += new DragEventHandler(DragDropHandler);

            FileInfo fileInfo = new FileInfo(labelFilePath.Text);
            if (fileInfo.Exists)
            {
                Scan(fileInfo);
            }
            else
            {
                labelFilePath.Text = "(Drag filter file HERE)";
                labelFilePath.ForeColor = Color.Green;
            }

            m_initializing = false;
        }

        private void DragEnterHandler(object? sender, DragEventArgs e)
        {
            FileInfo? fileInfo = GetValidFileInfoForFilterFile(e.Data);
            if (fileInfo != null)
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void DragDropHandler(object? sender, DragEventArgs e)
        {
            FileInfo? fileInfo = GetValidFileInfoForFilterFile(e.Data);
            if (fileInfo != null)
            {
                labelFilePath.Text = fileInfo.FullName;
                m_initializing = true;
                Scan(fileInfo);
                m_initializing = false;
            }
        }

        private FileInfo? GetValidFileInfoForFilterFile(IDataObject? dataObject)
        {
            if (dataObject != null)
            {
                if (dataObject.GetData(DataFormats.FileDrop) is string[] fileNames)
                {
                    if (fileNames.Length == 1)
                    {
                        FileInfo fileInfo = new FileInfo(fileNames.First());
                        if (fileInfo.Exists && fileInfo.Extension == ".filter")
                        {
                            return fileInfo;
                        }
                    }
                }
            }

            return null;
        }

        private void rbRings_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is RadioButton rb)
            {
                m_checkboxGroups[EquipmentClass.Rings].OnCheck(rb);
            }
        }

        private void rbAmulets_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is RadioButton rb)
            {
                m_checkboxGroups[EquipmentClass.Amulets].OnCheck(rb);
            }
        }

        private void rbBelts_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is RadioButton rb)
            {
                m_checkboxGroups[EquipmentClass.Belts].OnCheck(rb);
            }
        }

        private void rbShields_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is RadioButton rb)
            {
                m_checkboxGroups[EquipmentClass.Shields].OnCheck(rb);
            }
        }

        private void rbWeapons_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is RadioButton rb)
            {
                m_checkboxGroups[EquipmentClass.Weapons].OnCheck(rb);
            }
        }

        private void rbChest_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is RadioButton rb)
            {
                m_checkboxGroups[EquipmentClass.BodyArmors].OnCheck(rb);
            }
        }

        private void rbBoots_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is RadioButton rb)
            {
                m_checkboxGroups[EquipmentClass.Boots].OnCheck(rb);
            }
        }

        private void rbGloves_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is RadioButton rb)
            {
                m_checkboxGroups[EquipmentClass.Gloves].OnCheck(rb);
            }
        }

        private void rbHelmets_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is RadioButton rb)
            {
                m_checkboxGroups[EquipmentClass.Helmets].OnCheck(rb);
            }
        }
    }
}
