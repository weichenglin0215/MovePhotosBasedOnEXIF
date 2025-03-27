using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MetadataExtractor;
using MetadataExtractor.Formats.Exif;

namespace MovePhotosBasedOnEXIF
{
    public partial class Form_MovePhotosBasedOnEXIF : Form
    {
        //public System.Drawing.Imaging.PropertyItem[] PropertyItems { get; }
        public override System.Drawing.Size MinimumSize { get; set; }

        DateTime dateTime = DateTime.Now;

        public Form_MovePhotosBasedOnEXIF()
        {
            InitializeComponent();
            //在Form1.Design.cs中已經被自動宣告了，這裡可以不用加這一行。
            //this.Load += new EventHandler(Form1_Load);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Console.WriteLine("Form1_Load()");
            SetFormSize();
            //textBox_Path.Text = System.Windows.Forms.Application.StartupPath;
            textBox_Path.Text = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
        }

        private void SetFormSize()
        {
            MinimumSize = new Size(725, 480);
            // Retrieve the working rectangle from the Screen class
            // using the PrimaryScreen and the WorkingArea properties.
            System.Drawing.Rectangle workingRectangle = Screen.PrimaryScreen.WorkingArea;

            // Set the size of the form slightly less than size of 
            // working rectangle.
            //this.Size = new System.Drawing.Size(workingRectangle.Width - 10, workingRectangle.Height - 10);

            // Set the location so the entire form is visible.
            //this.Location = new System.Drawing.Point(5, 5);
            Point newPosition = new Point(0, 0);
            newPosition.X = (workingRectangle.Width - this.Width) / 2;
            newPosition.Y = (workingRectangle.Height - this.Height) / 2;
            this.Location = newPosition;

        }

        private void button_Path_Click(object sender, EventArgs e)
        {
            try
            {
                string startupPath = Application.StartupPath;
                using (FolderBrowserDialog dialog = new FolderBrowserDialog())
                {
                    dialog.Description = "請選取原始照片目錄";
                    dialog.ShowNewFolderButton = true;
                    //dialog.RootFolder = Environment.SpecialFolder.MyComputer;
                    dialog.SelectedPath = textBox_Path.Text;
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        string folder = dialog.SelectedPath;
                        textBox_Path.Text = dialog.SelectedPath;
                        //foreach (string fileName in Directory.GetFiles(folder, "*.xml", SearchOption.TopDirectoryOnly))
                        //{
                        //    SQLGenerator.GenerateSQLTransactions(Path.GetFullPath(fileName));
                        //}
                    }
                }
                //using (OpenFileDialog dialog = new OpenFileDialog())
                //{
                //    dialog.Filter = "xml files (*.xml)|*.xml";
                //    dialog.Multiselect = false;
                //    dialog.InitialDirectory = ".";
                //    dialog.Title = "Select file (only in XML format)";
                //    if (dialog.ShowDialog() == DialogResult.OK)
                //    {
                //        SQLGenerator.GenerateSQLTransactions(Application.StartupPath + Settings.Default.xmlFile);
                //    }
                //}
            }
            catch (Exception exc)
            {
                MessageBox.Show("Import failed because " + exc.Message + " , please try again later.");
            }
        }

        void MoveToNewDirectory(Boolean bRenameOr)
        {
            if (listBox_Files.Items.Count > 0)
                listBox_Files.Items.Clear();
            try
            {
                int moveCount = 0;
                SearchOption option = SearchOption.AllDirectories;
                if (!checkBox_IncludeSubDir.Checked)
                    option = SearchOption.TopDirectoryOnly;
                string[] extensions = { "*.jpg", "*.jpeg", "*.gif", "*.png", "*.psd", "*.HEIC", "*.mp4", "*.mov" };
                var files = from file in GetFilesName(@textBox_Path.Text, extensions, option)
                //var files = from file in Directory.EnumerateFiles(@textBox_Path.Text, "*.jpg" , option) //包含找子目錄以下的檔案
                //var files = from file in Directory.EnumerateFiles(@textBox_Path.Text, "*." + textBox_ExtName.Text, option) 
                //包含找子目錄以下的檔案
                //讀取檔案內容並判斷是否含有"Microsoft"字串
                //from line in File.ReadLines(file)
                //where line.Contains("Microsoft")
                
                select new
                {
                    //files 是檔名列表
                    //f.File 是全路徑檔名
                    File = file,
                    //Line = line
                };

                if (files.Count() == 0) //該目錄沒有照片檔案
                {
                    label_Status.Text = "該目錄無任何照片檔案！";
                    listBox_Files.Items.Add("該目錄無任何照片檔案！");
                    listBox_Files.Items.Add(" ");
                    return;
                }

                listBox_Files.BeginUpdate();
                foreach (var f in files) //從每一個照片檔案去取資料
                {
                    //ShowMetaData(f.File); //顯示照片細節資料
                    int[] tmpYMD = { 1900,01,01 };
                    if (GetMetaDataDateTime(f.File, ref tmpYMD)) //確定有取得照片EXIF資料
                    {

                        string newSubFolder = @textBox_Path.Text + "\\" + tmpYMD[0].ToString().PadLeft(4, '0')
                            + "\\" + tmpYMD[0].ToString().PadLeft(4, '0')
                            + "-" + tmpYMD[1].ToString().PadLeft(2, '0')
                            + "\\" + tmpYMD[0].ToString().PadLeft(4, '0')
                            + "-" + tmpYMD[1].ToString().PadLeft(2, '0')
                            + "-" + tmpYMD[2].ToString().PadLeft(2, '0');
                        int extNum = f.File.LastIndexOf('\\');
                        string fileName = f.File.Substring(extNum+1); //單純檔名，不含路徑
                        if (!System.IO.Directory.Exists(newSubFolder)) //新目錄不存在
                        {
                            if (bRenameOr) //正式執行
                            {
                                System.IO.Directory.CreateDirectory(newSubFolder);
                                listBox_Files.Items.Add("正式*** 建立目錄： " + newSubFolder);
                            }
                            else //測試
                            {
                                listBox_Files.Items.Add("測試--- 建立目錄： " + newSubFolder);
                            }
                        }

                        if (f.File == newSubFolder + "\\" + fileName) //新舊目錄檔名相同，則不搬移。
                        {
                            if (bRenameOr) //正式執行
                            {
                                listBox_Files.Items.Add("正式***  目錄位置相同，不搬移 " + f.File);
                            }
                            else //新舊目錄檔名相同，則不搬移。
                            {
                                listBox_Files.Items.Add("測試--- 目錄位置相同，不搬移 " + f.File);
                            }
                        }
                        else if (System.IO.File.Exists(newSubFolder + "\\" + fileName)) //新目錄已經有相同檔名的照片檔，就不搬移過去。
                        {
                            if (bRenameOr) //正式執行
                            {
                                listBox_Files.Items.Add("正式*** 新目錄已經有相同檔名的照片檔，不搬移 " + f.File);
                            }
                            else //新舊目錄檔名相同，則不搬移。
                            {
                                listBox_Files.Items.Add("測試--- 新目錄已經有相同檔名的照片檔，不搬移 " + f.File);
                            }
                        }
                        else //新舊目錄檔名不同，搬移照片至新目錄。
                        {
                            //listBox_Files.Items.Add(f.File);
                            //listBox_Files.Items.Add(newSubFolder + "\\" + fileName);
                            //listBox_Files.Items.Add(newSubFolder);
                            //listBox_Files.Items.Add(fileName);
                            if (bRenameOr) //正式執行
                            {
                                System.IO.File.Move(f.File, newSubFolder + "\\" + fileName);
                                listBox_Files.Items.Add("正式*** " + f.File + " 搬移至 " + newSubFolder + "\\" + fileName);
                                moveCount++;
                            }
                            else //測試
                            {
                                listBox_Files.Items.Add("測試--- " + f.File + " 搬移至 " + newSubFolder + "\\" + fileName);
                                moveCount++;
                            }
                        }
                    }
                    else //未取得照片EXIF資料
                    {
                        listBox_Files.Items.Add("查不出 " + f.File + " 的EXIF資料");
                    }
                }
                Console.WriteLine("{0} files found.", files.Count().ToString());
                label_Status.Text = "該目錄下 " + moveCount + " 檔案已搬移至新目錄。";
                listBox_Files.Items.Add("搬移結束");
                listBox_Files.Items.Add(" ");
                listBox_Files.TopIndex = listBox_Files.Items.Count - 1;
                listBox_Files.EndUpdate();
            }


            catch (UnauthorizedAccessException UAEx)
            {
                Console.WriteLine(UAEx.Message);
            }
            catch (PathTooLongException PathEx)
            {
                Console.WriteLine(PathEx.Message);
            }
        }

        List<string> FindFiles(string pathName, string[] extName, bool isSubDir)
        {
            try
            {
                int moveCount = 0;
                SearchOption option = SearchOption.AllDirectories;
                if (!isSubDir)
                    option = SearchOption.TopDirectoryOnly;
                //string[] extName = { "*.jpg", "*.jpeg", "*.gif", "*.png", "*.psd", "*.HEIC", "*.mp4", "*.mov" };
                var files = from file in GetFilesName(@pathName, extName, option)
                //讀取檔案內容並判斷是否含有"Microsoft"字串
                //from line in File.ReadLines(file)
                //where line.Contains("Microsoft")
                select new
                {
                    //files 是檔名列表
                    //f.File 是全路徑檔名
                    File = file,
                    //Line = line
                };
                List<string> tmpStringList = new List<string>();
                foreach (var item in files)
                {
                    tmpStringList.Add(item.File);
                }
                Console.WriteLine("{0} files found.", tmpStringList.Count().ToString());
                return tmpStringList;
            }


            catch (UnauthorizedAccessException UAEx)
            {
                Console.WriteLine(UAEx.Message);
                return null;
            }
            catch (PathTooLongException PathEx)
            {
                Console.WriteLine(PathEx.Message);
                return null;
            }
        }


        // Takes same patterns, and executes in parallel
        public static IEnumerable<string> GetFilesName(string path,
                            string[] searchPatterns,
                            SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            return searchPatterns.AsParallel()
                   .SelectMany(searchPattern =>
                          System.IO.Directory.EnumerateFiles(path, searchPattern, searchOption));
        }
        private void button_Test_Click(object sender, EventArgs e)
        {
            Console.WriteLine("buttonTest_Click");
            MoveToNewDirectory(false);

        }

        private void button_MovePic_Click(object sender, EventArgs e)
        {
            Console.WriteLine("button_MovePic_Click");
            MoveToNewDirectory(true);

        }

        public void ShowMetaData(string f)
        {
            Console.WriteLine("ShowMetaData() " + f);
            var directories = ImageMetadataReader.ReadMetadata(f);
            foreach (var directory in directories)
            {
                foreach (var tag in directory.Tags)
                {
                    listBox_Files.Items.Add(tag);
                    listBox_Files.Items.Add(tag.Type);
                    //Console.WriteLine(tag);
                }
            }
        }
        
        public bool GetMetaDataDateTime(string pathFileName, ref int[] dateYMD)
        {
            //listBox_Files.Items.Add("檔名 :" + pathFileName);
            //Console.WriteLine("ShowMetaDataDateTime() " + pathFileName);
            var directories = ImageMetadataReader.ReadMetadata(pathFileName);
            //listBox_Files.Items.Add("Metadata組數-directories :" + directories.Count);
            //Console.WriteLine("ShowMetaDataDateTime() " + directories);

            // obtain the Exif SubIFD directory
            //var subIfdDirectory = directories.OfType<ExifSubIfdDirectory>().FirstOrDefault();
            int tmp = directories.OfType<ExifSubIfdDirectory>().Count();
            //listBox_Files.Items.Add("directories.OfType<ExifSubIfdDirectory>().Count()" + tmp);
            //var directory = directories.OfType<ExifSubIfdDirectory>().ElementAt(1);
            var directory = directories.OfType<ExifSubIfdDirectory>().FirstOrDefault();
            var directoryIfd0 = directories.OfType<ExifIfd0Directory>().FirstOrDefault();
            //listBox_Files.Items.Add("directory :" + directory.Name);
            //Console.WriteLine("ShowMetaDataDateTime() " + directory);
            //以下判斷沒錯，但是寫法不好，有些不會進去判斷？？？
            if (directory != null)
            {
                // query the tag's value
                if (directory.TryGetDateTime(ExifDirectoryBase.TagDateTimeOriginal, out dateTime))
                {
                    //listBox_Files.Items.Add("EXIF 日期 :" + dateTime);
                    //listBox_Files.Items.Add("EXIF(直取) 年 :" + dateTime.Year);
                    //listBox_Files.Items.Add("EXIF(直取) 月 :" + dateTime.Month);
                    //listBox_Files.Items.Add("EXIF(直取) 日 :" + dateTime.Day);
                    //Console.WriteLine("dateTime " + dateTime);
                    dateYMD[0] = dateTime.Year;
                    dateYMD[1] = dateTime.Month;
                    dateYMD[2] = dateTime.Day;
                    return true;
                }
                else if (directoryIfd0.TryGetDateTime(ExifIfd0Directory.TagDateTime, out dateTime))
                //if (directories[directories.Count - 1].TryGetDateTime(3, out dateTime))
                {
                    //listBox_Files.Items.Add("EXIF(偷抓) 日期 :" + dateTime);
                    //listBox_Files.Items.Add("EXIF(偷抓) 年 :" + dateTime.Year);
                    //listBox_Files.Items.Add("EXIF(偷抓) 月 :" + dateTime.Month);
                    //listBox_Files.Items.Add("EXIF(偷抓) 日 :" + dateTime.Day);
                    dateYMD[0] = dateTime.Year;
                    dateYMD[1] = dateTime.Month;
                    dateYMD[2] = dateTime.Day;
                    return true;
                }
                else
                {
                    int extNum = pathFileName.LastIndexOf('.');
                    bool tmpReturn = false;
                    switch (pathFileName.Substring(extNum).ToUpper())
                    {
                        case ".JPG":
                        case ".JPEG":
                            //var directoryJPG = directories.OfType<MetadataExtractor.Formats.Jpeg.JpegDirectory>().FirstOrDefault();
                            //if (directoryJPG.TryGetDateTime(MetadataExtractor.Formats.Jpeg.JpegDirectory.？？？, out dateTime)) //沒有關於時間的資料
                            if (directories[directories.Count - 1].TryGetDateTime(3, out dateTime))
                            {
                                //listBox_Files.Items.Add("EXIF(偷抓) 日期 :" + dateTime);
                                //listBox_Files.Items.Add("EXIF(偷抓) 年 :" + dateTime.Year);
                                //listBox_Files.Items.Add("EXIF(偷抓) 月 :" + dateTime.Month);
                                //listBox_Files.Items.Add("EXIF(偷抓) 日 :" + dateTime.Day);
                                dateYMD[0] = dateTime.Year;
                                dateYMD[1] = dateTime.Month;
                                dateYMD[2] = dateTime.Day;
                                tmpReturn = true;

                            }
                            else
                            {
                                tmpReturn = false;

                                //listBox_Files.Items.Add("JPG日期 :" + directories[2].Tags[2]);
                            }
                            break;
                        case ".PNG":
                            if (directories[directories.Count - 1].TryGetDateTime(3, out dateTime))
                            //if (directories[2].TryGetDateTime(3, out dateTime))
                            {
                                //listBox_Files.Items.Add("EXIF(偷抓) 日期 :" + dateTime);
                                //listBox_Files.Items.Add("EXIF(偷抓) 年 :" + dateTime.Year);
                                //listBox_Files.Items.Add("EXIF(偷抓) 月 :" + dateTime.Month);
                                //listBox_Files.Items.Add("EXIF(偷抓) 日 :" + dateTime.Day);
                                dateYMD[0] = dateTime.Year;
                                dateYMD[1] = dateTime.Month;
                                dateYMD[2] = dateTime.Day;
                                tmpReturn = true;
                            }
                            else
                            {
                                tmpReturn = false;

                                //listBox_Files.Items.Add("PNG日期 :" + directories[2].Tags[2]);
                            }
                            break;
                        case ".GIF":
                            if (directories[directories.Count - 1].TryGetDateTime(3, out dateTime))
                            {
                                //listBox_Files.Items.Add("EXIF(偷抓) 日期 :" + dateTime);
                                //listBox_Files.Items.Add("EXIF(偷抓) 年 :" + dateTime.Year);
                                //listBox_Files.Items.Add("EXIF(偷抓) 月 :" + dateTime.Month);
                                //listBox_Files.Items.Add("EXIF(偷抓) 日 :" + dateTime.Day);
                                dateYMD[0] = dateTime.Year;
                                dateYMD[1] = dateTime.Month;
                                dateYMD[2] = dateTime.Day;
                                tmpReturn = true;
                            }
                            else
                            {
                                tmpReturn = false;

                                //listBox_Files.Items.Add("GIF日期 :" + directories[directories.Count -1].Tags[2]);
                            }

                            break;
                        case ".PSD":
                            if (directories[directories.Count - 1].TryGetDateTime(3, out dateTime))
                            {
                                //listBox_Files.Items.Add("EXIF(偷抓) 日期 :" + dateTime);
                                //listBox_Files.Items.Add("EXIF(偷抓) 年 :" + dateTime.Year);
                                //listBox_Files.Items.Add("EXIF(偷抓) 月 :" + dateTime.Month);
                                //listBox_Files.Items.Add("EXIF(偷抓) 日 :" + dateTime.Day);
                                dateYMD[0] = dateTime.Year;
                                dateYMD[1] = dateTime.Month;
                                dateYMD[2] = dateTime.Day;
                                tmpReturn = true;
                            }
                            else
                            {
                                tmpReturn = false;

                                //listBox_Files.Items.Add("GIF日期 :" + directories[directories.Count -1].Tags[2]);
                            }

                            break;
                        case ".HEIC": //沒有詳細資料，只取Windows檔案修改日期。
                                      //var directoryHEIC = directories.OfType <MetadataExtractor.Formats.QuickTime.QuickTimeFileTypeDirectory>().FirstOrDefault();

                            if (directories[directories.Count - 1].TryGetDateTime(3, out dateTime))
                            {
                                //listBox_Files.Items.Add("EXIF(偷抓) 日期 :" + dateTime);
                                //listBox_Files.Items.Add("EXIF(偷抓) 年 :" + dateTime.Year);
                                //listBox_Files.Items.Add("EXIF(偷抓) 月 :" + dateTime.Month);
                                //listBox_Files.Items.Add("EXIF(偷抓) 日 :" + dateTime.Day);
                                dateYMD[0] = dateTime.Year;
                                dateYMD[1] = dateTime.Month;
                                dateYMD[2] = dateTime.Day;
                                tmpReturn = true;
                            }
                            else
                            {
                                tmpReturn = false;

                                //listBox_Files.Items.Add("GIF日期 :" + directories[directories.Count -1].Tags[2]);
                            }

                            break;
                        case ".MP4": //沒有詳細資料，只取Windows檔案修改日期。
                            if (directories[directories.Count - 1].TryGetDateTime(3, out dateTime))
                            {
                                //listBox_Files.Items.Add("EXIF(偷抓) 日期 :" + dateTime);
                                //listBox_Files.Items.Add("EXIF(偷抓) 年 :" + dateTime.Year);
                                //listBox_Files.Items.Add("EXIF(偷抓) 月 :" + dateTime.Month);
                                //listBox_Files.Items.Add("EXIF(偷抓) 日 :" + dateTime.Day);
                                dateYMD[0] = dateTime.Year;
                                dateYMD[1] = dateTime.Month;
                                dateYMD[2] = dateTime.Day;
                                tmpReturn = true;
                            }
                            else
                            {
                                tmpReturn = false;

                                //listBox_Files.Items.Add("GIF日期 :" + directories[directories.Count -1].Tags[2]);
                            }

                            break;
                        case ".MOV":
                            var directoryMOV = directories.OfType<MetadataExtractor.Formats.QuickTime.QuickTimeMovieHeaderDirectory>().FirstOrDefault();
                            if (directoryMOV.TryGetDateTime(MetadataExtractor.Formats.QuickTime.QuickTimeMovieHeaderDirectory.TagCreated, out dateTime))
                            {
                                //listBox_Files.Items.Add("EXIF(偷抓) 日期 :" + dateTime);
                                //listBox_Files.Items.Add("EXIF(偷抓) 年 :" + dateTime.Year);
                                //listBox_Files.Items.Add("EXIF(偷抓) 月 :" + dateTime.Month);
                                //listBox_Files.Items.Add("EXIF(偷抓) 日 :" + dateTime.Day);
                                dateYMD[0] = dateTime.Year;
                                dateYMD[1] = dateTime.Month;
                                dateYMD[2] = dateTime.Day;
                                tmpReturn = true;
                            }
                            else if (directories[directories.Count - 1].TryGetDateTime(3, out dateTime))
                            {
                                //listBox_Files.Items.Add("EXIF(偷抓) 日期 :" + dateTime);
                                //listBox_Files.Items.Add("EXIF(偷抓) 年 :" + dateTime.Year);
                                //listBox_Files.Items.Add("EXIF(偷抓) 月 :" + dateTime.Month);
                                //listBox_Files.Items.Add("EXIF(偷抓) 日 :" + dateTime.Day);
                                dateYMD[0] = dateTime.Year;
                                dateYMD[1] = dateTime.Month;
                                dateYMD[2] = dateTime.Day;
                                tmpReturn = true;
                            }
                            else
                            {
                                tmpReturn = false;

                                //listBox_Files.Items.Add("GIF日期 :" + directories[directories.Count -1].Tags[2]);
                            }

                            break;
                        default:
                            tmpReturn = false;

                            break;
                    }
                    return tmpReturn;
                }
            }
            else
            {
                return false;
            }
        }
        
        private void ExtractMetaData(PaintEventArgs e)
        {
            //網路抓來的，沒用到
            try
            {
                // Create an Image object. 
                Image theImage = new Bitmap("c:\\fakePhoto.jpg");
                //Image theImage = new Bitmap(f);

                // Get the PropertyItems property from image.
                System.Drawing.Imaging.PropertyItem[] propItems = theImage.PropertyItems;

                // Set up the display.
                Font font1 = new Font("Arial", 10);
                SolidBrush blackBrush = new SolidBrush(Color.Black);
                int X = 0;
                int Y = 0;

                // For each PropertyItem in the array, display the id, 
                // type, and length.
                int count = 0;
                foreach (var propItem in propItems)
                {
                    e.Graphics.DrawString("Property Item " +
                        count.ToString(), font1, blackBrush, X, Y);
                    Y += font1.Height;

                    e.Graphics.DrawString("   ID: 0x" +
                        propItem.Id.ToString("x"), font1, blackBrush, X, Y);
                    Y += font1.Height;

                    e.Graphics.DrawString("   type: " +
                        propItem.Type.ToString(), font1, blackBrush, X, Y);
                    Y += font1.Height;

                    e.Graphics.DrawString("   length: " +
                        propItem.Len.ToString() +
                        " bytes", font1, blackBrush, X, Y);
                    Y += font1.Height;
                    count += 1;
                }
                font1.Dispose();
            }
            catch (Exception)
            {
                MessageBox.Show("There was an error." +
                    "Make sure the path to the image file is valid.");
            }
        }

    }
}
