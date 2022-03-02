using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Billing_ADSL
{
    public partial class Form1 : Form
    {
        BillingDriver bd;

        bool LoadExcelSheetOnlyOnce = true;
        bool billingflag = false;
        bool adslflag = false;
        bool stop = false;

        int seconds, minutes, hours = 0;

        DataTable OriginalDataTable = new DataTable();
        public Form1()
        {
            InitializeComponent();
        }

        private void billingADSLButton_Click(object sender, EventArgs e)
        {
            //controling controls
            ADSLButton.Enabled = false;
            billingButton.Enabled = false;
            billingADSLButton.Enabled = false;
            breakButton.Enabled = true;

            OriginalDataTable.Columns.Add("عدد الفواتير الغير مدفوعة", typeof(System.String));
            OriginalDataTable.Columns.Add("نوع الخطأ", typeof(System.String));

            OriginalDataTable.Columns.Add("حالة الرقم/القطاع", typeof(System.String));
            OriginalDataTable.Columns.Add("المنطقة", typeof(System.String));
            OriginalDataTable.Columns.Add("السنترال", typeof(System.String));
            OriginalDataTable.Columns.Add("MSAN Code", typeof(System.String));
            OriginalDataTable.Columns.Add("Data Service", typeof(System.String));

            timer1.Start();
            billingflag = true;
            adslflag = true;
            Parallel.Invoke(async () => await ExecuteADSL(), async () => await ExecuteBilling());
        }
        private async Task ExecuteBilling()
        {
            try
            {
                int RowsCount = OriginalDataTable.Rows.Count;
                List<BillingClient> Clients = new List<BillingClient>();
                for (int i = 0; i < RowsCount; i++)
                {
                    BillingClient c = new BillingClient
                    {
                        Landline = Convert.ToInt32(OriginalDataTable.Rows[i]["رقم التليفون"]),
                        Code = Convert.ToInt32(OriginalDataTable.Rows[i]["كود المحافظة"])
                    };
                    Clients.Add(c);
                }
                int counter = 0;

                int peekCounter = RowsCount;
                BeginInvoke((MethodInvoker)delegate
                {
                    billingCounterLabel.Text = "1/" + peekCounter;
                });

                foreach (BillingClient c in Clients)
                {
                    if (counter % 100 == 0)
                    {
                        if (bd == null)
                            bd = new BillingDriver();
                        else
                        {
                            bd.Driver.Close();
                            bd = new BillingDriver();
                        }
                    }
                    BillingClient cl = await BillingAsyncInject(bd, c) as BillingClient;
                    OriginalDataTable.Rows[counter]["عدد الفواتير الغير مدفوعة"] = cl.NonePaidInvoicesCount;
                    OriginalDataTable.Rows[counter]["نوع الخطأ"] = cl.PhoneError;

                    BeginInvoke((MethodInvoker)delegate
                    {
                        billingCounterLabel.Text = (counter + 1) + "/" + peekCounter;
                    });
                    ++counter;
                    if (stop)
                    {
                        break;
                    }
                }
                bd.Driver.Close();

                if (billingflag && adslflag)
                    billingflag = false;
                else
                {
                    SaveTo();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private async Task ExecuteADSL()
        {
            try
            {
                int RowsCount = OriginalDataTable.Rows.Count;
                List<ADSLClient> Clients = new List<ADSLClient>();
                for (int i = 0; i < RowsCount; i++)
                {
                    ADSLClient c = new ADSLClient();
                    c.Landline = Convert.ToInt32(OriginalDataTable.Rows[i]["land line"]);
                    c.Code = Convert.ToInt32(OriginalDataTable.Rows[i]["area code"]);
                    Clients.Add(c);
                }

                int counter = 0;
                ADSLDriver ad = new ADSLDriver();

                int peekCounter = RowsCount;
                if (peekCounter < 10)
                    counterLabel.Text = "1/" + peekCounter;

                ad.Driver.Navigate().GoToUrl("https://te.thetradingcompany.me:6443/te_login.php");
                ad.Driver.FindElement(By.Id("txtusername")).SendKeys("ma173315");
                ad.Driver.FindElement(By.Id("txtpassword")).SendKeys("123456");
                ad.Driver.FindElement(By.Name("button")).Click();

                WebDriverWait wait = new WebDriverWait(ad.Driver, TimeSpan.FromSeconds(15));

                wait.Until(p => p.Url == "https://te.thetradingcompany.me:6443/te_login.php");
                ad.Driver.FindElement(By.CssSelector("[href='adsl_tetelbesearch.php']")).Click();

                foreach (ADSLClient c in Clients)
                {
                    ADSLClient cl = await ADSLAsyncInject(ad, c) as ADSLClient;
                    OriginalDataTable.Rows[counter]["حالة الرقم/القطاع"] = cl.State;
                    OriginalDataTable.Rows[counter]["المنطقة"] = cl.Area;
                    OriginalDataTable.Rows[counter]["السنترال"] = cl.Central;
                    OriginalDataTable.Rows[counter]["MSAN Code"] = cl.MSANCode;
                    OriginalDataTable.Rows[counter]["Data Service"] = cl.DataService;

                    counterLabel.Text = (counter + 1) + "/" + peekCounter;
                    ++counter;
                    if (stop)
                    {
                        break;
                    }
                }
                ad.Driver.Close();

                if (adslflag && billingflag)
                    adslflag = false;
                else
                {
                    SaveTo();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Wrong User Name or Password.");
            }

        }
        private Task<Client> BillingAsyncInject(BillingDriver d, Client ci)
        {
            return Task.Factory.StartNew(() => d.Inject(ci));
        }
        private Task<Client> ADSLAsyncInject(ADSLDriver d, ADSLClient c)
        {
            return Task.Factory.StartNew(() => d.Inject(c));
        }

        static public DataTable RemoveDuplicateRows(DataTable dTable, string colName)
        {
            Hashtable hTable = new Hashtable();
            ArrayList duplicateList = new ArrayList();

            //Add list of all the unique item value to hashtable, which stores combination of key, value pair.
            //And add duplicate item value in arraylist.
            foreach (DataRow drow in dTable.Rows)
            {
                if (hTable.Contains(drow[colName]))
                    duplicateList.Add(drow);
                else
                    hTable.Add(drow[colName], string.Empty);
            }

            //Removing a list of duplicate items from datatable.
            foreach (DataRow dRow in duplicateList)
                dTable.Rows.Remove(dRow);

            //Datatable which contains unique records will be return as output.
            return dTable;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (seconds < 10 && minutes < 10 && hours < 10)
                elapsedTimeLabel.Text = "0" + hours + ":0" + minutes + ":0" + seconds;
            else if (minutes < 10 && hours < 10)
                elapsedTimeLabel.Text = "0" + hours + ":0" + minutes + ":" + seconds;
            else if (hours < 10)
            {
                if (seconds < 10)
                    elapsedTimeLabel.Text = "0" + hours + ":" + minutes + ":0" + seconds;
                else
                    elapsedTimeLabel.Text = "0" + hours + ":" + minutes + ":" + seconds;
            }
            else
            {
                if (seconds < 10 && minutes < 10)
                    elapsedTimeLabel.Text = hours + ":0" + minutes + ":0" + seconds;
                else if (minutes < 10)
                    elapsedTimeLabel.Text = hours + ":0" + minutes + ":" + seconds;
                else
                    elapsedTimeLabel.Text = hours + ":" + minutes + ":" + seconds;
            }

            seconds++;
            if (seconds > 59)
            {
                minutes++;
                seconds = 0;
            }
            if (minutes > 59)
            {
                hours++;
                minutes = 0;
            }
        }

        private void loadFromButton_Click(object sender, EventArgs e)
        {
            //contoling controls
            if (!string.IsNullOrWhiteSpace(saveToTextBox.Text))
                loadExcelSheetButton.Enabled = true;

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
            ofd.Title = "Load Excel File";
            if (ofd.ShowDialog() == DialogResult.OK)
                this.loadFromTextBox.Text = ofd.FileName;
        }

        private void saveToButton_Click(object sender, EventArgs e)
        {
            //controling controls
            if (!string.IsNullOrWhiteSpace(loadFromTextBox.Text))
                loadExcelSheetButton.Enabled = true;

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            //saveFileDialog1.InitialDirectory = @"C:\";
            saveFileDialog1.Title = "Save Excel Files";

            saveFileDialog1.DefaultExt = "xlsx";
            saveFileDialog1.Filter = "Excel Files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                saveToTextBox.Text = saveFileDialog1.FileName;
            else
                return;
        }

        private void billingButton_Click(object sender, EventArgs e)
        {
            //controling controls
            ADSLButton.Enabled = false;
            billingADSLButton.Enabled = false;
            billingButton.Enabled = false;
            breakButton.Enabled = true;

            OriginalDataTable.Columns.Add("عدد الفواتير الغير مدفوعة", typeof(System.String));
            OriginalDataTable.Columns.Add("نوع الخطأ", typeof(System.String));

            timer1.Start();
            billingflag = true;
            ExecuteBilling();
        }

        private void ADSLButton_Click(object sender, EventArgs e)
        {
            //controling controls
            billingADSLButton.Enabled = false;
            billingButton.Enabled = false;
            ADSLButton.Enabled = false;
            breakButton.Enabled = true;

            OriginalDataTable.Columns.Add("حالة الرقم/القطاع", typeof(System.String));
            OriginalDataTable.Columns.Add("المنطقة", typeof(System.String));
            OriginalDataTable.Columns.Add("السنترال", typeof(System.String));
            OriginalDataTable.Columns.Add("MSAN Code", typeof(System.String));
            OriginalDataTable.Columns.Add("Data Service", typeof(System.String));

            timer1.Start();
            adslflag = true;
            ExecuteADSL();
        }

        private void breakButton_Click(object sender, EventArgs e)
        {
            stop = true;

            timer1.Stop();
        }

        private void newLoginLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Login loginfrm = new Login();
            loginfrm.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            loadExcelSheetButton.Enabled = false;
            billingButton.Enabled = false;
            ADSLButton.Enabled = false;
            billingADSLButton.Enabled = false;
            breakButton.Enabled = false;
        }

        private void loadExcelSheetButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (LoadExcelSheetOnlyOnce == true)
                {
                    string conn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + loadFromTextBox.Text + ";Extended Properties='Excel 12.0;IMEX=1;HDR=NO;TypeGuessRows=1;ImportMixedTypes=Text'";//FMT=Delimited;CharacterSet=65001;
                    using (OleDbConnection con = new OleDbConnection(conn))
                    {
                        con.Open();

                        OleDbCommand command;
                        if (sheetNameTextBox.Text == "")
                            command = new OleDbCommand("select * from [Sheet1$]", con);
                        else
                            command = new OleDbCommand("select * from [" + sheetNameTextBox.Text + "$]", con);

                        using (OleDbDataAdapter da = new OleDbDataAdapter(command))
                        {
                            da.Fill(OriginalDataTable);

                            //add to the datatable it's columns' names
                            DataRow firstRow = OriginalDataTable.Rows[0];
                            for (int i = 0; i < OriginalDataTable.Columns.Count; i++)
                            {
                                if (!string.IsNullOrWhiteSpace(firstRow[i].ToString())) // handle empty cell
                                    OriginalDataTable.Columns[i].ColumnName = firstRow[i].ToString().Trim();
                            }
                            OriginalDataTable.Rows.RemoveAt(0);

                            //removing the duplicate from رقم التليفون column to make use it as a primary key later
                           // OriginalDataTable = RemoveDuplicateRows(OriginalDataTable, "land line");

                            //OriginalDataTable.PrimaryKey = new DataColumn[] { OriginalDataTable.Columns["land line"] };
                            ExcelSheetDataGridView.DataSource = OriginalDataTable;
                        }
                    }
                    LoadExcelSheetOnlyOnce = false;

                    //controling controls
                    billingButton.Enabled = true;
                    ADSLButton.Enabled = true;
                    billingADSLButton.Enabled = true;
                    saveToButton.Enabled = false;
                    loadFromButton.Enabled = false;
                    loadExcelSheetButton.Enabled = false;
                }
                else
                {
                    MessageBox.Show("You can load it only once.\n please restart the program for another file.");
                }
            }
            catch (OleDbException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void SaveTo()
        {
            Microsoft.Office.Interop.Excel.Application excel;
            Microsoft.Office.Interop.Excel.Workbook worKbooK;
            Microsoft.Office.Interop.Excel.Worksheet worKsheeT;
            Microsoft.Office.Interop.Excel.Range celLrangE;

            try
            {
                if (saveToTextBox.Text != "")
                {
                    excel = new Microsoft.Office.Interop.Excel.Application();
                    excel.Visible = false;
                    excel.DisplayAlerts = false;
                    worKbooK = excel.Workbooks.Add(Type.Missing);


                    worKsheeT = (Microsoft.Office.Interop.Excel.Worksheet)worKbooK.ActiveSheet;
                    worKsheeT.Name = "Sheet1";

                    int length = OriginalDataTable.Columns.Count;
                    for (int i = 0; i < length; i++)
                    {
                        worKsheeT.Cells[1, i + 1] = OriginalDataTable.Columns[i].ColumnName;
                    }


                    int rowcount = 1;

                    foreach (DataRow datarow in OriginalDataTable.Rows)
                    {
                        rowcount += 1;
                        for (int i = 1; i <= OriginalDataTable.Columns.Count; i++)
                        {
                            worKsheeT.Cells[rowcount, i] = datarow[i - 1].ToString();
                            if (rowcount > 3)
                            {
                                if (i == OriginalDataTable.Columns.Count)
                                {
                                    if (rowcount % 2 == 0)
                                    {
                                        celLrangE = worKsheeT.Range[worKsheeT.Cells[rowcount, 1], worKsheeT.Cells[rowcount, OriginalDataTable.Columns.Count]];
                                    }

                                }
                            }
                        }

                    }


                    celLrangE = worKsheeT.Range[worKsheeT.Cells[1, 1], worKsheeT.Cells[rowcount, OriginalDataTable.Columns.Count]];

                    celLrangE = worKsheeT.Range[worKsheeT.Cells[1, 1], worKsheeT.Cells[2, OriginalDataTable.Columns.Count]];

                    worKbooK.SaveAs(saveToTextBox.Text); ;
                    worKbooK.Close();
                    excel.Quit();
                    MessageBox.Show("Successfully Created.");
                    System.Windows.Forms.Application.Exit();
                }
                else
                    MessageBox.Show("Please select a path to save the file.");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                worKsheeT = null;
                celLrangE = null;
                worKbooK = null;
            }
        }

    }
}
