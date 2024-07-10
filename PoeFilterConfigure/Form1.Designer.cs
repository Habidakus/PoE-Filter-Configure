namespace PoeFilterConfigure
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            groupRing = new GroupBox();
            rbRingOff = new RadioButton();
            rbRingChaos = new RadioButton();
            rbRingAny = new RadioButton();
            rbRingDivine = new RadioButton();
            groupAmulets = new GroupBox();
            rbAmuletsOff = new RadioButton();
            rbAmuletsChaos = new RadioButton();
            rbAmuletsAny = new RadioButton();
            rbAmuletsDivine = new RadioButton();
            groupShields = new GroupBox();
            rbShieldsOff = new RadioButton();
            rbShieldsChaos = new RadioButton();
            rbShieldsAny = new RadioButton();
            rbShieldsDivine = new RadioButton();
            groupBelts = new GroupBox();
            rbBeltsOff = new RadioButton();
            rbBeltsChaos = new RadioButton();
            rbBeltsAny = new RadioButton();
            rbBeltsDivine = new RadioButton();
            groupBoots = new GroupBox();
            rbBootsOff = new RadioButton();
            rbBootsChaos = new RadioButton();
            rbBootsAny = new RadioButton();
            rbBootsDivine = new RadioButton();
            groupHelmets = new GroupBox();
            rbHelmetsOff = new RadioButton();
            rbHelmetsChaos = new RadioButton();
            rbHelmetsAny = new RadioButton();
            rbHelmetsDivine = new RadioButton();
            groupGloves = new GroupBox();
            rbGlovesOff = new RadioButton();
            rbGlovesChaos = new RadioButton();
            rbGlovesAny = new RadioButton();
            rbGlovesDivine = new RadioButton();
            groupBody = new GroupBox();
            rbBodyOff = new RadioButton();
            rbBodyChaos = new RadioButton();
            rbBodyAny = new RadioButton();
            rbBodyDivine = new RadioButton();
            groupWeapons = new GroupBox();
            rbWeaponsOff = new RadioButton();
            rbWeaponsChaos = new RadioButton();
            rbWeaponsAny = new RadioButton();
            rbWeaponsDivine = new RadioButton();
            labelFilePath = new Label();
            groupRing.SuspendLayout();
            groupAmulets.SuspendLayout();
            groupShields.SuspendLayout();
            groupBelts.SuspendLayout();
            groupBoots.SuspendLayout();
            groupHelmets.SuspendLayout();
            groupGloves.SuspendLayout();
            groupBody.SuspendLayout();
            groupWeapons.SuspendLayout();
            SuspendLayout();
            // 
            // groupRing
            // 
            groupRing.Controls.Add(rbRingOff);
            groupRing.Controls.Add(rbRingChaos);
            groupRing.Controls.Add(rbRingAny);
            groupRing.Controls.Add(rbRingDivine);
            groupRing.Location = new Point(12, 75);
            groupRing.Name = "groupRing";
            groupRing.Size = new Size(85, 138);
            groupRing.TabIndex = 0;
            groupRing.TabStop = false;
            groupRing.Text = "Rings";
            // 
            // rbRingOff
            // 
            rbRingOff.AutoSize = true;
            rbRingOff.Location = new Point(10, 25);
            rbRingOff.Name = "rbRingOff";
            rbRingOff.Size = new Size(51, 24);
            rbRingOff.TabIndex = 0;
            rbRingOff.TabStop = true;
            rbRingOff.Text = "Off";
            rbRingOff.UseVisualStyleBackColor = true;
            rbRingOff.CheckedChanged += rbRings_CheckedChanged;
            // 
            // rbRingChaos
            // 
            rbRingChaos.AutoSize = true;
            rbRingChaos.Location = new Point(10, 52);
            rbRingChaos.Name = "rbRingChaos";
            rbRingChaos.Size = new Size(68, 24);
            rbRingChaos.TabIndex = 1;
            rbRingChaos.TabStop = true;
            rbRingChaos.Text = "60-74";
            rbRingChaos.UseVisualStyleBackColor = true;
            rbRingChaos.CheckedChanged += rbRings_CheckedChanged;
            // 
            // rbRingAny
            // 
            rbRingAny.AutoSize = true;
            rbRingAny.Location = new Point(10, 79);
            rbRingAny.Name = "rbRingAny";
            rbRingAny.Size = new Size(56, 24);
            rbRingAny.TabIndex = 2;
            rbRingAny.TabStop = true;
            rbRingAny.Text = "60+";
            rbRingAny.UseVisualStyleBackColor = true;
            rbRingAny.CheckedChanged += rbRings_CheckedChanged;
            // 
            // rbRingDivine
            // 
            rbRingDivine.AutoSize = true;
            rbRingDivine.Location = new Point(10, 106);
            rbRingDivine.Name = "rbRingDivine";
            rbRingDivine.Size = new Size(56, 24);
            rbRingDivine.TabIndex = 3;
            rbRingDivine.TabStop = true;
            rbRingDivine.Text = "75+";
            rbRingDivine.UseVisualStyleBackColor = true;
            rbRingDivine.CheckedChanged += rbRings_CheckedChanged;
            // 
            // groupAmulets
            // 
            groupAmulets.Controls.Add(rbAmuletsOff);
            groupAmulets.Controls.Add(rbAmuletsChaos);
            groupAmulets.Controls.Add(rbAmuletsAny);
            groupAmulets.Controls.Add(rbAmuletsDivine);
            groupAmulets.Location = new Point(103, 75);
            groupAmulets.Name = "groupAmulets";
            groupAmulets.Size = new Size(85, 138);
            groupAmulets.TabIndex = 4;
            groupAmulets.TabStop = false;
            groupAmulets.Text = "Amulets";
            // 
            // rbAmuletsOff
            // 
            rbAmuletsOff.AutoSize = true;
            rbAmuletsOff.Location = new Point(10, 25);
            rbAmuletsOff.Name = "rbAmuletsOff";
            rbAmuletsOff.Size = new Size(51, 24);
            rbAmuletsOff.TabIndex = 0;
            rbAmuletsOff.TabStop = true;
            rbAmuletsOff.Text = "Off";
            rbAmuletsOff.UseVisualStyleBackColor = true;
            rbAmuletsOff.CheckedChanged += rbAmulets_CheckedChanged;
            // 
            // rbAmuletsChaos
            // 
            rbAmuletsChaos.AutoSize = true;
            rbAmuletsChaos.Location = new Point(10, 52);
            rbAmuletsChaos.Name = "rbAmuletsChaos";
            rbAmuletsChaos.Size = new Size(68, 24);
            rbAmuletsChaos.TabIndex = 1;
            rbAmuletsChaos.TabStop = true;
            rbAmuletsChaos.Text = "60-74";
            rbAmuletsChaos.UseVisualStyleBackColor = true;
            rbAmuletsChaos.CheckedChanged += rbAmulets_CheckedChanged;
            // 
            // rbAmuletsAny
            // 
            rbAmuletsAny.AutoSize = true;
            rbAmuletsAny.Location = new Point(10, 79);
            rbAmuletsAny.Name = "rbAmuletsAny";
            rbAmuletsAny.Size = new Size(56, 24);
            rbAmuletsAny.TabIndex = 2;
            rbAmuletsAny.TabStop = true;
            rbAmuletsAny.Text = "60+";
            rbAmuletsAny.UseVisualStyleBackColor = true;
            rbAmuletsAny.CheckedChanged += rbAmulets_CheckedChanged;
            // 
            // rbAmuletsDivine
            // 
            rbAmuletsDivine.AutoSize = true;
            rbAmuletsDivine.Location = new Point(10, 106);
            rbAmuletsDivine.Name = "rbAmuletsDivine";
            rbAmuletsDivine.Size = new Size(56, 24);
            rbAmuletsDivine.TabIndex = 3;
            rbAmuletsDivine.TabStop = true;
            rbAmuletsDivine.Text = "75+";
            rbAmuletsDivine.UseVisualStyleBackColor = true;
            rbAmuletsDivine.CheckedChanged += rbAmulets_CheckedChanged;
            // 
            // groupShields
            // 
            groupShields.Controls.Add(rbShieldsOff);
            groupShields.Controls.Add(rbShieldsChaos);
            groupShields.Controls.Add(rbShieldsAny);
            groupShields.Controls.Add(rbShieldsDivine);
            groupShields.Location = new Point(285, 75);
            groupShields.Name = "groupShields";
            groupShields.Size = new Size(85, 138);
            groupShields.TabIndex = 6;
            groupShields.TabStop = false;
            groupShields.Text = "Shields";
            // 
            // rbShieldsOff
            // 
            rbShieldsOff.AutoSize = true;
            rbShieldsOff.Location = new Point(10, 25);
            rbShieldsOff.Name = "rbShieldsOff";
            rbShieldsOff.Size = new Size(51, 24);
            rbShieldsOff.TabIndex = 0;
            rbShieldsOff.TabStop = true;
            rbShieldsOff.Text = "Off";
            rbShieldsOff.UseVisualStyleBackColor = true;
            rbShieldsOff.CheckedChanged += rbShields_CheckedChanged;
            // 
            // rbShieldsChaos
            // 
            rbShieldsChaos.AutoSize = true;
            rbShieldsChaos.Location = new Point(10, 52);
            rbShieldsChaos.Name = "rbShieldsChaos";
            rbShieldsChaos.Size = new Size(68, 24);
            rbShieldsChaos.TabIndex = 1;
            rbShieldsChaos.TabStop = true;
            rbShieldsChaos.Text = "60-74";
            rbShieldsChaos.UseVisualStyleBackColor = true;
            rbShieldsChaos.CheckedChanged += rbShields_CheckedChanged;
            // 
            // rbShieldsAny
            // 
            rbShieldsAny.AutoSize = true;
            rbShieldsAny.Location = new Point(10, 79);
            rbShieldsAny.Name = "rbShieldsAny";
            rbShieldsAny.Size = new Size(56, 24);
            rbShieldsAny.TabIndex = 2;
            rbShieldsAny.TabStop = true;
            rbShieldsAny.Text = "60+";
            rbShieldsAny.UseVisualStyleBackColor = true;
            rbShieldsAny.CheckedChanged += rbShields_CheckedChanged;
            // 
            // rbShieldsDivine
            // 
            rbShieldsDivine.AutoSize = true;
            rbShieldsDivine.Location = new Point(10, 106);
            rbShieldsDivine.Name = "rbShieldsDivine";
            rbShieldsDivine.Size = new Size(56, 24);
            rbShieldsDivine.TabIndex = 3;
            rbShieldsDivine.TabStop = true;
            rbShieldsDivine.Text = "75+";
            rbShieldsDivine.UseVisualStyleBackColor = true;
            rbShieldsDivine.CheckedChanged += rbShields_CheckedChanged;
            // 
            // groupBelts
            // 
            groupBelts.Controls.Add(rbBeltsOff);
            groupBelts.Controls.Add(rbBeltsChaos);
            groupBelts.Controls.Add(rbBeltsAny);
            groupBelts.Controls.Add(rbBeltsDivine);
            groupBelts.Location = new Point(194, 75);
            groupBelts.Name = "groupBelts";
            groupBelts.Size = new Size(85, 138);
            groupBelts.TabIndex = 5;
            groupBelts.TabStop = false;
            groupBelts.Text = "Belts";
            // 
            // rbBeltsOff
            // 
            rbBeltsOff.AutoSize = true;
            rbBeltsOff.Location = new Point(10, 25);
            rbBeltsOff.Name = "rbBeltsOff";
            rbBeltsOff.Size = new Size(51, 24);
            rbBeltsOff.TabIndex = 0;
            rbBeltsOff.TabStop = true;
            rbBeltsOff.Text = "Off";
            rbBeltsOff.UseVisualStyleBackColor = true;
            rbBeltsOff.CheckedChanged += rbBelts_CheckedChanged;
            // 
            // rbBeltsChaos
            // 
            rbBeltsChaos.AutoSize = true;
            rbBeltsChaos.Location = new Point(10, 52);
            rbBeltsChaos.Name = "rbBeltsChaos";
            rbBeltsChaos.Size = new Size(68, 24);
            rbBeltsChaos.TabIndex = 1;
            rbBeltsChaos.TabStop = true;
            rbBeltsChaos.Text = "60-74";
            rbBeltsChaos.UseVisualStyleBackColor = true;
            rbBeltsChaos.CheckedChanged += rbBelts_CheckedChanged;
            // 
            // rbBeltsAny
            // 
            rbBeltsAny.AutoSize = true;
            rbBeltsAny.Location = new Point(10, 79);
            rbBeltsAny.Name = "rbBeltsAny";
            rbBeltsAny.Size = new Size(56, 24);
            rbBeltsAny.TabIndex = 2;
            rbBeltsAny.TabStop = true;
            rbBeltsAny.Text = "60+";
            rbBeltsAny.UseVisualStyleBackColor = true;
            rbBeltsAny.CheckedChanged += rbBelts_CheckedChanged;
            // 
            // rbBeltsDivine
            // 
            rbBeltsDivine.AutoSize = true;
            rbBeltsDivine.Location = new Point(10, 106);
            rbBeltsDivine.Name = "rbBeltsDivine";
            rbBeltsDivine.Size = new Size(56, 24);
            rbBeltsDivine.TabIndex = 3;
            rbBeltsDivine.TabStop = true;
            rbBeltsDivine.Text = "75+";
            rbBeltsDivine.UseVisualStyleBackColor = true;
            rbBeltsDivine.CheckedChanged += rbBelts_CheckedChanged;
            // 
            // groupBoots
            // 
            groupBoots.Controls.Add(rbBootsOff);
            groupBoots.Controls.Add(rbBootsChaos);
            groupBoots.Controls.Add(rbBootsAny);
            groupBoots.Controls.Add(rbBootsDivine);
            groupBoots.Location = new Point(467, 75);
            groupBoots.Name = "groupBoots";
            groupBoots.Size = new Size(85, 138);
            groupBoots.TabIndex = 4;
            groupBoots.TabStop = false;
            groupBoots.Text = "Boots";
            // 
            // rbBootsOff
            // 
            rbBootsOff.AutoSize = true;
            rbBootsOff.Location = new Point(10, 25);
            rbBootsOff.Name = "rbBootsOff";
            rbBootsOff.Size = new Size(51, 24);
            rbBootsOff.TabIndex = 0;
            rbBootsOff.TabStop = true;
            rbBootsOff.Text = "Off";
            rbBootsOff.UseVisualStyleBackColor = true;
            rbBootsOff.CheckedChanged += rbBoots_CheckedChanged;
            // 
            // rbBootsChaos
            // 
            rbBootsChaos.AutoSize = true;
            rbBootsChaos.Location = new Point(10, 52);
            rbBootsChaos.Name = "rbBootsChaos";
            rbBootsChaos.Size = new Size(68, 24);
            rbBootsChaos.TabIndex = 1;
            rbBootsChaos.TabStop = true;
            rbBootsChaos.Text = "60-74";
            rbBootsChaos.UseVisualStyleBackColor = true;
            rbBootsChaos.CheckedChanged += rbBoots_CheckedChanged;
            // 
            // rbBootsAny
            // 
            rbBootsAny.AutoSize = true;
            rbBootsAny.Location = new Point(10, 79);
            rbBootsAny.Name = "rbBootsAny";
            rbBootsAny.Size = new Size(56, 24);
            rbBootsAny.TabIndex = 2;
            rbBootsAny.TabStop = true;
            rbBootsAny.Text = "60+";
            rbBootsAny.UseVisualStyleBackColor = true;
            rbBootsAny.CheckedChanged += rbBoots_CheckedChanged;
            // 
            // rbBootsDivine
            // 
            rbBootsDivine.AutoSize = true;
            rbBootsDivine.Location = new Point(10, 106);
            rbBootsDivine.Name = "rbBootsDivine";
            rbBootsDivine.Size = new Size(56, 24);
            rbBootsDivine.TabIndex = 3;
            rbBootsDivine.TabStop = true;
            rbBootsDivine.Text = "75+";
            rbBootsDivine.UseVisualStyleBackColor = true;
            rbBootsDivine.CheckedChanged += rbBoots_CheckedChanged;
            // 
            // groupHelmets
            // 
            groupHelmets.Controls.Add(rbHelmetsOff);
            groupHelmets.Controls.Add(rbHelmetsChaos);
            groupHelmets.Controls.Add(rbHelmetsAny);
            groupHelmets.Controls.Add(rbHelmetsDivine);
            groupHelmets.Location = new Point(558, 75);
            groupHelmets.Name = "groupHelmets";
            groupHelmets.Size = new Size(85, 138);
            groupHelmets.TabIndex = 7;
            groupHelmets.TabStop = false;
            groupHelmets.Text = "Helmets";
            // 
            // rbHelmetsOff
            // 
            rbHelmetsOff.AutoSize = true;
            rbHelmetsOff.Location = new Point(10, 25);
            rbHelmetsOff.Name = "rbHelmetsOff";
            rbHelmetsOff.Size = new Size(51, 24);
            rbHelmetsOff.TabIndex = 0;
            rbHelmetsOff.TabStop = true;
            rbHelmetsOff.Text = "Off";
            rbHelmetsOff.UseVisualStyleBackColor = true;
            rbHelmetsOff.CheckedChanged += rbHelmets_CheckedChanged;
            // 
            // rbHelmetsChaos
            // 
            rbHelmetsChaos.AutoSize = true;
            rbHelmetsChaos.Location = new Point(10, 52);
            rbHelmetsChaos.Name = "rbHelmetsChaos";
            rbHelmetsChaos.Size = new Size(68, 24);
            rbHelmetsChaos.TabIndex = 1;
            rbHelmetsChaos.TabStop = true;
            rbHelmetsChaos.Text = "60-74";
            rbHelmetsChaos.UseVisualStyleBackColor = true;
            rbHelmetsChaos.CheckedChanged += rbHelmets_CheckedChanged;
            // 
            // rbHelmetsAny
            // 
            rbHelmetsAny.AutoSize = true;
            rbHelmetsAny.Location = new Point(10, 79);
            rbHelmetsAny.Name = "rbHelmetsAny";
            rbHelmetsAny.Size = new Size(56, 24);
            rbHelmetsAny.TabIndex = 2;
            rbHelmetsAny.TabStop = true;
            rbHelmetsAny.Text = "60+";
            rbHelmetsAny.UseVisualStyleBackColor = true;
            rbHelmetsAny.CheckedChanged += rbHelmets_CheckedChanged;
            // 
            // rbHelmetsDivine
            // 
            rbHelmetsDivine.AutoSize = true;
            rbHelmetsDivine.Location = new Point(10, 106);
            rbHelmetsDivine.Name = "rbHelmetsDivine";
            rbHelmetsDivine.Size = new Size(56, 24);
            rbHelmetsDivine.TabIndex = 3;
            rbHelmetsDivine.TabStop = true;
            rbHelmetsDivine.Text = "75+";
            rbHelmetsDivine.UseVisualStyleBackColor = true;
            rbHelmetsDivine.CheckedChanged += rbHelmets_CheckedChanged;
            // 
            // groupGloves
            // 
            groupGloves.Controls.Add(rbGlovesOff);
            groupGloves.Controls.Add(rbGlovesChaos);
            groupGloves.Controls.Add(rbGlovesAny);
            groupGloves.Controls.Add(rbGlovesDivine);
            groupGloves.Location = new Point(649, 75);
            groupGloves.Name = "groupGloves";
            groupGloves.Size = new Size(85, 138);
            groupGloves.TabIndex = 8;
            groupGloves.TabStop = false;
            groupGloves.Text = "Gloves";
            // 
            // rbGlovesOff
            // 
            rbGlovesOff.AutoSize = true;
            rbGlovesOff.Location = new Point(10, 25);
            rbGlovesOff.Name = "rbGlovesOff";
            rbGlovesOff.Size = new Size(51, 24);
            rbGlovesOff.TabIndex = 0;
            rbGlovesOff.TabStop = true;
            rbGlovesOff.Text = "Off";
            rbGlovesOff.UseVisualStyleBackColor = true;
            rbGlovesOff.CheckedChanged += rbGloves_CheckedChanged;
            // 
            // rbGlovesChaos
            // 
            rbGlovesChaos.AutoSize = true;
            rbGlovesChaos.Location = new Point(10, 52);
            rbGlovesChaos.Name = "rbGlovesChaos";
            rbGlovesChaos.Size = new Size(68, 24);
            rbGlovesChaos.TabIndex = 1;
            rbGlovesChaos.TabStop = true;
            rbGlovesChaos.Text = "60-74";
            rbGlovesChaos.UseVisualStyleBackColor = true;
            rbGlovesChaos.CheckedChanged += rbGloves_CheckedChanged;
            // 
            // rbGlovesAny
            // 
            rbGlovesAny.AutoSize = true;
            rbGlovesAny.Location = new Point(10, 79);
            rbGlovesAny.Name = "rbGlovesAny";
            rbGlovesAny.Size = new Size(56, 24);
            rbGlovesAny.TabIndex = 2;
            rbGlovesAny.TabStop = true;
            rbGlovesAny.Text = "60+";
            rbGlovesAny.UseVisualStyleBackColor = true;
            rbGlovesAny.CheckedChanged += rbGloves_CheckedChanged;
            // 
            // rbGlovesDivine
            // 
            rbGlovesDivine.AutoSize = true;
            rbGlovesDivine.Location = new Point(10, 106);
            rbGlovesDivine.Name = "rbGlovesDivine";
            rbGlovesDivine.Size = new Size(56, 24);
            rbGlovesDivine.TabIndex = 3;
            rbGlovesDivine.TabStop = true;
            rbGlovesDivine.Text = "75+";
            rbGlovesDivine.UseVisualStyleBackColor = true;
            rbGlovesDivine.CheckedChanged += rbGloves_CheckedChanged;
            // 
            // groupBody
            // 
            groupBody.Controls.Add(rbBodyOff);
            groupBody.Controls.Add(rbBodyChaos);
            groupBody.Controls.Add(rbBodyAny);
            groupBody.Controls.Add(rbBodyDivine);
            groupBody.Location = new Point(740, 75);
            groupBody.Name = "groupBody";
            groupBody.Size = new Size(85, 138);
            groupBody.TabIndex = 9;
            groupBody.TabStop = false;
            groupBody.Text = "Chest";
            // 
            // rbBodyOff
            // 
            rbBodyOff.AutoSize = true;
            rbBodyOff.Location = new Point(10, 25);
            rbBodyOff.Name = "rbBodyOff";
            rbBodyOff.Size = new Size(51, 24);
            rbBodyOff.TabIndex = 0;
            rbBodyOff.TabStop = true;
            rbBodyOff.Text = "Off";
            rbBodyOff.UseVisualStyleBackColor = true;
            rbBodyOff.CheckedChanged += rbChest_CheckedChanged;
            // 
            // rbBodyChaos
            // 
            rbBodyChaos.AutoSize = true;
            rbBodyChaos.Location = new Point(10, 52);
            rbBodyChaos.Name = "rbBodyChaos";
            rbBodyChaos.Size = new Size(68, 24);
            rbBodyChaos.TabIndex = 1;
            rbBodyChaos.TabStop = true;
            rbBodyChaos.Text = "60-74";
            rbBodyChaos.UseVisualStyleBackColor = true;
            rbBodyChaos.CheckedChanged += rbChest_CheckedChanged;
            // 
            // rbBodyAny
            // 
            rbBodyAny.AutoSize = true;
            rbBodyAny.Location = new Point(10, 79);
            rbBodyAny.Name = "rbBodyAny";
            rbBodyAny.Size = new Size(56, 24);
            rbBodyAny.TabIndex = 2;
            rbBodyAny.TabStop = true;
            rbBodyAny.Text = "60+";
            rbBodyAny.UseVisualStyleBackColor = true;
            rbBodyAny.CheckedChanged += rbChest_CheckedChanged;
            // 
            // rbBodyDivine
            // 
            rbBodyDivine.AutoSize = true;
            rbBodyDivine.Location = new Point(10, 106);
            rbBodyDivine.Name = "rbBodyDivine";
            rbBodyDivine.Size = new Size(56, 24);
            rbBodyDivine.TabIndex = 3;
            rbBodyDivine.TabStop = true;
            rbBodyDivine.Text = "75+";
            rbBodyDivine.UseVisualStyleBackColor = true;
            rbBodyDivine.CheckedChanged += rbChest_CheckedChanged;
            // 
            // groupWeapons
            // 
            groupWeapons.Controls.Add(rbWeaponsOff);
            groupWeapons.Controls.Add(rbWeaponsChaos);
            groupWeapons.Controls.Add(rbWeaponsAny);
            groupWeapons.Controls.Add(rbWeaponsDivine);
            groupWeapons.Location = new Point(376, 75);
            groupWeapons.Name = "groupWeapons";
            groupWeapons.Size = new Size(85, 138);
            groupWeapons.TabIndex = 7;
            groupWeapons.TabStop = false;
            groupWeapons.Text = "Weapons";
            // 
            // rbWeaponsOff
            // 
            rbWeaponsOff.AutoSize = true;
            rbWeaponsOff.Location = new Point(10, 25);
            rbWeaponsOff.Name = "rbWeaponsOff";
            rbWeaponsOff.Size = new Size(51, 24);
            rbWeaponsOff.TabIndex = 0;
            rbWeaponsOff.TabStop = true;
            rbWeaponsOff.Text = "Off";
            rbWeaponsOff.UseVisualStyleBackColor = true;
            rbWeaponsOff.CheckedChanged += rbWeapons_CheckedChanged;
            // 
            // rbWeaponsChaos
            // 
            rbWeaponsChaos.AutoSize = true;
            rbWeaponsChaos.Location = new Point(10, 52);
            rbWeaponsChaos.Name = "rbWeaponsChaos";
            rbWeaponsChaos.Size = new Size(68, 24);
            rbWeaponsChaos.TabIndex = 1;
            rbWeaponsChaos.TabStop = true;
            rbWeaponsChaos.Text = "60-74";
            rbWeaponsChaos.UseVisualStyleBackColor = true;
            rbWeaponsChaos.CheckedChanged += rbWeapons_CheckedChanged;
            // 
            // rbWeaponsAny
            // 
            rbWeaponsAny.AutoSize = true;
            rbWeaponsAny.Location = new Point(10, 79);
            rbWeaponsAny.Name = "rbWeaponsAny";
            rbWeaponsAny.Size = new Size(56, 24);
            rbWeaponsAny.TabIndex = 2;
            rbWeaponsAny.TabStop = true;
            rbWeaponsAny.Text = "60+";
            rbWeaponsAny.UseVisualStyleBackColor = true;
            rbWeaponsAny.CheckedChanged += rbWeapons_CheckedChanged;
            // 
            // rbWeaponsDivine
            // 
            rbWeaponsDivine.AutoSize = true;
            rbWeaponsDivine.Location = new Point(10, 106);
            rbWeaponsDivine.Name = "rbWeaponsDivine";
            rbWeaponsDivine.Size = new Size(56, 24);
            rbWeaponsDivine.TabIndex = 3;
            rbWeaponsDivine.TabStop = true;
            rbWeaponsDivine.Text = "75+";
            rbWeaponsDivine.UseVisualStyleBackColor = true;
            rbWeaponsDivine.CheckedChanged += rbWeapons_CheckedChanged;
            // 
            // labelFilePath
            // 
            labelFilePath.AutoSize = true;
            labelFilePath.Location = new Point(12, 25);
            labelFilePath.MinimumSize = new Size(450, 0);
            labelFilePath.Name = "labelFilePath";
            labelFilePath.Size = new Size(620, 20);
            labelFilePath.TabIndex = 10;
            labelFilePath.Text = "C:\\Users\\tuba\\Documents\\My Games\\Path of Exile\\NeverSink's filter - 4-VERY-STRICT.filter";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(832, 246);
            Controls.Add(labelFilePath);
            Controls.Add(groupWeapons);
            Controls.Add(groupBody);
            Controls.Add(groupGloves);
            Controls.Add(groupHelmets);
            Controls.Add(groupBoots);
            Controls.Add(groupBelts);
            Controls.Add(groupShields);
            Controls.Add(groupAmulets);
            Controls.Add(groupRing);
            Name = "Form1";
            Text = "Form1";
            groupRing.ResumeLayout(false);
            groupRing.PerformLayout();
            groupAmulets.ResumeLayout(false);
            groupAmulets.PerformLayout();
            groupShields.ResumeLayout(false);
            groupShields.PerformLayout();
            groupBelts.ResumeLayout(false);
            groupBelts.PerformLayout();
            groupBoots.ResumeLayout(false);
            groupBoots.PerformLayout();
            groupHelmets.ResumeLayout(false);
            groupHelmets.PerformLayout();
            groupGloves.ResumeLayout(false);
            groupGloves.PerformLayout();
            groupBody.ResumeLayout(false);
            groupBody.PerformLayout();
            groupWeapons.ResumeLayout(false);
            groupWeapons.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox groupRing;
        private RadioButton rbRingAny;
        private RadioButton rbRingChaos;
        private RadioButton rbRingOff;
        private RadioButton rbRingDivine;
        private GroupBox groupAmulets;
        private RadioButton rbAmuletsOff;
        private RadioButton rbAmuletsAny;
        private RadioButton rbAmuletsChaos;
        private RadioButton rbAmuletsDivine;
        private GroupBox groupShields;
        private RadioButton rbShieldsOff;
        private RadioButton rbShieldsChaos;
        private RadioButton rbShieldsAny;
        private RadioButton rbShieldsDivine;
        private GroupBox groupBelts;
        private RadioButton rbBeltsOff;
        private RadioButton rbBeltsChaos;
        private RadioButton rbBeltsAny;
        private RadioButton rbBeltsDivine;
        private GroupBox groupBoots;
        private RadioButton rbBootsOff;
        private RadioButton rbBootsChaos;
        private RadioButton rbBootsAny;
        private RadioButton rbBootsDivine;
        private GroupBox groupHelmets;
        private RadioButton rbHelmetsOff;
        private RadioButton rbHelmetsChaos;
        private RadioButton rbHelmetsAny;
        private RadioButton rbHelmetsDivine;
        private GroupBox groupGloves;
        private RadioButton rbGlovesOff;
        private RadioButton rbGlovesChaos;
        private RadioButton rbGlovesAny;
        private RadioButton rbGlovesDivine;
        private GroupBox groupBody;
        private RadioButton rbBodyOff;
        private RadioButton rbBodyChaos;
        private RadioButton rbBodyAny;
        private RadioButton rbBodyDivine;
        private GroupBox groupWeapons;
        private RadioButton rbWeaponsOff;
        private RadioButton rbWeaponsChaos;
        private RadioButton rbWeaponsAny;
        private RadioButton rbWeaponsDivine;
        private Label labelFilePath;
    }
}
