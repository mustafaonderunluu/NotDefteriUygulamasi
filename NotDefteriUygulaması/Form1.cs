using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NotDefteriUygulaması
{
    public partial class Form1 : Form
    {
        private int TabCount = 0; //sekme Sayısı(belge sayısı aktif olan) 
        public Form1()
        {
            InitializeComponent();
        }
        #region Metotlar
        #region Sekmeler
        private void AddTab() //sekme ekleme 
        {

            RichTextBox Body = new RichTextBox();
            Body.Name = "body";   //aşağıya yazdığımız body ile aynı olmak zorunda.
            Body.Dock = DockStyle.Fill; //Nerde olucak? Ekrana yayıcak tabcontrol1 içine 
            Body.ContextMenuStrip = contextMenuStrip1; // belge içerisinde sağ click yapıldıgında ekrana gelicek olanlar ayarlandı.


            TabPage NewPage = new TabPage();
            TabCount += 1;  // yeni belge girildiğinde belge sayısı 1 arttırılır 

            string DocumentText = "Belge " + TabCount;
            NewPage.Name = DocumentText;
            NewPage.Text = DocumentText;
            NewPage.Controls.Add(Body);


            tabControl1.TabPages.Add(NewPage);



        }
        private void RemoveTab() //sekme kapat

        {
            if (tabControl1.TabPages.Count > 1) //en azından 1 sekme açık olmalı
            {

                tabControl1.TabPages.Remove(tabControl1.SelectedTab);


            }
            else {
                tabControl1.TabPages.Remove(tabControl1.SelectedTab); //tek bir belge varsa onuda kaldırıyorsa
                AddTab();// yeni belge açıyor


            }




        }
        private void RemoveAllTabs() //Tüm sekmeleri kapat
        {
            foreach (TabPage Page in tabControl1.TabPages) {


                tabControl1.TabPages.Remove(Page);

            }
            AddTab();

        }
        private void RemoveAllTabsButThis() //Açık olan haricinde sekmeleri kaldır
        {
            foreach (TabPage Page in tabControl1.TabPages) {

                if (Page.Name != tabControl1.SelectedTab.Name) //aktif olan sekmenin ismiyle diğer sekmelerin ismi bir değilse onları kaldırıyoruz  
                {

                    tabControl1.TabPages.Remove(Page);

                }

            }


        }
        #endregion
        #endregion
        #region SaveAndOpen(KaydetAç)

        private void Save() //kaydetme
        {
            saveFileDialog1.FileName = tabControl1.SelectedTab.Name;
            saveFileDialog1.InitialDirectory =
        Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            //ortamımızdan (yazdığımız yazılım) geçerli klasörü alıyoruz.
            //son kısım bizim bilgisayarımız içerisindeki ortamdan özel bir klasör (yani belgelerimizin içine kaydediyoruz )
            saveFileDialog1.Filter = "RTF | * .rtf ";
            saveFileDialog1.Title = "Save";

            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                if (saveFileDialog1.FileName.Length > 0) {


                    GetCurrnetDocument.SaveFile(saveFileDialog1.FileName, RichTextBoxStreamType.RichText);
                }


            }

        }
        private void SaveAs() //Farklı isimle Kaydet
        {
            saveFileDialog1.FileName = tabControl1.SelectedTab.Name;
            saveFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            saveFileDialog1.Filter = "Text Files |*.txt| C# dosyası |*.cs| Tüm Dosyalar|*.* ";
            saveFileDialog1.Title = "farklı kaydet";
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                if (saveFileDialog1.FileName.Length > 0)
                {


                    GetCurrnetDocument.SaveFile(saveFileDialog1.FileName, RichTextBoxStreamType.PlainText);//basit metin 
                }


            }

        }

        private void Open() //Belge Açma

        {
            openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            openFileDialog1.Filter = "RTF| *.rtf|Text dosyası|*.txt|Tüm dosyalar|*.*";
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (openFileDialog1.FileName.Length > 0) {

                    GetCurrnetDocument.LoadFile(openFileDialog1.FileName, RichTextBoxStreamType.PlainText);


                }

            }



        }




        #endregion
        #region Özellikler
        private RichTextBox GetCurrnetDocument //aktif  belgeyi al 
        {
            get { return (RichTextBox)tabControl1.SelectedTab.Controls["Body"]; }


        }


        #region TextFunctions

        private void Undo() {

            GetCurrnetDocument.Undo();



        }
        private void Redo()
        {
            GetCurrnetDocument.Redo();

        }
        private void Cut() {


            GetCurrnetDocument.Cut();
        }
        private void Copy() {
            GetCurrnetDocument.Copy();
        }
        private void Paste()
        {
            GetCurrnetDocument.Paste();
        }
        private void SelectAll() {

            GetCurrnetDocument.SelectAll();


        }





        #endregion
        #region Font
        private void GetFontCollection() //Fontların Yüklenmesi
        {
            InstalledFontCollection InsFonts = new InstalledFontCollection();
            foreach (FontFamily item in InsFonts.Families) {
                toolStripComboBox1FontType.Items.Add(item.Name);


            }
            toolStripComboBox1FontType.SelectedIndex = 0;

        }
        private void PopulateFontSize() //Font Ölçeklerini Oluştur
        {
            for (int i = 0; i <= 75; i++) {


                toolStripComboBox2FontSıze.Items.Add(i);
            }
            toolStripComboBox2FontSıze.SelectedIndex = 12; //varsayılan olarak seçildi 


        }

        #endregion



        #endregion

        private void düzenToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void geriAlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Undo();
        }

        private void yineleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Redo();
        }

        private void kesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cut();
        }

        private void kopyalaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Copy();
        }

        private void yapıştırToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Paste();
        }

        private void tümünüSeçToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectAll();
        }

        private void yeniToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddTab();
        }

        private void açToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Open();
        }

        private void kaydetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void farklıKaydetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAs();
        }

        private void toolStrip1_Click(object sender, EventArgs e)
        {
            AddTab();
        }

        private void removetoolStripButton1_Click(object sender, EventArgs e)
        {
            RemoveTab();
        }

        private void açToolStripButton_Click(object sender, EventArgs e)
        {
            Open();
        }

        private void kaydetToolStripButton_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void yazdırToolStripButton_Click(object sender, EventArgs e)
        {

        }

        private void kesToolStripButton_Click(object sender, EventArgs e)
        {
            Cut();
        }

        private void kopyalaToolStripButton_Click(object sender, EventArgs e)
        {
            Copy();
        }

        private void yapıştırToolStripButton_Click(object sender, EventArgs e)
        {
            Paste();
        }

        private void toolStripButton1Bold_Click(object sender, EventArgs e)
        {
            Font BoldFont = new Font(GetCurrnetDocument.SelectionFont.FontFamily, GetCurrnetDocument.SelectionFont.SizeInPoints, FontStyle.Bold);
            Font RegularFont = new Font(GetCurrnetDocument.SelectionFont.FontFamily, GetCurrnetDocument.SelectionFont.SizeInPoints, FontStyle.Regular);
            if (GetCurrnetDocument.SelectionFont.Bold)
            {

                GetCurrnetDocument.SelectionFont = RegularFont;

            }
            else {
                GetCurrnetDocument.SelectionFont = BoldFont;

            }
        }

        private void toolStripButton2İtalik_Click(object sender, EventArgs e)

        {
            Font ItalicFont = new Font(GetCurrnetDocument.SelectionFont.FontFamily, GetCurrnetDocument.SelectionFont.SizeInPoints, FontStyle.Italic);
            Font RegularFont = new Font(GetCurrnetDocument.SelectionFont.FontFamily, GetCurrnetDocument.SelectionFont.SizeInPoints, FontStyle.Regular);
            if (GetCurrnetDocument.SelectionFont.Italic)
            {

                GetCurrnetDocument.SelectionFont = RegularFont;

            }
            else
            {
                GetCurrnetDocument.SelectionFont = ItalicFont;

            }
        }

        private void toolStripButton3UnderLine_Click(object sender, EventArgs e)
        
            {
                Font UnderlineFont = new Font(GetCurrnetDocument.SelectionFont.FontFamily, GetCurrnetDocument.SelectionFont.SizeInPoints, FontStyle.Underline);
                Font RegularFont = new Font(GetCurrnetDocument.SelectionFont.FontFamily, GetCurrnetDocument.SelectionFont.SizeInPoints, FontStyle.Regular);
                if (GetCurrnetDocument.SelectionFont.Underline)
                {

                    GetCurrnetDocument.SelectionFont = RegularFont;

                }
                else
                {
                    GetCurrnetDocument.SelectionFont = UnderlineFont;

                }

            }

        private void toolStripButton4StrikeOut_Click(object sender, EventArgs e)
        
            {
                Font StrikeoutFont = new Font(GetCurrnetDocument.SelectionFont.FontFamily, GetCurrnetDocument.SelectionFont.SizeInPoints, FontStyle.Strikeout);
                Font RegularFont = new Font(GetCurrnetDocument.SelectionFont.FontFamily, GetCurrnetDocument.SelectionFont.SizeInPoints, FontStyle.Regular);
                if (GetCurrnetDocument.SelectionFont.Strikeout)
                {

                    GetCurrnetDocument.SelectionFont = RegularFont;

                }
                else
                {
                    GetCurrnetDocument.SelectionFont = StrikeoutFont;

                }

            }

        private void toolStripButton5İncrease_Click(object sender, EventArgs e)
        {
            float NewFontSize = GetCurrnetDocument.SelectionFont.SizeInPoints + 2;
            Font NewSize = new Font(GetCurrnetDocument.SelectionFont.Name, NewFontSize, GetCurrnetDocument.SelectionFont.Style);
            GetCurrnetDocument.SelectionFont = NewSize;

        }

        private void toolStriptButton8Upper_Click(object sender, EventArgs e)
        {
            GetCurrnetDocument.SelectedText = GetCurrnetDocument.SelectedText.ToUpper();

        }

        private void toolStripButtonLower1_Click(object sender, EventArgs e)
        {
            GetCurrnetDocument.SelectedText = GetCurrnetDocument.SelectedText.ToLower();

        }

        private void toolStripButton6Decrase_Click(object sender, EventArgs e)
        {
            float NewFontSize = GetCurrnetDocument.SelectionFont.SizeInPoints - 2;
            Font NewSize = new Font(GetCurrnetDocument.SelectionFont.Name, NewFontSize, GetCurrnetDocument.SelectionFont.Style);
            GetCurrnetDocument.SelectionFont = NewSize;
        }

        private void toolStriptButton7FountColour_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            { 
            GetCurrnetDocument.SelectionColor = colorDialog1.Color;
            
            
            }


        }

        private void toolStripDropDownButton1BackColour_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem1Gray_Click(object sender, EventArgs e)
        {
            GetCurrnetDocument.SelectionBackColor = Color.Lime;
        }

        private void toolStripMenuItem2Orange_Click(object sender, EventArgs e)
        {
            GetCurrnetDocument.SelectionBackColor = Color.Orange;
        }

        private void toolStripMenuItem3Yellow_Click(object sender, EventArgs e)
        {
            GetCurrnetDocument.SelectionBackColor = Color.Yellow;
        }

        private void toolStripComboBox1FontType_Click(object sender, EventArgs e)
        {

        }

        private void toolStripComboBox1FontType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Font NewFont = new Font(toolStripComboBox1FontType.SelectedItem.ToString(), GetCurrnetDocument.SelectionFont.Size, GetCurrnetDocument.SelectionFont.Style);
            GetCurrnetDocument.SelectionFont= NewFont;

        }

        private void toolStripComboBox2FontSıze_SelectedIndexChanged(object sender, EventArgs e)
        {
            float NewSize;
            float.TryParse(toolStripComboBox2FontSıze.SelectedItem.ToString(),
                out NewSize);
            Font NewFont = new Font(GetCurrnetDocument.SelectionFont.Name, NewSize, GetCurrnetDocument.SelectionFont.Style);
            GetCurrnetDocument.SelectionFont= NewFont;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Undo();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Redo();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Cut();
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            Copy();
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            Paste();
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            RemoveTab();
        }

        private void toolStriptMenuItem8_Click(object sender, EventArgs e)
        {
            RemoveAllTabs();
        }

        private void toolStriptMenuItem9_Click(object sender, EventArgs e)
        {
            RemoveAllTabsButThis();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            AddTab();
            GetFontCollection();
            PopulateFontSize();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (GetCurrnetDocument.Text.Length > 0) {
                toolStripStatusLabel1.Text = "Toplam Karakter Sayısı = "+
                    GetCurrnetDocument.Text.Length.ToString();
            
            }
        }
    }
    }
