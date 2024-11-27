#pragma warning disable IDE1006 // Naming Styles

using System.Security.Principal;
using System.Text;

namespace PoeFilterConfigure
{
    public partial class Form1 : Form
    {
        //DateTime _when = DateTime.MinValue;
        bool _focusOnGoal = false;

        public Form1()
        {
            m_initializing = true;
            InitializeComponent();

            Dictionary<RadioButton, CellsSelection> shieldCellSelection = new()
            {
                { rbShields4Cells, new CellsSelection(4, 4) },
                { rbShields6Cells, new CellsSelection(4, 6) },
                { rbShields8Cells, new CellsSelection(4, 8) },
            };

            Dictionary<RadioButton, CellsSelection> weaponCellSelection = new()
            {
                { rbWeapons3Cells, new CellsSelection(3, 3) },
                { rbWeapons4Cells, new CellsSelection(3, 4) },
                { rbWeapons6Cells, new CellsSelection(3, 6) },
                { rbWeapons8Cells, new CellsSelection(3, 8) },
            };

            m_checkboxGroups = new()
            {
                { EquipmentClass.Rings, new CheckboxGroup(this, EquipmentClass.Rings, rbRingAny, rbRingChaos, rbRingDivine, rbRingOff) },
                { EquipmentClass.Amulets, new CheckboxGroup(this, EquipmentClass.Amulets, rbAmuletsAny, rbAmuletsChaos, rbAmuletsDivine, rbAmuletsOff) },
                { EquipmentClass.Belts, new CheckboxGroup(this, EquipmentClass.Belts, rbBeltsAny, rbBeltsChaos, rbBeltsDivine, rbBeltsOff) },
                { EquipmentClass.BodyArmors, new CheckboxGroup(this, EquipmentClass.BodyArmors, rbBodyAny, rbBodyChaos, rbBodyDivine, rbBodyOff) },
                { EquipmentClass.Boots, new CheckboxGroup(this, EquipmentClass.Boots, rbBootsAny, rbBootsChaos, rbBootsDivine, rbBootsOff) },
                { EquipmentClass.Gloves, new CheckboxGroup(this, EquipmentClass.Gloves, rbGlovesAny, rbGlovesChaos, rbGlovesDivine, rbGlovesOff) },
                { EquipmentClass.Helmets, new CheckboxGroup(this, EquipmentClass.Helmets, rbHelmetsAny, rbHelmetsChaos, rbHelmetsDivine, rbHelmetsOff) },
                {
                    EquipmentClass.Shields,
                    new CheckboxGroup(this, EquipmentClass.Shields, rbShieldsAny, rbShieldsChaos, rbShieldsDivine, rbShieldsOff, shieldCellSelection)
                },
                {
                    EquipmentClass.Weapons,
                    new CheckboxGroup(this, EquipmentClass.Weapons, rbWeaponsAny, rbWeaponsChaos, rbWeaponsDivine, rbWeaponsOff, weaponCellSelection)
                },
            };

            ResetGoal();

            ReadUserProfile();


            System.Windows.Forms.Timer etaTimer = new();
            etaTimer.Interval = 1000 / 30; // 30 times a second
            etaTimer.Tick += new EventHandler(OnEtaTimer);
            etaTimer.Start();

            AllowDrop = true;
            DragEnter += new DragEventHandler(DragEnterHandler);
            DragDrop += new DragEventHandler(DragDropHandler);

            FileInfo fileInfo = new FileInfo(labelFilePath.Text);
            if (fileInfo.Exists)
            {
                try
                {
                    Scan(fileInfo);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
            else
            {
                labelFilePath.Text = "(Drag filter file HERE)";
                labelFilePath.ForeColor = Color.Green;
            }

            m_initializing = false;
        }

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

        class CellsSelection
        {
            readonly int _min;
            readonly int _max;
            internal CellsSelection(int min, int cells) { _min = min; _max = cells; }
            public int Count { get { return _max; } }
            public IEnumerable<Tuple<int, string>> Dimensions
            {
                get
                {
                    if (_min <= 3 && _max >= 3)
                    {
                        yield return Tuple.Create(3, "\tWidth 1\n\tHeight 3");
                    }

                    if (_min <= 4 && _max >= 4)
                    {
                        yield return Tuple.Create(4, "\tWidth 2\n\tHeight 2");
                    }

                    if (_min <= 6 && _max >= 6)
                    {
                        yield return Tuple.Create(6, "\tWidth 2\n\tHeight 3");
                    }

                    if (_min <= 8 && _max >= 8)
                    {
                        yield return Tuple.Create(8, "\tWidth 2\n\tHeight 4");
                    }

                    yield break;
                }
            }
        }

        class CheckboxGroup
        {
            readonly Form1 _parent;
            readonly EquipmentClass _equipmentClass;
            readonly RadioButton _any;
            readonly RadioButton _chaos;
            readonly RadioButton _divine;
            readonly RadioButton _off;
            readonly Dictionary<RadioButton, CellsSelection>? _cellsSelections;

            internal CheckboxGroup(Form1 parent, EquipmentClass equipmentClass, RadioButton any, RadioButton chaos, RadioButton divine, RadioButton off, Dictionary<RadioButton, CellsSelection>? cellsSelections = null)
            {
                _parent = parent;
                _equipmentClass = equipmentClass;
                _any = any;
                _chaos = chaos;
                _divine = divine;
                _off = off;
                _cellsSelections = cellsSelections;

                EnableButtons(false);
            }

            internal Tuple<RadioButton, CellsSelection> GetCellSelection(int cells)
            {
                if (_cellsSelections == null)
                    throw new Exception($"Can not query {_equipmentClass} for cell selections");

                KeyValuePair<RadioButton, CellsSelection>[] match = _cellsSelections.Where(a => a.Value.Count == cells).ToArray();
                if (match.Length == 0)
                    throw new Exception($"No entry in {_equipmentClass} with a cell count of {cells}");
                else if (match.Length > 1)
                    throw new Exception($"Multiple entries in {_equipmentClass} with a cell count of {cells}");

                return Tuple.Create(match[0].Key, match[0].Value);
            }

            internal void OnCellsCheck(RadioButton rb)
            {
                if (!rb.Checked)
                {
                    return;
                }

                try
                {
                    if (_cellsSelections == null)
                    {
                        throw new Exception($"No cell selection set defined for {_equipmentClass}");
                    }

                    if (_cellsSelections.TryGetValue(rb, out CellsSelection? selection))
                    {
                        _parent.UpdateCells(_equipmentClass, selection);
                    }
                    else
                    {
                        throw new Exception($"No cell selection defined for radio button {rb.Name}");
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }

            internal void OnCheck(RadioButton rb)
            {
                if (!rb.Checked)
                {
                    return;
                }

                try
                {
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
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
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
                foreach (CheckboxGroup group in groups)
                {
                    group.EnableButtons(true);
                }
            }
        };

        private const int c_version = 1;
        private const string c_PFCSTART = "#PFC-START";
        private const string c_SHOWTAG = "PFCSHOW";
        private const string c_PFCEND = "#PFC-END";

        private bool m_initializing = false;
        private Dictionary<EquipmentClass, CheckboxGroup> m_checkboxGroups;
        private Dictionary<EquipmentClass, SelectionLevelRange> m_selectionLevelRangeConfiguration = new();
        private Dictionary<EquipmentClass, CellsSelection> m_cellSelectionConfiguration = new();
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
            rbShields4Cells.Checked = true;
            rbWeapons3Cells.Checked = true;
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
                        if (defs == null || defs.Length < 2 || defs.Length > 3)
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

                        m_selectionLevelRangeConfiguration[equipClass] = selectionLevelRange;
                        m_checkboxGroups[equipClass].SetSelectionLevelRange(selectionLevelRange);

                        if (defs.Length == 3)
                        {
                            if (equipClass == EquipmentClass.Shields || equipClass == EquipmentClass.Weapons)
                            {
                                if (!int.TryParse(defs[2], out int cells))
                                {
                                    throw new Exception($"Bad cell count: {line}");
                                }

                                Tuple<RadioButton, CellsSelection> match = m_checkboxGroups[equipClass].GetCellSelection(cells);
                                match.Item1.Checked = true;
                                m_cellSelectionConfiguration[equipClass] = match.Item2;
                            }
                        }
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

            var accessControl = fileInfo.GetAccessControl();
            string accountName = WindowsIdentity.GetCurrent().Name;
            System.Security.AccessControl.FileSystemAccessRule rule =
                new System.Security.AccessControl.FileSystemAccessRule(
                    accountName,
                    System.Security.AccessControl.FileSystemRights.Modify,
                    System.Security.AccessControl.AccessControlType.Allow);
            accessControl.AddAccessRule(rule);
            fileInfo.SetAccessControl(accessControl);

            //File.SetUnixFileMode(fileInfo.FullName, UnixFileMode.UserWrite | UnixFileMode.UserRead | UnixFileMode.GroupRead | UnixFileMode.GroupWrite);
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
            AddFilters(sb, EquipmentClass.Rings, "\"Rings\"");
            AddFilters(sb, EquipmentClass.Amulets, "\"Amulets\"");
            AddFilters(sb, EquipmentClass.Belts, "\"Belts\"");
            AddFilters(sb, EquipmentClass.BodyArmors, "\"Body Armours\"");
            AddFilters(sb, EquipmentClass.Boots, "\"Boots\"");
            AddFilters(sb, EquipmentClass.Gloves, "\"Gloves\"");
            AddFilters(sb, EquipmentClass.Helmets, "\"Helmets\"");
            AddFilters(sb, EquipmentClass.Shields, "\"Shields\"");
            AddFilters(sb, EquipmentClass.Weapons, "\"Bows\" \"Claws\" \"Daggers\" \"One Hand Axes\" \"One Hand Maces\" \"One Hand Swords\" \"Rune Daggers\" \"Sceptres\" \"Staves\" \"Thrusting One Hand Swords\" \"Two Hand Axes\" \"Two Hand Maces\" \"Two Hand Swords\" \"Wands\" \"Warstaves\"");
            return sb.ToString();
        }

        private void AddFilters(StringBuilder sb, EquipmentClass equipClass, string classText)
        {
            if (!m_selectionLevelRangeConfiguration.TryGetValue(equipClass, out SelectionLevelRange selectionLevelRange))
            {
                throw new Exception($"Uninitialized equipment class {equipClass}");
            }

            if (selectionLevelRange == SelectionLevelRange.Off)
            {
                return;
            }

            if (m_cellSelectionConfiguration.TryGetValue(equipClass, out CellsSelection? cellsSelection))
            {
                foreach (Tuple<int, string> dimensions in cellsSelection.Dimensions)
                {
                    AddFilter(sb, equipClass, classText, selectionLevelRange, dimensions);
                }
            }
            else
            {
                AddFilter(sb, equipClass, classText, selectionLevelRange, null);
            }
        }

        private static void AddFilter(StringBuilder sb, EquipmentClass equipClass, string classText, SelectionLevelRange selectionLevelRange, Tuple<int, string>? dimensions)
        {
            sb.AppendLine("");

            if (dimensions != null)
            {
                sb.AppendLine($"Show # {c_SHOWTAG} {equipClass} {selectionLevelRange} {dimensions.Item1}");
            }
            else
            {
                sb.AppendLine($"Show # {c_SHOWTAG} {equipClass} {selectionLevelRange}");
            }

            sb.AppendLine("\tRarity Rare");
            sb.AppendLine("\tIdentified False");
            sb.AppendLine($"\tClass == {classText}");

            if (dimensions != null)
            {
                sb.AppendLine(dimensions.Item2);
            }

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
            sb.AppendLine("\tMinimapIcon 2 Purple Diamond");
            sb.AppendLine("\tSetTextColor 0 0 0 255");
            sb.AppendLine("\tSetBorderColor 0 0 0 255");
            sb.AppendLine("\tSetBackgroundColor 153 102 204 255");
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
            var backupPath = $"{fileInfo.DirectoryName}/{filenameWithoutExtension}_pfc{fileInfo.Extension}";

            FileInfo backupInfo = new FileInfo(backupPath);
            if (!backupInfo.Exists)
            {
                File.Copy(fileInfo.FullName, backupInfo.FullName);
            }
        }

        private void WriteFile()
        {
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

        void Update(EquipmentClass equipmentClass, SelectionLevelRange selectionLevelRange)
        {
            m_selectionLevelRangeConfiguration[equipmentClass] = selectionLevelRange;
            if (m_initializing)
                return;

            WriteFile();
        }

        private void UpdateCells(EquipmentClass equipmentClass, CellsSelection selection)
        {
            m_cellSelectionConfiguration[equipmentClass] = selection;
            if (m_initializing)
                return;

            WriteFile();
        }

        private FileInfo ConfigFilePath
        {
            get
            {
                string userRoamingPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                return new(Path.Combine(userRoamingPath, "PoeFilterConfigure", "config.txt"));
            }
        }

        private void ReadUserProfile()
        {
            FileInfo configFilePath = ConfigFilePath;
            if (!configFilePath.Exists)
            {
                return;
            }

            using StreamReader sr = new(configFilePath.FullName);
            if (!(sr.ReadLine() is string configurationLine))
            {
                return;
            }

            if (!int.TryParse(configurationLine, out int configurationVersion))
            {
                return;
            }

            if (configurationVersion != c_version)
            {
                return;
            }

            if (!(sr.ReadLine() is string fileLine))
            {
                return;
            }

            FileInfo fileInfo = new FileInfo(fileLine);
            if (fileInfo.Exists)
            {
                labelFilePath.Text = fileInfo.FullName;
            }
        }

        private void WriteUserProfile()
        {
            DirectoryInfo? configDirectory = ConfigFilePath.Directory;
            if (configDirectory == null)
            {
                throw new Exception($"Failed to find directory for {ConfigFilePath.FullName}");
            }

            if (!configDirectory.Exists)
            {
                configDirectory.Create();
            }

            using StreamWriter sw = new(ConfigFilePath.FullName);
            sw.WriteLine($"{c_version}");
            sw.WriteLine(labelFilePath.Text);
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
                WriteUserProfile();

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

        private void rbShields_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is RadioButton rb)
            {
                m_checkboxGroups[EquipmentClass.Shields].OnCheck(rb);
            }
        }

        private void rbShields_CellsCheckedChanged(object sender, EventArgs e)
        {
            if (sender is RadioButton rb)
            {
                m_checkboxGroups[EquipmentClass.Shields].OnCellsCheck(rb);
            }
        }

        private void rbWeapons_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is RadioButton rb)
            {
                m_checkboxGroups[EquipmentClass.Weapons].OnCheck(rb);
            }
        }

        private void rbWeapons_CellsCheckedChanged(object sender, EventArgs e)
        {
            if (sender is RadioButton rb)
            {
                m_checkboxGroups[EquipmentClass.Weapons].OnCellsCheck(rb);
            }
        }

        private float _goal = 0;
        private AdaptiveETA? _adaptiveEta = null;
        private float? _firstData = null;
        private float? _latestData = null;

        private void textBoxGoal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData != Keys.Enter)
            {
                return;
            }

            if (float.TryParse(textBoxGoal.Text, out float goal))
            {
                _goal = goal;
                _adaptiveEta = new AdaptiveETA(goal);
                textBoxGoal.Hide();
                label2.Hide();

                textBoxUpdate.Text = string.Empty;
                textBoxUpdate.Show();
                label6.Show();

                lblGoal.Text = goal.ToString();
                lblGoal.Show();
                label5.Show();

                buttonResetETA.Show();

                _firstData = null;
                _latestData = null;

                e.Handled = true;
                e.SuppressKeyPress = true;

                textBoxUpdate.Focus();
            }
            else
            {
                //System.Media.SystemSounds.Asterisk.Play();
                textBoxGoal.Text = string.Empty;
                textBoxGoal.Focus();
            }
        }

        private void textBoxUpdate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData != Keys.Enter)
            {
                return;
            }

            if (float.TryParse(textBoxUpdate.Text, out float value))
            {
                textBoxUpdate.Text = string.Empty;
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else
            {
                textBoxUpdate.Text = string.Empty;
                return;
            }

            lblLastUpdate.Text = value.ToString();
            lblLastUpdate.Show();
            label3.Show();

            if (_adaptiveEta == null)
                throw new Exception("Why isn't adaptive eta initialized?");
            _adaptiveEta.Add(value);

            if (_firstData == null)
            {
                _firstData = value;
                return;
            }

            label1.Show();
            label4.Show();
            lblRate.Show();
            lblETA_duration.Show();
            lblETA_time.Show();
            lblETA_estimateValue.Show();
            lblETA_evText.Show();

            _latestData = value;

            UpdateETADisplay();
        }

        private void OnEtaTimer(Object? myObject, EventArgs myEventArgs)
        {
            UpdateETADisplay();

            if (_focusOnGoal)
            {
                if (textBoxGoal.Visible)
                {
                    textBoxGoal.Focus();
                }

                _focusOnGoal = false;
            }
        }

        private void UpdateETADisplay()
        {
            if (_adaptiveEta == null)
                return;

            DateTime now = DateTime.Now;
            (double currentEstimate, DateTime when, double ratePerSecond) = _adaptiveEta.GetEstimate(now);

            if (currentEstimate == double.NaN)
            {
                lblETA_estimateValue.Hide();
                lblETA_evText.Hide();
            }
            else
            {
                lblETA_evText.Show();
                lblETA_estimateValue.Show();
                lblETA_estimateValue.Text = $"{Math.Floor(currentEstimate)}";
            }

            if (ratePerSecond == double.NaN)
            {
                lblRate.Text = $"-na-";
            }
            else if (ratePerSecond < (1f / 3600f))
            {
                lblRate.Text = $"{ratePerSecond * (24f * 3600f)} / day";
            }
            else if (ratePerSecond < (1f / 60f))
            {
                lblRate.Text = $"{ratePerSecond * 3600f:F1} / hour";
            }
            else if (ratePerSecond < 1f)
            {
                lblRate.Text = $"{ratePerSecond * 60f:F1} / min";
            }
            else
            {
                lblRate.Text = $"{ratePerSecond:F1} / sec";
            }

            if (when == DateTime.MinValue || when == DateTime.MaxValue)
            {
                lblETA_duration.Hide();
                lblETA_time.Hide();

                return;
            }

            lblETA_duration.Show();

            if (when <= now)
            {
                lblETA_duration.Text = "Achieved";
                lblETA_time.Hide();
                return;
            }

            lblETA_time.Show();

            double secondsToGoal = (when - now).TotalSeconds;
            if (when.Day == now.Day)
            {
                lblETA_time.Text = $"{when.ToShortTimeString()}";
            }
            else if (secondsToGoal < 3600 * 24 * 5)
            {
                lblETA_time.Text = $"{when.DayOfWeek} {when.ToShortTimeString()}";
            }
            else
            {
                lblETA_time.Text = when.ToString();
            }

            if (secondsToGoal < 90.0)
            {
                lblETA_duration.Text = $"{Math.Round(secondsToGoal)} seconds";
            }
            else if (secondsToGoal < 3600.0)
            {
                int min = (int)secondsToGoal / 60;
                int sec = (int)Math.Round(secondsToGoal - (min * 60));
                lblETA_duration.Text = $"{min}m {sec}s";
            }
            else if (secondsToGoal < 3600 * 36)
            {
                int hrs = (int)secondsToGoal / 3600;
                int sec = (int)Math.Round(secondsToGoal - (hrs * 3600));
                lblETA_duration.Text = $"{hrs}h {sec / 60}m";
            }
            else
            {
                int days = (int)secondsToGoal / (24 * 3600);
                int sec = (int)Math.Round(secondsToGoal - (days * 24 * 3600));
                lblETA_duration.Text = $"{days} days {sec / 3600} hours";
            }
        }

        private void buttonResetETA_Click(object sender, EventArgs e)
        {
            ResetGoal();
        }

        private void ResetGoal()
        {
            _adaptiveEta = null;

            lblGoal.Hide();
            lblETA_time.Hide();
            lblETA_duration.Hide();
            lblETA_estimateValue.Hide();
            lblETA_evText.Hide();
            lblLastUpdate.Hide();
            lblRate.Hide();
            label1.Hide();
            label3.Hide();
            label4.Hide();
            label5.Hide();

            textBoxGoal.Text = string.Empty;
            textBoxGoal.Show();
            label2.Show();

            textBoxUpdate.Text = string.Empty;
            textBoxUpdate.Hide();
            label6.Hide();

            buttonResetETA.Hide();
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            _focusOnGoal = true;
        }
    }
}
