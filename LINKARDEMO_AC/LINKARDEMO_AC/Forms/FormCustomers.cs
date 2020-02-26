using LinkarClient;
using LinkarCommon;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LINKARDEMO
{
    public partial class FormCustomers : Form
    {

        LinkarClt _LinkarClt;
        bool isNew = false;
        string filename = "LK.CUSTOMERS";

        public FormCustomers(LinkarClt linkarClt)
        {
            _LinkarClt = linkarClt;
            InitializeComponent();
            txtHelp.Text = "In this form the \"Select\" method is used with \"calculated\" option to load all data selected.\r\nIn the select box you can write a customer name or id.\r\nTo load the data, a standard class is used that collects the MV buffer and assigns it to each property.\r\nThis class has CRUD methods to see how they work.";   
            cLkCustomersBindingSource.DataSource = new Customers();
        }

        #region -- Operations Bar

        private void btnNew_Click(object sender, EventArgs e)
        {
            //Create new CLkCustomer object
            Customer clkcustomer = new Customer();
            cLkCustomersBindingSource.Add(clkcustomer);
            cLkCustomersBindingSource.MoveLast();
            ChangeBarStatus(true, true);



            ChangeBarStatus(true, true);

            Customer customer = new Customer();
            if (cLkCustomersBindingSource.DataSource == null)
                cLkCustomersBindingSource.DataSource = new Customers();
            cLkCustomersBindingSource.Add(customer);

            isNew = true;
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            //Get current customer
            Customer clkcustomer = GetCurrentRecord();
            if (clkcustomer != null)
            {
                ChangeBarStatus(true, false);
                isNew = false;
            }
            else
                MessageBox.Show("Need select one record");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //Get current customer
            Customer clkcustomer = GetCurrentRecord();
            if (clkcustomer != null)
            {
                if (MessageBox.Show("Do you want delete this record?", "Delete Record", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    List<Customer> lstDeleted = new List<Customer>();
                    lstDeleted.Add(clkcustomer);

                    string dataOrg = "";

                    //DeleteOptions deleteOptions = new DeleteOptions(false, new RecoverRecordIdTypeLinkar(false, "", ""), false);
                    DeleteOptions deleteOptions = new DeleteOptions(false, new RecoverIdType());

                    try
                    {
                        List<Customer> lstCustomers = this._LinkarClt.Delete<Customer>(filename, lstDeleted, dataOrg, deleteOptions);
                        cLkCustomersBindingSource.Remove(clkcustomer);
                    }
                    catch(Exception ex)
                    {
                        string msgErr = FormDemo.GetExceptionInfo(ex);
                        MessageBox.Show(this, msgErr, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            string selectClause = this.txtSelect.Text;
            string sortClause = "";
            string dictClause = "";
            string preSelectClause = "";

            bool calculated = true;
            bool dictionaries = true;
            bool conversion = false;
            bool formatSpec = false;
            bool originalBuffer = true;
            bool onlyitemid = false;
            bool pagination = false;

            int regPage = 10;
            int numPag = 1;

            SelectOptions selectOptions = new SelectOptions(onlyitemid, pagination, regPage, numPag, calculated, conversion, formatSpec, originalBuffer, dictionaries);

            string customVars = "";
            string error;

            //DateTime dtantes = DateTime.Now;
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            try
            {
                List<Customer> lstCustomers = this._LinkarClt.Select<Customer>(filename, selectClause, sortClause, dictClause, preSelectClause, selectOptions, customVars);
                if (lstCustomers != null)
                    cLkCustomersBindingSource.DataSource = lstCustomers;
            }
            catch (Exception ex)
            {
                string msgErr = FormDemo.GetExceptionInfo(ex);
                MessageBox.Show(this, msgErr, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //DateTime dtdespues = DateTime.Now;

            //TimeSpan ts = dtdespues - dtantes;
            //MessageBox.Show(ts.Minutes + " - " + ts.Seconds + " - " + ts.Milliseconds);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //Get current customer
            //Customer clkcustomer = GetCurrentRecord();
            //if (clkcustomer != null)
            //{
            //    GetRecord(ref clkcustomer);
            //    //Save or create new item
            //    if (clkcustomer.Status != LinkarMainClass.StatusTypes.NEW)
            //    {

            //        string ge = "";
            //        //Call the WriteItem method from CLkCustomer
            //        clkcustomer.WriteRecord(out ge, "");

            //        if (!string.IsNullOrEmpty(ge))
            //        {
            //            MessageBox.Show(ge.Replace(DBMV_Mark.VM_str, "\r\n"));
            //        }
            //        else
            //        {
            //            ChangeBarStatus(false, false);
            //        }
            //    }
            //    else
            //    {
            //        string ge = "";
            //        //Call the NewItem method from CLkCustomer
            //        clkcustomer.NewRecord(out ge, "");

            //        if (!string.IsNullOrEmpty(ge))
            //        {
            //            MessageBox.Show(ge.Replace(DBMV_Mark.VM_str, "\r\n"));
            //        }
            //        else
            //        {
            //            ChangeBarStatus(false, false);
            //        }
            //    }
            //}

            Customer clkcustomer = GetCurrentRecord();
            if (clkcustomer != null)
            {
                List<Customer> lstupd = new List<Customer>();
                GetRecord(ref clkcustomer);
                lstupd.Add(clkcustomer);

                string error;

                bool optimisticLock = false;
                bool readAfter = false;
                bool calculated = false;
                bool dictionaries = false;
                bool conversion = false;
                bool formatSpec = false;
                bool originalBuffer = false;

                string dataOrg = "";

                string customVars = "";

                Exception exErr = null;
                if (isNew)
                {
                    try
                    {
                        RecordIdType recordIdType = new RecordIdType();
                        NewOptions newOptions = new NewOptions(recordIdType, readAfter, calculated, conversion, formatSpec, originalBuffer, dictionaries);
                        List<Customer> lstCustomers = this._LinkarClt.New(filename, lstupd, newOptions, customVars);
                    }
                    catch(Exception ex)
                    {
                        exErr = ex;
                    }
                }
                else
                {
                    try
                    {
                        UpdateOptions updateOptions = new UpdateOptions(optimisticLock, readAfter, calculated, conversion, formatSpec, originalBuffer, dictionaries);
                        List<Customer> lstCustomers = this._LinkarClt.Update(filename, lstupd, updateOptions, dataOrg, customVars);
                    }
                    catch(Exception ex)
                    {
                        exErr = ex;
                    }
                }

                if(exErr != null)
                {
                    string msgErr = FormDemo.GetExceptionInfo(exErr);
                    MessageBox.Show(this, msgErr, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                ChangeBarStatus(false, false);
                isNew = false;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {          
            ChangeBarStatus(false, false);
            isNew = false;
        }

        #endregion

        #region -- Methods

        private void ChangeBarStatus(bool inEditMode, bool isNew)
        {
            if (inEditMode)
            {
                btnNew.Enabled = false;
                btnModify.Enabled = false;
                btnDelete.Enabled = false;
                btnSelect.Enabled = false;
                txtSelect.Enabled = false;
                dgRecords.Enabled = false;

                btnSave.Enabled = true;
                btnCancel.Enabled = true;
                txtCode.Enabled = isNew;
                txtName.Enabled = true;
                txtAddress.Enabled = true;
                txtPhone.Enabled = true;
            }
            else
            {
                btnNew.Enabled = true;
                btnModify.Enabled = true;
                btnDelete.Enabled = true;
                btnSelect.Enabled = true;
                txtSelect.Enabled = true;
                dgRecords.Enabled = true;

                btnSave.Enabled = false;
                btnCancel.Enabled = false;
                txtCode.Enabled = false;
                txtName.Enabled = false;
                txtAddress.Enabled = false;
                txtPhone.Enabled = false;
            }
        }

        private Customer GetCurrentRecord()
        {
            Customer record = null;
            if (cLkCustomersBindingSource != null && cLkCustomersBindingSource.Current != null)
                record = (Customer)cLkCustomersBindingSource.Current;                           
            return record;
        }

        private void DrawRecord(Customer record)
        {
            if (record != null)
            {
                txtCode.Text = record.Code;
                txtName.Text = record.Name;
                txtAddress.Text = record.Address;
                txtPhone.Text = record.Phone;
            }
        }

        private void GetRecord(ref Customer record)
        {
            record.Code = txtCode.Text;
            record.Name = txtName.Text;
            record.Address = txtAddress.Text;
            record.Phone = txtPhone.Text;
        }

        #endregion

        private void cLkCustomersBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            DrawRecord(GetCurrentRecord());
        }
    }
}
